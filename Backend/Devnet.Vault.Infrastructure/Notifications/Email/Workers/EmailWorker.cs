using Devnet.Vault.Application.Notifications.Email.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Devnet.Vault.Infrastructure.Notifications.Email.Workers;

public class EmailWorker(IEmailQueue _queue, IEmailSender _sender,
    ILogger<EmailWorker> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Email Worker started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var email = await _queue.DequeueAsync(stoppingToken);
                await _sender.SendAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
