using Devnet.Vault.Application.Features.VaultItems.Interfaces;
using MediatR;
using static Devnet.Vault.Application.Features.VaultItems.Commands.VaultItemsCommands;

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
    public Task<bool> Handle(UpdateVaultItemGroupCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        return _vaultItemsRepository.MoveEntryToNewGroup(
            req.VaultEntryId,
            req.GroupId,
            req.GroupType,
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