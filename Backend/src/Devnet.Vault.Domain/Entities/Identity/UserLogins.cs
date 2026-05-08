namespace Devnet.Vault.Domain.Entities.Identity;

public class UserLogins
{
    public long UserLoginId { get; set; }
    public long UserId { get; set; } // Related to UserDetails.UserId
    public string RefreshTokenHash { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public string? IpAddress { get; set; }
    public UserDetails User { get; set; } = null!;
}
