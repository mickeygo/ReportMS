using ReportMS.Domain.Models.ReportModule.ReportAggregate;
using System.Data.Entity.ModelConfiguration;

namespace ReportMS.Domain.Repositories.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// 报表所在的数据库值对象的复杂类型配置类
    /// </summary>
    internal class DatabaseComplexTypeConfiguration : ComplexTypeConfiguration<Database>
    {
        public DatabaseComplexTypeConfiguration()
        { }
    }
}
