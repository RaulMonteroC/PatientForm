namespace PatientForm.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> Login(string username, string password);
}