using Devnet.Vault.Domain.Entities.Identity;

namespace Devnet.Vault.Application.Features.Auth.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<UserDetails?> RegisterNewUserAsync(UserDetails userDetails, CancellationToken cancellationToken);
    Task<bool> SaveUserLoginDetailsAsync(UserLogins userLogins, CancellationToken cancellationToken);
    Task<UserLogins?> GetUserLoginByRefreshTokenHashAsync(string refreshTokenHash, CancellationToken cancellationToken);
    Task<bool> RevokeUserLoginAsync(long userLoginId, CancellationToken cancellationToken);
}
