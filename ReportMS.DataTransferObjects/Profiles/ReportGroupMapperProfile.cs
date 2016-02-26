using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 报表分组映射配置
    /// </summary>
    internal class ReportGroupMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<ReportGroup, ReportGroupDto>();
            AutoMapperAdapter.Register<ReportGroupItem, ReportGroupItemDto>();
        }
    }
}
