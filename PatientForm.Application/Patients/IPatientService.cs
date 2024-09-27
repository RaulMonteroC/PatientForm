
using PatientForm.Application.DTOs;

namespace PatientForm.Application.Patients;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAll(int page, int pageSize);
    Task Save(PatientDto patient);
    Task Update(PatientDto patient);
    Task Delete(string id);
}