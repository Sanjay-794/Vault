using Devnet.Vault.Application.Utilities;
using Devnet.Vault.Domain.Constants.Messages;
using FluentValidation;

namespace Devnet.Vault.Application.Features.Groups.Commands;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.GroupValidationMessages.GROUP_NAME_REQUIRED);
    }
}

public class UpdateGroupNameCommandValidator : AbstractValidator<UpdateGroupNameCommand>
{
    public UpdateGroupNameCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.GroupValidationMessages.GROUP_NAME_REQUIRED);

        RuleFor(x => x.Request.GroupId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.GroupValidationMessages.INVALID_GROUP_ID);
    }
}

public class UpdateGroupFavouriteCommandValidator : AbstractValidator<UpdateGroupFavouriteCommand>
{
    public UpdateGroupFavouriteCommandValidator()
    {
        RuleFor(x => x.Request.GroupId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.GroupValidationMessages.INVALID_GROUP_ID);
    }
}

public class UpdateGroupParentCommandValidator : AbstractValidator<UpdateGroupParentCommand>
{
    public UpdateGroupParentCommandValidator()
    {
        RuleFor(x => x.Request.GroupId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.GroupValidationMessages.INVALID_GROUP_ID);

        RuleFor(x => x.Request.ParentGroupId)
            .NotEqual(0)
            .WithMessage(ValidationMessages.GroupValidationMessages.INVALID_GROUP_PARENT_ID);
    }
}

public class UpdateGroupMetaDataJsonCommandValidator : AbstractValidator<UpdateGroupMetaDataJsonCommand>
{
    public UpdateGroupMetaDataJsonCommandValidator()
    {
        RuleFor(x => x.Request.GroupId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.GroupValidationMessages.INVALID_GROUP_ID);

        RuleFor(x => x.Request.MetadataJson)
            .NotEmpty()
            .WithMessage(ValidationMessages.GroupValidationMessages.METADATA_REQUIRED)
            .Must(Validators.IsValidJson)
            .WithMessage(ValidationMessages.GroupValidationMessages.INVALID_METDATA_JSON); ;
    }
}
public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.Request.GroupId)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.GroupValidationMessages.INVALID_GROUP_ID);
    }
}