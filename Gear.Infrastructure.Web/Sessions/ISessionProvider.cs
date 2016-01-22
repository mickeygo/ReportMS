namespace Gear.Infrastructure.Web.Sessions
{
    /// <summary>
    /// 表示实现接口类为 Session 类
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// 获取当前 Session 中指定 name 的值
        /// </summary>
        /// <param name="name">Session 名</param>
        /// <returns>Session 对象</returns>
        object Get(string name);

        /// <summary>
        /// 向当前 Session 中添加指定的值
        /// </summary>
        /// <param name="name">Session 名</param>
        /// <param name="value">Session 值</param>
        void Add(string name, object value);

        /// <summary>
        /// 移除当前 Session 中指定的值
        /// </summary>
        /// <param name="name"></param>
        void Remove(string name);

        /// <summary>
        /// 移除当前 Session 中所有的值
        /// </summary>
        void Clear();
    }
}
