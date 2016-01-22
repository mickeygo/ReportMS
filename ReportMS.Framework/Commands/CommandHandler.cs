using ReportMS.Framework.Application;
using ReportMS.Framework.Repositories;

namespace ReportMS.Framework.Commands
{
    /// <summary>
    /// 表示命令处理者
    /// </summary>
    /// <typeparam name="TCommand">要处理的命令类型</typeparam>
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
         where TCommand : ICommand
    {
        #region IHandler<TCommand> Members

        /// <summary>
        /// 处理指定消息
        /// </summary>
        /// <param name="command">要处理的命令</param>
        public abstract void Handle(TCommand command);

        #endregion

        #region Protected Properties

        /// <summary>
        /// 获取领域仓储的实例，该仓储能当前命令处理者用于执行仓储操作
        /// </summary>
        protected virtual IDomainRepository DomainRepository
        {
            get { return AppRuntime.Instance.CurrentApplication.ObjectContainer.Resolve<IDomainRepository>(); }
        }

        #endregion
    }
}
