using System.Security.Cryptography;
using System.Text;

namespace CoffeeHouse.Core.Extensions
{
    public static class HashExtensions
    {
        static string Key { get; } = "0!@34567*9Admin@Mintec.C0m";

        public static string ComputeStringToSha256Hash(string plainText)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }

        public static string Encrypt(this string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                using (var aesCryptoServiceProvider = Aes.Create())
                {
                    aesCryptoServiceProvider.Key = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Key));
                    aesCryptoServiceProvider.Mode = CipherMode.ECB;
                    aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;

                    using (var transform = aesCryptoServiceProvider.CreateEncryptor())
                    {
                        byte[] textBytes = Encoding.UTF8.GetBytes(input);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public static string Decrypt(this string cipher)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                using (var aesCryptoServiceProvider = Aes.Create())
                {
                    aesCryptoServiceProvider.Key = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Key));
                    aesCryptoServiceProvider.Mode = CipherMode.ECB;
                    aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;

                    using (var transform = aesCryptoServiceProvider.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }
}
