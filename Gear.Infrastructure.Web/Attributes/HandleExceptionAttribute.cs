using System;
using System.Web.Mvc;

namespace Gear.Infrastructure.Web.Attributes
{
    /// <summary>
    /// 异常处理筛选器，处理每个应用请求中的异常行为. 参照 HandleErrorAttribute
    /// 在全局筛选器注册.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class HandleExceptionAttribute : HandleErrorAttribute
    {
        private bool _mailNotification = true;

        public override void OnException(ExceptionContext filterContext)
        {
            if (this.MailNotification)
                this.Mail();

            if (this.LogException)
                this.Log();

            base.OnException(filterContext);
        }

        private void Mail()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Log()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示是否将错误信息通过邮件通知给系统管理员
        /// </summary>
        public bool MailNotification
        {
            get { return this._mailNotification; }
            set { this._mailNotification = value; }
        }

        /// <summary>
        /// 获取一个<see cref="System.Boolean"/>值，表示是否将错误信息记录到文档中
        /// </summary>
        public bool LogException { get; set; }
    }
}
