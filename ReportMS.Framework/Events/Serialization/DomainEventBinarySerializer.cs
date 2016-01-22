using ReportMS.Framework.Serialization;

namespace ReportMS.Framework.Events.Serialization
{
    /// <summary>
    /// 领域事件二进制序列器
    /// </summary>
    public class DomainEventBinarySerializer : ObjectBinarySerializer, IDomainEventSerializer
    {
    }
}
