using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Gear.Infrastructure.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;
using ReportMS.Test.Common;

namespace ReportMS.Test.Storage
{
    [TestClass]
    public class DapperTest
    {
        string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["rms"].ConnectionString;
        }

        IDbConnection GetSqlServerConnection()
        {
            var connectionString = this.GetConnectionString();
            if (connectionString == null)
                throw new StorageException("ConnectionString is null.");
            return new SqlConnection(connectionString);
        }

        [TestMethod]
        public void SqlServerQueryEnumerable_Test()
        {
            var conn = this.GetSqlServerConnection();
            var sql = "SELECT * FROM TestTemp";
            //var sqlError = "SELECT * FROM TestTemp_Error";  // 测试异常情况下，Dapper 内部是否会关闭 Connection 连接（测试结果：会关闭）

            try
            {
                var tests = conn.Query<TestTemp>(sql);
                Assert.IsNotNull(tests);
                Assert.IsTrue(!tests.Any());
            }
            catch
            {
                
            }
            
            Assert.IsTrue(conn.State == ConnectionState.Closed);
        }

        [TestMethod]
        public void SqlServerQueryParamterEnumerable_Test()
        {
            var conn = this.GetSqlServerConnection();
            var sql = "SELECT * FROM TestTemp WHERE Name = @Name AND Enabled = @Enabled";

            var persons = conn.Query<TestTemp>(sql, new { Name = "apple", Enabled = true });

            var detail = Utils.LookupEntityDetail(persons);
            Assert.IsNull(detail, detail);
            Assert.IsNotNull(persons);
            Assert.IsTrue(persons.Count() == 1);
        }

        [TestMethod]
        public void SqlServerQueryParamterEnumerable2_Test()
        {
            var conn = this.GetSqlServerConnection();
            var sql = "SELECT * FROM TestTemp WHERE Name = @Name AND Enabled = @Enabled";

            var param = new Dictionary<string, object> { { "Name", "apple" }, { "Enabled", true } };

            var persons = conn.Query<TestTemp>(sql, param);

            var detail = Utils.LookupEntityDetail(persons);
            Assert.IsNull(detail, detail);
            Assert.IsNotNull(persons);
            Assert.IsTrue(persons.Count() == 1);
        }

        [TestMethod]
        public void SqlServerQueryParamterEnumerable3_Test()
        {
            var conn = this.GetSqlServerConnection();
            var sql = "SELECT * FROM TestTemp WHERE Name = @Name AND Enabled = @Enabled";

            var param = new Dictionary<string, object> { { "@Name", "apple" } };

            var persons = conn.Query<TestTemp>(sql, param);

            var detail = Utils.LookupEntityDetail(persons);
            Assert.IsNull(detail, detail);
            Assert.IsNotNull(persons);
            Assert.IsTrue(persons.Count() == 1);
        }

        [TestMethod]
        public void SqlServerQueryCount_Test()
        {
            var conn = this.GetSqlServerConnection();
            var sql = "SELECT COUNT(1) FROM TestTemp";
            //var sqlError = "SELECT COUNT(1) FROM TestTemp_Error";  // 测试异常情况下，Dapper 内部是否会关闭 Connection 连接（测试结果：会关闭）

            try
            {
                var count = conn.ExecuteScalar<int>(sql);
                Assert.IsTrue(count == 6);
            }
            catch
            {

            }

            Assert.IsTrue(conn.State == ConnectionState.Closed);
        }

        [TestMethod]
        public void SqlServerDataReader_Test()
        {
            var conn = this.GetSqlServerConnection();
            var sql = "SELECT COUNT(1) FROM TestTemp";

            using (var reader = conn.ExecuteReader(new CommandDefinition(sql), CommandBehavior.CloseConnection))
            {
                Assert.IsTrue(reader.Read());
            }

            Assert.IsTrue(conn.State == ConnectionState.Closed);
        }

        [TestMethod]
        public void QueryMulit_Test()
        {
            var conn = this.GetSqlServerConnection();

            var sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT	report.*, field.* ");
            sqlQuery.Append(" FROM RMS_Report report");
            sqlQuery.Append(" INNER JOIN RMS_ReportField field ON field.ReportId = report.ReportId");
            sqlQuery.Append(" WHERE report.ReportId = @ReportId");
            sqlQuery.Append(" ORDER BY report.ReportId, field.Sort");

            Report report = null;

            var result = conn.Query<Report, ReportField, Report>(sqlQuery.ToString(), (r, f) =>
            {
                if (report == null)
                    report = r;
                report.Fields.Add(f);

                return report;
            },
                new {ReportId = new Guid("88BE0A9A-9DBD-41AC-A2BB-69853FF905DF")}, null, true, "ReportId").ToList();

            Assert.IsNull(report, report.Fields.Count.ToString());
        }

    }

    class TestTemp
    {
        public Guid ID { get; private set; }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public double Weight { get; private set; }

        public string Address { get; private set; }

        public bool Enabled { get; private set; }

        public DateTime? CreatedDate { get; private set; }
    }
}
