using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 菜单角色 Dto 映射配置
    /// </summary>
    internal class MenuRoleMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<MenuRole, MenuRoleDto>();
        }
    }
}
