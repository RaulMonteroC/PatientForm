using PatientForm.Domain.Entities;

namespace PatientForm.Domain.Repositories;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> Get(int page, int pageSize);
}