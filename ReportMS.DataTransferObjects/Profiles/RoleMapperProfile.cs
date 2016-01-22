using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 角色 Dto 映射配置
    /// </summary>
    internal class RoleMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<Role, RoleDto>();
        }
    }
}
