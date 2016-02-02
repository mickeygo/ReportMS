namespace ReportMS.DataTransferObjects.Dtos
{
    /// <summary>
    /// 数据库中 Table / View 的模式 Dto
    /// </summary>
    public class TableSchemaDto
    {
        /// <summary>
        /// 获取或设置表/视图目录，即表/视图所在的数据库
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
        /// 获取或设置列名
        /// </summary>
        public string ColunmName { get; set; }

        /// <summary>
        /// 获取或设置列的原始的数据类型
        /// </summary>
        public string OriginalDataType { get; set; }

        /// <summary>
        /// 获取或设置列在表/视图中的位置
        /// </summary>
        public int OrdinalPosition { get; set; }

        /// <summary>
        /// 获取或设置列的长度.
        /// 对于 uniqueidentifier、bit、datetime 等非 char、varchar、nchar、nvarchar 等数据类型会显示为 null
        /// </summary>
        public int? MaximumLength { get; set; }

        /// <summary>
        /// 获取或设置列转换后的类型
        /// </summary>
        public string DataType { get; set; }
    }
}
