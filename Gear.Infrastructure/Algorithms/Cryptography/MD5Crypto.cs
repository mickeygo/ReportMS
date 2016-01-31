using System.Security.Cryptography;
using System.Text;

namespace Gear.Infrastructure.Algorithms.Cryptography
{
    /// <summary>
    /// MD5 (Message Digest Algorithm 5) 非对称加密
    /// </summary>
    public static class MD5Crypto
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <returns>加密后的字符串，以十六进制呈现</returns>
        public static string Encrypt(string encryptString)
        {
            using (var md5 = MD5.Create())
            {
                var datas = md5.ComputeHash(Encoding.Unicode.GetBytes(encryptString));

                var builder = new StringBuilder();
                foreach (var data in datas)
                {
                    builder.Append(data.ToString("x2")); // 每个字符以两位的形式的十六进制输出
                }

                return builder.ToString();
            }
        }
    }
}
