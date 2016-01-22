using System;
using System.Collections;
using System.Collections.Generic;

namespace ReportMS.Framework
{
    /// <summary>
    /// 表示一个包含一个特定页面的整个对象集的一组对象的集合
    /// </summary>
    public class PagedResult<T> : ICollection<T>
    {
        /// <summary>
        /// 获取或设置记录总数目
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// 获取或设置有效的页面总数目
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 获取或设置每个页面的记录数目
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 获取或设置页数
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 获取数据源
        /// </summary>
        public IList<T> Data { get; private set; }

        /// <summary>
        /// 初始化实例
        /// </summary>
        public PagedResult()
        {
            this.Data = new List<T>();
        }

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="totalRecords">总记录数</param>
        /// <param name="totalPages">总页面数据</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageNumber">当前页面数</param>
        /// <param name="data">数据集</param>
        public PagedResult(int totalRecords, int totalPages, int pageSize, int pageNumber, IList<T> data)
        {
            this.TotalRecords = totalRecords;
            this.TotalPages = totalPages;
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data = data;
        }

        #region ICollection<T> Members

        /// <summary>
        /// 添加数据集
        /// </summary>
        /// <param name="item">数据</param>
        public void Add(T item)
        {
            this.Data.Add(item);
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            this.Data.Clear();
        }

        /// <summary>
        /// 是否包含一个指定项
        /// </summary>
        /// <param name="item">包含的指定对象</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return this.Data.Contains(item);
        }

        /// <summary>
        /// 复杂集合中的元素到数组
        /// </summary>
        /// <param name="array">要储存数据的一维数组</param>
        /// <param name="arrayIndex">基于 0 索引的复制的起始位置</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Data.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 获取数据集中元素数目
        /// </summary>
        public int Count
        {
            get { return this.Data.Count; }
        }

        /// <summary>
        /// 获取一个值，表示集合是否是只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return this.Data.IsReadOnly; }
        }

        /// <summary>
        /// 从集合中移除一个指定的对象
        /// </summary>
        /// <param name="item">要移除的对象</param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return this.Data.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// 获取集合迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// 获取集合迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }

        #endregion
    }
}
