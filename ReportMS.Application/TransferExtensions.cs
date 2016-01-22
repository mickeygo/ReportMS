using System.Collections.Generic;
using Gear.Infrastructure;
using Gear.Utility.Adapters;

namespace ReportMS.Application
{
    /// <summary>
    /// 对象关系映射(转换)扩展类
    /// </summary>
    public static class TransferExtensions
    {
        /// <summary>
        /// 将实体映射到指定的 Dto 对象。参数为 null，则返回 null
        /// </summary>
        /// <typeparam name="TDto">要映射的 Dto 对象类型</typeparam>
        /// <param name="entity">要映射的实体</param>
        /// <returns><typeparam name="TDto" />对象实例</returns>
        public static TDto MapAs<TDto>(this IEntity entity)
            where TDto : class, new()
        {
            if (entity == null)
                return null;

            return AutoMapperAdapter.Adapt<TDto>(entity);
        }

        /// <summary>
        /// 将实体映射到指定的 Dto 对象。参数为 null，则返回 null
        /// </summary>
        /// <typeparam name="TDto">要映射的 Dto 对象类型</typeparam>
        /// <param name="entities">要映射的实体集</param>
        /// <returns><typeparam name="TDto" />对象实例集</returns>
        public static IEnumerable<TDto> MapAs<TDto>(this IEnumerable<IEntity> entities)
            where TDto : class, new()
        {
            if (entities == null)
                return null;

            return AutoMapperAdapter.AdaptToEnumerable<TDto>(entities);
        }
    }
}
