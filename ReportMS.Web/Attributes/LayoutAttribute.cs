using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ReportMS.Web.Attributes
{
    /// <summary>
    /// 站点布局特性。
    /// 可以设置布局模板和页面宽度
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class LayoutAttribute : ActionFilterAttribute
    {
        public LayoutAttribute(Layout layout)
        {
            this.Layout = layout;
        }

        public LayoutAttribute(Wide wide)
        {
            this.Wide = wide;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var isView = !filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest();
            if (!isView)
                return;

            var view = (ViewResult)filterContext.Result;
            view.MasterName = WebSiteLayout.GetLayout(this.Layout);
            view.ViewBag.ScreenWide = WebSiteWide.GetWide(this.Wide);
        }

        public Layout Layout { get; set; }

        public Wide Wide { get; set; }
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

    /// <summary>
    /// 屏幕宽度布局
    /// </summary>
    public enum Wide
    {
        /// <summary>
        /// 没有指定屏幕宽度布局
        /// </summary>
        None = 0,

        /// <summary>
        /// 宽屏布局，内容宽度占据 100%
        /// </summary>
        Widescreen100 = 1,

        /// <summary>
        /// 宽屏布局，内容宽度占据 98%
        /// </summary>
        Widescreen98 = 2,

        /// <summary>
        /// 宽屏布局，内容宽度占据 90%
        /// </summary>
        Widescreen90 = 3,

        /// <summary>
        /// 宽屏布局，内容宽度占据 80%
        /// </summary>
        Widescreen80 = 4,

        /// <summary>
        /// 宽屏布局，内容宽度占据 70%
        /// </summary>
        Widescreen70 = 5,

        /// <summary>
        /// 宽屏布局，内容宽度占据 60%
        /// </summary>
        Widescreen60 = 6
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

    internal static class WebSiteWide
    {
        private static readonly IDictionary<Wide, string> WideCss = new Dictionary<Wide, string>
        {
            {Wide.None, String.Empty},
            {Wide.Widescreen100, "wide-100"},
            {Wide.Widescreen98, "wide-98"},
            {Wide.Widescreen90, "wide-90"},
            {Wide.Widescreen80, "wide-80"},
            {Wide.Widescreen70, "wide-70"},
            {Wide.Widescreen60, "wide-60"}
        };

        public static string GetWide(Wide wideFilter)
        {
            return WideCss[wideFilter];
        }
    }
}