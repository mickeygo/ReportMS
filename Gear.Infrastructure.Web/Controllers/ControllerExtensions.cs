﻿using System;
using System.Text;
using System.Web.Mvc;
using Gear.Infrastructure.Web.Attributes;
using Gear.Infrastructure.Web.Authorization;
using Gear.Infrastructure.Web.Json;

namespace Gear.Infrastructure.Web.Controllers
{
    /// <summary>
    /// ASP.NET MVC Controller 扩展类
    /// </summary>
    public static class ControllerExtensions
    {
        #region Public Methods

        /// <summary>
        /// 用户身份验证
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="filterContext">授权上下文</param>
        /// <returns>true 验证通过，否则为 false</returns>
        public static bool Authenticate(this Controller controller, AuthorizationContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            if (IsAllowAnonymous(controller, actionDescriptor))
                return true;

            var authentication = OwinAuthenticationManager.OwinAuthentication;
            return authentication.Identity.IsAuthenticated;
        }

        /// <summary>
        /// 当前 action / controller 是否允许匿名用户访问
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="actionDescriptor">Action 描述</param>
        /// <returns>True 表示允许匿名访问；否则为 false</returns>
        public static bool IsAllowAnonymous(this Controller controller, ActionDescriptor actionDescriptor)
        {
            return IsDefinedAttribute<AllowAnonymousAttribute>(controller, actionDescriptor);
        }

        /// <summary>
        /// 当前 action / controller 是否允许所有的已认证的用户访问
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="actionDescriptor">Action 描述</param>
        /// <returns>True 表示允许匿名访问；否则为 false</returns>
        public static bool IsAllowAuthenticated(this Controller controller, ActionDescriptor actionDescriptor)
        {
            return IsDefinedAttribute<AllowAuthenticatedAttribute>(controller, actionDescriptor);
        }

        /// <summary>
        /// 是否 action / controller 有定义指定的特性
        /// </summary>
        /// <typeparam name="T">定义的特性类型</typeparam>
        /// <param name="controller">控制器</param>
        /// <param name="actionDescriptor">Action 描述</param>
        /// <param name="inherit">特性是否允许继承，默认为 true</param>
        /// <returns>是否允许继承，默认为 true</returns>
        public static bool IsDefinedAttribute<T>(this Controller controller, ActionDescriptor actionDescriptor,
            bool inherit = true) where T : Attribute
        {
            var controllerDescriptor = actionDescriptor.ControllerDescriptor;

            return actionDescriptor.IsDefined(typeof (T), true)
                   || controllerDescriptor.IsDefined(typeof (T), true);
        }

        /// <summary>
        /// Json 序列化
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="data">要序列化的数据</param>
        /// <param name="contentType">输出响应的 Context MIME</param>
        /// <param name="contentEncoding">序列化后的内容编码</param>
        /// <param name="behavior">请求 Json 序列化的行为</param>
        /// <returns>基于 Newtonsoft 的 JsonResult</returns>
        public static NewtonsoftJsonResult JsonSerialize(this Controller controller, object data, string contentType,
            Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new NewtonsoftJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #endregion
    }
}
