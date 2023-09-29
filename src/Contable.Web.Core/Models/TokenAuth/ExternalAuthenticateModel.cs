using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;

namespace Contable.Web.Models.TokenAuth
{
    public class ExternalAuthenticateModel
    {
        public string UserNameOrEmailAddress { get; set; }

        [Required]
        [DisableAuditing]
        public string Password { get; set; }
    }
}