using System;
using System.Collections.Generic;
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

        private readonly ThreadLocal<Dictionary<Guid, object>> localNewCollection =
            new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());

        private readonly ThreadLocal<Dictionary<Guid, object>> localModifiedCollection =
            new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());

        private readonly ThreadLocal<Dictionary<Guid, object>> localDeletedCollection =
            new ThreadLocal<Dictionary<Guid, object>>(() => new Dictionary<Guid, object>());

        private readonly ThreadLocal<bool> localCommitted = new ThreadLocal<bool>(() => true);

        #endregion

        #region Protected Methods

        /// <summary>
        /// 清除仓储上下文中所有的注册信息
        /// </summary>
        /// <remarks>注:仅能在仓储上下文被成功提交后才能调用此方法</remarks>
        protected void ClearRegistrations()
        {
            this.localNewCollection.Value.Clear();
            this.localModifiedCollection.Value.Clear();
            this.localDeletedCollection.Value.Clear();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"><see cref="System.Boolean"/>是否需要显示地释放资料</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.localCommitted.Dispose();
                this.localDeletedCollection.Dispose();
                this.localModifiedCollection.Dispose();
                this.localNewCollection.Dispose();
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取一个枚举器，包含所有的要添加到仓储上下文中的对象
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> NewCollection
        {
            get { return localNewCollection.Value; }
        }

        /// <summary>
        /// 获取一个枚举器，包含所有的在仓储上下文中需要修改的对象
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> ModifiedCollection
        {
            get { return localModifiedCollection.Value; }
        }

        /// <summary>
        /// 获取一个枚举器，包含所有的在仓储上下文中需要删除的对象
        /// </summary>
        protected IEnumerable<KeyValuePair<Guid, object>> DeletedCollection
        {
            get { return localDeletedCollection.Value; }
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
        /// 注册一个新的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要添加的对象</param>
        public virtual void RegisterNew<TAggregateRoot>(TAggregateRoot obj) where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");
            if (this.localModifiedCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException(
                    "The object cannot be registered as a new object since it was marked as modified.");
            if (this.localNewCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException("The object has already been registered as a new object.");

            this.localNewCollection.Value.Add(obj.ID, obj);
            this.localCommitted.Value = false;
        }

        /// <summary>
        /// 注册一个要修改的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要修改的对象</param>
        public virtual void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");
            if (this.localDeletedCollection.Value.ContainsKey(obj.ID))
                throw new InvalidOperationException(
                    "The object cannot be registered as a modified object since it was marked as deleted.");

            if (!this.localModifiedCollection.Value.ContainsKey(obj.ID)
                && !this.localNewCollection.Value.ContainsKey(obj.ID))
            {
                this.localModifiedCollection.Value.Add(obj.ID, obj);
            }

            this.localCommitted.Value = false;
        }

        /// <summary>
        /// 注册一个要删除的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要删除的对象</param>
        public virtual void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
            where TAggregateRoot : class, IAggregateRoot
        {
            if (obj.ID.Equals(Guid.Empty))
                throw new ArgumentException("The ID of the object is empty.", "obj");

            if (this.localNewCollection.Value.ContainsKey(obj.ID))
            {
                if (localNewCollection.Value.Remove(obj.ID))
                    return;
            }

            var removedFromModified = this.localModifiedCollection.Value.Remove(obj.ID);
            var addedToDeleted = false;
            if (!this.localDeletedCollection.Value.ContainsKey(obj.ID))
            {
                this.localDeletedCollection.Value.Add(obj.ID, obj);
                addedToDeleted = true;
            }
            this.localCommitted.Value = !(removedFromModified || addedToDeleted);
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
            get { return this.localCommitted.Value; }
            protected set { this.localCommitted.Value = value; }
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
