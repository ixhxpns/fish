using System.Security.Cryptography;
using System.Text;

public class AesEncryptionService
{
    private readonly byte[] key;
    private readonly byte[] iv;

    public AesEncryptionService(string encryptionKey)
    {
        // 使用 SHA256 從提供的密鑰生成一個 256 位的密鑰和 IV
        using var sha256 = SHA256.Create();
        var keyBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey));
        key = new byte[16];
        iv = new byte[16];
        Array.Copy(keyBytes, key, 16);
        Array.Copy(keyBytes, 16, iv, 0, 16);
    }

    public string Encrypt(string plainText)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream();
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        using (var streamWriter = new StreamWriter(cryptoStream))
        {
            streamWriter.Write(plainText);
        }

        var encrypted = memoryStream.ToArray();
        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string cipherText)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }
}