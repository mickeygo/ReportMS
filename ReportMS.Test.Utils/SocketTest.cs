using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils
{
    [TestClass]
    public class SocketTest
    {
        [TestMethod]
        public void TcpClientConnect_Test()
        {
            TcpClient client = null;
            try
            {
                // rma-biaoqian.acn.advantech.corp
                client = new TcpClient("rma-biaoqian.acn.advantech.corp", 18088);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (client != null)
                    client.Close();
            }
        }

        [TestMethod]
        public void GetIpFormHostName_Test()
        {
            // PC000916  -- 172.22.28.22
            // rma-biaoqian  -- 172.21.67.70
            // A-100005866 -- 172.21.131.31

            //var hostEntry = Dns.GetHostEntry("189.84.192.194");
            //var hostName = Dns.GetHostName();

            var addresses = Dns.GetHostAddresses("189.84.192.194");
            var address = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

            Assert.IsNull(address.ToString(), address.ToString());
        }
    }
}
