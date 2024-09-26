using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientForm.Infrastructure.Configuration;

namespace PatientForm.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionSettings>(configuration.GetSection("ConnectionStrings"));
    }
}