using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Contable.Authorization.Users.Dto
{
    public class CreateOrUpdateUserInput
    {
        [Required]
        public UserEditDto User { get; set; }

        [Required]
        public string[] AssignedRoleNames { get; set; }
    }
}