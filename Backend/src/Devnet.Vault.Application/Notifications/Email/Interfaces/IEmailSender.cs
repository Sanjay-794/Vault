using Devnet.Vault.Application.Notifications.Email.Models;

namespace Devnet.Vault.Application.Notifications.Email.Interfaces;

public interface IEmailSender
{
    Task SendAsync(EmailMessage message);
}
