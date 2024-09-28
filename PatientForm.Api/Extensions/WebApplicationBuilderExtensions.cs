using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using PatientForm.Api.Authorization;
using Serilog;

namespace PatientForm.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    private const string BEARER_AUTH = "BearerAuth";
    private const string BEARER_SCHEME = "Bearer";
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
                                       {
                                           c.AddSecurityDefinition(BEARER_AUTH,
                                                                   new OpenApiSecurityScheme
                                                                   {
                                                                       Type = SecuritySchemeType.Http,
                                                                       Scheme = BEARER_SCHEME
                                                                   });
                                   
                                           c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                    {
                                                                        {
                                                                            new OpenApiSecurityScheme
                                                                            {
                                                                                Reference = new OpenApiReference
                                                                                            {
                                                                                                Type = ReferenceType.SecurityScheme, 
                                                                                                Id = BEARER_AUTH
                                                                                            }
                                                                            },
                                                                            []
                                                                        }
                                                                    });
                                       });
        
        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    }

    public static void ConfigureAuthorization(this WebApplicationBuilder builder)
    {
        var tokenProvider = new RsaJwtTokenProvider("localhost", "public", "patient-api");
        builder.Services.AddSingleton<ITokenProvider>(tokenProvider);

        builder.Services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
                             {
                                 options.RequireHttpsMetadata = false;
                                 options.TokenValidationParameters = tokenProvider.GetValidationParameters();
                             });

        builder.Services
               .AddAuthorizationBuilder()
               .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser()
                                .Build());
    }
}