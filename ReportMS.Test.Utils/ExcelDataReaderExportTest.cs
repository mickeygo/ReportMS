using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Gear.Utility.IO.Excels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Utils
{
    [TestClass]
    public class ExcelDataReaderExportTest
    {
        [TestMethod]
        public void DataReaderExcelExportTest()
        {
            var sql = "SELECT Name, Sex, Age, Birthdate, Address FROM Students";
            var dataReader = this.ExecuteReader(sql);

            var excel = ExcelFactory.Create("test_sheet", dataReader);
            var bytes = excel.SaveAsBytes();

            this.Save(@"D:\DataReader_Test.xlsx", bytes);
        }

        void Save(string path, byte[] bytes)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Count());
            }
        }

        private IDataReader ExecuteReader(string sqlQuery)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["reportDebug"].ConnectionString;

            DbConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                if (con.State != ConnectionState.Open)
                    con.Open();
                using (var cmd = con.CreateCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlQuery;
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception)
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                    con.Dispose();
                }

                throw;
            }
        }
    }
}
