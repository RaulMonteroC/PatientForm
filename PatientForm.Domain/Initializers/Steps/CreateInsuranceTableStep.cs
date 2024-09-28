using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers.Steps;

internal class CreateInsuranceTableStep(IDatabaseInitializerRepository repository,
                                      ILogger<CreateInsuranceTableStep> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Creating insurance table");
        
        await repository.RunQuery(@"
            IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
			            WHERE TABLE_SCHEMA = 'dbo' 
			            AND  TABLE_NAME = 'tblInsurance'))
			            
	            CREATE TABLE dbo.tblInsurance 
                (
		            Id CHAR(36) NOT NULL DEFAULT (newid()),
		            Number VARCHAR(30) NOT NULL,
		            Company VARCHAR(50) NOT NULL,
		            ExpirationDate DATE NOT NULL,
		            PhotoUrl VARCHAR(300) NULL
		            CONSTRAINT pk_tblInsurance PRIMARY KEY (Id)
	            );");
    }
}