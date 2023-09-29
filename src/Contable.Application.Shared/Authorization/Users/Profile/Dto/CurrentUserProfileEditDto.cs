using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Contable.Application;

namespace Contable.Authorization.Users.Profile.Dto
{
    public class CurrentUserProfileEditDto
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Surname2 { get; set; }
    }
}