using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers.Steps;

internal class AddDefaultUserStep(IDatabaseInitializerRepository repository,
                                  ILogger<AddDefaultUserStep> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Inserting default user record on tblUsers table");
        
        await repository.RunQuery(@"
            DECLARE @responseMessage NVARCHAR(250)
            EXEC dbo.sp_add_user
                      @username = 'Admin',
                      @password = '123',
                      @firstName = 'Admin',
                      @lastName = 'Administrator',
                      @responseMessage = @responseMessage OUTPUT
            ");
    }
}