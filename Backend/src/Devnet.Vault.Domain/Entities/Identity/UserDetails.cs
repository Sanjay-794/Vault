using Devnet.Vault.Domain.Common;
using Devnet.Vault.Domain.Entities.Masters;

namespace Devnet.Vault.Domain.Entities.Identity;

public class UserDetails : AuditProperty
{
    public long UserId { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Email { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }
    public int CountryId { get; private set; } // Related to Countries.CountryId
    public int RoleId { get; private set; } // Related to Roles.RoleId
    public DateTime? LastLoginDate { get; private set; }
    public bool IsDeactivated { get; private set; }
    public DateTime? DeactivatedAt { get; private set; }
    public long? DeactivatedBy { get; private set; }

    public Countries? Country { get; private set; }
    public Roles? Role { get; private set; }
}