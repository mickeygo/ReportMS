using System;

namespace ReportMS.Framework.Generators
{
    /// <summary>
    /// 表示身份标识生成器
    /// </summary>
    public sealed class IdentityGenerator : IIdentityGenerator
    {
        #region Private Fields

        private static readonly IdentityGenerator instance = new IdentityGenerator();
        private readonly IIdentityGenerator generator;

        #endregion

        #region Ctor

        private IdentityGenerator()
        {
            this.generator = new SequentialIdentityGenerator();
        }

        #endregion
        /// <summary>
        ///  获取<c>IdentityGenerator</c>实例对象.
        /// </summary>
        public static IdentityGenerator Instance
        {
            get { return instance; }
        }

        #region IIdentityGenerator Members

        /// <summary>
        /// 生成身份标识
        /// </summary>
        /// <returns></returns>
        public object Generate()
        {
            return generator.Generate();
        }

        #endregion
    }
}
