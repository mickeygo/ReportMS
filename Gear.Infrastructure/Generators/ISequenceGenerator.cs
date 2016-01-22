namespace Gear.Infrastructure.Generators
{
    /// <summary>
    /// 表示实现接口类是队列生成器
    /// </summary>
    public interface ISequenceGenerator
    {
        /// <summary>
        /// 获取下一个队列
        /// </summary>
        object Next { get; }
    }
}
