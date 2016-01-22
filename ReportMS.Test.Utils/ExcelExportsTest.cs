using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Gear.Utility.IO.Excels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils
{
    [TestClass]
    public class ExcelExportsTest
    {
        [TestMethod]
        public void CollectionExcelExportTest()
        {
            var datas = new List<Excel_Test>
            {
                new Excel_Test {Name = "AAA", Age = 18, BirthDate = DateTime.Now, Enabled = true},
                new Excel_Test {Name = "BBB", Age = 19, BirthDate = DateTime.Now, Enabled = true},
                new Excel_Test {Name = "CCC", Age = 17, BirthDate = DateTime.Now, Enabled = true},
                new Excel_Test {Name = "DDD", Age = 16, BirthDate = DateTime.Now, Enabled = true},
                new Excel_Test {Name = "EEE", Age = 18, BirthDate = DateTime.Now, Enabled = true}
            };

            var excel = ExcelFactory.Create("test_sheet", datas);
            var bytes = excel.SaveAsBytes();

            this.Save(@"D:\Collection_Test.xlsx", bytes);

            Assert.IsNotNull(bytes);
        }

        void Save(string path, byte[] bytes)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Count());
            }
        }
    }

    class Excel_Test
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Enabled { get; set; }
    }
}
