using System.Data.Entity;
using System.Threading;
using Gear.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Gear.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// 表示为基于 Microsoft EntityFramework 仓储上下文。
    /// 使用 EntityFramework 的 DbContext 上下文管理对象
    /// </summary>
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        #region Private Fields

        // remark: we use the DbContext to replace the ThreadLocal<DbContext> for using the async method.
        private readonly DbContext efContext;
        private readonly object sync = new object();

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化一新的<c>EntityFrameworkRepositoryContext</c>实例
        /// </summary>
        /// <param name="context">DataBase 上下文对象，派生自 DbContext</param>
        public EntityFrameworkRepositoryContext(IDbContext context)
        {
            this.efContext = context.Context;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// 重写，在 EntityFramework 的 DbContext 对象中注册一个新的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要添加的对象</param>
        public override void RegisterNew(object obj)
        {
            this.efContext.Entry(obj).State = EntityState.Added;
            this.Committed = false;
        }

        /// <summary>
        /// 重写，在 EntityFramework 的 DbContext 对象中注册一个要修改的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要修改的对象</param>
        public override void RegisterModified(object obj)
        {
            this.efContext.Entry(obj).State = EntityState.Modified;
            this.Committed = false;
        }

        /// <summary>
        /// 重写，在 EntityFramework 的 DbContext 对象中注册一个要删除的实例到仓储上下文
        /// </summary>
        /// <param name="obj">要删除的对象</param>
        public override void RegisterDeleted(object obj)
        {
            this.efContext.Entry(obj).State = EntityState.Deleted;
            this.Committed = false;
        }

        #endregion

        #region IEntityFrameworkRepositoryContext Members

        /// <summary>
        /// 获取当前仓储上下文所使用的 Entity Framework的<see cref="DbContext"/>实例。
        /// </summary>
        public DbContext Context
        {
            get { return this.efContext; }
        }

        #endregion

        #region Override Uow

        /// <summary>
        /// 获得一个<see cref="System.Boolean"/>值，该值表示当前的Unit Of Work是否支持Microsoft分布式事务处理机制。
        /// </summary>
        public override bool DistributedTransactionSupported
        {
            get { return true; }
        }

        /// <summary>
        /// 提交工作当然
        /// </summary>
        public override void Commit()
        {
            if (!this.Committed)
            {
                lock (sync)
                {
                    this.efContext.SaveChanges();
                }
                this.Committed = true;
            }
        }

        /// <summary>
        /// 异步提交工作单元
        /// </summary>
        /// <param name="cancellationToken">取消操作</param>
        /// <returns>Task</returns>
        public override async Task CommitAsync(CancellationToken cancellationToken)
        {
            if (!this.Committed)
            {
                await this.efContext.SaveChangesAsync(cancellationToken);
                this.Committed = true;
            }
        }

        /// <summary>
        /// 回滚工作单元
        /// </summary>
        public override void Rollback()
        {
            this.Committed = false;
        }

        #endregion

        /// <summary>
        /// 释放资源.
        /// 释放资源之前，若工作单元有注册对象但还没有提交，会提交此工作单元
        /// </summary>
        /// <param name="disposing"><see cref="System.Boolean"/>是否需要显示地释放资源</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Todo: The dispose method should no longer be responsible for the commit handling. 
                // Since the object container might handle the lifetime
                // of the repository context on the WCF per-request basis, users should
                // handle the commit logic by themselves.
                if (!this.Committed)
                    this.Commit();

                this.efContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
