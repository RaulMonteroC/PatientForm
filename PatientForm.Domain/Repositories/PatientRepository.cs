using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using PatientForm.Domain.Entities;
using PatientForm.Infrastructure.Configuration;

namespace PatientForm.Domain.Repositories;

internal class PatientRepository(IOptions<ConnectionSettings> settings) : AbstractRepository(settings), IPatientRepository
{
    public async Task<Patient?> Get(string id) =>
        (await ExecuteQuery(@"SELECT p.Id,
	                            p.Name,
	                            p.LastName,
	                            i.Number AS InsuranceNumber,
	                            p.PhoneNumber,
	                            p.Email
                            FROM tblPatient p
                            LEFT JOIN tblInsurance i ON p.InsuranceId = i.Id
                            WHERE p.Id = @id
                            AND p.DeletedAt IS NULL",
                     [new SqlParameter("@id", value: id)],
                     CommandType.Text,
                     reader => new Patient(reader["Id"].ToString()!,
                                           reader["Name"].ToString()!,
                                           reader["LastName"].ToString()!,
                                           reader["PhoneNumber"].ToString() ?? string.Empty,
                                           reader["Email"].ToString() ?? string.Empty)))
       .FirstOrDefault();
    
    public Task<IEnumerable<Patient>> Get(int page, int pageSize) =>
        ExecuteQuery("sp_fetch_patients",
                     [
                         new SqlParameter("@PageNumber", value: page),
                         new SqlParameter("@RowsOfPage", value: pageSize)
                     ],
                     CommandType.StoredProcedure,
                     reader => new Patient(reader["Id"].ToString()!,
                                           reader["Name"].ToString()!,
                                           reader["LastName"].ToString()!,
                                           reader["PhoneNumber"].ToString() ?? string.Empty,
                                           reader["Email"].ToString() ?? string.Empty));

    public Task Save(Patient patient) =>
        ExecuteActionQuery(@"INSERT INTO tblPatient (Name, LastName, PhoneNumber, Email, InsuranceId) 
                            VALUES(@name, @lastName, @phoneNumber, @email, @insuranceId)",
                           [
                               new SqlParameter("@name", value: patient.Name),
                               new SqlParameter("@lastName", patient.LastName),
                               new SqlParameter("@phoneNumber", patient.PhoneNumber),
                               new SqlParameter("@email", patient.Email),
                               new SqlParameter("@insuranceId", patient.Insurance != null ? patient.Insurance : DBNull.Value)
                           ],
                           CommandType.Text
                          );

    public Task Update(Patient patient) =>
        ExecuteActionQuery(@"UPDATE tblPatient SET 
                                    Name = @name,
                                    LastName = @lastName,
                                    PhoneNumber = @phoneNumber,
                                    Email = @email,
                                    InsuranceId = @insuranceId
                                    WHERE Id = @id",
                           [
                               new SqlParameter("@id", value: patient.Id),
                               new SqlParameter("@name", value: patient.Name),
                               new SqlParameter("@lastName", patient.LastName),
                               new SqlParameter("@phoneNumber", patient.PhoneNumber),
                               new SqlParameter("@email", patient.Email),
                               new SqlParameter("@insuranceId", patient.Insurance != null ? patient.Insurance : DBNull.Value)
                           ],
                           CommandType.Text
                          );

    public async Task<bool> Exists(string id) =>
        (await ExecuteQuery("SELECT TOP 1 Id FROM tblPatient WHERE Id = @id",
                            [new SqlParameter("@id", value: id)],
                            CommandType.Text,
                            reader => reader["Id"].ToString()!
                           ))
       .Any();
    
    public Task Delete(string id) =>
        ExecuteActionQuery("UPDATE tblPatient SET DeletedAt = GETDATE() WHERE Id = @id",
                           [new SqlParameter("@id", value: id)],
                           CommandType.Text
                          );
}