using ReportMS.Framework.Serialization;

namespace ReportMS.Framework.Events.Serialization
{
    /// <summary>
    /// 领域事件数据契约序列器
    /// </summary>
    public class DomainEventDataContractSerializer : ObjectDataContractSerializer, IDomainEventSerializer
    {
    }
}
