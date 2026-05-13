using Devnet.Vault.Domain.Common;
using Devnet.Vault.Domain.Entities.Vault;
using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Domain.Entities.Groups;

public class GroupDetails : AuditProperty
{
    public long GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public long? ParentGroupId { get; set; }
    public long OwnerId { get; set; }
    public bool IsFavourite { get; set; }
    public string MetadataJson { get; set; } = string.Empty;
    public GroupType GroupType { get; set; }

    // Parent folder
    public GroupDetails? ParentGroup { get; set; }

    // Child folders
    public ICollection<GroupDetails> ChildGroups { get; set; } = [];

    // Vault items inside this group
    public ICollection<VaultEntries> VaultEntries { get; set; } = [];
    public ICollection<VaultFiles> VaultFiles { get; set; } = [];

}
