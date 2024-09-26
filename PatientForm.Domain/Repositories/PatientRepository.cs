using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using PatientForm.Domain.Entities;
using PatientForm.Infrastructure.Configuration;

namespace PatientForm.Domain.Repositories;

internal class PatientRepository(IOptions<ConnectionSettings> settings) : IPatientRepository
{
    private readonly ConnectionSettings _settings = settings.Value;
    public async Task<IEnumerable<Patient>> Get(int page, int pageSize)
    {
        var patients = new List<Patient>();
        await using var connection = new SqlConnection(_settings.PatientDb);
        await using var command = new SqlCommand("sp_fetch_patients", connection);

        if (connection.State == ConnectionState.Closed)
            connection.Open();

        command.Parameters.Add("@PageNumber", SqlDbType.Int);
        command.Parameters.Add("@RowsOfPage", SqlDbType.Int);
        command.Parameters["@PageNumber"].Value = page;
        command.Parameters["@RowsOfPage"].Value = pageSize;
        
        command.CommandType = CommandType.StoredProcedure;

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            patients.Add(new Patient(new Guid(reader["Id"].ToString()!),
                                     reader["Name"].ToString()!,
                                     reader["LastName"].ToString()!,
                                     reader["PhoneNumber"].ToString() ?? string.Empty,
                                     reader["Email"].ToString() ?? string.Empty));
        }

        return patients;
    }
}