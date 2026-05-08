using Devnet.Vault.Domain.Common;
using Devnet.Vault.Domain.Entities.Masters;

namespace Devnet.Vault.Domain.Entities.Identity;

public class UserDetails : AuditProperty
{
    public long UserId { get; set; }
    public string? Name { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public int? CountryId { get; set; } // Related to Countries.CountryId
    public int? RoleId { get; set; } // Related to Roles.RoleId
    public DateTime? LastLoginDate { get; set; }
    public bool IsDeactivated { get; set; }
    public DateTime? DeactivatedAt { get; set; }
    public long? DeactivatedBy { get; set; }

    public Countries? Country { get; set; }
    public Roles? Role { get; set; }
}