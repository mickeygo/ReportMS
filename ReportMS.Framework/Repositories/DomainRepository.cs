using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ReportMS.Framework.Repositories
{
    /// <summary>
    /// 领域仓储基类
    /// </summary>
    public abstract class DomainRepository : DisposableObject, IDomainRepository
    {
        #region Private Fields
        private readonly HashSet<ISourcedAggregateRoot> saveHash = new HashSet<ISourcedAggregateRoot>();
        private readonly Action<ISourcedAggregateRoot> delegatedUpdateAndClearAggregateRoot = ar =>
        {
            ar.GetType().GetMethod(SourcedAggregateRoot.UpdateVersionAndClearUncommittedEventsMethodName,
                BindingFlags.NonPublic | BindingFlags.Instance).Invoke(ar, null);
        };
        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取存储的聚合根集合
        /// </summary>
        protected HashSet<ISourcedAggregateRoot> SaveHash
        {
            get { return this.saveHash; }
        }

        /// <summary>
        /// 获取更新聚合根版本并清除未提交的聚合事件的方法
        /// </summary>
        protected Action<ISourcedAggregateRoot> DelegatedUpdateAndClearAggregateRoot
        {
            get { return this.delegatedUpdateAndClearAggregateRoot; }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 提交在领域仓储中注册的更改项
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task DoCommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            
        }

        /// <summary>
        /// 创建聚合根实例，被创建的聚合根必须含有默认构造函数
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
        /// <returns></returns>
        protected TAggregateRoot CreateAggregateRootInstance<TAggregateRoot>()
            where TAggregateRoot : class, ISourcedAggregateRoot
        {
            var aggregateRootType = typeof(TAggregateRoot);
            var constructor = aggregateRootType
                .GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(p =>
                {
                    var parameters = p.GetParameters();
                    return !parameters.Any();
                }).FirstOrDefault();

            if (constructor != null)
                return constructor.Invoke(null) as TAggregateRoot;

            throw new RepositoryException("At least one parameterless constructor should be defined on the aggregate root type '{0}'.", typeof(TAggregateRoot));
        }

        #endregion

        #region IDomainRepository Members

        /// <summary>
        /// 获取指定的聚合根实例
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
        /// <param name="aggregateRootId">要获取的聚合根 ID</param>
        /// <returns></returns>
        public abstract TAggregateRoot Get<TAggregateRoot>(Guid aggregateRootId)
            where TAggregateRoot : class, ISourcedAggregateRoot;

        /// <summary>
        /// 保存指定聚合根中的聚合对象
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合根类型</typeparam>
        /// <param name="aggregateRoot">聚合根对象</param>
        public virtual void Save<TAggregateRoot>(TAggregateRoot aggregateRoot) 
            where TAggregateRoot : class, ISourcedAggregateRoot
        {
            if (!this.saveHash.Contains(aggregateRoot))
                this.saveHash.Add(aggregateRoot);
            this.Committed = false;
        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// 获取工作单元是否支持分布式事务（MS-DTC）
        /// </summary>
        public abstract bool DistributedTransactionSupported { get; }

        /// <summary>
        /// 获取事务工作单元是否已成功提交
        /// </summary>
        public bool Committed { get; protected set; }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            Task.WaitAll(CommitAsync());
        }

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns>取消通知操作</returns>
        public Task CommitAsync()
        {
            return this.CommitAsync(CancellationToken.None);
        }

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <param name="cancellationToken">取消通知操作</param>
        /// <returns></returns>
        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await this.DoCommitAsync(cancellationToken);
            this.saveHash.ToList().ForEach(this.delegatedUpdateAndClearAggregateRoot);
            this.saveHash.Clear();
            this.Committed = true;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public abstract void Rollback();

        #endregion
    }
}
