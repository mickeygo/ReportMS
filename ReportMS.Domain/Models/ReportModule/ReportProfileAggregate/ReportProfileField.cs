using System;
using Gear.Infrastructure;

namespace ReportMS.Domain.Models.ReportModule.ReportProfileAggregate
{
    /// <summary>
    /// 报表配置详细的字段信息
    /// </summary>
    public class ReportProfileField : Entity
    {
        #region Properties

        public Guid ReportProfileId { get; private set; }

        public virtual ReportProfile ReportProfile { get; private set; }

        public string FieldName { get; private set; }
        
        #endregion

        #region Ctor

        private ReportProfileField()
        {
        }

        /// <summary>
        /// 初始化一个新的<c>ReportProfileField</c>实例
        /// </summary>
        /// <param name="reportProfileId">隶属的报表配置 Id</param>
        /// <param name="fieldName">要配置的字段名</param>
        public ReportProfileField(Guid reportProfileId, string fieldName)
        {
            ReportProfileId = reportProfileId;
            FieldName = fieldName;

            this.GenerateNewIdentity();
        }

        #endregion
    }
}
