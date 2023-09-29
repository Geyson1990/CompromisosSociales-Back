using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace Contable.Web.Models.Ui
{
    public class LoginModel
    {
        public string UserNameOrEmailAddress { get; set; }

        [DisableAuditing]
        public string Password { get; set; }

        [Required]
        [DisableAuditing]
        public string RequestUser { get; set; }

        [Required]
        [DisableAuditing]
        public string RequestPassword { get; set; }

        public bool RememberMe { get; set; }

        public string TenancyName { get; set; }
    }
}
