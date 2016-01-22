using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.AccountModule;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// Actions 映射配置
    /// </summary>
    internal class ActionsMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<Actions, ActionsDto>();
        }
    }
}
