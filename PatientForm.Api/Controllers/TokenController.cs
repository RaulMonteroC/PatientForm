// using Microsoft.AspNetCore.Mvc;
// using PatientForm.Api.Authorization;
// using PatientForm.Api.Filters;
// using PatientForm.Domain.Entities;
//
// namespace PatientForm.Api.Controllers;
//
// [Route("api/token")]
// [ApiController]
// [LogActionFilter]
// public class TokenController(ITokenProvider tokenProvider) : ControllerBase
// {
//     public JsonWebToken Get([FromQuery] string grantType, 
//                             [FromQuery] string username, 
//                             [FromQuery] string password, 
//                             [FromQuery] string refreshToken)
//     {
//         // Authenticate depending on the grant type.
//         var user = grantType == "RefreshToken" ? GetUserByToken(refreshToken) : GetUserByCredentials(username, password);
//
//         if (user == null)
//             throw new UnauthorizedAccessException("No!");
//
//         var ageInMinutes = 20;  // However long you want...
//
//         var expiry = DateTime.UtcNow.AddMinutes(ageInMinutes);
//
//         var token = new JsonWebToken {
//                                          AccessToken = tokenProvider.CreateToken(user, expiry),
//                                          ExpiresIn   = ageInMinutes * 60
//                                      };
//
//         if (grantType != "RefreshToken")
//             token.RefreshToken = GenerateRefreshToken(user);
//
//         return token;
//     }
//
//     private User GetUserByToken(string refreshToken)
//     {
//         // TODO: Check token against your database.
//         if (refreshToken == "test")
//             return new User { UserName = "test" };
//
//         return null;
//     }
//
//     private User GetUserByCredentials(string username, string password)
//     {
//         // TODO: Check username/password against your database.
//         if (username == password)
//             return new User { UserName = username };
//
//         return null;
//     }
//
//     private string GenerateRefreshToken(User user)
//     {
//         // TODO: Create and persist a refresh token.
//         return "test";
//     }
// }