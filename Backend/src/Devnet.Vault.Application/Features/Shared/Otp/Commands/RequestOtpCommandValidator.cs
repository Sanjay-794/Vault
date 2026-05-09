using Devnet.Vault.Application.Utilities;
using Devnet.Vault.Domain.Constants.Messages;
using FluentValidation;

namespace Devnet.Vault.Application.Features.Shared.Otp.Commands;

public class RequestOtpCommandValidator : AbstractValidator<RequestOtpCommand>
{
    public RequestOtpCommandValidator()
    {
        RuleFor(x => x.Request.Identifier)
            .NotEmpty()
            .WithMessage(ValidationMessages.AuthValidationMessages.IDENTIFIER_REQUIRED)
            .Must(IsValidEmailOrPhone)
            .WithMessage(ValidationMessages.AuthValidationMessages.IDENTIFIER_INVALID);
    }

    private static bool IsValidEmailOrPhone(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            return false;

        identifier = identifier.Trim();

        return Validators.IsValidEmail(identifier) || Validators.IsValidPhone(identifier);
    }
}