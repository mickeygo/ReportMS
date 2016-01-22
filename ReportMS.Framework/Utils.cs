using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ReportMS.Framework.Serialization;

namespace ReportMS.Framework
{
    /// <summary>
    /// 表示框架的工具集
    /// </summary>
    public static class Utils
    {
        #region Private Constants
        private const int InitialPrime = 23;
        private const int FactorPrime = 29;
        #endregion

        /// <summary>
        /// 获取对象的 HashCode，基于该对象的属性属性集合的
        /// </summary>
        /// <param name="hashCodesForProperties">对象的每个属性的 HashCode 集合</param>
        /// <returns></returns>
        public static int GetHashCode(params int[] hashCodesForProperties)
        {
            unchecked
            {
                return hashCodesForProperties.Aggregate(InitialPrime, (current, code) => current*FactorPrime + code);
            }
        }

        /// <summary>
        /// 生成指定长度的唯一标识<see cref="System.String"/>值
        /// </summary>
        /// <param name="length">要生成的标识长度</param>
        /// <returns>唯一标识的<see cref="System.String"/>值</returns>
        public static string GetUniqueIdentifier(int length)
        {
            while (true)
            {
                var maxSize = length;
                var a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                var chars = a.ToCharArray();
                var size = maxSize;
                var data = new byte[1];
                using (var crypto = new RNGCryptoServiceProvider())
                {
                    crypto.GetNonZeroBytes(data);
                    data = new byte[size];
                    crypto.GetNonZeroBytes(data);
                }

                var result = new StringBuilder(size);
                foreach (var b in data)
                {
                    result.Append(chars[b%(chars.Length - 1)]);
                }

                // Unique identifiers cannot begin with 0-9
                if (result[0] >= '0' && result[0] <= '9')
                    continue;

                return result.ToString();
            }
        }

        #region Extension

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="serializer">序列器</param>
        /// <param name="type">要反序列化的对象类型</param>
        /// <param name="stream">要反序列化的字节流</param>
        /// <returns>反序列化的对象实例</returns>
        public static object Deserialize(this IObjectSerializer serializer, Type type, byte[] stream)
        {
            var deserializeMethodInfo = serializer.GetType().GetMethod("Deserialize");
            return deserializeMethodInfo.MakeGenericMethod(type).Invoke(serializer, new object[] { stream });
        }

        #endregion
    }
}
