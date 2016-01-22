using System;
using System.Text;

namespace Gear.Infrastructure.Storage.Builder
{
    /// <summary>
    /// 分页 SQL 语句生成器
    /// </summary>
    public class SqlSelectPagingClauseBuilder
    {
        private int _pgingTempTableCount;
        private readonly string _pagingRowName = "ROW_NUM";
        private readonly StringBuilder _sqlBuilder = new StringBuilder();

        #region Ctor

         /// <summary>
        /// 初始化一个新的<c>SqlSelectPagingClauseBuilder</c>实例
        /// </summary>
        /// <param name="sqlQuery">SQL 语句</param>
        /// <param name="start">起始数, 表示从第多少行数据开始取值（包括此行数据）</param>
        /// <param name="length">取多少数据，从起始数开始，如从 10 行开始，取 6 长度，结果集为 [10, 15]</param>
        public SqlSelectPagingClauseBuilder(string sqlQuery, int start, int length)
        {
            this.Paginate(sqlQuery, start, length);
        }

        #endregion

        #region Private Methods

        private void Out(string str)
        {
            this._sqlBuilder.AppendLine(str);
        }

        private string GetPagingTempTable()
        {
            var prefixPagingTable = "T_Paging_";
            return string.Format("{0}{1}", prefixPagingTable, (this._pgingTempTableCount++).ToString());
        }

        private void Paginate(string sqlQuery, int start, int length)
        {
            this.Out("SELECT * FROM ( ");
            this.PagingSqlWrapper(sqlQuery);
            this.Out(string.Format(" ) AS {0} ", this.GetPagingTempTable()));
            this.PagingSqlFilterClause(start, length);
        }

        private void PagingSqlWrapper(string sqlQuery)
        {
            this.PagingSqlRowNumberHeader(sqlQuery);
            this.Out(sqlQuery.Replace(";", ""));
            this.Out(string.Format(" ) AS {0} ", this.GetPagingTempTable()));
        }

        private void PagingSqlRowNumberHeader(string sqlQuery)
        {
            this.Out(string.Format("SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS {1}, * FROM ( ", this.ResolveRownumberParams(sqlQuery), this._pagingRowName));
        }

        // 分页，start 为起始数.
        // length 为页长度
        private void PagingSqlFilterClause(int start, int length)
        {
            this.Out(string.Format(" WHERE {0} >= {1} AND {0} < {2}", this._pagingRowName, start, (start + length)));
        }

        private string ResolveRownumberParams(string sqlQuery)
        {
            var sqlClauseBuilder = new SqlSelectClauseBuilder(sqlQuery);
            var columns = sqlClauseBuilder.GetOuterSelectColumns();
            return String.Join(",", columns);
        }

        #endregion

        /// <summary>
        /// 重写，将元素的 SQL 转换为带分页的 SQL 语句
        /// </summary>
        /// <returns><see cref="System.String"/></returns>
        public override string ToString()
        {
            return this._sqlBuilder.ToString();
        }
    }
}
