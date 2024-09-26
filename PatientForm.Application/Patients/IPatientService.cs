
using PatientForm.Application.DTOs;

namespace PatientForm.Application.Patients;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAll(int page, int pageSize);
}