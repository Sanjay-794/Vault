using Devnet.Vault.Application.Features.VaultItems.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.VaultItems.Commands;

public class VaultItemsCommands
{
    public record AddNewVaultItemCommand(AddVaultItemRequest Request, long OwnerId) : IRequest<AddVaultItemResponse>;
    public record UpdateVaultItemTitleCommand(UpdateVaultItemTitleDto Request, long OwnerId) : IRequest<bool>;
    public record UpdateVaultItemDataCommand(UpdateVaultItemDataDto Request, long OwnerId) : IRequest<bool>;
    public record UpdateVaultItemGroupCommand(UpdateVaultItemGroupDto Request, long OwnerId) : IRequest<bool>;
    public record DeleteVaultItemCommand(long VaultEntryId, long OwnerId) : IRequest<bool>;
}
