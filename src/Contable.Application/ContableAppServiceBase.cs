using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using Microsoft.AspNetCore.Identity;
using Contable.Authorization.Users;
using Contable.MultiTenancy;
using Abp.UI;
using Contable.Manager;

namespace Contable
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class ContableAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public ResourceManager ResourceManager { get; set; }

        public FunctionManager FunctionManager { get; set; }

        public ReportManager ReportManager { get; set; }

        public string DefaultTitleMessage
        {
            get => ContableConsts.DefaultTitleText;
        }
        protected ContableAppServiceBase()
        {
            LocalizationSourceName = ContableConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual User GetCurrentUser()
        {
            return AsyncHelper.RunSync(GetCurrentUserAsync);
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }

        protected virtual Tenant GetCurrentTenant()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetById(AbpSession.GetTenantId());
            }
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected void VerifyCount(int value)
        {
            if (value == 0)
                throw new UserFriendlyException(DefaultTitleMessage, L("RecordNotFound"));
        }
    }
}