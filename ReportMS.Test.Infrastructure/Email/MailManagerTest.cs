using System;
using Gear.Infrastructure.Net.Mail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Infrastructure.Email
{
    [TestClass]
    public class MailManagerTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SendMail_Test()
        {
            var manager = new MailManager("Gear Mail Test", "Test", "gang.yang@advantech.com.cn");
            manager.Send();
        }
    }
}
