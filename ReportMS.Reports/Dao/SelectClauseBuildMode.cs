namespace ReportMS.Reports.Dao
{
    /// <summary>
    /// 生成 SELECT 子句的模式
    /// </summary>
    public enum SelectClauseBuildMode
    {
        /// <summary>
        /// 生成原始的 SELECT 子句（不转换类型）
        /// </summary>
        Raw = 1,

        /// <summary>
        /// 生成 SELECT 子句, 若存在匿名，则使用匿名
        /// </summary>
        WithAlias = 2,

        /// <summary>
        /// 生成 SELECT 子句，将类型转换为 NVARCHAR 类型，若存在匿名，则使用匿名
        /// </summary>
        StringWithAlias = 3
    }
}
