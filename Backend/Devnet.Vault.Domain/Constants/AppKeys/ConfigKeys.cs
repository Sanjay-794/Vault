namespace Devnet.Vault.Domain.Constants.AppKeys;


/// <summary>
/// Constants for configuration keys used in the application.
/// These keys are used to access specific settings from the configuration files (e.g., appsettings.json) or environment variables.
/// </summary>
public static class ConfigKeys
{
    public const string CORS_ALLOWED_ORIGINS_KEY = "CorsSettings:AllowedOrigins";
    public const string CORS_POLICY_NAME = "Allow_Vault_Client";

    public const string JWT_SETTINGS_KEY = "JwtSettings";
    public const string JWT_SETTINGS_AUDIENCE_KEY = "JwtSettings:Audience";
    public const string JWT_SETTINGS_ISSUER_KEY = "JwtSettings:Issuer";
    public const string JWT_SETTINGS_SECRET_KEY = "JwtSettings:SecretKey";

    public const string EMAIL_SETTINGS_KEY = "EmailSettings";

    public const string CONNECTION_STRINGS_KEY = "ConnectionStrings";
    public const string MYSQL_CONNECTION_STRINGS_KEY = "ConnectionStrings:MySqlConnection";

    public const string ENCRYPTION_SETTINGS_KEY = "EncryptionSettings";
}
