using Gear.Infrastructure.Algorithms.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Infrastructure.Algorithms.Cryptography
{
    [TestClass]
    public class MD5Test
    {
        [TestMethod]
        public void Encrypt_Test()
        {
            var result = MD5Crypto.Encrypt("gang.yang@advantech.com.cn1234");

            Assert.IsTrue(result == "a2fbcb5af71a2187ae86c702c7f04af1", result);
        }
    }
}
