using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Domain.Entities.Identity;
using Devnet.Vault.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Devnet.Vault.Infrastructure.Persistence.Repositories.Implementations.Account;


public class UserRepository(AppDbContext _dbContext) : IUserRepository
{
    public async Task<UserDetails?> GetUserDetailsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<UserDetails?> GetUserDetailsByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
    }

    public async Task<UserDetails?> GetUserDetailsByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
    }
}
