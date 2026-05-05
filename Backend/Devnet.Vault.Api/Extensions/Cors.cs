using Devnet.Vault.Domain.Constants.AppKeys;

namespace Devnet.Vault.Api.Extensions;

public static class Cors
{
    public static IServiceCollection AddCORSPolicy(this IServiceCollection _services, IConfiguration _config)
    {
        var allowedOrigins = _config
            .GetSection(ConfigKeys.CORS_ALLOWED_ORIGINS)
            .Get<string[]>() ?? [];

        _services.AddCors(options =>
        {
            options.AddPolicy(ConfigKeys.CORS_POLICY_NAME, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        return _services;
    }
}
