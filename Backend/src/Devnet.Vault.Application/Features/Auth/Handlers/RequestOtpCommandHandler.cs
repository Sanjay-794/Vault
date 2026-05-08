using Devnet.Vault.Application.Features.Auth.Commands;
using Devnet.Vault.Application.Features.Auth.DTOs;
using Devnet.Vault.Application.Features.Shared.Cache.Interfaces.Services;
using Devnet.Vault.Application.Features.Shared.Otp.Interfaces.Services;
using Devnet.Vault.Application.Notifications.Email.Interfaces;
using Devnet.Vault.Application.Notifications.Email.Models;
using Devnet.Vault.Application.Security.Encryption.Interfaces;
using Devnet.Vault.Domain.Enums;
using MediatR;

namespace Devnet.Vault.Application.Features.Auth.Handlers;

public class RequestOtpCommandHandler(IOtpGenerator _otpGenerator, ICacheService _cacheService, IEncryptionService _encryptionService,
    IEmailQueue _emailQueue) : IRequestHandler<RequestOtpCommand, RequestOtpResponse>
{
    private const int OTP_LENGTH = 6;
    private const int OTP_EXPIRY_MINUTES = 10;

    public async Task<RequestOtpResponse> Handle(RequestOtpCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;

        // Validate identifier type
        var isEmail = req.IdentifierType == AuthType.Email;
        var isPhone = req.IdentifierType == AuthType.Phone;

        if (!isEmail && !isPhone)
            throw new InvalidOperationException("IdentifierType must be either 'email' or 'phone'");

        // Generate OTP
        var otp = _otpGenerator.Generate(OTP_LENGTH);

        // Create cache key
        var cacheKey = $"o:{req.IdentifierType}:{_encryptionService.Encrypt(req.Identifier)}";

        // Store OTP in Redis cache with expiry
        await _cacheService.SetAsync(
            cacheKey,
            otp,
            TimeSpan.FromMinutes(OTP_EXPIRY_MINUTES));

        // Send OTP via email
        if (isEmail)
            SendOtpByEmailAsync(req.Identifier, otp);
        else if (isPhone)
            throw new NotImplementedException("SMS functionality is under development"); // TODO: Implement SMS functionality

        return new RequestOtpResponse
        {
            Success = true,
            Message = $"OTP has been sent to {req.Identifier}",
            CacheKey = cacheKey,
            ExpirySeconds = OTP_EXPIRY_MINUTES * 60
        };
    }

    private void SendOtpByEmailAsync(string email, string otp)
    {
        var subject = "Your Authentication OTP";
        var body = $@"
            <html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2>Authentication Code</h2>
                    <p>Your One-Time Password (OTP) is:</p>
                    <h1 style='color: #007bff; letter-spacing: 5px;'>{otp}</h1>
                    <p>This code will expire in {OTP_EXPIRY_MINUTES} minutes.</p>
                    <p>If you didn't request this code, please ignore this email.</p>
                    <hr>
                    <small style='color: #666;'>This is an automated message. Please do not reply.</small>
                </body>
            </html>";

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
