namespace Devnet.Vault.Domain.Entities.Identity;

public class UserLogins
{
    public long UserLoginId { get; private set; }
    public long UserId { get; private set; } // Related to UserDetails.UserId
    public string RefreshTokenHash { get; private set; } = null!;
    public DateTime CreatedDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public bool IsRevoked { get; private set; }
    public string? IpAddress { get; private set; }
    public UserDetails User { get; private set; } = null!;
}
