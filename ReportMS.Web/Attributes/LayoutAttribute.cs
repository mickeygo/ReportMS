using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ReportMS.Web.Attributes
{
    /// <summary>
    /// 站点布局特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LayoutAttribute : ActionFilterAttribute
    {
        public LayoutAttribute(Layout layout)
        {
            this.Layout = layout;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var isView = !filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest();
            if (isView)
            {
                var view = (ViewResult) filterContext.Result;
                view.MasterName = WebSiteLayout.GetLayout(this.Layout);
            }
        }

        public Layout Layout { get; set; }
    }

    /// <summary>
    /// 站点布局样式
    /// </summary>
    public enum Layout
    {
        /// <summary>
        /// 没有指定布局，会使用当前页面的布局
        /// </summary>
        None = 0,

        /// <summary>
        /// 不含有 Web Title （应用程序标题）的布局
        /// </summary>
        WithoutWebTitle = 1
    }

    internal static class WebSiteLayout
    {
        private static readonly IDictionary<Layout, string> LayoutContainer = new Dictionary<Layout, string>
        {
            {Layout.None, String.Empty},
            {Layout.WithoutWebTitle, "~/Views/Shared/_Layout_Without_WebTitle.cshtml"}
        };

        public static string GetLayout(Layout layoutFilter)
        {
            return LayoutContainer[layoutFilter];
        }
    }
}