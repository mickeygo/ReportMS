namespace ReportMS.Framework.Commands
{
    /// <summary>
    /// 表示实现的类是命令处理类
    /// </summary>
    /// <typeparam name="TCommand">要处理的命令的类型</typeparam>
    public interface ICommandHandler<TCommand> : IHandler<TCommand> 
        where TCommand : ICommand
    {

    }
}
