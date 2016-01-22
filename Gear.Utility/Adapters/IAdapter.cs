namespace Gear.Utility.Adapters
{
    /// <summary>
    /// 表示实现接口类为适配器
    /// </summary>
    public interface IAdapter
    {
        /// <summary>
        /// 将源对象实例适配到一个新的<typeparam name="TTarget"/>类型实例
        /// </summary>
        /// <typeparam name="TSource">源实体类型</typeparam>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="source">要速配的对象实例</param>
        /// <returns><paramref name="source"/> 映射到 <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TSource, TTarget>(TSource source)
            where TTarget : class,new()
            where TSource : class;

        /// <summary>
        /// 源对象实例适配到一个新的<typeparam name="TTarget"/>类型实例
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="source">源实体类型</param>
        /// <returns><paramref name="source"/> 映射到 <typeparamref name="TTarget"/></returns>
        TTarget Adapt<TTarget>(object source)
            where TTarget : class,new();
    }
}
