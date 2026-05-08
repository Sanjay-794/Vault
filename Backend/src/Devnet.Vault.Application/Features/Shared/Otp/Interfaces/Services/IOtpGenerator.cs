namespace Devnet.Vault.Application.Features.Shared.Otp.Interfaces.Services;

public interface IOtpGenerator
{
    /// <summary>
    /// Generate an OTP code.
    /// </summary>
    /// <param name="length">Number of digits to generate. Default is 6.</param>
    /// <returns>The generated OTP code.</returns>
    string Generate(int length = 6);
}
