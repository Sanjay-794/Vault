namespace Devnet.Vault.Application.Security.Encryption.Interfaces;

public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}
