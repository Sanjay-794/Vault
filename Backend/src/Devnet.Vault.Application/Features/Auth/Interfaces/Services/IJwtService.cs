using Devnet.Vault.Domain.Entities.Identity;

namespace Devnet.Vault.Application.Features.Auth.Interfaces.Services;

public interface IJwtService
{
    string GenerateAccessToken(UserDetails user);
    string GenerateRefreshToken();
}
