using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.ReportGroupAggregate;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 报表分组对应的角色 映射配置 
    /// </summary>
    internal class ReportGroupRoleMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<ReportGroupRole, ReportGroupRoleDto>();
        }
    }
}
