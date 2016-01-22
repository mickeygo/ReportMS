namespace Gear.Infrastructure.Repositories
{
    /// <summary>
    /// 表示实现接口的实体可以软删除
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示此实体是否可用
        /// </summary>
        bool Enabled { get; }
    }
}
