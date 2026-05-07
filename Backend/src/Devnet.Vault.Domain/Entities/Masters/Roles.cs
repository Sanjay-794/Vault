using Devnet.Vault.Domain.Common;
using Devnet.Vault.Domain.Entities.Identity;

namespace Devnet.Vault.Domain.Entities.Masters;

public class Roles : AuditProperty
{
    public int RoleId { get; private set; }
    public string RoleName { get; private set; } = null!;
    public string? Description { get; private set; }
    public ICollection<RoleFeatures> RoleFeatures { get; private set; } = [];
}
