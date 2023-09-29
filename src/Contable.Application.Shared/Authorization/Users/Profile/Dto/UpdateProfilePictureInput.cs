using System;
using System.ComponentModel.DataAnnotations;
using Abp.Extensions;
using Abp.Runtime.Validation;

namespace Contable.Authorization.Users.Profile.Dto
{
    public class UpdateProfilePictureInput
    {
        public string Token { get; set; }
        public string Extension { get; set; }
    }
}