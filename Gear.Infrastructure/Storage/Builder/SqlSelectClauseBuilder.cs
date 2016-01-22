using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gear.Infrastructure.Storage.Builder
{
    /// <summary>
    /// RDBM SQL SELECT 语句生成器
    /// </summary>
    internal class SqlSelectClauseBuilder
    {
        private readonly string _sqlQuery;

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>SqlSelectClauseBuilder</c>实例
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句</param>
        public SqlSelectClauseBuilder(string sqlQuery)
        {
            this._sqlQuery = sqlQuery;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取 SELECT 语句中字段名
        /// 注：字段名不含有 表名, 如 (dbo. 、sap.) 等
        /// </summary>
        /// <returns>字符串数组</returns>
        public string[] GetOuterSelectColumns()
        {
            var outerSelectClause = this.ExtractOuterSelectClause(this._sqlQuery);
            var outerSelectClauseWithoutSelectAndFrom = Regex.Replace(outerSelectClause, @"SELECT|FROM", "", RegexOptions.IgnoreCase);

            Func<MatchCollection, string> match = matchs =>
            {
                var len = matchs.Count;
                return matchs[len - 1].Value;
            };

            return (from column in outerSelectClauseWithoutSelectAndFrom.Split(',')
                        select match(Regex.Matches(column, @"[a-zA-Z0-9_\[\]]+"))).ToArray();
        }

        /// <summary>
        /// 建制返回数量的 SQL 子句，eg： SELECT COUNT(1) FROM ...
        /// </summary>
        /// <returns>返回数量的 SQL 子句</returns>
        public string BuildSelectCountClause()
        {
            if (this.CheckSelectCountClause(this._sqlQuery))
                return this._sqlQuery;
            return "SELECT COUNT(1) FROM " + this.ExtractOuterRemainingSelectClause(this._sqlQuery);
        }

        #endregion

        #region Private Methods

        // 提取最外层的 select ... from 语句
        private string ExtractOuterSelectClause(string sqlQuery)
        {
            var fromPosition = sqlQuery.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);
            if (fromPosition == -1)
                throw new IndexOutOfRangeException("The SQL keyword 'FROM' not found in the sql clause.");

            return sqlQuery.Substring(0, fromPosition + 4);
        }

        // 提取 SELECT 语句，排除最外层的 select ... from 部分
        private string ExtractOuterRemainingSelectClause(string sqlQuery)
        {
            var fromPosition = sqlQuery.IndexOf("FROM", StringComparison.OrdinalIgnoreCase);
            if (fromPosition == -1)
                throw new IndexOutOfRangeException("The SQL keyword 'FROM' not found in the sql clause.");

            return sqlQuery.Substring(fromPosition + 4);
        }

        // 检查 SQL 语句是否是返回数量的 SQL 子句
        private bool CheckSelectCountClause(string sqlQuery)
        {
            var removeSelectAndFromClause = this.ExtractOuterSelectClause(sqlQuery);
            return removeSelectAndFromClause.IndexOf("COUNT(1)", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   removeSelectAndFromClause.IndexOf("COUNT(*)", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion        
    }
}
