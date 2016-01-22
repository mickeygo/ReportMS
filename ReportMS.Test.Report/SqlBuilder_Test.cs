using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportMS.Reports.Dao;

namespace ReportMS.Test.Report
{
    [TestClass]
    public class SqlBuilder_Test
    {
        private string _tableName;
        private string _tableAliasName;
        private List<Tuple<string, string>> _fields;
        private List<Tuple<string, string, string>> _operators;

        [TestInitialize]
        public void Init()
        {
            this._tableName = "table_test";
            this._tableAliasName = "table_alias_test";

            this._fields = new List<Tuple<string, string>>
            {
                Tuple.Create("Field_1", "Field_Alias_1"),
                Tuple.Create("Field_2", "Field_Alias_2"),
                Tuple.Create("Field_3", "Field_Alias_3"),
                Tuple.Create("Field_4", "Field_Alias_4"),
                Tuple.Create("Field_5", "")
            };

            this._operators = new List<Tuple<string, string, string>>
            {
                Tuple.Create("Field_1", "=", "value_1"),
                Tuple.Create("Field_2", ">=", "value_2"),
                Tuple.Create("Field_3", "<", "value_3"),
                Tuple.Create("Field_4", "<=", "value_4"),
                Tuple.Create("Field_5", "<>", "value_5")
            };
        }

        [TestMethod]
        public void SqlSelectClauseBuilderTest()
        {
            var selectBuilder = new SqlSelectClauseBuilder(this._tableName, SelectClauseBuildMode.StringWithAlias);
            foreach (var field in this._fields)
                selectBuilder.AddField(field.Item1, field.Item2);

            var selectResult = selectBuilder.ToString();
            Assert.IsTrue(String.IsNullOrWhiteSpace(selectResult), selectResult);
        }

        [TestMethod]
        public void SqlWhereClauseBuilderTest()
        {
            var whereBuilder = new SqlWhereClauseBuilder();
            this._operators.ForEach(o =>
            {
                whereBuilder.AddParameterValue(o.Item1, o.Item2, o.Item3);
            });

            var whereResult = whereBuilder.ToString();
            //Assert.IsTrue(String.IsNullOrWhiteSpace(whereResult), whereResult);

            var paramResult = new StringBuilder();
            var parameters = whereBuilder.GetParameterAndValues();
            foreach (var p in parameters)
            {
                paramResult.AppendFormat("{0}:{1};", p.Key, p.Value);
            }

            var result = whereResult + paramResult.ToString();
            Assert.Fail(result, result);
        }

        [TestMethod]
        public void SqlClauseBuilderTest()
        {
            var selectBuilder = new SqlSelectClauseBuilder(this._tableName, SelectClauseBuildMode.StringWithAlias);
            foreach (var field in this._fields)
                selectBuilder.AddField(field.Item1, field.Item2);

            var whereBuilder = new SqlWhereClauseBuilder();
            this._operators.ForEach(o =>
            {
                whereBuilder.AddParameterValue(o.Item1, o.Item2, o.Item3);
            });

            var sqlResult = selectBuilder.ToString() + whereBuilder.ToString();
            Assert.IsNull(sqlResult, sqlResult);
        }

        [TestMethod]
        public void SqlBagTest()
        {
            var whereBuilder = new SqlWhereClauseBuilder();
            this._operators.ForEach(o =>
            {
                whereBuilder.AddParameterValue(o.Item1, o.Item2, o.Item3);
            });

            var parameters = whereBuilder.GetParameterAndValues();

            var parameterBuilder = new SqlParameterBuilder();
            foreach (var p in parameters)
            {
                parameterBuilder.AddParameterValue(p.Key, p.Value);
            }

            var parames = parameterBuilder.GetParameters();
            var result = new StringBuilder();
            foreach (var p in parames)
            {
                result.AppendFormat("{0}:{1};", p.ParameterName, p.Value);
            }

            Assert.Fail(result.ToString(), result);
        }
    }
}
