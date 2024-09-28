using Microsoft.Extensions.Logging;
using PatientForm.Domain.Initializers.Steps;

namespace PatientForm.Domain.Initializers;

public class DatabaseInitializer(IEnumerable<IInitializationStep> steps,
                                 ILogger<DatabaseInitializer> logger) : IDatabaseInitializer
{
    public async Task Initialize()
    {
        logger.LogInformation("Start database initialization");
        
        foreach (var step in steps)
        {
            await step.Execute();
        }
        
        logger.LogInformation("complete database initialization");
    }
}