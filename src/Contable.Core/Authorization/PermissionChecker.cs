using Abp.Authorization;
using Contable.Authorization.Roles;
using Contable.Authorization.Users;

namespace Contable.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
