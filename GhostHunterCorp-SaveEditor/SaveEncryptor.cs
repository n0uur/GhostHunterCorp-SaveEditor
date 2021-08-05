using System;
using System.Security.Cryptography;
using System.Text;

namespace GhostHunterCorp_SaveEditor
{
    public struct EncryptedText
    {
        public string Text;
        public string Iv;
    }

    public static class SaveEncryptor
    {
        public static EncryptedText Encrypted (string plainText, string password)
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateIV();
                aes.Key = ConvertToKeyBytes(aes, password);

                var textBytes = Encoding.UTF8.GetBytes(plainText);

                var aesEncryptor = aes.CreateEncryptor();
                var encryptedBytes = aesEncryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);

                return new EncryptedText
                {
                    Text = Convert.ToBase64String(encryptedBytes),
                    Iv = Convert.ToBase64String(aes.IV),
                };
            }
        }
        
        public static string Decrypted(EncryptedText encryptedText, string password)
        {
            return Decrypted(encryptedText.Text, encryptedText.Iv, password);
        }

        public static string Decrypted(string encryptedText, string iv, string password)
        {
            using (Aes aes = Aes.Create())
            {
                var ivBytes = Convert.FromBase64String(iv);
                var encryptedTextBytes = Convert.FromBase64String(encryptedText);

                var decryptor = aes.CreateDecryptor(ConvertToKeyBytes(aes, password), ivBytes);
                var decryptedBytes = decryptor.TransformFinalBlock(encryptedTextBytes, 0, encryptedTextBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
        
        private static byte[] ConvertToKeyBytes(SymmetricAlgorithm algorithm, string password)
        {
            algorithm.GenerateKey();

            var keyBytes = Encoding.UTF8.GetBytes(password);
            var validKeySize = algorithm.Key.Length;

            if (keyBytes.Length != validKeySize)
            {
                var newKeyBytes = new byte[validKeySize];
                Array.Copy(keyBytes, newKeyBytes, Math.Min(keyBytes.Length, validKeySize));
                keyBytes = newKeyBytes;
            }

            return keyBytes;
        }
    }
}