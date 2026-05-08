namespace Devnet.Vault.Application.Features.Shared.Otp.Interfaces.Services;

public interface IOtpValidationService
{
    /// <summary>
    /// Validate the provided OTP against the stored value.
    /// </summary>
    /// <param name="key">The OTP storage key, typically derived from user or transaction context.</param>
    /// <param name="otp">The OTP code supplied by the caller.</param>
    /// <returns>True when the OTP is valid; otherwise false.</returns>
    Task<bool> ValidateAsync(string key, string otp);
}
