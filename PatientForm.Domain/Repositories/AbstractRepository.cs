using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PatientForm.Infrastructure.Configuration;

namespace PatientForm.Domain.Repositories;

public class AbstractRepository(IOptions<ConnectionSettings> settings,
                                ILogger logger)
{
    private readonly ConnectionSettings _settings = settings.Value;

    protected async Task<int> ExecuteActionQuery(string query, 
                                            SqlParameter[] parameters,
                                            CommandType type)
    {
        await using var connection = new SqlConnection(_settings.PatientDb);
        await using var command = new SqlCommand(query, connection);
        var returnParameter = new SqlParameter("@ret", -1);

        if (connection.State == ConnectionState.Closed)
            await connection.OpenAsync();

        command.Parameters.AddRange(parameters);
        
        if (type == CommandType.StoredProcedure)
        {
            returnParameter = command.Parameters
                                     .Add(new SqlParameter("@ret", SqlDbType.Int) {Direction = ParameterDirection.ReturnValue});   
        }

        command.CommandType = type;
        
        await command.ExecuteNonQueryAsync();
        
        LogCommandQuery(command);

        return (int) returnParameter.Value;
    }
    
    protected async Task<IEnumerable<T>> ExecuteQuery<T>(string query, 
                                                         SqlParameter[] parameters, 
                                                         CommandType type, 
                                                         Func<SqlDataReader,T> parseFunction)
    {
        var returnedValues = new List<T>();
        await using var connection = new SqlConnection(_settings.PatientDb);
        await using var command = new SqlCommand(query, connection);

        if (connection.State == ConnectionState.Closed)
            await connection.OpenAsync();

        command.Parameters.AddRange(parameters);
        command.CommandType = type;

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            returnedValues.Add(parseFunction(reader));
        }
        
        LogCommandQuery(command);

        return returnedValues;
    }

    private void LogCommandQuery(SqlCommand command )
    {
        var query = new StringBuilder(command.CommandText);

        foreach (SqlParameter p in command.Parameters)
        {
            query.Replace(p.ParameterName, p.Value?.ToString());
        }
        
        logger.LogInformation("QUERY: {query}", query);
    }
}