using System.IO;
using System.Xml.Serialization;

namespace ReportMS.Framework.Serialization
{
    /// <summary>
    /// Xml 序列器
    /// </summary>
    public class ObjectXmlSerializer : IObjectSerializer
    {
        #region IObjectSerializer Members

        /// <summary>
        /// 将对象序列化为字节
        /// </summary>
        /// <typeparam name="TObject">要序列化的对象类型</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>序列化后的字节</returns>
        public virtual byte[] Serialize<TObject>(TObject obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(TObject));
            using (var ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 将字节流反序列化为对象
        /// </summary>
        /// <typeparam name="TObject">要反序列化的对象类型</typeparam>
        /// <param name="stream">要反序列化的流字节</param>
        /// <returns>要反序列化的对象实例</returns>
        public virtual TObject Deserialize<TObject>(byte[] stream)
        {
            var xmlSerializer = new XmlSerializer(typeof(TObject));
            using (var ms = new MemoryStream(stream))
            {
                return (TObject)xmlSerializer.Deserialize(ms);
            }
        }

        #endregion
    }
}
