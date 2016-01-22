namespace ReportMS.Test.Common
{
    /// <summary>
    /// 公共辅助类
    /// </summary>
    public static class Helper
    {
        static Helper()
        {
            Init();
        }

        public static void Init()
        {
            BootStrapper.Start();
        }
    }
}
