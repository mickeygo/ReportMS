using Gear.Infrastructure.Algorithms.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Infrastructure.Algorithms.Cryptography
{
    [TestClass]
    public class AESTest
    {
        private const string scrambledKey = "rms";

        [TestMethod]
        public void Encrypt_Test()
        {
            var encryptedStr = AESCrypto.Encrypt("gang.yang", scrambledKey);
            Assert.IsTrue(encryptedStr == "WDDJcMk/+qc0CJ2QYjqXlg==", encryptedStr);
        }

        [TestMethod]
        public void Decrypt_Test()
        {
            var decryptedStr = AESCrypto.Decrypt("WDDJcMk/+qc0CJ2QYjqXlg==", scrambledKey);
            Assert.IsTrue(decryptedStr == "gang.yang", decryptedStr);
        }
    }
}
