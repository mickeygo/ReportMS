using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ReportMS.Framework.Repositories
{
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        #region Private Fields
        private readonly ConcurrentDictionary<object, byte> newCollection = new ConcurrentDictionary<object, byte>();
        private readonly ConcurrentDictionary<object, byte> modifiedCollection = new ConcurrentDictionary<object, byte>();
        private readonly ConcurrentDictionary<object, byte> deletedCollection = new ConcurrentDictionary<object, byte>();
        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取一个集合，包含所有要在仓储中添加的对象
        /// </summary>
        protected ConcurrentDictionary<object, byte> NewCollection
        {
            get { return this.newCollection; }
        }

        /// <summary>
        /// 获取一个集合，包含所有要在仓储中修改的对象
        /// </summary>
        protected ConcurrentDictionary<object, byte> ModifiedCollection
        {
            get { return this.modifiedCollection; }
        }
        /// <summary>
        /// 获取一个集合，包含所有要在仓储中删除的对象
        /// </summary>
        protected ConcurrentDictionary<object, byte> DeletedCollection
        {
            get { return this.deletedCollection; }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 清楚所有的注册对象
        /// </summary>
        protected void ClearRegistrations()
        {
            this.newCollection.Clear();
            this.modifiedCollection.Clear();
            this.deletedCollection.Clear();
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposing">A <see cref="System.Boolean"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.ClearRegistrations();
        }

        #endregion

        #region IRepositoryContext Members

        /// <summary>
        /// 获取聚合仓储上下文的唯一标识
        /// </summary>
        public Guid ID { get; private set; }

        /// <summary>
        /// 注册一个新的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要添加的对象</param>
        public virtual void RegisterNew(object obj)
        {
            newCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);
            this.Committed = false;
        }

        /// <summary>
        /// 注册一个要修改的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要修改的对象</param>
        public void RegisterModified(object obj)
        {
            if (this.deletedCollection.ContainsKey(obj))
                throw new InvalidOperationException("The object cannot be registered as a modified object since it was marked as deleted.");
            if (!this.modifiedCollection.ContainsKey(obj) && !(this.newCollection.ContainsKey(obj)))
                this.modifiedCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);

            this.Committed = false;
        }

        /// <summary>
        /// 注册一个要删除的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要删除的对象</param>
        public void RegisterDelete(object obj)
        {
            byte @byte;
            if (this.newCollection.ContainsKey(obj))
            {
                this.newCollection.TryRemove(obj, out @byte);
                return;
            }
            var removedFromModified = this.modifiedCollection.TryRemove(obj, out @byte);
            var addedToDeleted = false;
            if (!this.deletedCollection.ContainsKey(obj))
            {
                this.deletedCollection.AddOrUpdate(obj, byte.MinValue, (o, b) => byte.MinValue);
                addedToDeleted = true;
            }

            this.Committed = !(removedFromModified || addedToDeleted);
        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// 获取工作单元是否支持分布式事务（MS-DTC）
        /// </summary>
        public virtual bool DistributedTransactionSupported
        {
            get { return false; }
        }

        /// <summary>
        /// 获取事务工作单元是否已成功提交
        /// </summary>
        public virtual bool Committed { get; protected set; }

        /// <summary>
        /// 提交事务
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        public Task CommitAsync()
        {
            return this.CommitAsync(CancellationToken.None);
        }

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <param name="cancellationToken">传递操作被取消的通知</param>
        /// <returns></returns>
        public abstract Task CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 回滚事务
        /// </summary>
        public abstract void Rollback();

        #endregion
    }
}
