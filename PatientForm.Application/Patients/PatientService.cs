using PatientForm.Application.DTOs;
using PatientForm.Domain.Entities;
using PatientForm.Domain.Repositories;
using PatientForm.Infrastructure.Exceptions;

namespace PatientForm.Application.Patients;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    public async Task<PatientDto> Get(string id)
    {
        var patient = await patientRepository.Get(id);
        
        if(patient == null)
            throw new NotFoundException(nameof(Patient), id);

        return PatientDto.FromEntity(patient);
    }
    
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
        if (string.IsNullOrEmpty(patient.Id) || !await patientRepository.Exists(patient.Id))
        {
            throw new NotFoundException(nameof(Patient), patient.Id);
        }
            
        await patientRepository.Update(patient.ToEntity());
    }
    
    public async Task Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !await patientRepository.Exists(id))
        {
            throw new NotFoundException(nameof(Patient), id);
        }

        await patientRepository.Delete(id);
    }
}