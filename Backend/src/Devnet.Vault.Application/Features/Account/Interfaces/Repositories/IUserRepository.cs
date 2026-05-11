using Devnet.Vault.Domain.Entities.Identity;

namespace Devnet.Vault.Application.Features.Account.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<UserDetails?> GetUserDetailsByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<UserDetails?> GetUserDetailsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    public Task<UserDetails?> GetUserDetailsByUserIdAsync(long userId, CancellationToken cancellationToken);
    Task<bool> UpdateUserDetailsAsync(long userId, long updatedBy, string? profileUrl, string? name, CancellationToken cancellationToken);
    Task<bool> DeactivateUserAsync(long userId, long deactivatedBy, CancellationToken cancellationToken);
    Task<bool> DeleteUserAsync(long userId, long deletedBy, CancellationToken cancellationToken);

}
