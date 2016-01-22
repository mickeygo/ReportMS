using Gear.Infrastructure.Storage.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Storage.SqlBuilder
{
    [TestClass]
    public class SqlCountClauseTest
    {
        [TestMethod]
        public void CountSql_Test()
        {
            var sqlQuery = "SELECT ID AS Num, Name, Age, Weight, Address, Enabled, CreatedDate FROM TestTemp";
            var countSql = new SqlSelectCountClauseBuilder(sqlQuery);

            var result = countSql.ToString();

            Assert.Fail(result);
        }
    }
}
