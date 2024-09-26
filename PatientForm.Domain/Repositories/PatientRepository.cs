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
            await connection.OpenAsync();

        command.Parameters.AddWithValue("@PageNumber", page);
        command.Parameters.AddWithValue("@RowsOfPage", pageSize);
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

    public async Task Save(Patient patient)
    {
        await using var connection = new SqlConnection(_settings.PatientDb);
        await using var command = new SqlCommand(GetQuery(), connection);

        if (connection.State == ConnectionState.Closed)
            await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@name", patient.Name);
        command.Parameters.AddWithValue("@lastName", patient.LastName);
        command.Parameters.AddWithValue("@phoneNumber", patient.PhoneNumber);
        command.Parameters.AddWithValue("@email", patient.Email);
        command.Parameters.AddWithValue("@insuranceId", patient.Insurance != null? patient.Insurance : DBNull.Value);
        
        command.CommandType = CommandType.Text;

        await command.ExecuteNonQueryAsync();

        await using var reader = await command.ExecuteReaderAsync();

        string GetQuery() => @"INSERT INTO tblPatient (Name, LastName, PhoneNumber, Email, InsuranceId) 
                               VALUES(@name, @lastName, @phoneNumber, @email, @insuranceId)"; 
    }
}