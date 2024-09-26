using Microsoft.Extensions.DependencyInjection;
using PatientForm.Application.Patients;
using PatientForm.Domain.Repositories;

namespace PatientForm.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();
    }
}