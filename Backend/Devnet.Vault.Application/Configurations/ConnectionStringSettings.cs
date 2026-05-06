namespace Devnet.Vault.Application.Configurations;

public class ConnectionStringSettings
{
    public string MySqlConnection { get; set; } = string.Empty;
    public string RedisConnection { get; set; } = string.Empty;
}
