using System.ServiceModel;
using Gear.Infrastructure.Services.ApplicationServices;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为用户查询应用服务
    /// </summary>
    [ServiceContract]
    public interface IUserQueryService : IApplicationQueryService
    {
        /// <summary>
        /// 查找拥有指定用户名的用户信息
        /// </summary>
        /// <param name="userName">要查询的用户名</param>
        /// <returns>用户 Dto 对象</returns>
        [OperationContract]
        [FaultContract(typeof (FaultData))]
        UserDto Find(string userName);
    }
}
