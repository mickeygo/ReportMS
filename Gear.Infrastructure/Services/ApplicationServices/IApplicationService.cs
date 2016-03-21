using System;

namespace Gear.Infrastructure.Services.ApplicationServices
{
    /// <summary>
    /// 表示实现该接口的类型为应用层服务 Application Service Contract
    /// </summary>
    public interface IApplicationService : IApplicationQueryService, IDisposable
    {
    }
}
