namespace ReportMS.Framework.Generators
{
    /// <summary>
    /// 表示实现类是队列生成器
    /// </summary>
    public interface ISequenceGenerator
    {
        /// <summary>
        /// 获取下一个队列
        /// </summary>
        object Next { get; }
    }
}
