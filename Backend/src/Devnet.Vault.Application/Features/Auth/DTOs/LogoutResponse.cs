namespace Devnet.Vault.Application.Features.Auth.DTOs;

/// <summary>
/// DTO for logout response
/// </summary>
public record LogoutResponse
{
    public bool Success { get; init; }
    public string Message { get; init; } = "Logged out successfully";
}
