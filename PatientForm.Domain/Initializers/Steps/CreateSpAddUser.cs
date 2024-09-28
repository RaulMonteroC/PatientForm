using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers.Steps;

internal class CreateSpAddUser(IDatabaseInitializerRepository repository,
                               ILogger<CreateSpAddUser> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Creating sp_add_user stored procedure");

        await repository.RunQuery(@"
            CREATE OR ALTER PROCEDURE dbo.sp_add_user
            (
                @username VARCHAR(50), 
                @password VARCHAR(50),
                @firstName VARCHAR(50) = NULL, 
                @lastName VARCHAR(50) = NULL,
                @responseMessage NVARCHAR(250) OUTPUT
            )
            AS
            BEGIN
                SET NOCOUNT ON

	            DECLARE @salt VARCHAR(36) = CAST(NEWID() AS VARCHAR(36))
                BEGIN TRY

                    INSERT INTO tblUser(username, PasswordHash, salt, FirstName, LastName)
                    VALUES(@username, HASHBYTES('SHA2_512', @password + @salt), @salt, @firstName, @lastName)

                    SET @responseMessage='Success'

                END TRY
                BEGIN CATCH
                    SET @responseMessage=ERROR_MESSAGE() 
                END CATCH
            END");
    }
}