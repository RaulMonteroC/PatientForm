using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers;

public class CreateInsuranceTableStep(IDatabaseInitializerRepository repository,
                                      ILogger<CreateInsuranceTableStep> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Creating insurance table");
        
        await repository.RunQuery(@"
            IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
			            WHERE TABLE_SCHEMA = 'dbo' 
			            AND  TABLE_NAME = 'tblInsurance'))
			            
	            CREATE TABLE dbo.tblInsurance (
		            Id char(36) NOT NULL DEFAULT (newid()),
		            Number varchar(30) NOT NULL,
		            Company varchar(50) NOT NULL,
		            ExpirationDate date NOT NULL,
		            PhotoUrl varchar(300) NULL
		            CONSTRAINT pk_tblInsurance PRIMARY KEY (Id)
	            );");
    }
}