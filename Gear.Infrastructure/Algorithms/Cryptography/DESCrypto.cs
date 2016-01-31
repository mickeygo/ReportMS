using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Gear.Infrastructure.Algorithms.Cryptography
{
    /// <summary>
    /// DES (Data Encryption Standard) 对称加密
    /// </summary>
    public static class DESCrypto
    {
        // 密钥向量
        private static readonly byte[] keysIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <param name="scrambledKey">对称秘钥 (多于 8 位截取至 8 位; 少于 8 位用 0 右补齐到 8 位)</param>
        /// <returns>加密后的字符串, 以 Base64 w位输出</returns>
        public static string Encrypt(string encryptString, string scrambledKey)
        {
            var desKey = GenerateSecretkey(scrambledKey);
            var data = Encoding.UTF8.GetBytes(encryptString);

            using (var des = DES.Create())
            using (var mstream = new MemoryStream())
            using (var cstream = new CryptoStream(mstream, des.CreateEncryptor(desKey, keysIV), CryptoStreamMode.Write))
            {
                cstream.Write(data, 0, data.Length);
                cstream.FlushFinalBlock();

                return Convert.ToBase64String(mstream.ToArray());
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptString">要解密的字符串</param>
        /// <param name="scrambledKey">对称秘钥 (多于 8 位截取至 8 位; 少于 8 位用 0 右补齐到 8 位)</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string decryptString, string scrambledKey)
        {
            var desKey = GenerateSecretkey(scrambledKey);

            // 转码方式与加密时相同
            var data = Convert.FromBase64String(decryptString);

            using (var des = DES.Create())
            using (var mstream = new MemoryStream())
            using (var cstream = new CryptoStream(mstream, des.CreateDecryptor(desKey, keysIV), CryptoStreamMode.Write))
            {
                cstream.Write(data, 0, data.Length);
                cstream.FlushFinalBlock();

                // 编码方式与加密时相同
                return Encoding.UTF8.GetString(mstream.ToArray());
            }
        }

        // 将秘钥长度设置为 8 位长度(多于 8 位截取至 8 位; 少于 8 位用 0 右补齐到 8 位)，然后转为 byte 数组
        private static byte[] GenerateSecretkey(string scrambledKey)
        {
            string desKey;

            if (scrambledKey.Length == 8)
                desKey = scrambledKey;
            else if (scrambledKey.Length > 8)
                desKey = scrambledKey.Substring(0, 8);
            else
                desKey = scrambledKey.PadRight(8, '0');

            return Encoding.ASCII.GetBytes(desKey);
        }
    }
}
