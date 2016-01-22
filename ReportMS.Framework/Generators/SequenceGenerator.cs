namespace ReportMS.Framework.Generators
{
    /// <summary>
    /// 队列生成器
    /// </summary>
    public class SequenceGenerator : ISequenceGenerator
    {
        #region Private Fields
        private static readonly SequenceGenerator instance = new SequenceGenerator();
        private readonly ISequenceGenerator generator;
        #endregion

        #region Ctor

        private SequenceGenerator()
        {
            this.generator = new SequentialIdentityGenerator();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// 获取<c>SequenceGenerator</c>实例对象.
        /// </summary>
        public static SequenceGenerator Instance
        {
            get { return instance; }
        }
        #endregion

        #region ISequenceGenerator Members
        /// <summary>
        /// 获取下一个队列
        /// </summary>
        public object Next
        {
            get { return generator.Next; }
        }

        #endregion
    }
}
