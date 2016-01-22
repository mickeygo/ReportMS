using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportMS.Framework.Storage.Builders
{
    /// <summary>
    /// WHERE 语句建制结果
    /// </summary>
    public sealed class WhereClauseBuildResult
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置生成的 WHERE 子句
        /// </summary>
        public string WhereClause { get; set; }

        /// <summary>
        /// 获取或设置包含参数和参数值映射关系的<c>Dictionary&lt;string, object&gt;</c>实例
        /// </summary>
        public Dictionary<string, object> ParameterValues { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// 初始化<c>WhereClauseBuildResult</c>实例
        /// </summary>
        public WhereClauseBuildResult() { }

        /// <summary>
        /// 初始化<c>WhereClauseBuildResult</c>实例
        /// </summary>
        /// <param name="whereClause">生成的 WHERE 子句</param>
        /// <param name="parameterValues">包含参数和参数值映射关系的<c>Dictionary&lt;string, object&gt;</c>实例</param>
        public WhereClauseBuildResult(string whereClause, Dictionary<string, object> parameterValues)
        {
            this.WhereClause = whereClause;
            this.ParameterValues = parameterValues;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 返回 WHERE 子句建制结果的内容
        /// </summary>
        /// <returns>WHERE 子句建制结果的内容 <c>System.String</c> 对象</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(WhereClause);
            sb.Append(Environment.NewLine);
            this.ParameterValues.ToList().ForEach(kvp =>
            {
                sb.Append(string.Format("{0} = [{1}] (Type: {2})", kvp.Key, kvp.Value.ToString(),
                    kvp.Value.GetType().FullName));
                sb.Append(Environment.NewLine);
            });
            return sb.ToString();
        }

        #endregion
    }
}
