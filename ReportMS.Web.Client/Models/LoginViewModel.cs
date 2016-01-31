using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReportMS.Web.Client.Models
{
    public class LoginViewModel
    {
        [Required]
        [Description("User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Description("Password")]
        public string Password { get; set; }

        [Description("Remember me?")]
        public bool RememberMe { get; set; }
    }
}
