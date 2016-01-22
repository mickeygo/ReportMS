namespace ReportMS.Framework.Snapshots
{
    /// <summary>
    /// 表示实现类是快照发起者
    /// </summary>
    public interface ISnapshotOriginator
    {
        /// <summary>
        /// 从指定的快照中生成发起者
        /// </summary>
        /// <param name="snapshot">发起者创建的快照</param>
        void BuildFromSnapshot(ISnapshot snapshot);

        /// <summary>
        /// 创建快照
        /// </summary>
        /// <returns>基于当前发起者创建的快照</returns>
        ISnapshot CreateSnapshot();
    }
}
