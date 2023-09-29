using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Contable.Application;

namespace Contable.Authorization.Users.Dto
{
    //Mapped to/from User in CustomDtoMapper
    public class UserEditDto
    {
        /// <summary>
        /// Set null to create a new user. Set user's Id to update a user
        /// </summary>
        public long? Id { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [StringLength(UserConsts.Surname2MaxLength)]
        public string Surname2 { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(UserConsts.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        // Not used "Required" attribute since empty value is used to 'not change password'
        //[StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }

        [StringLength(UserConsts.DocumentMaxLength)]
        public string Document { get; set; }

        public UserPersonDto Person { get; set; }

        public bool GeneratePerson { get; set; }

        public PersonType Type { get; set; }

        public UserAlertResponsibleDto AlertResponsible { get; set; }
    }
}