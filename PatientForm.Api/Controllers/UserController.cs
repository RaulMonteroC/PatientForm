using Microsoft.AspNetCore.Mvc;
using PatientForm.Api.Filters;
using PatientForm.Application.DTOs;
using PatientForm.Application.Users;

namespace PatientForm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[LogActionFilter]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] UserCredentials user)
    {
        await userService.Login(user.Username, user.Password);
        
        return Accepted();
    }
}