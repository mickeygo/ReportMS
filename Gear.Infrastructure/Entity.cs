using System;
using Gear.Infrastructure.Generators;
using Gear.Infrastructure.Utility;

namespace Gear.Infrastructure
{
    /// <summary>
    /// 领域实体基类
    /// </summary>
    /// <typeparam name="TKey">实体对象标识类型</typeparam>
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        #region IEntity<TKey> Members

        public TKey ID { get; set; }

        #endregion

        #region override

        /// <summary>
        /// 重写 Equals 方法，比较当前对象和另一对象是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as IEntity<TKey>;
            if (other == null)
                return false;

            return this.ID.Equals(other.ID);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(this.ID.GetHashCode());
        }

        #endregion
    }

    /// <summary>
    /// 领域实体基类，主键类型为 Guid
    /// </summary>
    public abstract class Entity : Entity<Guid>, IEntity
    {
        /// <summary>
        /// 检查该实体是否是临时的
        /// </summary>
        /// <returns>True, 表示实体有身份标识；false，表示主体没有身份标识，为临时的</returns>
        public bool IsTransient()
        {
            return this.ID == Guid.Empty;
        }

        /// <summary>
        /// 生成新的 identity
        /// </summary>
        public void GenerateNewIdentity()
        {
            this.ID = (Guid)IdentityGenerator.Instance.Generate();
        }
    }
}
