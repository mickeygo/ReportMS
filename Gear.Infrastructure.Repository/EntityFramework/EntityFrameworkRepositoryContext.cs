using System.Data.Entity;
using System.Threading;
using Gear.Infrastructure.Repositories;

namespace Gear.Infrastructure.Repository.EntityFramework
{
    /// <summary>
    /// 表示为 Microsoft EntityFramework 仓储上下文
    /// </summary>
    public class EntityFrameworkRepositoryContext : RepositoryContext, IEntityFrameworkRepositoryContext
    {
        private readonly ThreadLocal<DbContext> localDbContext;

        #region Ctor

        /// <summary>
        /// 初始化一新的<c>EntityFrameworkRepositoryContext</c>实例
        /// </summary>
        /// <param name="context">DataBase 上下文对象，派生自 DbContext</param>
        public EntityFrameworkRepositoryContext(IDbContext context)
        {
            this.localDbContext = new ThreadLocal<DbContext>(() => context.Context);
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// 注册一个新的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要添加的对象</param>
        public override void RegisterNew<TAggregateRoot>(TAggregateRoot obj)
        {
            this.localDbContext.Value.Set<TAggregateRoot>().Add(obj);
            this.Committed = false;
        }

        /// <summary>
        /// 注册一个要修改的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要修改的对象</param>
        public override void RegisterModified<TAggregateRoot>(TAggregateRoot obj)
        {
            this.localDbContext.Value.Entry(obj).State = EntityState.Modified;
            this.Committed = false;
        }

        /// <summary>
        /// 注册一个要删除的实例到仓储上下文
        /// </summary>
        /// <typeparam name="TAggregateRoot">要注册的实例类型</typeparam>
        /// <param name="obj">要删除的对象</param>
        public override void RegisterDeleted<TAggregateRoot>(TAggregateRoot obj)
        {
            this.localDbContext.Value.Set<TAggregateRoot>().Remove(obj);
            this.Committed = false;
        }

        #endregion

        #region IEntityFrameworkRepositoryContext Members

        /// <summary>
        /// 获取 Database 上下文
        /// </summary>
        public DbContext Context
        {
            get { return this.localDbContext.Value; }
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
                this.localDbContext.Value.SaveChanges();
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
        /// 释放资源,释放资源之前，会将没有提交的工作单元提交
        /// </summary>
        /// <param name="disposing"><see cref="System.Boolean"/>是否需要显示地释放资料</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.Committed)
                    this.Commit();

                this.localDbContext.Value.Dispose();
                this.localDbContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
