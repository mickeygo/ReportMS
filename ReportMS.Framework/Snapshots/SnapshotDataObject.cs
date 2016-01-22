using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ReportMS.Framework.Snapshots.Serialization;

namespace ReportMS.Framework.Snapshots
{
    /// <summary>
    /// 快照数据对象
    /// </summary>
    [Serializable, XmlRoot, DataContract]
    public class SnapshotDataObject : IEntity
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置 <see cref="System.Byte"/> 类型数组，用来呈现快照数据的二进制内容
        /// </summary>
        [XmlElement, DataMember]
        public byte[] SnapshotData { get; set; }

        /// <summary>
        /// 获取或设置聚合根标识
        /// </summary>
        [XmlElement, DataMember]
        public Guid AggregateRootID { get; set; }

        /// <summary>
        /// 获取或设置聚合根类型的程序集限定类型名
        /// </summary>
        [XmlElement, DataMember]
        public string AggregateRootType { get; set; }

        /// <summary>
        /// 获取或设置快照类型的程序集限定类型名
        /// </summary>
        [XmlElement, DataMember]
        public string SnapshotType { get; set; }

        /// <summary>
        /// 获取或设置快照版本号
        /// </summary>
        /// <remarks>快照版本与事件版本相等</remarks>
        [XmlElement, DataMember]
        public long Version { get; set; }

        /// <summary>
        /// 获取或设置快照生成的时间戳
        /// </summary>
        [XmlElement, DataMember]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 获取或设置快照数据对象的标识
        /// </summary>
        [XmlElement, DataMember]
        public Guid ID { get; set; }

        #endregion

        #region Private Methods

        private static ISnapshotSerializer GetSnapshotSerializer()
        {
            // TODO: ReportMS.Framework.Snapshots.SnapshotDataObject: using Config

            //ApworksConfigSection config = AppRuntime.Instance.CurrentApplication.ConfigSource.Config;
            //if (config.Serializers == null ||
            //    config.Serializers.SnapshotSerializer == null ||
            //    string.IsNullOrEmpty(config.Serializers.SnapshotSerializer.Provider) ||
            //    string.IsNullOrWhiteSpace(config.Serializers.SnapshotSerializer.Provider))
            //{
            //    serializer = new SnapshotXmlSerializer();
            //}
            //else
            //{
            //    string typeName = config.Serializers.SnapshotSerializer.Provider;
            //    Type serializerType = Type.GetType(typeName);
            //    if (serializerType == null)
            //        throw new InfrastructureException("The serializer defined by type '{0}' doesn't exist.", typeName);
            //    serializer = (ISnapshotSerializer)Activator.CreateInstance(serializerType);
            //}

            return new SnapshotXmlSerializer();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 从当前快照数据对象中提取快照
        /// </summary>
        /// <returns>快照实例</returns>
        public ISnapshot ExtractSnapshot()
        {
            var serializer = GetSnapshotSerializer();
            var snapshotType = Type.GetType(SnapshotType);
            if (snapshotType == null)
                return null;
            return (ISnapshot)serializer.Deserialize(snapshotType, this.SnapshotData);
        }
        /// <summary>
        /// 从聚合根中创建快照数据对象
        /// </summary>
        /// <param name="aggregateRoot">快照被创建的聚合根</param>
        /// <returns>快照数据对象</returns>
        public static SnapshotDataObject CreateFromAggregateRoot(ISourcedAggregateRoot aggregateRoot)
        {
            var serializer = GetSnapshotSerializer();
            var snapshot = aggregateRoot.CreateSnapshot();

            return new SnapshotDataObject
            {
                AggregateRootID = aggregateRoot.ID,
                AggregateRootType = aggregateRoot.GetType().AssemblyQualifiedName,
                Version = aggregateRoot.Version,
                SnapshotType = snapshot.GetType().AssemblyQualifiedName,
                Timestamp = snapshot.Timestamp,
                SnapshotData = serializer.Serialize(snapshot)
            };
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this.AggregateRootID.GetHashCode(),
                this.AggregateRootType.GetHashCode(),
                this.ID.GetHashCode(),
                this.SnapshotData.GetHashCode(),
                this.SnapshotType.GetHashCode(),
                this.Timestamp.GetHashCode(),
                this.Version.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as SnapshotDataObject;
            if (other == null)
                return false;
            return this.ID == other.ID;
        }
        #endregion
    }
}
