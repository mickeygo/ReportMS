using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportMS.Web.Client.Helpers
{
    /// <summary>
    /// 仓储参数处理.
    /// 用于将字典集合参数与字符串参数互相转换
    /// </summary>
    public static class StorageParameter
    {
        public const string Concatenation = "&";
        public const string Assignment = "=";

        #region Public Methods

        /// <summary>
        /// 将参数转换为字符串参数
        /// </summary>
        /// <param name="parameters">要转换的参数</param>
        /// <returns>字符串参数</returns>
        public static string ConvertParameterToString(IDictionary<string, object> parameters)
        {
            if (parameters == null)
                return null;

            var pms = (from param in parameters
                       select string.Format("{0}{1}{2}", param.Key, Assignment, param.Value)).AsEnumerable();

            return string.Join(Concatenation, pms);
        }

        /// <summary>
        /// 将字符串参数转换为 IDictionary 类型参数
        /// </summary>
        /// <param name="parameters">要转换的字符串参数</param>
        /// <returns>字典结合参数</returns>
        public static IDictionary<string, object> ConvertStringToParameter(string parameters)
        {
            if (String.IsNullOrWhiteSpace(parameters))
                return null;

            return (from pms in parameters.Split(Concatenation.ToCharArray())
                let p = pms.Split(Assignment.ToCharArray())
                select p).ToDictionary(k => k[0].Trim(), v => (object) v[1].Trim());
        }

        #endregion
    }
}
