using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
    /// <summary>
    /// Mvc 中 ViewModel 扩展类
    /// </summary>
    public static class MvcViewModelExtensions
    {
        /// <summary>
        /// 是否有数据。
        /// 若对象为 null 或 空，则返回 false，否则返回 true
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="model">ViewModels</param>
        /// <returns>true 表示有数据； false 表示无数据</returns>
        public static bool HasAny<T>(this IEnumerable<T> model)
        {
            return model != null && model.Any();
        }

        /// <summary>
        /// 是否有指定的数据。
        /// 若对象为 null 或 空，则返回 false
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="model">ViewModels</param>
        /// <param name="predicate">判断条件</param>
        /// <returns>true 表示有数据； false 表示无数据</returns>
        public static bool HasAny<T>(this IEnumerable<T> model, Func<T, bool> predicate)
        {
            return model != null && model.Any(predicate);
        }
    }
}
