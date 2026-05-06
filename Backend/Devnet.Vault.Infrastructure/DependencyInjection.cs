using Devnet.Vault.Domain.Constants.AppKeys;
using Devnet.Vault.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Devnet.Vault.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection _services, IConfiguration _config)
    {
        var connectionString = _config[ConfigKeys.MYSQL_CONNECTION_STRINGS_KEY];

        _services.AddScoped<DbConnectionFactory>();

        _services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
        });

        return _services;
    }
}