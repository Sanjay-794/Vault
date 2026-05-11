using Devnet.Vault.Domain.Constants.Messages;
using FluentValidation;

namespace Devnet.Vault.Application.Features.Account.Commands;

public class DeleteAccountCommandValidator
    : AbstractValidator<DeactivateAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(x => x.Request.OtpCacheKey)
            .NotEmpty()
            .WithMessage(ValidationMessages.AuthValidationMessages.OTP_CACHE_KEY_REQUIRED);

        RuleFor(x => x.Request.Otp)
            .NotEmpty()
            .Matches(@"^\d{4,6}$")
            .WithMessage(ValidationMessages.OtpValidationMessages.OTP_LENGTH_INVALID);
    }
}
