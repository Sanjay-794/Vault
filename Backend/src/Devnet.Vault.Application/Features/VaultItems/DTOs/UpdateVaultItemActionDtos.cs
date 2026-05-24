using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.VaultItems.DTOs;

public class UpdateVaultItemActionDtos
{
    public long VaultEntryId { get; set; }
}

public class UpdateVaultItemTitleDto : UpdateVaultItemActionDtos
{
    public string Title { get; set; } = null!;
}

public class UpdateVaultItemDataDto : UpdateVaultItemActionDtos
{
    public string EncryptedData { get; set; } = null!;
}

public class UpdateVaultItemGroupDto : UpdateVaultItemActionDtos
{
    public long GroupId { get; set; }
    public GroupType GroupType { get; set; }
}