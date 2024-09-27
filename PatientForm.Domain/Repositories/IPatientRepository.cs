using PatientForm.Domain.Entities;

namespace PatientForm.Domain.Repositories;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> Get(int page, int pageSize);
    Task Save(Patient patient);
    Task Update(Patient patient);
    Task Delete(string id);
    Task<bool> Exists(string id);
}