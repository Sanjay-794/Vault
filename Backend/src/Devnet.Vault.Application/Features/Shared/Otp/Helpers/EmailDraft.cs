namespace Devnet.Vault.Application.Features.Shared.Otp.Helpers;

public static class EmailDraft
{
    public static (string Subject, string Body) GetAuthEmailBody(string otp, int otpExpiryMinutes)
    {
        var subject = "Your Authentication OTP";
        var body = $@"
            <html>`
                <body style='font-family: Arial, sans-serif;'>
                    <h2>Authentication Code</h2>
                    <p>Your One-Time Password (OTP) is:</p>
                    <h1 style='color: #007bff; letter-spacing: 5px;'>{otp}</h1>
                    <p>This code will expire in {otpExpiryMinutes} minutes.</p>
                    <p>If you didn't request this code, please ignore this email.</p>
                    <hr>
                    <small style='color: #666;'>This is an automated message. Please do not reply.</small>
                </body>
            </html>";

        return (subject, body);
    }
}
