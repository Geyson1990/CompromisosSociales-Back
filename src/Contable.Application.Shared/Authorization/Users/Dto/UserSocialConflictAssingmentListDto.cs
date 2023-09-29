using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Authorization.Users.Dto
{
    public class UserSocialConflictAssingmentListDto
    {
        public long UserId { get; set; }
        public List<UserSocialConflictAssingmentDto> Assignments { get; set; }
    }
}
