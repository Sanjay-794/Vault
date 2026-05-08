using PhoneNumbers;
using System.Net.Mail;

namespace Devnet.Vault.Application.Utilities;

public static class Validators
{
    public static bool IsValidEmail(string email)
    {
        try
        {
            var mail = new MailAddress(email);

            return mail.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidPhone(string phone)
    {
        try
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();

            var parsed = phoneUtil.Parse(phone, null);

            return phoneUtil.IsValidNumber(parsed);
        }
        catch
        {
            return false;
        }
    }
}
