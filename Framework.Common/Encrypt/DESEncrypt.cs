using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Common.Encrypt
{
    public sealed class DESEncrypt
    {
        private static Encoding encoding = Encoding.UTF8;

        private static void CheckKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) { throw new ArgumentNullException("key"); }

            if (key.Length < 8) { throw new ArgumentException("key 长度至少8位"); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encode(string encryptString, string key)
        {
            CheckKey(key);

            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                provider.Key = encoding.GetBytes(key.Substring(0, 8));
                provider.IV = provider.Key;
                byte[] bytes = encoding.GetBytes(encryptString);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, provider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytes, 0, bytes.Length);
                        cs.FlushFinalBlock();
                        StringBuilder builder = new StringBuilder();
                        foreach (byte b in ms.ToArray())
                        {
                            builder.AppendFormat("{0:X2}", b);
                        }
                        return builder.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decode(string encryptString, string key)
        {
            CheckKey(key);

            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                provider.Key = encoding.GetBytes(key.Substring(0, 8));
                provider.IV = provider.Key;
                byte[] buffer = new byte[encryptString.Length / 2];
                for (int i = 0; i < (encryptString.Length / 2); i++)
                {
                    buffer[i] = (byte)Convert.ToInt32(encryptString.Substring(i * 2, 2), 0x10);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, provider.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(buffer, 0, buffer.Length);
                        cs.FlushFinalBlock();
                        return encoding.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}