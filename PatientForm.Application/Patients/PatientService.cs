using PatientForm.Application.DTOs;
using PatientForm.Domain.Entities;
using PatientForm.Domain.Repositories;
using PatientForm.Infrastructure.Exceptions;

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
    
    public async Task Update(PatientDto patient)
    {
        if (patient.Id == Guid.Empty || !await patientRepository.Exists(patient.Id.ToString()))
        {
            throw new NotFoundException(nameof(Patient), patient.Id.ToString());
        }
            
        await patientRepository.Update(patient.ToEntity());
    }
}