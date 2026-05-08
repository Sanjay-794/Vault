using Devnet.Vault.Application.Configurations;
using Devnet.Vault.Application.Features.Account.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Auth.Commands;
using Devnet.Vault.Application.Features.Auth.DTOs;
using Devnet.Vault.Application.Features.Auth.Interfaces.Repositories;
using Devnet.Vault.Application.Features.Auth.Interfaces.Services;
using Devnet.Vault.Application.Security.Encryption.Interfaces;
using Devnet.Vault.Domain.Constants.AppSettings;
using Devnet.Vault.Domain.Entities.Identity;
using MediatR;
using Microsoft.Extensions.Options;

namespace Devnet.Vault.Application.Features.Auth.Handlers;

public class RefreshTokenCommandHandler(IAuthRepository _authRepository, IUserRepository _userRepository,
    IJwtService _jwtService, IEncryptionService _encryptionService,
    IOptions<JwtSettings> _jwtSettings) : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly JwtSettings _settings = _jwtSettings.Value;

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var req = request.Request;

        if (string.IsNullOrWhiteSpace(req.RefreshToken))
            throw new InvalidOperationException("Refresh token is required");

        // Hash the provided refresh token to match with database
        var refreshTokenHash = _encryptionService.Hash(req.RefreshToken);

        // Get user login by refresh token hash
        var userLogin = await _authRepository.GetUserLoginByRefreshTokenHashAsync(refreshTokenHash, cancellationToken)
            ?? throw new InvalidOperationException("Invalid or expired refresh token");

        if (userLogin.UserId != request.UserIdClaim)
            throw new InvalidOperationException("Invalid credentials");

        // Validate token expiry
        if (userLogin.ExpiryDate.HasValue && userLogin.ExpiryDate < DateTime.UtcNow)
            throw new InvalidOperationException("Refresh token has expired");

        // Get user details
        var user = await _userRepository.GetUserDetailsByUserIdAsync(userLogin.UserId, cancellationToken)
            ?? throw new InvalidOperationException("User not found");

        // Generate new tokens
        var newAccessToken = _jwtService.GenerateAccessToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();
        var newRefreshTokenHash = _encryptionService.Hash(newRefreshToken);

        // Revoke old refresh token
        var revoked = await _authRepository.RevokeUserLoginAsync(userLogin.UserLoginId, cancellationToken);
        if (!revoked)
            throw new InvalidOperationException("Failed to revoke old token");

        // Save new login details
        var newUserLogin = new UserLogins
        {
            UserId = user.UserId,
            RefreshTokenHash = newRefreshTokenHash,
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(AppTimes.REFRESH_TOKEN_EXPIRY_TIME_IN_DAYS),
            IsRevoked = false,
            IpAddress = userLogin.IpAddress
        };

        var loginSaved = await _authRepository.SaveUserLoginDetailsAsync(newUserLogin, cancellationToken);
        if (!loginSaved)
            throw new InvalidOperationException("Failed to save new login details");

        // Return authentication response with new tokens
        return new AuthResponse
        {
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Name = user.Name,
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            IsNewUser = false,
            ExpiryMinutes = _settings.ExpiryMinutes
        };
    }
}
