using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Contable.Authorization.Users;
using Contable.MultiTenancy;

namespace Contable.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}