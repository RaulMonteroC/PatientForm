using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PatientForm.Application.Patients;
using PatientForm.Application.Users;

namespace PatientForm.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;
        
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddValidatorsFromAssembly(assembly)
                .AddFluentValidationAutoValidation();
    }
}