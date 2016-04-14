namespace Gear.Infrastructure.Algorithms.Cryptography
{
    /// <summary>
    /// 密码工厂，用于加密和解密
    /// </summary>
    public static class CryptoFactory
    {
        /// <summary>
        /// DES 加密和解密
        /// </summary>
        public static Crypto DES
        {
            get { return new DESCrypto(); }
        }

        /// <summary>
        /// AES 加密和解密
        /// </summary>
        public static Crypto AES
        {
            get { return new AESCrypto(); }
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        public static Crypto MD5
        {
            get { return new MD5Crypto(); }
        }
    }
}
