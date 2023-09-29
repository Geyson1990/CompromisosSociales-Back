using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Contable.Application;

namespace Contable.Authorization.Users.Dto
{
    public class UserListDto : EntityDto<long>, IPassivable, IHasCreationTime
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Surname2 { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationTime { get; set; }
        public PersonType Type { get; set; }
        public UserPersonDto Person { get; set; }
        public UserAlertResponsibleDto AlertResponsible { get; set; }
        public List<UserListRoleDto> Roles { get; set; }
    }
}