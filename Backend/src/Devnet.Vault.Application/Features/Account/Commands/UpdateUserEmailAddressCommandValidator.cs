using Devnet.Vault.Application.Utilities;
using Devnet.Vault.Domain.Constants.Messages;
using FluentValidation;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Account.Commands;

public class UpdateUserEmailAddressCommandValidator : AbstractValidator<UpdateUserEmailAddressCommand>
{
    public UpdateUserEmailAddressCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage(ExceptionMessages.GENERIC_NULL_ERROR);

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage(ProfileMessages.INVALID_EMAIL_ADDRESS)
                .Must(Validators.IsValidEmail).WithMessage(ProfileMessages.INVALID_EMAIL_ADDRESS);

            RuleFor(x => x.Request.OtpCacheKey)
                .NotEmpty().WithMessage(AuthValidationMessages.OTP_CACHE_KEY_REQUIRED);

            RuleFor(x => x.Request.Otp)
                .NotEmpty().WithMessage(OtpValidationMessages.OTP_LENGTH_INVALID);
        });
    }
}
