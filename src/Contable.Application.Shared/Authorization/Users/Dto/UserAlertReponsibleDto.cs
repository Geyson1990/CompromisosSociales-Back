using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Authorization.Users.Dto
{
    public class UserAlertResponsibleDto : EntityDto
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
