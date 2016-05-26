using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.ReportAggregate;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// Report 映射配置
    /// </summary>
    internal class ReportMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<Report, ReportDto>();
            AutoMapperAdapter.Register<ReportField, ReportFieldDto>();
        }
    }
}
