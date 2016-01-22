using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ReportMS.Test.Common
{
    /// <summary>
    /// 测试工具类
    /// </summary>
    public static class Utils
    {
        public static string LookupEntityDetail<T>(T obj)
        {
            var sb = new StringBuilder();
            foreach (var prop in TypeDescriptor.GetProperties(typeof(T)).Cast<PropertyDescriptor>())
            {
                sb.AppendLine(string.Format("{0}-{1};", prop.Name, prop.GetValue(obj)));
            }
            return sb.ToString();
        }

        public static string LookupEntityDetail<T>(IEnumerable<T> objs)
        {
            var sb = new StringBuilder();
            foreach (var obj in objs)
            {
                sb.AppendLine(LookupEntityDetail(obj));
            }
            return sb.ToString();
        }

    }
}
