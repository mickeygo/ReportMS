using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Web.UnitTests
{
    [TestClass]
    public class HttpRequestTest
    {
        private Uri _uri;

        [TestInitialize]
        public void Initialize()
        {
            this._uri = new Uri("http://localhost:9528/acl/home/");
        }

        [TestMethod]
        public void GetTennant_Test()
        {
            var request = WebRequest.Create(this._uri);
            var response = (HttpWebResponse)request.GetResponse();

            var responseStream = response.GetResponseStream();

            var stream = new StreamReader(responseStream);
            var result = stream.ReadToEnd();

            stream.Close();
            responseStream.Close();
            response.Close();

            Assert.IsNull(result, result);
        }
    }
}
