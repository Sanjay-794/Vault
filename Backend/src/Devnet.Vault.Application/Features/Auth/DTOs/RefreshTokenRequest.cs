namespace Devnet.Vault.Application.Features.Auth.DTOs;

/// <summary>
/// DTO for refresh token request
/// </summary>
public record RefreshTokenRequest
{
    public required string RefreshToken { get; init; }
}
