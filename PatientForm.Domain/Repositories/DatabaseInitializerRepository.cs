using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PatientForm.Infrastructure.Configuration;

namespace PatientForm.Domain.Repositories;

internal class DatabaseInitializerRepository(IOptions<ConnectionSettings> settings, 
                                             ILogger<DatabaseInitializerRepository> logger) 
    : AbstractRepository(settings, logger),
      IDatabaseInitializerRepository
{
    public Task<int> RunQuery(string query) =>
        ExecuteActionQuery(query, 
                           Array.Empty<SqlParameter>(), 
                           CommandType.Text);
}