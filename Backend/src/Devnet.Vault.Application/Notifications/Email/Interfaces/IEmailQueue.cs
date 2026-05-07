using Devnet.Vault.Application.Notifications.Email.Models;

namespace Devnet.Vault.Application.Notifications.Email.Interfaces;

public interface IEmailQueue
{
    void Enqueue(EmailMessage message);
    Task<EmailMessage> DequeueAsync(CancellationToken ct);
}
