using AutoMapper;
using Gear.Utility.Adapters;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.RdbmsAggregate;

namespace ReportMS.DataTransferObjects.Profiles
{
    /// <summary>
    /// 关系型数据库映射配置类
    /// </summary>
    internal class RdbmsMapperProfile : Profile
    {
        protected override void Configure()
        {
            AutoMapperAdapter.Register<Rdbms, RdbmsDto>();
        }
    }
}
