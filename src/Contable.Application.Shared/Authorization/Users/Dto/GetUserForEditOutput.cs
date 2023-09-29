using System;
using System.Collections.Generic;
using Contable.Organizations.Dto;

namespace Contable.Authorization.Users.Dto
{
    public class GetUserForEditOutput
    {
        public UserEditDto User { get; set; }

        public UserRoleDto[] Roles { get; set; }

        public UserAlertResponsibleDto[] Responsibles { get; set; }
    }
}