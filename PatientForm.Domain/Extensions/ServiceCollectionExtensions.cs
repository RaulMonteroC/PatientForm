using Microsoft.Extensions.DependencyInjection;
using PatientForm.Domain.Initializers;
using PatientForm.Domain.Initializers.Steps;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDatabaseInitializerRepository, DatabaseInitializerRepository>();

        services.AddScoped<IInitializationStep, CreateInsuranceTableStep>();
        services.AddScoped<IInitializationStep, CreatePatientTableStep>();
        services.AddScoped<IInitializationStep, CreateUserTableStep>();
        services.AddScoped<IInitializationStep, CreateSpFetchPatientsStep>();
        services.AddScoped<IInitializationStep, CreateSpAddUser>();
        services.AddScoped<IInitializationStep, CreateSpLoginUserStep>();
        services.AddScoped<IInitializationStep, AddDefaultUserStep>();
        services.AddScoped<IInitializationStep, SeedPatientTableStep>();
        
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
    }
}