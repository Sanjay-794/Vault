using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.Auth.DTOs;

/// <summary>
/// DTO for register or login request with OTP validation
/// </summary>
public record RegisterOrLoginRequest
{
    public required string Identifier { get; init; }
    public required AuthType IdentifierType { get; init; } // "email" or "phone"
    public required string Otp { get; init; }
    public required string OtpCacheKey { get; init; }
    public int? CountryId { get; init; }
    public string? IpAddress { get; init; }
}
