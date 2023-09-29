using Abp.Application.Services.Dto;
using Contable.Application;

namespace Contable.Sessions.Dto
{
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string ProfilePicture { get; set; }

        public string Document { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Surname2 { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string Role { get; set; }

        public PersonType Type { get; set; }
    }
}
