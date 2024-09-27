using Microsoft.Extensions.DependencyInjection;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}