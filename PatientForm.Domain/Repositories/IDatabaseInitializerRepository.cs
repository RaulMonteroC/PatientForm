namespace PatientForm.Domain.Repositories;

public interface IDatabaseInitializerRepository
{
    Task<int> RunQuery(string query);
}