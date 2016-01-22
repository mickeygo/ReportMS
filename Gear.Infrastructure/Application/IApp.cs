namespace Gear.Infrastructure.Application
{
    /// <summary>
    /// 表示实现的类是应用程序
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// 获取对象容器
        /// </summary>
        ObjectContainer ObjectContainer { get; }

        /// <summary>
        /// 启动应用程序
        /// </summary>
        void Start();
    }
}
