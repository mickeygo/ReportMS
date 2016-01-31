using System.Text;
using System.Web.Mvc;
using Gear.Infrastructure.Authentication;
using Gear.Infrastructure.Web.Authorization;
using Gear.Infrastructure.Web.Json;
using Gear.Infrastructure.Web.Membership;
using Gear.Infrastructure.Web.MultiTenancy;
using Gear.Infrastructure.Web.Outputs;

namespace Gear.Infrastructure.Web.Controllers
{
    /// <summary>
    /// ASP.NET MVC 控制器基类
    /// </summary>
    public abstract class GearController : Controller
    {
        #region

        private IAuthentication _authentication;

        #endregion

        #region Public Properties

        /// <summary>
        /// 获取租户名称
        /// </summary>
        public string TenantName
        {
            get { return this.GetTenantName(); }
        }

        /// <summary>
        /// 当前登录人员是否是系统管理者成员
        /// </summary>
        public bool IsAdministrator
        {
            get { return MemberManager.IsCurrentLoginUserInAdministrator(); }
        }

        /// <summary>
        /// 获取当前登录人员信息。包括当前的验证信息
        /// </summary>
        public IAuthentication LoginUser
        {
            get
            {
                return this._authentication ?? (this._authentication = OwinAuthenticationManager.OwinAuthentication);
            }
        }

        /// <summary>
        /// 导出文件, 通过 Http Response OutPutStream 流中文件输出
        /// </summary>
        public IOutput Output
        {
            get { return new MvcFileOutput(this.ControllerContext); }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 基于 Newtonsoft 的 Json 序列化
        /// </summary>
        /// <param name="data">要序列化的数据</param>
        /// <returns>基于 Newtonsoft 的 JsonResult</returns>
        protected NewtonsoftJsonResult NewtonsoftJson(object data)
        {
            return this.NewtonsoftJson(data, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// 基于 Newtonsoft 的 Json 序列化
        /// </summary>
        /// <param name="data">要序列化的数据</param>
        /// <param name="behavior">请求 Json 序列化的行为</param>
        /// <returns>基于 Newtonsoft 的 JsonResult</returns>
        protected NewtonsoftJsonResult NewtonsoftJson(object data, JsonRequestBehavior behavior)
        {
            return this.NewtonsoftJson(data, null, behavior);
        }

        /// <summary>
        /// 基于 Newtonsoft 的 Json 序列化
        /// </summary>
        /// <param name="data">要序列化的数据</param>
        /// <param name="contentType">输出响应的 Context MIME</param>
        /// <param name="behavior">请求 Json 序列化的行为</param>
        /// <returns>基于 Newtonsoft 的 JsonResult</returns>
        protected NewtonsoftJsonResult NewtonsoftJson(object data, string contentType, JsonRequestBehavior behavior)
        {
            return this.NewtonsoftJson(data, contentType, null, behavior);
        }

        /// <summary>
        /// 基于 Newtonsoft 的 Json 序列化
        /// </summary>
        /// <param name="data">要序列化的数据</param>
        /// <param name="contentType">输出响应的 Context MIME</param>
        /// <param name="contentEncoding">序列化后的内容编码</param>
        /// <param name="behavior">请求 Json 序列化的行为</param>
        /// <returns>基于 Newtonsoft 的 JsonResult</returns>
        protected NewtonsoftJsonResult NewtonsoftJson(object data, string contentType,
            Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return this.JsonSerialize(data, contentType, contentEncoding, behavior);
        }

        #endregion

        #region Private Methods

        private string GetTenantName()
        {
            var tennant = new TenantManager(this.RouteData);
            return tennant.GetCurrentTenant();
        }

        #endregion
    }
}
