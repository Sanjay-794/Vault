using Devnet.Vault.Application.Features.Account.Commands;
using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Shared.Otp.Interfaces.Services;
using MediatR;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Account.Handlers;

public class DeactivateAccountCommandHandler(IUserRepository _userRepository, IOtpValidationService _otpValidationService)
    : IRequestHandler<DeactivateAccountCommand, bool>
{
    public async Task<bool> Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
    {
        // Validate OTP
        var isOtpValid = await _otpValidationService.ValidateAsync(request.Request.OtpCacheKey, request.Request.Otp);

        if (!isOtpValid)
            throw new InvalidOperationException(OtpValidationMessages.OTP_INVALID);

        var success = await _userRepository.DeactivateUserAsync(request.UserId, request.DeactivatedBy, cancellationToken);

        return success;
    }
}