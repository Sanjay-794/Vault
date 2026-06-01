using Devnet.Vault.Application.Features.VaultItems.Interfaces;
using MediatR;
using static Devnet.Vault.Application.Features.VaultItems.Commands.VaultItemsCommands;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.VaultItems.Handlers;

public class UpdateVaultItemTitleCommandHandler(IVaultItemsRepository _vaultItemsRepository)
    : IRequestHandler<UpdateVaultItemTitleCommand, bool>
{
    public Task<bool> Handle(UpdateVaultItemTitleCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        return _vaultItemsRepository.UpdateEntryTitle(
            req.Title,
            req.VaultEntryId,
            request.OwnerId,
            request.OwnerId,
            cancellationToken);
    }
}

public class UpdateVaultItemDataCommandHandler(IVaultItemsRepository _vaultItemsRepository)
    : IRequestHandler<UpdateVaultItemDataCommand, bool>
{
    public Task<bool> Handle(UpdateVaultItemDataCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        return _vaultItemsRepository.UpdateEncryptedData(
            req.EncryptedData,
            req.VaultEntryId,
            request.OwnerId,
            request.OwnerId,
            cancellationToken);
    }
}

public class UpdateVaultItemGroupCommandHandler(IVaultItemsRepository _vaultItemsRepository)
    : IRequestHandler<UpdateVaultItemGroupCommand, bool>
{
    public async Task<bool> Handle(UpdateVaultItemGroupCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;

        if (req.GroupId != null)
        {
            var isGroupValid = await _vaultItemsRepository.IsGroupValidForItems(req.GroupId ?? 0, request.OwnerId, cancellationToken);
            if (!isGroupValid)
                throw new InvalidOperationException(VaultEntryValidationMessages.INVALID_VAULT_ENTRY_GROUP_ID);
        }

        return await _vaultItemsRepository.MoveEntryToNewGroup(
            req.VaultEntryId,
            req.GroupId,
            request.OwnerId,
            request.OwnerId,
            cancellationToken);
    }
}

public class DeleteVaultItemCommandHandler(IVaultItemsRepository _vaultItemsRepository)
    : IRequestHandler<DeleteVaultItemCommand, bool>
{
    public Task<bool> Handle(DeleteVaultItemCommand request, CancellationToken cancellationToken)
    {
        return _vaultItemsRepository.DeleteEntry(
            request.VaultEntryId,
            request.OwnerId,
            request.OwnerId,
            cancellationToken);
    }
}