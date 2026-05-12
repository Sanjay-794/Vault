using Devnet.Vault.Application.Utilities;
using Devnet.Vault.Domain.Constants.Messages;
using FluentValidation;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Account.Commands;

public class UpdateUserPhoneNumberCommandValidator : AbstractValidator<UpdateUserPhoneNumberCommand>
{
    public UpdateUserPhoneNumberCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage(ExceptionMessages.GENERIC_NULL_ERROR);

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.PhoneNumber)
                .NotEmpty().WithMessage(ProfileMessages.INVALID_PHONE_NUMBER)
                .Must(Validators.IsValidPhone).WithMessage(ProfileMessages.INVALID_PHONE_NUMBER);

            RuleFor(x => x.Request.CountryId)
                .GreaterThan(0).WithMessage(ProfileMessages.INVALID_COUNTRY_CODE);

            RuleFor(x => x.Request.OtpCacheKey)
                .NotEmpty().WithMessage(AuthValidationMessages.OTP_CACHE_KEY_REQUIRED);

            RuleFor(x => x.Request.Otp)
                .NotEmpty().WithMessage(OtpValidationMessages.OTP_LENGTH_INVALID);
        });
    }
}
