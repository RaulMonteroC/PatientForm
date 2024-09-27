using Microsoft.Extensions.Logging;
using PatientForm.Application.DTOs;
using PatientForm.Domain.Entities;
using PatientForm.Domain.Repositories;
using PatientForm.Infrastructure.Exceptions;

namespace PatientForm.Application.Patients;

public class PatientService(IPatientRepository patientRepository,
                            ILogger<PatientService> logger) : IPatientService
{
    public async Task<PatientDto> Get(string id)
    {
        logger.LogInformation("Getting patient with Id {id}", id);
        
        var patient = await patientRepository.Get(id);
        
        if(patient == null)
            throw new NotFoundException(nameof(Patient), id);

        return PatientDto.FromEntity(patient);
    }
    
    public async Task<IEnumerable<PatientDto>> GetAll(int page, int pageSize)
    {
        logger.LogInformation("Getting {pageSize} patients from page {page}", pageSize, page);
        
        var patients = await patientRepository.Get(page, pageSize);

        return patients.Select(PatientDto.FromEntity);
    }

    public async Task Save(PatientDto patient)
    {
        logger.LogInformation("Saving patient information for {name} {LastName}", patient.Name, patient.LastName);
        
        await patientRepository.Save(patient.ToEntity());
    }
    
    public async Task Update(PatientDto patient)
    {
        logger.LogInformation("Updating patient information for patient id {id}", patient.Id);
        
        if (string.IsNullOrEmpty(patient.Id) || !await patientRepository.Exists(patient.Id))
        {
            throw new NotFoundException(nameof(Patient), patient.Id);
        }
            
        await patientRepository.Update(patient.ToEntity());
    }
    
    public async Task Delete(string id)
    {
        logger.LogInformation("Deleting patient information for patient id {id}", id);
        
        if (string.IsNullOrEmpty(id) || !await patientRepository.Exists(id))
        {
            throw new NotFoundException(nameof(Patient), id);
        }

        await patientRepository.Delete(id);
    }
}