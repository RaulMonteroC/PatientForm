using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using PatientForm.Domain.Entities;
using PatientForm.Infrastructure.Configuration;

namespace PatientForm.Domain.Repositories;

internal class AbstractRepository(IOptions<ConnectionSettings> settings)
{
    protected readonly ConnectionSettings Settings = settings.Value;

    protected async Task ExecuteActionQuery(string query, 
                                            SqlParameter[] parameters,
                                            CommandType type)
    {
        await using var connection = new SqlConnection(Settings.PatientDb);
        await using var command = new SqlCommand(query, connection);

        if (connection.State == ConnectionState.Closed)
            await connection.OpenAsync();

        command.Parameters.AddRange(parameters);

        command.CommandType = type;

        await command.ExecuteNonQueryAsync();

        await using var reader = await command.ExecuteReaderAsync();
    }
    
    protected async Task<IEnumerable<T>> ExecuteQuery<T>(string query, 
                                                         SqlParameter[] parameters, 
                                                         CommandType type, 
                                                         Func<SqlDataReader,T> parseFunction)
    {
        var returnedValues = new List<T>();
        await using var connection = new SqlConnection(Settings.PatientDb);
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

        return returnedValues;
    }
}