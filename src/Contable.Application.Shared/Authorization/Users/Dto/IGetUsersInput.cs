using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace Contable.Authorization.Users.Dto
{
    public interface IGetUsersInput : ISortedResultRequest
    {
        string Filter { get; set; }
        bool OnlyLockedUsers { get; set; }
    }
}