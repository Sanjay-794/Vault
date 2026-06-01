using FluentValidation;
using static Devnet.Vault.Application.Features.VaultItems.Commands.VaultItemsCommands;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.VaultItems.Commands;

public class AddNewVaultItemCommandValidator : AbstractValidator<AddNewVaultItemCommand>
{
    public AddNewVaultItemCommandValidator()
    {
        RuleFor(x => x.Request.Title)
            .NotEmpty()
            .WithMessage(VaultEntryValidationMessages.TITLE_REQUIRED);
        RuleFor(x => x.Request.Data)
            .NotEmpty()
            .WithMessage(VaultEntryValidationMessages.DATA_REQUIRED);
        RuleFor(x => x.Request.EntryType)
            .IsInEnum()
            .WithMessage(VaultEntryValidationMessages.INVALID_ENTRY_TYPE);
    }
}

public class UpdateVaultItemTitleCommandValidator : AbstractValidator<UpdateVaultItemTitleCommand>
{
    public UpdateVaultItemTitleCommandValidator()
    {
        RuleFor(x => x.Request.Title)
            .NotEmpty()
            .WithMessage(VaultEntryValidationMessages.TITLE_REQUIRED);

        RuleFor(x => x.Request.VaultEntryId)
            .GreaterThan(0)
            .WithMessage(VaultEntryValidationMessages.INVALID_VAULT_ENTRY_GROUP_ID);
    }
}

public class UpdateVaultItemDataCommandValidator : AbstractValidator<UpdateVaultItemDataCommand>
{
    public UpdateVaultItemDataCommandValidator()
    {
        RuleFor(x => x.Request.EncryptedData)
            .NotEmpty()
            .WithMessage(VaultEntryValidationMessages.DATA_REQUIRED);
        RuleFor(x => x.Request.VaultEntryId)
            .GreaterThan(0)
            .WithMessage(VaultEntryValidationMessages.INVALID_VAULT_ENTRY_GROUP_ID);
    }
}

public class DeleteVaultItemCommandValidator : AbstractValidator<DeleteVaultItemCommand>
{
    public DeleteVaultItemCommandValidator()
    {
        RuleFor(x => x.VaultEntryId)
            .GreaterThan(0)
            .WithMessage(VaultEntryValidationMessages.INVALID_VAULT_ENTRY_GROUP_ID);
    }
}