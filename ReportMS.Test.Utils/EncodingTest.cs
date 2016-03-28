using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils
{
    [TestClass]
    public class EncodingTest
    {
        [TestMethod]
        public void DefaultEncoding_Test()
        {
            var str = "abcde某某";
            var bytes = Encoding.Default.GetBytes(str);

            var count = bytes.Length;
            Assert.IsTrue(count == (5+ 2 * 2), count.ToString());
        }

        [TestMethod]
        public void UnicodeEncoding_Test()
        {
            var str = "abcde某某";
            var bytes = Encoding.Unicode.GetBytes(str);

            var count = bytes.Length;
            Assert.IsTrue(count == (5 + 2) * 2, count.ToString());
        }

        [TestMethod]
        public void UTF32Encoding_Test()
        {
            var str = "abcde某某";  
            var bytes = Encoding.UTF32.GetBytes(str);

            var count = bytes.Length;
            Assert.IsTrue(count == (5 + 2) * 4, count.ToString());
        }

        [TestMethod]
        public void UTF8Encoding_Test()
        {
            // abcde ==> 5
            // 某某 ==> 2 * 3
            var str = "abcde某某";
            var bytes = Encoding.UTF8.GetBytes(str);

            var count = bytes.Length;
            Assert.IsTrue(count == 11, count.ToString());
        }
    }
}
