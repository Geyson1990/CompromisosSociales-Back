using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace Contable.Authorization.Accounts.Dto
{
    public class SendPasswordResetCodeInput
    {
        public string EmailAddress { get; set; }
    }
}