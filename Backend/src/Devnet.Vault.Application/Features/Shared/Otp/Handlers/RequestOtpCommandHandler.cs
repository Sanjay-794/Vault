using Devnet.Vault.Application.Features.Shared.Cache.Interfaces.Services;
using Devnet.Vault.Application.Features.Shared.Otp.Commands;
using Devnet.Vault.Application.Features.Shared.Otp.DTOs;
using Devnet.Vault.Application.Features.Shared.Otp.Helpers;
using Devnet.Vault.Application.Features.Shared.Otp.Interfaces.Services;
using Devnet.Vault.Application.Notifications.Email.Interfaces;
using Devnet.Vault.Application.Notifications.Email.Models;
using Devnet.Vault.Application.Security.Encryption.Interfaces;
using Devnet.Vault.Domain.Enums;
using MediatR;
using static Devnet.Vault.Domain.Constants.Messages.ValidationMessages;

namespace Devnet.Vault.Application.Features.Shared.Otp.Handlers;

public class RequestOtpCommandHandler(IOtpGenerator _otpGenerator, ICacheService _cacheService, IEncryptionService _encryptionService,
    IEmailQueue _emailQueue) : IRequestHandler<RequestOtpCommand, RequestOtpResponse>
{
    private const int OTP_LENGTH = 6;
    private const int OTP_EXPIRY_MINUTES = 10;

    public async Task<RequestOtpResponse> Handle(RequestOtpCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;

        // Validate identifier type
        var isEmail = req.ChannelType == NotificationChannel.Email;
        var isSMS = req.ChannelType == NotificationChannel.SMS;

        if (!isEmail && !isSMS)
            throw new InvalidOperationException(OtpValidationMessages.CHANNEL_INVALID);

        // Generate OTP
        var otp = _otpGenerator.Generate(OTP_LENGTH);

        // Create cache key
        var cacheKey = $"o:{req.ChannelType}:{_encryptionService.Encrypt(req.Identifier)}";

        // Store OTP in Redis cache with expiry
        await _cacheService.SetAsync(
            cacheKey,
            otp,
            TimeSpan.FromMinutes(OTP_EXPIRY_MINUTES));

        // Send OTP via email
        if (isEmail)
            SendOtpByEmailAsync(req.Identifier, otp, req.Purpose);
        else if (isSMS)
            throw new NotImplementedException(OtpValidationMessages.SMS_UNDER_DEVELOPMENT); // TODO: Implement SMS functionality

        return new RequestOtpResponse
        {
            Success = true,
            Message = $"OTP has been sent to {req.Identifier}",
            CacheKey = cacheKey,
            ExpirySeconds = OTP_EXPIRY_MINUTES * 60
        };
    }

    private void SendOtpByEmailAsync(string email, string otp, OtpPurpose otpPurpose)
    {
        (string subject, string body) = otpPurpose switch
        {
            OtpPurpose.Authentication =>
                EmailDraft.GetAuthEmailBody(otp, OTP_EXPIRY_MINUTES),

            _ => throw new ArgumentOutOfRangeException(nameof(otpPurpose))
        };

        var emailMessage = new EmailMessage
        {
            To = [email],
            Subject = subject,
            Body = body,
            IsHtml = true
        };

        _emailQueue.Enqueue(emailMessage);
    }
}
