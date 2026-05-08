using Devnet.Vault.Application.Features.Auth.Commands;
using Devnet.Vault.Application.Features.Auth.DTOs;
using Devnet.Vault.Application.Features.Auth.Interfaces.Repositories;
using Devnet.Vault.Application.Security.Encryption.Interfaces;
using MediatR;

namespace Devnet.Vault.Application.Features.Auth.Handlers;

public class LogoutCommandHandler(IAuthRepository _authRepository, IEncryptionService _encryptionService)
    : IRequestHandler<LogoutCommand, LogoutResponse>
{
    public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var refreshToken = request.RefreshToken;

        if (userId <= 0)
            throw new InvalidOperationException("Invalid user ID");

        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new InvalidOperationException("Refresh token is required");

        // Hash the provided refresh token
        var refreshTokenHash = _encryptionService.Hash(refreshToken);

        // Get user login by refresh token hash
        var userLogin = await _authRepository.GetUserLoginByRefreshTokenHashAsync(refreshTokenHash, cancellationToken) ?? throw new InvalidOperationException("Invalid refresh token");

        // Verify the refresh token belongs to the user
        if (userLogin.UserId != userId)
            throw new InvalidOperationException("Refresh token does not belong to this user");

        // Revoke the user login session
        var revoked = await _authRepository.RevokeUserLoginAsync(userLogin.UserLoginId, cancellationToken);

        if (!revoked)
            throw new InvalidOperationException("Failed to logout");

        return new LogoutResponse
        {
            Success = true,
            Message = "Logged out successfully"
        };
    }
}