using System;
using System.Collections.Generic;
using System.Data;
using AutoMapper;
using AutoMapper.Mappers;

namespace Gear.Utility.Adapters
{
    /// <summary>
    /// 基于 AutoMapper 组件的适配器
    /// </summary>
    public static class AutoMapperAdapter
    {
        /// <summary>
        /// 适配器初始化
        /// </summary>
        /// <param name="action">初始化配置方法</param>
        public static void Initialize(Action<IConfiguration> action)
        {
            Mapper.Initialize(action);
        }

        static AutoMapperAdapter()
        {
            MapperRegistry.Mappers.Add(new DataReaderMapper());
        }

        /// <summary>
        /// 创建 <typeparam name="TSource" /> 到 <typeparam name="TTarget" /> 的映射关系
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <returns><c>IMappingExpression[TSource, TTarget]</c></returns>
        public static IMappingExpression<TSource, TTarget> Register<TSource, TTarget>()
        {
            return Mapper.CreateMap<TSource, TTarget>();
        }

        /// <summary>
        /// 创建 IDataReader 到 <typeparam name="TTarget" /> 的映射关系
        /// <typeparam name="TTarget" /> 不需要设为集合或迭代类型
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        public static void RegisterDataReader<TTarget>()
        {
            Mapper.CreateMap<IDataReader, TTarget>();
        }

        /// <summary>
        /// 清空所有的配置信息
        /// </summary>
        public static void Reset()
        {
            Mapper.Reset();
        }

        /// <summary>
        /// 将源对象实例适配到一个新的<typeparam name="TTarget"/>类型实例
        /// </summary>
        /// <typeparam name="TSource">源实体类型</typeparam>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="source">要速配的对象实例</param>
        /// <returns><paramref name="source"/> 映射到 <typeparamref name="TTarget"/></returns>
        public static TTarget Adapt<TSource, TTarget>(TSource source) 
            where TSource : class where TTarget : class, new()
        {
            return Mapper.Map<TTarget>(source);
        }

        /// <summary>
        /// 源对象实例适配到一个新的<typeparam name="TTarget"/>类型实例
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="source">源实体类型</param>
        /// <returns><paramref name="source"/> 映射到 <typeparamref name="TTarget"/></returns>
        public static TTarget Adapt<TTarget>(object source)
           where TTarget : class, new()
        {
            return Mapper.Map<TTarget>(source);
        }

        /// <summary>
        /// 源对象实例适配到一个新的<typeparam name="TTarget"/>类型实例
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="source">源实体类型</param>
        /// <returns><paramref name="source"/> 映射到 <typeparamref name="TTarget"/>集合</returns>
        public static IEnumerable<TTarget> AdaptToEnumerable<TTarget>(object source)
           where TTarget : class, new()
        {
            return Mapper.Map<IEnumerable<TTarget>>(source);
        }

        /// <summary>
        /// 源对象实例适配到一个新的<typeparam name="TTarget"/>类型实例
        /// </summary>
        /// <typeparam name="TTarget">目标实体类型</typeparam>
        /// <param name="source">源实体类型</param>
        /// <returns><paramref name="source"/> 映射到 <typeparamref name="TTarget"/>集合</returns>
        public static List<TTarget> AdaptToList<TTarget>(object source)
           where TTarget : class, new()
        {
            return Mapper.Map<List<TTarget>>(source);
        }
    }
}
