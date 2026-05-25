namespace Devnet.Vault.Application.Security.Encryption.Interfaces;

public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
    string Hash(string input);
    string EncryptWithUserKey(string plainText, string userKey);
    string DecryptWithUserKey(string encryptedText, string userKey);
}
