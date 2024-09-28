using Microsoft.AspNetCore.Mvc;
using PatientForm.Api.Authorization;
using PatientForm.Api.Filters;
using PatientForm.Application.DTOs;
using PatientForm.Application.Users;

namespace PatientForm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[LogActionFilter]
public class UserController(IUserService userService,
                            ITokenProvider tokenProvider) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] UserCredentials user)
    {
        await userService.Login(user.Username, user.Password);

        const int ageInMinutes = 20;
        
        var expiry = DateTime.UtcNow.AddMinutes(ageInMinutes);

        var token = new JsonWebToken
                    {
                        AccessToken = tokenProvider.CreateToken(user, expiry),
                        ExpiresIn = ageInMinutes * 60
                    };
        
        return Accepted(new LoginResult(token.AccessToken));
    }
}