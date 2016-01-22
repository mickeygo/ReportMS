namespace Gear.Infrastructure.Storage.Builder
{
    /// <summary>
    /// 查询数据数量的 SQL 语句生成器
    /// </summary>
    public class SqlSelectCountClauseBuilder
    {
        private readonly string _sqlQuery;

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>SqlSelectCountClauseBuilder</c>实例
        /// </summary>
        /// <param name="sqlQuery">SQL 查询语句</param>
        public SqlSelectCountClauseBuilder(string sqlQuery)
        {
            this._sqlQuery = sqlQuery;
        }

        #endregion

        #region Private Methods

        #endregion

        /// <summary>
        /// 重写，将元素的 SQL 转换查询数据数量的 SQL 语句
        /// </summary>
        /// <returns><see cref="System.String"/></returns>
        public override string ToString()
        {
            var sqlClauseBuilder = new SqlSelectClauseBuilder(this._sqlQuery);
            return sqlClauseBuilder.BuildSelectCountClause();
        }
    }
}
