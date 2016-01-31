using System;
using Gear.Utility.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils.Serialization
{
    [TestClass]
    public class NewtonsoftJsonSerializerTest
    {
        [TestMethod]
        public void Serialize_Test()
        {
            var model = new JsonTest()
            {
                Str = "gang.yang",
                Integer = 22,
                Number = 20.2020,
                Time = DateTime.Now
            };

            var json = new NewtonsoftJsonSerializer();
            var result = json.Serialize(model);

            Assert.IsNull(result, result);
        }

        [TestMethod]
        public void Deserialize_Test()
        {

        }
    }

    class JsonTest
    {
        public string Str { get; set; }

        public int Integer { get; set; }

        public double Number { get; set; }

        public DateTime Time { get; set; }
    }
}
