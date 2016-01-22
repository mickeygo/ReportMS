using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.TenantModule;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 租户 DTO 映射配置
    /// </summary>
    internal class TenantMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<Tenant, TenantDto>();
        }
    }
}
