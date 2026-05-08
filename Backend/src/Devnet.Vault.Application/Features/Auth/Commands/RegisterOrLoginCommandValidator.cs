using FluentValidation;

namespace Devnet.Vault.Application.Features.Auth.Commands;

public class RegisterOrLoginCommandValidator
: AbstractValidator<RegisterOrLoginCommand>
{
    public RegisterOrLoginCommandValidator()
    {
        RuleFor(x => x.Request.Identifier)
            .NotEmpty()
            .WithMessage("Identifier is required");

        RuleFor(x => x.Request.OtpCacheKey)
            .NotEmpty()
            .WithMessage("OtpCacheKey is required");

        RuleFor(x => x.Request.Otp)
            .NotEmpty()
            .Length(4, 6)
            .WithMessage("OTP must be 4–6 digits");
    }
}
