using Devnet.Vault.Application.Utilities;
using Devnet.Vault.Domain.Constants.Messages;
using FluentValidation;

namespace Devnet.Vault.Application.Features.Auth.Commands;

public class RegisterOrLoginCommandValidator
    : AbstractValidator<RegisterOrLoginCommand>
{
    public RegisterOrLoginCommandValidator()
    {
        RuleFor(x => x.Request.Identifier)
            .NotEmpty()
            .WithMessage(ValidationMessages.AuthValidationMessages.IDENTIFIER_REQUIRED)
            .Must(IsValidEmailOrPhone)
            .WithMessage(ValidationMessages.AuthValidationMessages.IDENTIFIER_INVALID);

        RuleFor(x => x.Request.OtpCacheKey)
            .NotEmpty()
            .WithMessage(ValidationMessages.AuthValidationMessages.OTP_CACHE_KEY_REQUIRED);

        RuleFor(x => x.Request.Otp)
            .NotEmpty()
            .Matches(@"^\d{4,6}$")
            .WithMessage(ValidationMessages.OtpValidationMessages.OTP_LENGTH_INVALID);
    }

    private static bool IsValidEmailOrPhone(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            return false;

        identifier = identifier.Trim();

        return Validators.IsValidEmail(identifier) || Validators.IsValidPhone(identifier);
    }


}