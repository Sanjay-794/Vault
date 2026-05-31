using Devnet.Vault.Domain.Entities.Vault;
using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.VaultItems.Interfaces;

public interface IVaultItemsRepository
{
    Task<VaultEntries?> AddNewVaultItem(VaultEntries vaultEntry, CancellationToken cancellationToken);
    Task<bool> DoesEntryExists(VaultEntries vaultEntry, CancellationToken cancellationToken);
    Task<bool> IsGroupValidForItems(long entryId, long ownerId, CancellationToken cancellationToken);
    Task<VaultEntries?> ReuseDeletedEntry(VaultEntries vaultEntry, CancellationToken cancellationToken);
    Task<bool> UpdateEntryTitle(string title, long entryId, long ownerId, long updatedBy, CancellationToken cancellationToken);
    Task<bool> UpdateEncryptedData(string encryptedData, long entryId, long ownerId, long updatedBy, CancellationToken cancellationToken);
    Task<bool> DeleteEntry(long entryId, long ownerId, long deletedBy, CancellationToken cancellationToken);
    Task<bool> MoveEntryToNewGroup(long entryId, long? newGroupId, GroupType groupType, long ownerId, long updatedBy, CancellationToken cancellationToken);
}
