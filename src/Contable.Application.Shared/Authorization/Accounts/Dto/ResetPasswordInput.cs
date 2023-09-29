using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Abp.Auditing;
using Abp.Runtime.Security;
using Abp.Runtime.Validation;

namespace Contable.Authorization.Accounts.Dto
{
    public class ResetPasswordInput
    {
        public string EmailAddress { get; set; }
        public string ResetCode { get; set; }

        [DisableAuditing]
        public string Password { get; set; }
    }
}