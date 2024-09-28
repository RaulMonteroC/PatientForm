using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers.Steps;

internal class CreateUserTableStep(IDatabaseInitializerRepository repository,
                                   ILogger<CreateUserTableStep> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Creating user table");

        await repository.RunQuery(@"
            IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
			            WHERE TABLE_SCHEMA = 'dbo' 
			            AND  TABLE_NAME = 'tblUser'))
			            
	            CREATE TABLE dbo.tblUser 
	            (
		            Id VARCHAR(36) NOT NULL DEFAULT (NEWID()),
		            Username VARCHAR(50) NOT NULL,
		            PasswordHash binary(64) NOT NULL,
		            Salt VARCHAR(36) NOT NULL,
		            FirstName VARCHAR(50) NULL,
		            LastName VARCHAR(50) NULL
		            CONSTRAINT pk_tblUser_Id PRIMARY KEY (Id)
	            );");
    }
}