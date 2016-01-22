using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// SQL 包
    /// </summary>
    public class SqlBag
    {
        private readonly StringBuilder sqlBuilder = new StringBuilder();
        private readonly SqlSelectClauseBuilder sqlSelectBuilder;
        private readonly SqlWhereClauseBuilder sqlWhereBuilder;

        private void Out(string s)
        {
            this.sqlBuilder.Append(s);
        }

        private void ClearStringBuilederIfNotNull()
        {
            if (this.sqlBuilder.Length > 0)
                this.sqlBuilder.Clear();
        }

        /// <summary>
        /// 初始化<c>SqlBag</c>实例
        /// </summary>
        /// <param name="tableOrViewName">表/视图名称</param>
        /// <param name="fields">SELECT 字段集合(field/alias field), 值为 null 表示改字段不使用匿名</param>
        /// <param name="operators">字段操作集合</param>
        /// <param name="model">生成 SELECT 子句的模式 [Raw, WithAlias, StringWithAlias]</param>
        public SqlBag(string tableOrViewName, IDictionary<string, string> fields, IEnumerable<SqlOperator> operators, SelectClauseBuildMode model)
            : this(tableOrViewName, null, fields, operators, model)
        { }

        /// <summary>
        /// 初始化<c>SqlBag</c>实例
        /// </summary>
        /// <param name="tableOrViewName">表/视图名称</param>
        /// <param name="tableOrViewAlias">表/视图的匿名, 为 null 表示不用匿名表/视图</param>
        /// <param name="fields">SELECT 字段集合(field/alias field), 值为 null 表示改字段不使用匿名</param>
        /// <param name="operators">字段操作集合</param>
        /// <param name="model">生成 SELECT 子句的模式 [Raw, WithAlias, StringWithAlias]</param>
        public SqlBag(string tableOrViewName, string tableOrViewAlias, IDictionary<string, string> fields, IEnumerable<SqlOperator> operators, SelectClauseBuildMode model)
        {
            this.sqlSelectBuilder = new SqlSelectClauseBuilder(tableOrViewName, tableOrViewAlias, model);
            foreach (var field in fields)
                this.sqlSelectBuilder.AddField(field.Key, field.Value);

            this.sqlWhereBuilder = new SqlWhereClauseBuilder();
            foreach (var item in operators)
                this.sqlWhereBuilder.AddParameterValue(item.Name, item.Operator, item.Value);
        }

        private void BuildSqlSelectString()
        {
            this.Out(this.sqlSelectBuilder.ToString());
        }

        private void BuildSqlWhereString()
        {
            this.Out(" ");
            this.Out(this.sqlWhereBuilder.ToString());
        }

        /// <summary>
        /// 建置 SQL 语句
        /// </summary>
        /// <returns>string</returns>
        public string BuildSqlString()
        {
            this.ClearStringBuilederIfNotNull();
            this.BuildSqlSelectString();
            this.BuildSqlWhereString();
            return this.sqlBuilder.ToString();
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="includeParameterChar">是否包含参数符（如 @、:、?)，默认为 true</param>
        /// <returns>参数字典</returns>
        public IDictionary<string, object> GetSqlParameters(bool includeParameterChar = true)
        {
            var parms = this.sqlWhereBuilder.GetParameterAndValues();
            if (includeParameterChar)
                return parms.ToDictionary(k => k.Key, v => (object)v.Value);

            var parameterChars = new [] { '@', ':', ';'};
            return parms.ToDictionary(k => k.Key.TrimStart(parameterChars), v => (object)v.Value);
        }
    }
}
