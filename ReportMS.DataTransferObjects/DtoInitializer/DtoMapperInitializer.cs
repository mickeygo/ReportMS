using System;
using System.Linq;
using AutoMapper;
using Gear.Infrastructure;
using Gear.Utility.Adapters;

namespace ReportMS.DataTransferObjects.DtoInitializer
{
    /// <summary>
    /// DTO 映射类启动程序
    /// </summary>
    public class DtoMapperInitializer : IApplicationStartup
    {
        /// <summary>
        /// 对应用层服务进行初始化, 在程序启动执行
        /// 有关于 Mapper 对应关系配置文件初始化设置
        /// </summary>
        public void Initialize()
        {
            var profiles = (from a in AppDomain.CurrentDomain.GetAssemblies()
                            where a.FullName.StartsWith("ReportMS.DataTransferObjects", StringComparison.OrdinalIgnoreCase)
                            from t in a.GetTypes()
                            where t.BaseType == typeof(Profile)
                            select t).AsEnumerable();

            AutoMapperAdapter.Initialize(cfg =>
            {
                foreach (var item in profiles.Where(item => item.FullName != "AutoMapper.SelfProfiler`2"))
                {
                    cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });
        }
    }
}
