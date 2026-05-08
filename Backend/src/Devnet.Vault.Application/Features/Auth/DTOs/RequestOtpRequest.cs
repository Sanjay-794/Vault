using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.Auth.DTOs;

/// <summary>
/// DTO for requesting OTP
/// </summary>
public record RequestOtpRequest
{
    public required string Identifier { get; init; }
    public required AuthType IdentifierType { get; init; }// "email" or "phone"
}
