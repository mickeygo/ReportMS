using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReportMS.Test.Web.Encoding
{
    [TestClass]
    public class HtmlEncodingTest
    {
        [TestMethod]
        public void HtmlEncode_Test()
        {
            var html = new StringBuilder();
            html.Append("<ol>");
            html.Append("<li style=\"text-align: left;\">");
            html.Append("这里我可以写一些输入提示<em></em><br/>");
            html.Append("</li>");
            html.Append("</ol>");

            var encodingResult = HttpUtility.HtmlEncode(html); // &lt;ol&gt;&lt;li style=&quot;text-align: left;&quot;&gt;这里我可以写一些输入提示&lt;em&gt;&lt;/em&gt;&lt;br/&gt;&lt;/li&gt;&lt;/ol&gt;

            Assert.IsNull(encodingResult, encodingResult);
        }

        [TestMethod]
        public void HtmlDecode_Test()
        {
            var encodeHtml = "&lt;ol&gt;&lt;li style=&quot;text-align: left;&quot;&gt;这里我可以写一些输入提示&lt;em&gt;&lt;/em&gt;&lt;br/&gt;&lt;/li&gt;&lt;/ol&gt;";

            var decodingResult = HttpUtility.HtmlDecode(encodeHtml);  // <ol><li style="text-align: left;">这里我可以写一些输入提示<em></em><br/></li></ol>

            Assert.IsNull(decodingResult, decodingResult);
        }

        [TestMethod]
        public void HtmlOutPut_Test()
        {
            var encodingHtml = "&lt;ol&gt;&lt;li style=&quot;text-align: left;&quot;&gt;这里我可以写一些输入提示&lt;em&gt;&lt;/em&gt;&lt;br/&gt;&lt;/li&gt;&lt;/ol&gt;";
            var html = MvcHtmlString.Create(encodingHtml).ToHtmlString();
            Assert.IsNull(html, html);
        }

        [TestMethod]
        public void HtmlEecodeAgain_Test()
        {
            var encodeHtml = "&lt;ol&gt;&lt;li style=&quot;text-align: left;&quot;&gt;这里我可以写一些输入提示&lt;em&gt;&lt;/em&gt;&lt;br/&gt;&lt;/li&gt;&lt;/ol&gt;";

            var encodingResult = HttpUtility.HtmlEncode(encodeHtml);  // &amp;lt;ol&amp;gt;&amp;lt;li style=&amp;quot;text-align: left;&amp;quot;&amp;gt;这里我可以写一些输入提示&amp;lt;em&amp;gt;&amp;lt;/em&amp;gt;&amp;lt;br/&amp;gt;&amp;lt;/li&amp;gt;&amp;lt;/ol&amp;gt;

            Assert.IsNull(encodingResult, encodingResult);
        }
    }
}
