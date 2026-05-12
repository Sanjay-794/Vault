using Devnet.Vault.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Devnet.Vault.Application.Features.Account.DTOs;


public record UpdateProfileDetailsRequest
{
    public IFormFile? ProfilePicture { get; init; }
    public string? Name { get; init; }
}

public record RequestUpdateEmailOtpRequest
{
    public required string Email { get; init; }
}

public record RequestUpdatePhoneNumberOtpRequest
{
    public required string PhoneNumber { get; init; }
    public int CountryId { get; init; }
    public required NotificationChannel ChannelType { get; init; }
}

public record UpdatePhoneNumberRequest
{
    public required string PhoneNumber { get; init; }
    public int CountryId { get; init; }
    public required string Otp { get; init; }
    public required string OtpCacheKey { get; init; }
}

public record UpdateEmailAddressRequest
{
    public required string Email { get; init; }
    public required string Otp { get; init; }
    public required string OtpCacheKey { get; init; }
}

public record DeactivateAccountRequest
{
    public required string Otp { get; init; }
    public required string OtpCacheKey { get; init; }
}

public record DeleteAccountRequest
{
    public required string Otp { get; init; }
    public required string OtpCacheKey { get; init; }
}