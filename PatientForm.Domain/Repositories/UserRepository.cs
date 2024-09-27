using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PatientForm.Infrastructure.Configuration;

namespace PatientForm.Domain.Repositories;

public class UserRepository(IOptions<ConnectionSettings> settings,
                            ILogger<UserRepository> logger) 
    : AbstractRepository(settings,logger), 
      IUserRepository
{
    public async Task<bool> Login(string username, string password) =>
        await ExecuteActionQuery("sp_login_user",
                                 [
                                     new SqlParameter("@username", value: username),
                                     new SqlParameter("@password", value: password)
                                 ],
                                 CommandType.StoredProcedure) == 1;
}