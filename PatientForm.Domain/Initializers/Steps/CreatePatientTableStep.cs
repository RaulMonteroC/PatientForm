using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers.Steps;

internal class CreatePatientTableStep(IDatabaseInitializerRepository repository,
                                      ILogger<CreatePatientTableStep> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Creating patient table");
        
        await repository.RunQuery(@"
            IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
			            WHERE TABLE_SCHEMA = 'dbo' 
			            AND  TABLE_NAME = 'tblPatient'))
			            
	            CREATE TABLE dbo.tblPatient 
	            (
		            Id CHAR(36) NOT NULL DEFAULT (NEWID()),
		            Name VARCHAR(100) NOT NULL,
		            LastName VARCHAR(100) NOT NULL,
		            PhoneNumber VARCHAR(15) NULL,
		            Email VARCHAR(35) NULL,
		            InsuranceId CHAR(36) NULL DEFAULT (NULL),
		            UpdatedAt DATETIME NULL DEFAULT (GETDATE()),
		            DeletedAt DATETIME NULL
		            CONSTRAINT pk_tblPatient PRIMARY KEY (Id)
		            CONSTRAINT fk_tblPatient_tblInsurance FOREIGN KEY (InsuranceId) REFERENCES dbo.tblInsurance (Id)
	            );");
    }
}