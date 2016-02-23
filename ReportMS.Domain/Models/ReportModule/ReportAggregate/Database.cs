namespace ReportMS.Domain.Models.ReportModule.ReportAggregate
{
    /// <summary>
    /// 报表所在的数据库值对象
    /// </summary>
    public class Database
    {
        #region Properties

        /// <summary>
        /// 数据库连接名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 数据库的 Schema, 一般为 dbo
        /// </summary>
        public string Schema { get; private set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>Database</c>实例
        /// </summary>
        public Database()
        { }

        /// <summary>
        /// 初始化一个新的<c>Database</c>实例
        /// </summary>
        /// <param name="name">数据库名</param>
        /// <param name="schema">数据库的 Schema</param>
        public Database(string name, string schema)
        {
            this.Name = name;
            this.Schema = schema;
        }

        #endregion
    }
}
