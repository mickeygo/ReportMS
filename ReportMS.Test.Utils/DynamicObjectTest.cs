using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils
{
    [TestClass]
    public class DynamicObjectTest
    {
        [TestMethod]
        public void Dynamic_Test()
        {
            dynamic d = new {};
            d.AA = "AA";

            Assert.IsTrue(d.AA == "AA");
        }
    }
}
