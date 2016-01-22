namespace ReportMS.Framework.Storage
{
    /// <summary>
    /// 表示实现类是仓储映射关系解析器
    /// </summary>
    public interface IStorageMappingResolver
    {
        /// <summary>
        /// 用指定的类型解析表名
        /// </summary>
        /// <typeparam name="T">要解析的对象类型</typeparam>
        /// <returns>表名</returns>
        string ResolveTableName<T>() where T : class, new();

        /// <summary>
        /// 用指定的类型和属性名解析字段名
        /// </summary>
        /// <typeparam name="T">要解析的对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <returns>字段名</returns>
        string ResolveFieldName<T>(string propertyName) where T : class, new();

        /// <summary>
        /// 检查给定的属性是否是一个自动生成(auto-generated)的属性字段
        /// </summary>
        /// <typeparam name="T">要解析的对象类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <returns>True 表示是一个自动生成(auto-generated)的属性字段, 否则为 false.</returns>
        bool IsAutoIdentityField<T>(string propertyName) where T : class, new();
    }
}
