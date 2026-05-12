using Devnet.Vault.Application.Features.Account.Commands;
using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Shared.Otp.Interfaces.Services;
using Devnet.Vault.Application.Utilities;
using Devnet.Vault.Domain.Constants.Messages;
using MediatR;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Account.Handlers;

public class UpdateUserEmailAddressCommandHandler(IUserRepository _userRepository, IOtpValidationService _otpValidationService)
    : IRequestHandler<UpdateUserEmailAddressCommand, bool>
{
    public async Task<bool> Handle(UpdateUserEmailAddressCommand request, CancellationToken cancellationToken)
    {
        if (request == null || request.Request == null)
            throw new InvalidOperationException(ExceptionMessages.GENERIC_NULL_ERROR);

        var req = request.Request;
        var isOtpValid = await _otpValidationService.ValidateAsync(req.OtpCacheKey, req.Otp);
        if (!isOtpValid)
            throw new InvalidOperationException(OtpValidationMessages.OTP_INVALID);

        if (!Validators.IsValidEmail(req.Email))
            throw new InvalidOperationException(ProfileMessages.INVALID_EMAIL_ADDRESS);

        var existingUser = await _userRepository.GetUserDetailsByEmailAsync(req.Email, cancellationToken);
        if (existingUser != null && existingUser.UserId != request.UserId)
            throw new InvalidOperationException(ProfileMessages.EMAIL_ALREADY_IN_USE);

        var updated = await _userRepository.UpdateUserContactDetailsAsync(request.UserId, request.UpdatedBy, req.Email, null, null, cancellationToken);
        return updated;
    }
}
