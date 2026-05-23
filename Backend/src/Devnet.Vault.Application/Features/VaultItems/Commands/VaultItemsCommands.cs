using Devnet.Vault.Application.Features.VaultItems.DTOs;
using MediatR;

namespace Devnet.Vault.Application.Features.VaultItems.Commands;

public class VaultItemsCommands
{
    public record AddNewVaultItemCommand(AddVaultItemRequest Request, long OwnerId) : IRequest<AddVaultItemResponse>;
}
