using Devnet.Vault.Domain.Common;
using Devnet.Vault.Domain.Entities.Groups;
using Devnet.Vault.Domain.Entities.Identity;

namespace Devnet.Vault.Domain.Entities.Vault;

public class VaultFiles : AuditProperty
{
    public long VaultFileId { get; set; }

    // Original filename uploaded by user
    public string FileName { get; set; } = string.Empty;

    // Unique storage key/path in R2
    public string FileKey { get; set; } = string.Empty;

    // MIME type
    public string ContentType { get; set; } = string.Empty;

    // File size in bytes
    public long FileSize { get; set; }

    // Optional extension
    public string? Extension { get; set; } = string.Empty;

    // Owner
    public long OwnerId { get; set; }

    // Optional folder/group
    public long? GroupId { get; set; }

    public bool IsFavourite { get; set; }

    // Optional metadata
    public string? MetadataJson { get; set; }

    // Navigation
    public GroupDetails? Group { get; set; }

    public UserDetails? Owner { get; set; }
}
