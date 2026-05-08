namespace Devnet.Vault.Application.Features.Auth.DTOs;

/// <summary>
/// DTO for authentication response after successful register/login
/// </summary>
public record AuthResponse
{
    public long UserId { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Name { get; init; } = null!;
    public string AccessToken { get; init; } = null!;
    public string RefreshToken { get; init; } = null!;
    public bool IsNewUser { get; init; }
    public int ExpiryMinutes { get; init; }
}
