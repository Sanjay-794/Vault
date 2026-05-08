using Devnet.Vault.Domain.Entities.Identity;

namespace Devnet.Vault.Application.Features.Account.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<UserDetails?> GetUserDetailsByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<UserDetails?> GetUserDetailsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    public Task<UserDetails?> GetUserDetailsByUserIdAsync(long userId, CancellationToken cancellationToken);
}
