using Devnet.Vault.Application.Features.Auth.Interfaces.Repositories;
using Devnet.Vault.Domain.Entities.Identity;
using Devnet.Vault.Infrastructure.Persistence.Context;

namespace Devnet.Vault.Infrastructure.Persistence.Repositories.Implementations.Authentication;

public class AuthRepository(AppDbContext _dbContext) : IAuthRepository
{
    /// <summary>
    /// Register a new user in the system
    /// </summary>
    public async Task<UserDetails?> RegisterNewUserAsync(UserDetails userDetails, CancellationToken cancellationToken)
    {
        try
        {
            _dbContext.Users.Add(userDetails);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return userDetails;
        }
        catch (Exception ex)
        {
            _dbContext.ChangeTracker.Clear(); // Clear the change tracker to prevent inconsistent state
            throw new InvalidOperationException("Failed to register user", ex);
        }
    }

    /// <summary>
    /// Save user login details for session tracking
    /// </summary>
    public async Task<bool> SaveUserLoginDetailsAsync(UserLogins userLogins, CancellationToken cancellationToken)
    {
        try
        {
            _dbContext.UserLogins.Add(userLogins);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _dbContext.ChangeTracker.Clear(); // Clear the change tracker to prevent inconsistent state
            throw new InvalidOperationException("Failed to save login details", ex);
        }
    }
}
