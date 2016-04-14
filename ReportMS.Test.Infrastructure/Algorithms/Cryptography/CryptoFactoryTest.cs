using Gear.Infrastructure.Algorithms.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Infrastructure.Algorithms.Cryptography
{
    [TestClass]
    public class CryptoFactoryTest
    {
        [TestMethod]
        public void AES_Test()
        {
            var str = "gang.yang";
            var ecryptStr = CryptoFactory.AES.Encrypt(str);
            var result = CryptoFactory.AES.Decrypt(ecryptStr);

            Assert.IsNull(result, result);
        }

        [TestMethod]
        public void AES_Decrypt_Test()
        {
            var encryptString = "7kKIQMaiMaZbjUZS+Ocmk+A7W2WUQ2HDsNxmdy+W+m1UnXwo3p/cYKO1rdpk91UB";
            var result = CryptoFactory.AES.Decrypt(encryptString);

            Assert.IsNull(result, result);
        }
    }
}
