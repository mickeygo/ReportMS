using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.ReportProfileAggregate;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 报表配置信息 Dto 映射
    /// </summary>
    internal class ReportProfileMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<ReportProfile, ReportProfileDto>();
            AutoMapperAdapter.Register<ReportProfileField, ReportProfileFieldDto>();
        }
    }
}
