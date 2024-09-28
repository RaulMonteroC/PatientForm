using Microsoft.IdentityModel.Tokens;
using PatientForm.Application.DTOs;

namespace PatientForm.Api.Authorization;

public interface ITokenProvider
{
    string CreateToken(UserCredentials user, DateTime expiry);
    
    TokenValidationParameters GetValidationParameters();
}