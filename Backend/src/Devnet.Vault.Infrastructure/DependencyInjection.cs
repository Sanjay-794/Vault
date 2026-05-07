using Devnet.Vault.Application.Features.Auth.Interfaces.Services;
using Devnet.Vault.Application.Notifications.Email.Interfaces;
using Devnet.Vault.Application.Security.Encryption.Interfaces;
using Devnet.Vault.Domain.Constants.AppKeys;
using Devnet.Vault.Infrastructure.Notifications.Email.Queue;
using Devnet.Vault.Infrastructure.Notifications.Email.Services;
using Devnet.Vault.Infrastructure.Notifications.Email.Workers;
using Devnet.Vault.Infrastructure.Persistence.Context;
using Devnet.Vault.Infrastructure.Security;
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
        _services.AddSingleton<IEmailQueue, InMemoryEmailQueue>();
        _services.AddScoped<IEmailSender, SmtpEmailSender>();

        _services.AddHostedService<EmailWorker>();

        _services.AddScoped<IEncryptionService, EncryptionService>();
        _services.AddScoped<IJwtService, JwtService>();
        return _services;
    }
}