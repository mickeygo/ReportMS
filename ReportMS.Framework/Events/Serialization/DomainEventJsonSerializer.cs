using ReportMS.Framework.Serialization;

namespace ReportMS.Framework.Events.Serialization
{
    /// <summary>
    /// 领域时间 Json 序列器
    /// </summary>
    public class DomainEventJsonSerializer : ObjectJsonSerializer, IDomainEventSerializer
    {
    }
}
