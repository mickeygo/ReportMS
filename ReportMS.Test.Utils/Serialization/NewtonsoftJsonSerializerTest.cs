using System;
using System.Collections.Generic;
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

        [TestMethod]
        public void SerializeDictionary_Test()
        {
            var dics = new Dictionary<string, object> { { "Name", "gang.yang" }, { "Age", 28 }, { "Time", DateTime.Now} };
            var json = new NewtonsoftJsonSerializer();
            var result = json.Serialize(dics);

            // {"Name":"gang.yang","Age":28,"Time":"2016-03-09 14:11:13"}
            Assert.IsNull(result, result);
        }

        [TestMethod]
        public void DeserializeDictionary_Test()
        {
            var deserializeStr = "\"Name\":\"gang.yang\",\"Age\":28,\"Time\":\"2016-03-09 14:11:13\"";
            var json = new NewtonsoftJsonSerializer();
            var result = json.Deserialize<Dictionary<string, string>>(deserializeStr);

            // error
            Assert.IsNull(result);
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
