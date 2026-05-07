using Devnet.Vault.Application.Configurations;
using Devnet.Vault.Application.Security.Encryption.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Devnet.Vault.Infrastructure.Security;

public class EncryptionService(IOptions<EncryptionSettings> options) : IEncryptionService
{
    private readonly EncryptionSettings _settings = options.Value;

    public string Encrypt(string plainText)
    {
        var encoded = CustomEncode(plainText);
        var encrypted = EncryptAes(encoded);

        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string cipherText)
    {
        var cipherBytes = Convert.FromBase64String(cipherText);
        var decrypted = DecryptAes(cipherBytes);

        return CustomDecode(decrypted);
    }

    private byte[] EncryptAes(string input)
    {
        using var aes = CreateAes();

        var encryptor = aes.CreateEncryptor();
        var bytes = Encoding.UTF8.GetBytes(input);

        return encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
    }

    private string DecryptAes(byte[] cipherBytes)
    {
        using var aes = CreateAes();

        var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

        return Encoding.UTF8.GetString(plainBytes);
    }

    private Aes CreateAes()
    {
        var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_settings.AesKey);
        aes.IV = Encoding.UTF8.GetBytes(_settings.AesIV);
        return aes;
    }

    private string CustomEncode(string input)
    {
        var reversed = Reverse(input);
        var base64 = ToBase64(reversed);

        var shifted = ShiftWithKey(base64, _settings.AppKey, true);

        return $"v1:{shifted}";
    }

    private string CustomDecode(string input)
    {
        if (!input.StartsWith("v1:"))
            throw new Exception("Invalid format");

        var shifted = input[3..];

        var base64 = ShiftWithKey(shifted, _settings.AppKey, false);
        var reversed = FromBase64(base64);

        return Reverse(reversed);
    }

    private static string Reverse(string input)
        => new([.. input.Reverse()]);

    private static string ToBase64(string input)
        => Convert.ToBase64String(Encoding.UTF8.GetBytes(input));

    private static string FromBase64(string input)
        => Encoding.UTF8.GetString(Convert.FromBase64String(input));

    private static string ShiftWithKey(string input, string key, bool encrypt)
    {
        var result = new char[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            var keyChar = key[i % key.Length];
            int shift = keyChar % 5 + 1;

            result[i] = encrypt
                ? (char)(input[i] + shift)
                : (char)(input[i] - shift);
        }

        return new string(result);
    }
}