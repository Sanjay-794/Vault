using Devnet.Vault.Domain.Common;
using Devnet.Vault.Domain.Entities.Masters;

namespace Devnet.Vault.Domain.Entities.Identity;

public class RoleFeatures : AuditProperty
{
    public int RoleId { get; private set; } // Related to Roles.RoleId
    public int FeatureId { get; private set; } // Related to Features.FeatureId
    public Roles Role { get; private set; } = null!;
    public Features Feature { get; private set; } = null!;
}
