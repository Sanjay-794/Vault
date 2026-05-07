using Devnet.Vault.Domain.Common;
using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Domain.Entities.Masters;

public class Features : AuditProperty
{
    public int FeatureId { get; private set; }
    public string FeatureName { get; private set; } = null!;
    public string? Description { get; private set; }
    public int? SubfeatureId { get; private set; } // Related to Subfeatures.SubfeatureId
    public FeatureGroups FeatureGroup { get; private set; }
    public Features? ParentFeature { get; set; }
    public ICollection<Features> SubFeatures { get; set; } = [];
}