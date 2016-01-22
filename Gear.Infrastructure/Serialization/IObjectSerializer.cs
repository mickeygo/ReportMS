namespace Gear.Infrastructure.Serialization
{
    /// <summary>
    /// 表示实现类为对象序列器
    /// </summary>
    public interface IObjectSerializer
    {
        /// <summary>
        /// 将对象序列化为字节
        /// </summary>
        /// <typeparam name="TObject">要序列化的对象类型</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>序列化后的字节</returns>
        byte[] Serialize<TObject>(TObject obj);

        /// <summary>
        /// 将字节流反序列化为对象
        /// </summary>
        /// <typeparam name="TObject">要反序列化的对象类型</typeparam>
        /// <param name="stream">要反序列化的流字节</param>
        /// <returns>要反序列化的对象实例</returns>
        TObject Deserialize<TObject>(byte[] stream);
    }
}
