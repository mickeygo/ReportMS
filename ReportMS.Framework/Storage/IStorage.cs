using System;
using System.Collections.Generic;
using ReportMS.Framework.Specifications;

namespace ReportMS.Framework.Storage
{
    public interface IStorage : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 获取储存器中第一个对象
        /// </summary>
        /// <typeparam name="T">要获取的对象类型</typeparam>
        /// <returns>对象实例</returns>
        T SelectFirstOnly<T>() where T : class, new();

        /// <summary>
        /// 用给定的规约获取储存器中第一个对象
        /// </summary>
        /// <typeparam name="T">要获取的对象类型</typeparam>
        /// <param name="specification">规约</param>
        /// <returns>对象实例</returns>
        T SelectFirstOnly<T>(ISpecification<T> specification) where T : class, new();

        /// <summary>
        /// 获取储存器中记录数目
        /// </summary>
        /// <typeparam name="T">要获取的对象类型</typeparam>
        /// <returns>储存器中记录数目</returns>
        int GetRecordCount<T>() where T : class, new();

        /// <summary>
        /// 用给定的规约获取储存器中记录数目
        /// </summary>
        /// <typeparam name="T">要获取的对象类型</typeparam>
        /// <param name="specification">规约</param>
        /// <returns>储存器中记录数目</returns>
        int GetRecordCount<T>(ISpecification<T> specification) where T : class, new();

        /// <summary>
        /// 获取存储器中所有对象集合
        /// </summary>
        /// <typeparam name="T">要获取的对象类型</typeparam>
        /// <returns>对象集合</returns>
        IEnumerable<T> Select<T>() where T : class, new();

        /// <summary>
        /// 用给定的规约获取存储器中对象集合
        /// </summary>
        /// <typeparam name="T">要获取的对象类型</typeparam>
        /// <param name="specification">规约</param>
        /// <returns>对象集合</returns>
        IEnumerable<T> Select<T>(ISpecification<T> specification) where T : class, new();

        /// <summary>
        /// 用给定的规约获取存储器中排序的对象集合
        /// </summary>
        /// <typeparam name="T">要获取的对象类型</typeparam>
        /// <param name="specification">规约</param>
        /// <param name="orders"><c>PropertyBag</c> 属性包实例，包含排序字段</param>
        /// <param name="sortOrder">排序方式</param>
        /// <returns>排序后的对象集合</returns>
        IEnumerable<T> Select<T>(ISpecification<T> specification, PropertyBag orders, SortOrder sortOrder)
            where T : class, new();

        /// <summary>
        /// 向存储器中新增对象
        /// </summary>
        /// <typeparam name="T">要新增的对象类型</typeparam>
        /// <param name="allFields"><c>PropertyBag</c> 属性包实例，包含要新增的属性及属性值</param>
        void Insert<T>(PropertyBag allFields) where T : class, new();

        /// <summary>
        /// 从存储器中删除所有对象
        /// </summary>
        /// <typeparam name="T">要删除的对象类型</typeparam>
        void Delete<T>() where T : class, new();

        /// <summary>
        /// 从存储器中删除指定的对象
        /// </summary>
        /// <typeparam name="T">要删除的对象类型</typeparam>
        /// <param name="specification">规约</param>
        void Delete<T>(ISpecification<T> specification) where T : class, new();

        /// <summary>
        /// 用给定的值来更新所有的对象
        /// </summary>
        /// <typeparam name="T">要更新的对象类型</typeparam>
        /// <param name="newValues"><c>PropertyBag</c> 属性包实例，包含要更新的属性及属性值</param>
        void Update<T>(PropertyBag newValues) where T : class, new();

        /// <summary>
        /// 用给定的值和规约来更新所有的对象
        /// </summary>
        /// <typeparam name="T">要更新的对象类型</typeparam>
        /// <param name="newValues"><c>PropertyBag</c> 属性包实例，包含要更新的属性及属性值</param>
        /// <param name="specification">规约</param>
        void Update<T>(PropertyBag newValues, ISpecification<T> specification)
            where T : class, new();
    }
}
