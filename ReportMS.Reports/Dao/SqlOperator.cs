namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// SQL 操作
    /// </summary>
    public class SqlOperator
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作值
        /// </summary>
        public string Value { get; set; }
    }
}
