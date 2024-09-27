using PatientForm.Domain.Repositories;
using PatientForm.Infrastructure.Exceptions;

namespace PatientForm.Application.Users;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<bool> Login(string username, string password)
    {
         var userFound = await userRepository.Login(username, password);

         if (!userFound)
             throw new NotFoundException("Incorrect username or password");

         return userFound;
    }
}