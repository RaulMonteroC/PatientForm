using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers.Steps;

internal class CreateSpLoginUserStep(IDatabaseInitializerRepository repository,
                                     ILogger<CreateSpLoginUserStep> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Creating sp_login_user stored procedure");
        
        await repository.RunQuery(@"
            CREATE OR ALTER PROCEDURE dbo.sp_login_user
            (
                @username VARCHAR(36),
                @password VARCHAR(50)
            )
            AS
            BEGIN

                SET NOCOUNT ON

                DECLARE @userID VARCHAR(36)

                BEGIN
                    IF EXISTS (SELECT Id FROM tblUser 
        			            WHERE Username = @username
        			            AND PasswordHash = HASHBYTES('SHA2_512', @password+CAST(Salt AS VARCHAR(36))))
        	            RETURN 1
                    ELSE
        	            RETURN 0
                END
            END");
    }
}