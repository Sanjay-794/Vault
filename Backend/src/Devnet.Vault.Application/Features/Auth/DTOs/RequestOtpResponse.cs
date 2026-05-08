namespace Devnet.Vault.Application.Features.Auth.DTOs;

/// <summary>
/// DTO for OTP request response
/// </summary>
public record RequestOtpResponse
{
    public bool Success { get; init; }
    public string Message { get; init; } = null!;
    public string CacheKey { get; init; } = null!;
    public int ExpirySeconds { get; init; }
}
