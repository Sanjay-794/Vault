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

    public static (string Subject, string Body) GetAccountDeactivationEmailBody(string otp, int otpExpiryMinutes)
    {
        var subject = "Account Deactivation Verification";
        var body = $@"
            <html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2>Account Deactivation Code</h2>
                    <p>You have requested to deactivate your account. Your verification code is:</p>
                    <h1 style='color: #dc3545; letter-spacing: 5px;'>{otp}</h1>
                    <p>This code will expire in {otpExpiryMinutes} minutes.</p>
                    <p>If you didn't request this action, please contact support immediately.</p>
                    <hr>
                    <small style='color: #666;'>This is an automated message. Please do not reply.</small>
                </body>
            </html>";

        return (subject, body);
    }

    public static (string Subject, string Body) GetAccountDeletionEmailBody(string otp, int otpExpiryMinutes)
    {
        var subject = "Account Deletion Verification";
        var body = $@"
            <html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2>Account Deletion Code</h2>
                    <p>You have requested to permanently delete your account. Your verification code is:</p>
                    <h1 style='color: #dc3545; letter-spacing: 5px;'>{otp}</h1>
                    <p>This code will expire in {otpExpiryMinutes} minutes.</p>
                    <p><strong>Warning:</strong> This action cannot be undone. All your data will be permanently deleted.</p>
                    <p>If you didn't request this action, please contact support immediately.</p>
                    <hr>
                    <small style='color: #666;'>This is an automated message. Please do not reply.</small>
                </body>
            </html>";

        return (subject, body);
    }
}
