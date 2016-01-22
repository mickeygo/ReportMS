using Gear.Infrastructure.Storage.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Storage.SqlBuilder
{
    [TestClass]
    public class SqlPagingClauseTest
    {
        [TestMethod]
        public void PagingSql_Test()
        {
            var sqlQuery = "SELECT ID AS Num, Name, Age, Weight, Address, Enabled, CreatedDate FROM TestTemp";
            var pagingSql = new SqlSelectPagingClauseBuilder(sqlQuery, 2, 6);

            var result = pagingSql.ToString();

            Assert.Fail(result);
        }

        [TestMethod]
        public void PagingSql2_Test()
        {
            var sqlQuery = "SELECT ID AS Num, Name, Age P_Age, TestTemp.[Weight] AS Wgt, Address, Enabled, CreatedDate FROM TestTemp";
            var pagingSql = new SqlSelectPagingClauseBuilder(sqlQuery, 2, 6);

            var result = pagingSql.ToString();

            Assert.Fail(result);
        }
    }
}
