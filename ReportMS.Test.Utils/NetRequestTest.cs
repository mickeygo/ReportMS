using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils
{
    [TestClass]
    public class NetRequestTest
    {
        [TestMethod]
        public void RequsetUrlTest()
        {
            //var url = "http://sr-adamforum.advantech.com/handler/aereadanddownload.ashx";

            var uri = new Uri("https://www.baidu.com/");
            var request = WebRequest.Create(uri);
            request.Timeout = (int)TimeSpan.FromMinutes(10).TotalMilliseconds;

            using (var reponse = request.GetResponse())
            using (var reader = new StreamReader(reponse.GetResponseStream()))
            {
                Assert.Fail(reader.ReadToEnd());
            }
        }

        [TestMethod]
        public void TimeLengthTest()
        {
            var num = TimeSpan.FromMinutes(10).TotalMilliseconds;
            Assert.IsTrue(num == 10 * 60 * 1000, num.ToString());
        }

        [TestMethod]
        public void LocalHost_Test()
        {
            var hostName = Dns.GetHostName();
            var hostAddresses = Dns.GetHostAddresses(hostName);
            var hostAddressesIpv4 = hostAddresses.Where(h => h.AddressFamily == AddressFamily.InterNetwork);

            Assert.IsNull(hostAddressesIpv4);
        }
    }
}
