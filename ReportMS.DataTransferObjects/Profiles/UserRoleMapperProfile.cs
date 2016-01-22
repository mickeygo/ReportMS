using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 用户角色 Dto 配置
    /// </summary>
    internal class UserRoleMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<UserRole, UserRoleDto>();
        }
    }
}
