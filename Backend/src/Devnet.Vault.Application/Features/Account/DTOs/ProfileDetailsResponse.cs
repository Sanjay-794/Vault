namespace Devnet.Vault.Application.Features.Account.DTOs;

public record ProfileDetailsResponse
{
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? CountryName { get; init; }
    public string? CountryCallingCode { get; init; }
    public string? RoleName { get; init; }
    public DateTime? LastLoginDate { get; init; }
}