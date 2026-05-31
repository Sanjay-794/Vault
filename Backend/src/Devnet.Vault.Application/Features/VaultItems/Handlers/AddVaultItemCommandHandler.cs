using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Application.Features.VaultItems.DTOs;
using Devnet.Vault.Application.Features.VaultItems.Interfaces;
using Devnet.Vault.Application.Security.Encryption.Interfaces;
using Devnet.Vault.Domain.Entities.Vault;
using MediatR;
using static Devnet.Vault.Application.Features.VaultItems.Commands.VaultItemsCommands;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.VaultItems.Handlers;

public class AddVaultItemCommandHandler(IVaultItemsRepository _vaultItemsRepository, IUserRepository _userRepository,
    IEncryptionService _encryptionService) : IRequestHandler<AddNewVaultItemCommand, AddVaultItemResponse>
{
    public async Task<AddVaultItemResponse> Handle(AddNewVaultItemCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        var userId = request.OwnerId;
        var userSecretKey = await _userRepository.GetUserSpecificEncryptionKeyAsync(userId, cancellationToken);
        if (string.IsNullOrEmpty(userSecretKey))
            throw new InvalidOperationException(UserInfoMessages.USER_INFO_RETRIEVAL_FAILED);

        if (req.GroupId != null)
        {
            var isGroupValid = await _vaultItemsRepository.IsGroupValidForItems(req.GroupId ?? 0, userId, cancellationToken);
            if (!isGroupValid)
                throw new InvalidOperationException(VaultEntryValidationMessages.INVALID_VAULT_ENTRY_GROUP_ID);
        }

        var encryptedData = _encryptionService.EncryptWithUserKey(req.Data, userSecretKey);

        var candidate = new VaultEntries
        {
            Title = req.Title,
            EntryType = req.EntryType,
            EncryptedData = encryptedData,
            OwnerId = userId,
            GroupId = req.GroupId,
            IsFavourite = req.IsFavourite,
            CreatedBy = userId,
            CreatedDate = DateTime.UtcNow
        };


        var alreadyExists = await _vaultItemsRepository.DoesEntryExists(candidate, cancellationToken);
        if (alreadyExists)
            throw new InvalidOperationException(VaultEntryValidationMessages.ENTRY_ALREADY_EXISTS);

        // Try to reuse a deleted vault item id
        var reused = await _vaultItemsRepository.ReuseDeletedEntry(candidate, cancellationToken);
        if (reused != null)
            return new AddVaultItemResponse { VaultItemId = reused.VaultEntryId };

        var created = await _vaultItemsRepository.AddNewVaultItem(candidate, cancellationToken);
        return created == null
            ? throw new InvalidOperationException(VaultEntryValidationMessages.FAILED_ENTRY_CREATION)
            : new AddVaultItemResponse { VaultItemId = created.VaultEntryId };
    }
}
