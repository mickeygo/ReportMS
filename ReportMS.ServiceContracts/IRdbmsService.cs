using System;
using System.Collections.Generic;
using System.ServiceModel;
using Gear.Infrastructure.Services.ApplicationServices;
using ReportMS.DataTransferObjects;
using ReportMS.DataTransferObjects.Dtos;

namespace ReportMS.ServiceContracts
{
    /// <summary>
    /// 表示实现此接口的类为关系型数据库应用服务
    /// </summary>
    [ServiceContract]
    public interface IRdbmsService : IApplicationService
    {
        /// <summary>
        /// 查找所有的有效的关系型数据库
        /// </summary>
        /// <returns>关系型数据库 Dto 集合</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        IEnumerable<RdbmsDto> FindAllRdbms();

        /// <summary>
        /// 查找指定的关系型数据库
        /// </summary>
        /// <param name="rdbmsId">关系型数据库 Id</param>
        /// <returns>关系型数据库</returns>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        RdbmsDto FindRdbms(Guid rdbmsId);

        /// <summary>
        /// 创建关系型数据库.
        /// 对 用户名 和 密码 会采用 AES 方式加密
        /// </summary>
        /// <param name="rdbmsDto">关系型数据库 Dto</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void CreateRdbms(RdbmsDto rdbmsDto);

        /// <summary>
        /// 更新关系型数据库.
        /// 对 用户名 和 密码 会采用 AES 方式加密
        /// </summary>
        /// <param name="rdbmsDto">关系型数据库 Dto</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void UpdateRdbms(RdbmsDto rdbmsDto);

        /// <summary>
        /// 移除指定关系型数据库
        /// </summary>
        /// <param name="rdbmsId">要移除的关系型数据库 Id</param>
        [OperationContract]
        [FaultContract(typeof(FaultData))]
        void RemoveRdbms(Guid rdbmsId);
    }
}
