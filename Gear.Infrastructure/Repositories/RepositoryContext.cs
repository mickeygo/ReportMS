using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Gear.Infrastructure.Repositories
{
    /// <summary>
    /// 仓储上下文基类。
    /// 用于 注册新的、要更改的 或 要删除的 数据，并作用于工作单元
    /// </summary>
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        #region Private Fields

        private readonly Guid id = Guid.NewGuid();

        // remark: we use the ConcurrentDictionary to replace the ThreadLocal<T> for using the async method.
        private readonly ConcurrentDictionary<object, byte> newCollection = new ConcurrentDictionary<object, byte>();
        private readonly ConcurrentDictionary<object, byte> modifiedCollection = new ConcurrentDictionary<object, byte>();
        private readonly ConcurrentDictionary<object, byte> deletedCollection = new ConcurrentDictionary<object, byte>();
        private volatile bool committed = true;

        #endregion

        #region Protected Methods

        /// <summary>
        /// 清除仓储上下文中所有的注册信息
        /// </summary>
        /// <remarks>注:仅能在仓储上下文被成功提交后才能调用此方法</remarks>
        protected void ClearRegistrations()
        {
            this.newCollection.Clear();
            this.modifiedCollection.Clear();
            this.deletedCollection.Clear();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"><see cref="System.Boolean"/>是否需要显示地清除资料</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.ClearRegistrations();
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取一个并发字典，包含所有的要添加到仓储上下文中的对象。
        /// 取 Key 值表示要添加的实体对象
        /// </summary>
        protected ConcurrentDictionary<object, byte> NewCollection
        {
            get { return this.newCollection; }
        }

        /// <summary>
        /// 获取一个并发字典，包含所有的在仓储上下文中需要修改的对象。
        /// 取 Key 值表示要修改的实体对象
        /// </summary>
        protected ConcurrentDictionary<object, byte> ModifiedCollection
        {
            get { return this.modifiedCollection; }
        }

        /// <summary>
        /// 获取一个并发字典，包含所有的在仓储上下文中需要删除的对象。
        /// 取 Key 值表示要删除的实体对象
        /// </summary>
        protected ConcurrentDictionary<object, byte> DeletedCollection
        {
            get { return this.deletedCollection; }
        }

        #endregion

        #region IRepositoryContext Members

        /// <summary>
        /// 获取此仓储上下文的唯一身份标识符
        /// </summary>
        public Guid ID
        {
            get { return this.id; }
        }

        /// <summary>
        /// 注册一个新的实例到仓储上下文。
        /// </summary>
        /// <param name="obj">要添加的对象</param>
        public virtual void RegisterNew(object obj)
        {
            this.newCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);
            this.committed = false;
        }

        /// <summary>
        /// 注册一个要修改的实例到仓储上下文。
        /// 若该实例已被注册为删除，会抛出异常。
        /// </summary>
        /// <param name="obj">要修改的对象</param>
        public virtual void RegisterModified(object obj)
        {
            if (deletedCollection.ContainsKey(obj))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!modifiedCollection.ContainsKey(obj) && !(newCollection.ContainsKey(obj)))
                modifiedCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);

            this.committed = false;
        }

        /// <summary>
        /// 注册一个要删除的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要删除的对象</param>
        public virtual void RegisterDeleted(object obj)
        {
            byte @byte;
            if (newCollection.ContainsKey(obj))
            {
                newCollection.TryRemove(obj, out @byte);
                return;
            }

            if (modifiedCollection.ContainsKey(obj))
            {
                modifiedCollection.TryRemove(obj, out @byte);
                return;
            }

            if (!deletedCollection.ContainsKey(obj))
            {
                deletedCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);
                this.committed = true;
            }
        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// 获得一个<see cref="System.Boolean"/>值，该值表示当前的Unit Of Work是否支持Microsoft分布式事务处理机制。
        /// </summary>
        public abstract bool DistributedTransactionSupported { get; }

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示是否工作单元已提交
        /// </summary>
        public bool Committed
        {
            get { return this.committed; }
            protected set { this.committed = value; }
        }

        /// <summary>
        /// 提交工作单元
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// 异步提交工作单元
        /// </summary>
        /// <returns>Task</returns>
        public async Task CommitAsync()
        {
            await this.CommitAsync(CancellationToken.None);
        }

        /// <summary>
        /// 异步提交工作单元
        /// </summary>
        /// <param name="cancellationToken">取消操作</param>
        /// <returns>Task</returns>
        public abstract Task CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 回滚工作单元
        /// </summary>
        public abstract void Rollback();

        #endregion
    }
}
