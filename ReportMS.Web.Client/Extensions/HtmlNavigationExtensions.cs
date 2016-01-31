using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// Mvc 导航菜单扩展
    /// </summary>
    public static class HtmlNavigationExtensions
    {
        #region Private Fields

        private static readonly string projectName = "RMS";
        private static readonly string projectDefaultUrl = "~/Home/Index";
        private static readonly string navigationCss = "breadcrumb-trail clearfix";

        #endregion

        #region Public Methods

        /// <summary>
        /// 导航菜单
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="navigation">导航数据，key：导航内容；value：导航 url</param>
        /// <returns>MvcHtmlString</returns>
        public static IHtmlString Navigate(this HtmlHelper htmlHelper, IDictionary<string, string> navigation)
        {
            return Navigation(projectName, projectDefaultUrl, navigationCss, navigation);
        }

        /// <summary>
        /// 导航菜单，不包含 body 头
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="navigation">导航数据</param>
        /// <returns>MvcHtmlString</returns>
        public static IHtmlString Navigate(this HtmlHelper htmlHelper, params string[] navigation)
        {
            return Navigate(htmlHelper, false, navigation);
        }

        /// <summary>
        /// 导航菜单
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper</param>
        /// <param name="isGenarateBodyHeader">是否生成 Body 头</param>
        /// <param name="navigation">导航数据</param>
        /// <returns>MvcHtmlString</returns>
        public static IHtmlString Navigate(this HtmlHelper htmlHelper, bool isGenarateBodyHeader, params string[] navigation)
        {
            var html = NavigationWithoutLink(isGenarateBodyHeader, projectName, projectDefaultUrl, navigationCss, navigation);
            return MvcHtmlString.Create(html);
        }

        #endregion

        #region Private Methods

        private static string NavigationWithoutLink(bool isGenarateBodyHeader, string firstNavName, string firstNavUrl, string navClass, params string[] navigation)
        {
            var htmlBulider = new StringBuilder();

            // firstly
            var tagF = new TagBuilder("a");
            tagF.MergeAttribute("href", UrlHelper.GenerateContentUrl(firstNavUrl, new HttpContextWrapper(HttpContext.Current)));
            tagF.SetInnerText(firstNavName);

            var tagLiF = new TagBuilder("li")
            {
                InnerHtml = tagF.ToString()
            };

            htmlBulider.Append(tagLiF);

            var tagH1 = new TagBuilder("h1");

            if (navigation != null && navigation.Any())
            {
                int index = 0, count = navigation.Count();
                foreach (var nav in navigation)
                {
                    var tagLi = new TagBuilder("li");

                    var tagSpan01 = new TagBuilder("span");
                    tagSpan01.SetInnerText(" / " + nav);

                    tagLi.InnerHtml = tagSpan01.ToString();

                    if (++index == count)
                        tagH1.SetInnerText(nav);

                    htmlBulider.AppendLine(tagLi.ToString());
                }
            }

            var tagOl = new TagBuilder("ol");
            tagOl.AddCssClass(navClass);
            tagOl.InnerHtml = htmlBulider.ToString();

            var htmlBulider2 = new StringBuilder();
            htmlBulider2.Append(tagOl);

            if (isGenarateBodyHeader)
                htmlBulider2.Append(tagH1);

            return htmlBulider2.ToString();
        }

        private static IHtmlString Navigation(string firstNavName, string firstNavUrl, string navClass, IDictionary<string, string> navigation)
        {
            int index = 0, count = navigation.Keys.Count;
            var htmlBulider = new StringBuilder();

            // firstly
            var tagF = new TagBuilder("a");
            tagF.MergeAttribute("href", UrlHelper.GenerateContentUrl(firstNavUrl, new HttpContextWrapper(HttpContext.Current)));
            tagF.SetInnerText(firstNavName);

            var tagLiF = new TagBuilder("li")
            {
                InnerHtml = tagF.ToString()
            };

            htmlBulider.Append(tagLiF);

            // Other
            var tagH1 = new TagBuilder("h1");

            foreach (var nav in navigation)
            {
                var tagLi = new TagBuilder("li");

                var tagSpan = new TagBuilder("span");
                tagSpan.SetInnerText(" / ");

                if (index++ < count)
                {
                    var tagA = new TagBuilder("a");
                    tagA.MergeAttribute("href", nav.Value);
                    tagA.SetInnerText(nav.Key);

                    tagLi.InnerHtml = tagSpan.ToString() + tagA.ToString();
                }
                else
                {
                    var tagSpan01 = new TagBuilder("span");
                    tagSpan.SetInnerText(nav.Key);

                    tagH1.SetInnerText(nav.Key);
                    tagLi.InnerHtml = tagSpan.ToString() + tagSpan01.ToString();
                }

                htmlBulider.AppendLine(tagLi.ToString());
            }

            var tagOl = new TagBuilder("ol");
            tagOl.AddCssClass(navClass);
            tagOl.InnerHtml = htmlBulider.ToString();

            var htmlBulider2 = new StringBuilder();
            htmlBulider2.Append(tagOl);
            htmlBulider2.Append(tagH1);

            return MvcHtmlString.Create(htmlBulider2.ToString());
        }

        #endregion
    }
}
