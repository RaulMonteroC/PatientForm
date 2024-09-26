using PatientForm.Application.DTOs;
using PatientForm.Domain.Repositories;

namespace PatientForm.Application.Patients;

internal class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<IEnumerable<PatientDto>> GetAll(int page, int pageSize)
    {
        var patients = await patientRepository.Get(page, pageSize);

        return patients.Select(PatientDto.FromEntity);
    }
}