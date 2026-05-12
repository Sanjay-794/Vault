using Devnet.Vault.Application.Features.Account.Commands;
using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Shared.Otp.Interfaces.Services;
using Devnet.Vault.Application.Utilities;
using Devnet.Vault.Domain.Constants.Messages;
using MediatR;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Account.Handlers;

public class UpdateUserPhoneNumberCommandHandler(IUserRepository _userRepository, IOtpValidationService _otpValidationService)
    : IRequestHandler<UpdateUserPhoneNumberCommand, bool>
{
    public async Task<bool> Handle(UpdateUserPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        if (request == null || request.Request == null)
            throw new InvalidOperationException(ExceptionMessages.GENERIC_NULL_ERROR);

        var req = request.Request;
        var isOtpValid = await _otpValidationService.ValidateAsync(req.OtpCacheKey, req.Otp);
        if (!isOtpValid)
            throw new InvalidOperationException(OtpValidationMessages.OTP_INVALID);

        if (!Validators.IsValidPhone(req.PhoneNumber))
            throw new InvalidOperationException(ProfileMessages.INVALID_PHONE_NUMBER);

        if (req.CountryId <= 0)
            throw new InvalidOperationException(ProfileMessages.INVALID_COUNTRY_CODE);

        var existingUser = await _userRepository.GetUserDetailsByPhoneNumberAsync(req.PhoneNumber, cancellationToken);
        if (existingUser != null && existingUser.UserId != request.UserId)
            throw new InvalidOperationException(ProfileMessages.PHONE_NUMBER_ALREADY_IN_USE);

        var updated = await _userRepository.UpdateUserContactDetailsAsync(request.UserId, request.UpdatedBy, null, req.PhoneNumber, req.CountryId, cancellationToken);
        return updated;
    }
}
