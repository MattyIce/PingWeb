using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingWeb.Core.Utilities
{
    public class Encryption
    {
        /// <summary>
        /// Calculates the MD5 hash value of the specified string argument.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5Hash(string str)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
