using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 菜单 Dto 映射配置
    /// </summary>
    internal class MenuMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<MenuLevel, MenuLevelDto>();
            AutoMapperAdapter.Register<Menu, MenuDto>();
        }
    }
}
