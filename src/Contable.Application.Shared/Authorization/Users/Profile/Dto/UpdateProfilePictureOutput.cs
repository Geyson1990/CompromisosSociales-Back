using System;
using System.ComponentModel.DataAnnotations;
using Abp.Extensions;
using Abp.Runtime.Validation;

namespace Contable.Authorization.Users.Profile.Dto
{
    public class UpdateProfilePictureOutput
    {
        public string Resource { get; set; }
    }
}