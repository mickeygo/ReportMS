using System;
using System.Linq.Expressions;

namespace ReportMS.Framework.Storage.Builders
{
    /// <summary>
    /// 表示实现类是能建制能被关系型数据库的 SQL 语法解析的 WHERE 条件子句
    /// </summary>
    /// <typeparam name="T">能够对应到关系型数据库中具体表的对象类型</typeparam>
    public interface IWhereClauseBuilder<T>
        where T : class, new()
    {
        /// <summary>
        /// 用给定的表达式对象建制 WHERE 子句
        /// </summary>
        /// <param name="expression">对象表达式</param>
        /// <returns>包含建制结果的 <c>WhereClauseBuildResult</c> 实例</returns>
        WhereClauseBuildResult BuildWhereClause(Expression<Func<T, bool>> expression);
    }
}
