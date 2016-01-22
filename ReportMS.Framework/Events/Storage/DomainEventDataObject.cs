using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ReportMS.Framework.Events.Serialization;

namespace ReportMS.Framework.Events.Storage
{
    /// <summary>
    /// 领域事件数据对象
    /// </summary>
    [Serializable, XmlRoot, DataContract]
    public class DomainEventDataObject
    {
        #region Private Fields
        private readonly IDomainEventSerializer serializer;
        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>DomainEventDataObject</c>实例
        /// </summary>
        public DomainEventDataObject()
        {
            this.serializer = GetDomainEventSerializer();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取或设置当前领域事件对象的<see cref="System.Byte"/>值
        /// </summary>
        [XmlElement, DataMember]
        public byte[] Data { get; set; }

        /// <summary>
        /// 获取或设置领域事件类型程序集限定名
        /// </summary>
        [XmlElement, DataMember]
        public string AssemblyQualifiedEventType { get; set; }

        /// <summary>
        /// 获取或设置领域事件标识
        /// </summary>
        /// <remarks>Note that since the <c>DomainEventDataObject</c> is the data
        /// presentation of the corresponding domain event object, this identifier value
        /// can also be considered to be the identifier for the <c>DomainEventDataObject</c> instance.
        /// 
        /// </remarks>
        [XmlElement, DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置拥有当前领域事件实例的聚合根标识
        /// </summary>
        [XmlElement, DataMember]
        public Guid SourceID { get; set; }

        /// <summary>
        /// 获取或设置聚合根类型的程序集限定名
        /// </summary>
        [XmlElement, DataMember]
        public string AssemblyQualifiedSourceType { get; set; }

        /// <summary>
        /// 获取或设置事件生成的时间戳
        /// </summary>
        /// <remarks>由于可能在不同的系统中生成，建议使用标准的 UTC 时间格式</remarks>
        [XmlElement, DataMember]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 获取或设置领域事件数据对象的版本
        /// </summary>
        [XmlElement, DataMember]
        public long Version { get; set; }

        #endregion

        #region Private Methods

        /// <summary>
        /// 获取领域事件的序列器
        /// </summary>
        /// <returns>领域事件序列器实例</returns>
        private static IDomainEventSerializer GetDomainEventSerializer()
        {
            IDomainEventSerializer serializer;

            // TODO: ReportMS.Framework.Events.Storage.DomainEventDataObject  GetDomainEventSerializer

            //ApworksConfigSection config = AppRuntime.Instance.CurrentApplication.ConfigSource.Config;
            //if (config.Serializers == null ||
            //    config.Serializers.EventSerializer == null ||
            //    string.IsNullOrEmpty(config.Serializers.EventSerializer.Provider) ||
            //    string.IsNullOrWhiteSpace(config.Serializers.EventSerializer.Provider))
            //{
            //    serializer = new DomainEventXmlSerializer();
            //}
            //else
            //{
            //    string typeName = config.Serializers.EventSerializer.Provider;
            //    var serializerType = Type.GetType(typeName);
            //    if (serializerType == null)
            //        throw new InfrastructureException("The serializer defined by type '{0}' doesn't exist.", typeName);
            //    serializer = (IDomainEventSerializer)Activator.CreateInstance(serializerType);
            //}
            serializer = new DomainEventXmlSerializer();

            return serializer;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从指定的领域事件中创建并初始化领域事件数据对象
        /// </summary>
        /// <param name="entity">要创建并初始化领域事件数据对象实例</param>
        /// <returns>初始化的数据对象实例</returns>
        public static DomainEventDataObject FromDomainEvent(IDomainEvent entity)
        {
            var serializer = GetDomainEventSerializer();
            var obj = new DomainEventDataObject
            {
                Data = serializer.Serialize(entity),
                ID = entity.ID,
                AssemblyQualifiedEventType =
                    string.IsNullOrEmpty(entity.AssemblyQualifiedEventType)
                        ? entity.GetType().AssemblyQualifiedName
                        : entity.AssemblyQualifiedEventType,
                Timestamp = entity.Timestamp,
                Version = entity.Version,
                SourceID = entity.Source.ID,
                AssemblyQualifiedSourceType = entity.Source.GetType().AssemblyQualifiedName
            };

            return obj;
        }

        /// <summary>
        /// 将领域事件数据对象转换为其相应的领域事件实体实例
        /// </summary>
        /// <returns>由当前领域事件数据对象转换的领域事件实体实例</returns>
        public IDomainEvent ToDomainEvent()
        {
            if (string.IsNullOrEmpty(this.AssemblyQualifiedEventType))
                throw new ArgumentNullException("AssemblyQualifiedTypeName");
            if (this.Data == null || !this.Data.Any())
                throw new ArgumentNullException("Data");

            var type = Type.GetType(this.AssemblyQualifiedEventType);
            var ret = (IDomainEvent)serializer.Deserialize(type, this.Data);
            ret.ID = this.ID;

            return ret;
        }

        #endregion
    }
}
