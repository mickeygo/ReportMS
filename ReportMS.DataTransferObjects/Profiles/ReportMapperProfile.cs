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
            AutoMapperAdapter.Register<Report, ReportDto>()
                .ForMember(d => d.Database, o => o.MapFrom(s => s.Database.Name))
                .ForMember(d => d.Schema, o => o.MapFrom(s => s.Database.Schema));
        }
    }
}
