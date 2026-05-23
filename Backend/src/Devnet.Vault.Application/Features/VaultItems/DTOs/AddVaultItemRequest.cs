using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.VaultItems.DTOs;

public class AddVaultItemRequest
{
    public string Title { get; set; } = string.Empty;
    public EntryType EntryType { get; set; }
    public string Data { get; set; } = string.Empty;
    public bool IsFavourite { get; set; }
    public long? GroupId { get; set; }
}

public class AddVaultItemResponse
{
    public long VaultItemId { get; set; }
}