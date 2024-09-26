using PatientForm.Application.DTOs;
using PatientForm.Domain.Repositories;

namespace PatientForm.Application.Patients;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<IEnumerable<PatientDto>> GetAll(int page, int pageSize)
    {
        var patients = await patientRepository.Get(page, pageSize);

        return patients.Select(PatientDto.FromEntity);
    }

    public async Task Save(PatientDto patient)
    {
        await patientRepository.Save(patient.ToEntity());
    }
}