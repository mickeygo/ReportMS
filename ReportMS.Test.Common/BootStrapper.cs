using Gear.Infrastructure.Application;

namespace ReportMS.Test.Common
{
    /// <summary>
    /// 程序启动引导程序
    /// </summary>
    public class BootStrapper
    {
        private BootStrapper()
        { }

        /// <summary>
        /// 程序启动
        /// </summary>
        public static void Start()
        {
            AppRuntime.Initialize();
            AppRuntime.Instance.CurrentApplication.Start();
        }
    }
}
