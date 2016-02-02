namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 数据库模式 Dto
    /// </summary>
    public class DatabaseSchemaDto
    {
        /// <summary>
        /// 获取或设置表/视图目录, 即表/视图所在的数据库
        /// </summary>
        public string TableCatalog { get; set; }

        /// <summary>
        /// 获取或设置表/视图模式, e.g: dbo
        /// </summary>
        public string TableSchema { get; set; }

        /// <summary>
        /// 获取或设置表/视图名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 获取或设置表/视图类型（BASE TABLE / VIEW）
        /// </summary>
        public string TableType { get; set; }
    }
}
