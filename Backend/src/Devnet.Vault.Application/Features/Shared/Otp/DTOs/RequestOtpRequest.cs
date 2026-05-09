using Devnet.Vault.Domain.Enums;

namespace Devnet.Vault.Application.Features.Shared.Otp.DTOs;

/// <summary>
/// DTO for requesting OTP
/// </summary>
public record RequestOtpRequest
{
    public required string Identifier { get; init; }
    public required NotificationChannel ChannelType { get; init; } // "email" or "SMS"
    public required OtpPurpose Purpose { get; init; }
}
