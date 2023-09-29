using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace Contable.Authorization.Users.Profile.Dto
{
    public class ChangePasswordInput
    {
        [DisableAuditing]
        public string CurrentPassword { get; set; }

        [DisableAuditing]
        public string NewPassword { get; set; }
    }
}