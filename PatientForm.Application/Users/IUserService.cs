namespace PatientForm.Application.Users;

public interface IUserService
{
    Task<bool> Login(string username, string password);
}