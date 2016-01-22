namespace Gear.Infrastructure
{
    /// <summary>
    /// 表示实现此接口的为应用程序启动类
    /// 注：在应用程序第一次运行时初始化
    /// </summary>
    public interface IApplicationStartup
    {
        /// <summary>
        /// 初始化设置
        /// </summary>
        void Initialize();
    }
}
