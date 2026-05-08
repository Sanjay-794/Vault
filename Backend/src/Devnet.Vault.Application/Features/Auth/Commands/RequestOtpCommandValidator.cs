using Devnet.Vault.Application.Utilities;
using FluentValidation;

namespace Devnet.Vault.Application.Features.Auth.Commands;

public class RequestOtpCommandValidator : AbstractValidator<RequestOtpCommand>
{
    public RequestOtpCommandValidator()
    {
        RuleFor(x => x.Request.Identifier)
            .NotEmpty()
            .WithMessage("Identifier is required")
            .Must(IsValidEmailOrPhone)
            .WithMessage("Identifier must be a valid email or phone number");
    }

    private static bool IsValidEmailOrPhone(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            return false;

        identifier = identifier.Trim();

        return Validators.IsValidEmail(identifier) || Validators.IsValidPhone(identifier);
    }
}