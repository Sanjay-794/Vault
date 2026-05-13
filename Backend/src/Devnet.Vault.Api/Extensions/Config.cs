using Devnet.Vault.Application.Configurations;
using Devnet.Vault.Domain.Constants.AppKeys;

namespace Devnet.Vault.Api.Extensions;

public static class Config
{
    public static void BindAppsettings(this IServiceCollection _services, IConfiguration _config)
    {
        _services.Configure<ConnectionStringSettings>(_config.GetSection(ConfigKeys.CONNECTION_STRINGS_KEY));
        _services.Configure<CloudFareR2Settings>(_config.GetSection(ConfigKeys.R2_SETTINGS_KEY));
        _services.Configure<JwtSettings>(_config.GetSection(ConfigKeys.JWT_SETTINGS_KEY));
        _services.Configure<EmailSettings>(_config.GetSection(ConfigKeys.EMAIL_SETTINGS_KEY));
        _services.Configure<EncryptionSettings>(_config.GetSection(ConfigKeys.ENCRYPTION_SETTINGS_KEY));
    }
}
