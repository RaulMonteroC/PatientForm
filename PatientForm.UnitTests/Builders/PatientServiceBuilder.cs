using FakeItEasy;
using PatientForm.Application.Patients;
using PatientForm.Domain.Repositories;

namespace PatientForm.UnitTests.Builders;

public class PatientServiceBuilder : IBuilder<PatientService>
{
    private IPatientRepository _patientRepository = A.Fake<IPatientRepository>();

    public PatientServiceBuilder WithPatientRepository(IPatientRepository repository)
    {
        _patientRepository = repository;
        
        return this;
    }
    
    public PatientService Build() => new(_patientRepository);
}