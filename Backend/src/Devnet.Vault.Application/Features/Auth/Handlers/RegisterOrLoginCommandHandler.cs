using Devnet.Vault.Application.Configurations;
using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Auth.Commands;
using Devnet.Vault.Application.Features.Auth.DTOs;
using Devnet.Vault.Application.Features.Auth.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Auth.Interfaces.Services;
using Devnet.Vault.Application.Features.Shared.Otp.Interfaces.Services;
using Devnet.Vault.Application.Security.Encryption.Interfaces;
using Devnet.Vault.Domain.Entities.Identity;
using Devnet.Vault.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace Devnet.Vault.Application.Features.Auth.Handlers;

public class RegisterOrLoginCommandHandler(IOtpValidationService _otpValidationService, IUserRepository _userRepository, IAuthRepository _authRepository,
    IJwtService _jwtService, IEncryptionService _encryptionService,
    IOptions<JwtSettings> _jwtSettings) : IRequestHandler<RegisterOrLoginCommand, AuthResponse>
{
    private readonly JwtSettings _settings = _jwtSettings.Value;

    public async Task<AuthResponse> Handle(RegisterOrLoginCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;

        // Validate identifier type
        var isEmail = req.IdentifierType == AuthType.Email;
        var isPhone = req.IdentifierType == AuthType.Phone;

        if (!isEmail && !isPhone)
            throw new InvalidOperationException("IdentifierType must be either 'email' or 'phone'");

        // Validate OTP from Redis cache
        var isValidOtp = await _otpValidationService.ValidateAsync(req.OtpCacheKey, req.Otp);
        if (!isValidOtp)
            throw new InvalidOperationException("Invalid or expired OTP");

        // Check if user exists
        UserDetails? user = null;
        if (isEmail)
            user = await _userRepository.GetUserDetailsByEmailAsync(req.Identifier, cancellationToken);
        else if (isPhone)
            user = await _userRepository.GetUserDetailsByPhoneNumberAsync(req.Identifier, cancellationToken);

        var isNewUser = user == null;

        // Register new user if doesn't exist
        if (isNewUser)
        {
            user = new UserDetails
            {
                Email = isEmail ? req.Identifier : null,
                PhoneNumber = isPhone ? req.Identifier : null,
                CountryId = req.CountryId.HasValue ? req.CountryId.Value : null,
                IsDeactivated = false,
                CreatedBy = 0, // System user
                CreatedDate = DateTime.UtcNow
            };

            user = await _authRepository.RegisterNewUserAsync(user, cancellationToken)
                ?? throw new InvalidOperationException("Failed to register user");
        }
        else
        {
            // Update last login date for existing user
            user?.LastLoginDate = DateTime.UtcNow;
        }
        if (user == null)
            throw new InvalidOperationException("User not found");

        // Generate JWT tokens
        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Hash refresh token
        var refreshTokenHash = _encryptionService.Hash(refreshToken);

        // Save login details
        var userLogin = new UserLogins
        {
            UserId = user.UserId,
            RefreshTokenHash = refreshTokenHash,
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            IpAddress = req.IpAddress
        };

        var loginSaved = await _authRepository.SaveUserLoginDetailsAsync(userLogin, cancellationToken);
        if (!loginSaved)
            throw new InvalidOperationException("Failed to save login details");

        // Return authentication response
        return new AuthResponse
        {
            UserId = user.UserId,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Name = user.Name,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            IsNewUser = isNewUser,
            ExpiryMinutes = _settings.ExpiryMinutes
        };
    }


}
