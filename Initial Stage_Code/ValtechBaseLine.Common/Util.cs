using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ValtechBaseLine.Common
{
    public static class Util
    {
        //Private key for encyrpting and decrypting the email, should not be changed.
        private const string Key = "ASD89hasdfO90jKS";


        /// <summary>
        /// Encrypt the token and return the encrypted value
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>Token containing encrypted value</returns>
        public static string Encrypt(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            try
            {
                var key = Encoding.UTF8.GetBytes(Key); //must be 16 chars
                var rijndael = new RijndaelManaged
                {
                    BlockSize = 128,
                    IV = key,
                    KeySize = 128,
                    Key = key
                };

                var transform = rijndael.CreateEncryptor();
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        var buffer = Encoding.UTF8.GetBytes(token);

                        cs.Write(buffer, 0, buffer.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    ms.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Decrypt the encrypted value in the token and return the decrypted value
        /// </summary>
        /// <param name="token">encrypted token</param>
        /// <returns></returns>
        public static string Decrypt(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            try
            {
                var key = Encoding.UTF8.GetBytes(Key); //must be 16 chars
                var rijndael = new RijndaelManaged
                {
                    BlockSize = 128,
                    IV = key,
                    KeySize = 128,
                    Key = key
                };

                var buffer = Convert.FromBase64String(token);
                var transform = rijndael.CreateDecryptor();
                string decrypted;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        cs.Write(buffer, 0, buffer.Length);
                        cs.FlushFinalBlock();
                        decrypted = Encoding.UTF8.GetString(ms.ToArray());
                        cs.Close();
                    }
                    ms.Close();
                }

                return decrypted;
            }
            catch
            {
                return null;
            }
        }

        public static string GeneratePassword()
        {
            var rnd = new Random();
            return rnd.Next(111111, 999999).ToString(CultureInfo.InvariantCulture);
        }
    }
}
