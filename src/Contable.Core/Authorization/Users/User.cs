using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.Timing;
using Contable.Application;

namespace Contable.Authorization.Users
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User : AbpUser<User>
    {
        public virtual Guid? ProfilePictureId { get; set; }

        public virtual bool ShouldChangePasswordOnNextLogin { get; set; }

        public DateTime? SignInTokenExpireTimeUtc { get; set; }

        public string SignInToken { get; set; }

        public string GoogleAuthenticatorKey { get; set; }

        public List<UserOrganizationUnit> OrganizationUnits { get; set; }

        //Can add application specific user properties here

        [Column(TypeName = UserConsts.PersonIdType)]
        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = UserConsts.PersonType)]
        public PersonType Type { get; set; }

        [Column(TypeName = UserConsts.NameType)]
        public override string Name { get; set; }

        [Column(TypeName = UserConsts.SurnameType)]
        public override string Surname { get; set; }

        [Column(TypeName = UserConsts.Surname2Type)]
        public string Surname2 { get; set; }

        [Column(TypeName = UserConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = UserConsts.ProfilePictureType)]
        public string ProfilePicture { get; set; }

        [Column(TypeName = UserConsts.AlertResponsibleIdType)]
        [ForeignKey("AlertResponsible")]
        public int? AlertResponsibleId { get; set; }
        public AlertResponsible AlertResponsible { get; set; }

        [Column(TypeName = UserConsts.PasswordResetCodeType)]
        public override string PasswordResetCode { get; set; }

        [Column(TypeName = UserConsts.PasswordResetTimeType)]
        public DateTime? PasswordResetTime { get; set; }

        [Column(TypeName = UserConsts.EmailConfirmationCodeType)]
        public override string EmailConfirmationCode { get; set; }

        [Column(TypeName = UserConsts.EmailConfirmationTimeType)]
        public DateTime? EmailConfirmationTime { get; set; }

        public List<SocialConflictUser> UserSocialConflicts { get; set; }

        public User()
        {
            IsLockoutEnabled = true;
            IsTwoFactorEnabled = true;
        }

        /// <summary>
        /// Creates admin <see cref="User"/> for a tenant.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="emailAddress">Email address</param>
        /// <returns>Created <see cref="User"/> object</returns>
        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>(),
                OrganizationUnits = new List<UserOrganizationUnit>()
            };

            user.SetNormalizedNames();

            return user;
        }

        public string GetNameSurname()
        {
            return (Name ?? "").Trim() + " " + (Surname ?? "").Trim() + " " + (Surname2 ?? "").Trim();
        }

        public override void SetNewPasswordResetCode()
        {
            PasswordResetCode = new Random().Next(1, 1000000).ToString("000000"); ;
            PasswordResetTime = DateTime.Now.AddMinutes(Application.UserConsts.NotificationValidationMinutes);
        }

        public override void SetNewEmailConfirmationCode()
        {
            EmailConfirmationCode = new Random().Next(1, 1000000).ToString("000000");
            EmailConfirmationTime = DateTime.Now.AddMinutes(Application.UserConsts.NotificationValidationMinutes);
        }

        public void Unlock()
        {
            AccessFailedCount = 0;
            LockoutEndDateUtc = null;
        }

        public void SetSignInToken()
        {
            SignInToken = Guid.NewGuid().ToString();
            SignInTokenExpireTimeUtc = Clock.Now.AddMinutes(1).ToUniversalTime();
        }
    }
}