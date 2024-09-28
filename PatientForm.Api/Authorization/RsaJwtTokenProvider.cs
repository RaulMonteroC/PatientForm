using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using PatientForm.Application.DTOs;

namespace PatientForm.Api.Authorization;

public class RsaJwtTokenProvider : ITokenProvider
{
    private RsaSecurityKey _key;
    private string _algorithm;
    private string _issuer;
    private string _audience;

    public RsaJwtTokenProvider(string issuer, string audience, string keyName)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var parameters = new CspParameters { KeyContainerName = keyName };
            var provider = new RSACryptoServiceProvider(2048, parameters);
            _key = new RsaSecurityKey(provider);
        }
        else
        {
            _key = new RsaSecurityKey(RSA.Create().ExportParameters(true));   
        }

        _algorithm = SecurityAlgorithms.RsaSha256Signature;
        _issuer = issuer;
        _audience = audience;
    }

    public string CreateToken(UserCredentials user, DateTime expiry)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var identity = new ClaimsIdentity(new GenericIdentity(user.Username, "jwt"));

        var token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
                                                        {
                                                            Audience = _audience,
                                                            Issuer = _issuer,
                                                            SigningCredentials = new SigningCredentials(_key, _algorithm),
                                                            Expires = expiry.ToUniversalTime(),
                                                            Subject = identity
                                                        });

        return tokenHandler.WriteToken(token);
    }

    public TokenValidationParameters GetValidationParameters() =>
        new()
        {
            IssuerSigningKey = _key,
            ValidAudience = _audience,
            ValidIssuer = _issuer,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(0)
        };
}