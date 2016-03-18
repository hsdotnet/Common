using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Common.Encrypt
{
    public class MD5Encrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="encryptString">加密字符串</param>
        /// <returns></returns>
        public static string Encrypt(string encryptString)
        {
            if (encryptString == null) { throw new ArgumentNullException("encryptString is null"); }

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();

            byte[] byteArray = Encoding.UTF8.GetBytes(encryptString);

            byteArray = provider.ComputeHash(byteArray);

            StringBuilder builder = new StringBuilder();

            foreach (byte b in byteArray)
            {
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();
        }
    }
}