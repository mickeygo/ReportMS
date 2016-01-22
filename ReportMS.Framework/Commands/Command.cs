using System;

namespace ReportMS.Framework.Commands
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class Command : ICommand
    {
        #region Ctor

        /// <summary>
        /// 初始化一个新的<c>Command</c>实例
        /// </summary>
        protected Command()
        { }

        /// <summary>
        /// 初始化一个新的<c>Command</c>实例
        /// </summary>
        /// <param name="id">表示 Command 的标识符</param>
        protected Command(Guid id)
        {
            this.ID = id;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 重写，获取当前对象的 HashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Utils.GetHashCode(this.ID.GetHashCode());
        }

        /// <summary>
        /// 重写，判断当前对象与指定的对象是否是相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var command = obj as Command;
            if (command == null)
                return false;

            return this.ID == command.ID;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置命令的标识符
        /// </summary>
        public Guid ID { get; set; }

        #endregion
    }
}
