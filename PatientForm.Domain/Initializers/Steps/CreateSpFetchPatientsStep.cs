using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers.Steps;

internal class CreateSpFetchPatientsStep(IDatabaseInitializerRepository repository,
                                         ILogger<CreateSpFetchPatientsStep> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Creating sp_fetch_patients stored procedure");
        
        await repository.RunQuery(@"
            CREATE OR ALTER PROCEDURE sp_fetch_patients(@PageNumber AS INT, @RowsOfPage AS INT)
            AS
            SELECT p.Id,
	            p.Name,
	            p.LastName,
	            i.Number AS InsuranceNumber,
	            p.PhoneNumber,
	            p.Email
            FROM tblPatient p
            LEFT JOIN tblInsurance i ON p.InsuranceId = i.Id
            WHERE p.DeletedAt IS NULL
            ORDER BY Name, LastName
            OFFSET (@PageNumber-1)*@RowsOfPage ROWS
            FETCH NEXT @RowsOfPage ROWS ONLY");
    }
}