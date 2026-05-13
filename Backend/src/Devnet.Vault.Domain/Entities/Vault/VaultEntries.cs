using Devnet.Vault.Domain.Common;
using Devnet.Vault.Domain.Entities.Groups;
using Devnet.Vault.Domain.Entities.Identity;
using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Domain.Entities.Vault;

public class VaultEntries : AuditProperty
{
    public long VaultEntryId { get; set; }

    public string Title { get; set; } = string.Empty;
    public EntryType EntryType { get; set; }

    // Encrypted JSON/Text payload
    public string EncryptedData { get; set; } = string.Empty;

    public long OwnerId { get; set; }

    // Group
    public long? GroupId { get; set; }

    public bool IsFavourite { get; set; }

    // Navigation
    public GroupDetails? Group { get; set; }
    public UserDetails? Owner { get; set; }
}
