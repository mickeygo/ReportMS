using System;
using System.Collections.Generic;
using System.IO;
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

        [TestMethod]
        public void SendMailWithAttachment_Test()
        {
            var path = @"D:\Test.txt";

            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var attachmemts = new List<Tuple<Stream, string>> { Tuple.Create((Stream)fs, "MyTest.xlsx"), Tuple.Create((Stream)fs, "MyTest.txt") };
            var manager = new MailManager("Gear Mail Test", "Test", new[] { "gang.yang@advantech.com.cn" }, attachmemts);
            manager.Send();
        }

        [TestMethod]
        public void SendMailWithAttachment_MemoryStreamTest()
        {
            var path = @"D:\MyTest.xlsx";

            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            var attachmemts = new List<Tuple<Stream, string>> { Tuple.Create((Stream)fs, "MyTest.xlsx"), Tuple.Create((Stream)fs, "MyTest.txt") };
            var manager = new MailManager("Gear Mail Test", "Test", new[] { "gang.yang@advantech.com.cn" }, attachmemts);
            manager.Send();
        }
    }
}
