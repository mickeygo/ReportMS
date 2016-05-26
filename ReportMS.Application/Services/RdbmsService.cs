using System;
using System.Collections.Generic;
using System.Linq;
using ReportMS.DataTransferObjects.Dtos;
using ReportMS.Domain.Models.ReportModule.RdbmsAggregate;
using ReportMS.Domain.Repositories;
using ReportMS.ServiceContracts;

namespace ReportMS.Application.Services
{
    /// <summary>
    /// 关系型数据库应用服务
    /// </summary>
    public class RdbmsService : IRdbmsService
    {
        private readonly IRdbmsRepository _rdbmsRepository;

        public RdbmsService(IRdbmsRepository rdbmsRepository)
        {
            _rdbmsRepository = rdbmsRepository;
        }

        #region IRdbmsService Members

        public IEnumerable<RdbmsDto> FindAllRdbms()
        {
            var rdbmses = this._rdbmsRepository.FindAll().ToList();
            rdbmses.ForEach(s =>
                s.DecryptUserIdAndPwd()
            );

            return rdbmses.MapAs<RdbmsDto>();
        }

        public RdbmsDto FindRdbms(Guid rdbmsId)
        {
            var rdbms = this._rdbmsRepository.GetByKey(rdbmsId);
            rdbms.DecryptUserIdAndPwd();

            return rdbms.MapAs<RdbmsDto>();
        }

        public void CreateRdbms(RdbmsDto rdbmsDto)
        {
            // Todo: 验证 DB 是否能链接

            var rdbms = new Rdbms(rdbmsDto.Name, rdbmsDto.Description, rdbmsDto.Server, rdbmsDto.Catalog,
                rdbmsDto.UserId, rdbmsDto.Password, rdbmsDto.ReadOnly, rdbmsDto.Provider);
            
            this._rdbmsRepository.Add(rdbms);
        }

        public void UpdateRdbms(RdbmsDto rdbmsDto)
        {
            var rdbms = this._rdbmsRepository.GetByKey(rdbmsDto.ID);
            if (rdbms == null)
                return;

            // Todo: 验证 DB 是否能链接

            rdbms.Update(rdbmsDto.Name, rdbmsDto.Description, rdbmsDto.Server, rdbmsDto.Catalog,
                rdbmsDto.UserId, rdbmsDto.Password, rdbmsDto.ReadOnly, rdbmsDto.Provider);

            this._rdbmsRepository.Update(rdbms);
        }

        public void RemoveRdbms(Guid rdbmsId)
        {
            var rdbms = this._rdbmsRepository.GetByKey(rdbmsId);
            if (rdbms == null)
                return;

            rdbms.Disable();
            this._rdbmsRepository.Update(rdbms);
        }

        #endregion

        #region Private Methods

        private bool CheckDatabaseConnect(string server, string catalog, string userId, string password,
            string provider)
        {
            return true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._rdbmsRepository != null)
                this._rdbmsRepository.Context.Dispose();
        }

        #endregion
    }
}
