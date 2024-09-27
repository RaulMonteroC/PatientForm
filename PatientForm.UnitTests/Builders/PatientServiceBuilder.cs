using FakeItEasy;
using Microsoft.Extensions.Logging;
using PatientForm.Application.Patients;
using PatientForm.Domain.Repositories;

namespace PatientForm.UnitTests.Builders;

public class PatientServiceBuilder : IBuilder<PatientService>
{
    private IPatientRepository _patientRepository = A.Fake<IPatientRepository>();
    private ILogger<PatientService> _logger = A.Fake<ILogger<PatientService>>();

    public PatientServiceBuilder WithPatientRepository(IPatientRepository repository)
    {
        _patientRepository = repository;
        
        return this;
    }
    
    public PatientService Build() => new(_patientRepository,
                                         _logger);
}