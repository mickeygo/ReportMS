using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 用户 DTO 配置类
    /// </summary>
    internal class UserMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<User, UserDto>();
        }
    }
}
