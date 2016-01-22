using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Reports.Dao;
using ReportMS.Test.Common;

namespace ReportMS.Test.Report.DAO
{
    [TestClass]
    public class ReportDaoTest
    {
        [TestInitialize]
        public void Init()
        {
            BootStrapper.Start();
        }

        [TestMethod]
        public void AdoNet_Test()
        {
            var sql = "SELECT Name AS P1, Sex AS P2, Age AS P3, Birthdate AS P4, Address AS P5 FROM Students WHERE Age = @Age";

            var reader = DatabaseReader.Create("reportDebug").Reader.GetDataReader(sql, new { Age = 17 });

            var sb = new StringBuilder();
            while (reader.Read())
            {
                sb.AppendFormat("{0}|{1}|{2}|{3}|{4};", reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
            }

            if (!reader.IsClosed)
                reader.Close();

            Assert.IsNull(sb, sb.ToString());
        }

        [TestMethod]
        public void GetReport_Test()
        {
            var sqlQuery =
                "SELECT CAST(SNO AS NVARCHAR) AS P1, CAST(Name AS NVARCHAR) AS P2, CAST(Sex AS NVARCHAR) AS P3, CAST(Age AS NVARCHAR) AS P4 ";
            sqlQuery += " , CAST(Birthdate AS NVARCHAR) AS P5, CAST(Address AS NVARCHAR) AS P6 ";
            sqlQuery += " FROM Students  WHERE (Name = @k29yFVkbzH)";

            var student = DatabaseReader.Create("report")
                .Reader.SelectFirstOrDefault<AutoMapperTest>(sqlQuery, new Dictionary<string, object> { { "@k29yFVkbzH", "apple" } });

            Assert.IsNull(student, student.P1);
        }

        [TestMethod]
        public void GetReports_Test()
        {
            //var reports = ReportDao.Default.GetReports();

            //Assert.IsNull(reports);
        }
    }

    class AutoMapperTest
    {
        public string P1 { get; set; }

        public string P2 { get; set; }

        public string P3 { get; set; }

        public string P4 { get; set; }

        public string P5 { get; set; }

        public string P6 { get; set; }
    }
}
