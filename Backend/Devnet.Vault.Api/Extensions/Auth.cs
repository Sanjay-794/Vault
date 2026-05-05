using Devnet.Vault.Domain.Constants.AppKeys;
using Devnet.Vault.Domain.Constants.Messages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Devnet.Vault.Api.Extensions;

public static class Auth
{
    public static IServiceCollection AddAuthPolicy(this IServiceCollection _services, IConfiguration _config)
    {
        _services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var authHeader = context.Request.Headers.Authorization.FirstOrDefault();

                    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                    {
                        context.Token = authHeader["Bearer ".Length..].Trim();
                    }
                    else
                    {
                        context.Token = context.Request.Cookies["AccessToken"];
                    }

                    return Task.CompletedTask;
                }
            };
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = _config[ConfigKeys.JWT_SETTINGS_AUDIENCE_KEY],
                ValidIssuer = _config[ConfigKeys.JWT_SETTINGS_ISSUER_KEY],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config[ConfigKeys.JWT_SETTINGS_SECRET_KEY] ?? throw new Exception(ExceptionMessages.JWT_SECRET_KEY_NOT_FOUND)))

            };
        });

        return _services;
    }
}
