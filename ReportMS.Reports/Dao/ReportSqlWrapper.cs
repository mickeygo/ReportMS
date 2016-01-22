using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// SQL 报表包装类
    /// </summary>
    public class ReportSqlWrapper
    {
        private readonly char entitySegmentationSymbol = ';';
        private readonly char operatorSegmentationSymbol = ',';
        private readonly string operatorPattern = @"^P[1-99]$";
        private readonly string fieldPattern = @"^F[1-99]$";
        private readonly string tableOrViewIndexName = "T0";
        private readonly List<SqlOperator> sqlOperators = new List<SqlOperator>();
        private SqlBag sqlBag;

        #region Private Methods

        private IEnumerable<SqlOperator> GetReportOperators()
        {
            var nameValues = this.GetOperatorsFromWebForm();
            foreach (var pair in nameValues)
            {
                if (pair.Value.IndexOf(this.entitySegmentationSymbol) != -1)
                {
                    var vals = pair.Value.Split(this.entitySegmentationSymbol);
                    foreach (var repOperator in vals.Select(this.SetReportOperator).Where(rep => rep != null))
                        this.sqlOperators.Add(repOperator);
                }
                else
                {
                    var repOperator = this.SetReportOperator(pair.Value);
                    if (repOperator != null)
                        this.sqlOperators.Add(repOperator);
                }
            }

            return this.sqlOperators;
        }

        private Guid? GetReportTableOrViewIdFromWebForm()
        {
            var namevalueforms = HttpContext.Current.Request.Form;
            var tableOrViewId = namevalueforms[this.tableOrViewIndexName];
            if (String.IsNullOrWhiteSpace(tableOrViewId))
                return null;
            return new Guid(tableOrViewId);
        }

        // field / alias
        private IDictionary<string, string> GetFieldsFromWebForm()
        {
            var namevalueforms = HttpContext.Current.Request.Form;
            return (from key in namevalueforms.AllKeys.Where(k => Regex.IsMatch(k, this.fieldPattern))
                    let value = namevalueforms[key].Split(',')
                    select Tuple.Create(value[0], value[1])).ToDictionary(s => s.Item1, s => s.Item2);
        }

        private IDictionary<string, string> GetOperatorsFromWebForm()
        {
            var namevalueforms = HttpContext.Current.Request.Form;
            return namevalueforms.AllKeys.Where(k => Regex.IsMatch(k, this.operatorPattern)).ToDictionary(key => key, key => namevalueforms[key]);
        }

        private SqlOperator SetReportOperator(string condition)
        {
            var con = this.SplitCondition(condition);
            return con == null ? null : new SqlOperator { Name = con.Item1, Operator = con.Item2, Value = con.Item3 };
        }

        /// <summary>
        /// Name / Operator / Value
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private Tuple<string, string, string> SplitCondition(string condition)
        {
            if (String.IsNullOrWhiteSpace(condition))
                return null;

            var conditions = condition.Split(this.operatorSegmentationSymbol);
            if (conditions.Any() && conditions.Any(String.IsNullOrWhiteSpace))
                return null;

            return Tuple.Create(conditions[0], conditions[1], conditions[2]);
        }

        private void ExecuteSqlBagBuilder(string table, IDictionary<string, string> fields, IEnumerable<SqlOperator> operators, SelectClauseBuildMode model)
        {
            this.sqlBag = new SqlBag(table, fields, operators, model);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取 Table 或 View 的 Id 
        /// </summary>
        /// <returns>若有 Table 或 View，返回 Guid；否则返回 null.</returns>
        public Guid? GetReportTableOrViewId()
        {
            return this.GetReportTableOrViewIdFromWebForm();
        }

        /// <summary>
        /// 建置 SQL 包
        /// </summary>
        /// <param name="tableOrView">Table 或 View 名称</param>
        /// <param name="model">生成 SELECT 子句的模式 [Raw, WithAlias, StringWithAlias]</param>
        public void ExecuteSqlBagBuilder(string tableOrView, SelectClauseBuildMode model)
        {
            this.ExecuteSqlBagBuilder(tableOrView, this.GetFieldsFromWebForm(), this.GetReportOperators(), model);
        }

        /// <summary>
        /// 获取 SQL 语句
        /// </summary>
        /// <returns></returns>
        public string GetSqlClause()
        {
            return this.sqlBag.BuildSqlString();
        }

        /// <summary>
        /// 获取 Sql 参数集合
        /// </summary>
        /// <returns>参数字典</returns>
        public IDictionary<string, object> GetSqlParameters()
        {
            return this.sqlBag.GetSqlParameters();
        }

        #endregion
    }
}
