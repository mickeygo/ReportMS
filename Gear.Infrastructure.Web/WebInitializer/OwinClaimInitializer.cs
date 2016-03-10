using System.Security.Claims;
using System.Web.Helpers;

namespace Gear.Infrastructure.Web.WebInitializer
{
    /// <summary>
    /// Owin 初始化
    /// </summary>
    public class OwinClaimInitializer : IApplicationStartup
    {
        #region IApplicationStartup Members

        public void Initialize()
        {
            // AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            // 使用 anti-forgery token 提交表单时，作为用户唯一的 身份 Id
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Name; 
        }

        #endregion
    }
}
