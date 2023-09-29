using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using Contable.Editions;
using Contable.MultiTenancy.Payments;
using Contable.Sessions.Dto;
using Contable.UiCustomization;
using Contable.Authorization.Delegation;
using Abp.Domain.Repositories;
using Contable.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Contable.Configuration;

namespace Contable.Sessions
{
    public class SessionAppService : ContableAppServiceBase, ISessionAppService
    {
        private readonly IUiThemeCustomizerFactory _uiThemeCustomizerFactory;
        private readonly ISubscriptionPaymentRepository _subscriptionPaymentRepository;
        private readonly IUserDelegationConfiguration _userDelegationConfiguration;
        private readonly IRepository<Level> _levelRepository;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SessionAppService(
            IUiThemeCustomizerFactory uiThemeCustomizerFactory,
            ISubscriptionPaymentRepository subscriptionPaymentRepository,
            IUserDelegationConfiguration userDelegationConfiguration,
            IRepository<Level> levelRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _uiThemeCustomizerFactory = uiThemeCustomizerFactory;
            _subscriptionPaymentRepository = subscriptionPaymentRepository;
            _userDelegationConfiguration = userDelegationConfiguration;
            _levelRepository = levelRepository;
            _webHostEnvironment = webHostEnvironment;
            _configurationRoot = webHostEnvironment.GetAppConfiguration();
        }

        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var levels = _levelRepository
                .GetAll()
                .OrderBy(p => p.Index)
                .ToList();

            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>(),
                    Currency = ContableConsts.Currency,
                    CurrencySign = ContableConsts.CurrencySign,
                    AllowTenantsToChangeEmailSettings = ContableConsts.AllowTenantsToChangeEmailSettings,
                    UserDelegationIsEnabled = _userDelegationConfiguration.IsEnabled,
                    PrimaryLevels = ObjectMapper.Map<List<SessionLevelDto>>(levels.Where(p => p.Type == LevelType.PRIMARY)),
                    SecondaryLevels = ObjectMapper.Map<List<SessionLevelDto>>(levels.Where(p => p.Type == LevelType.SECONDARY)),
                    LoginKey = _configurationRoot["Authentication:AES:Key"],
                    LoginToken = _configurationRoot["Authentication:AES:IV"]
                }
            };

            var uiCustomizer = await _uiThemeCustomizerFactory.GetCurrentUiCustomizer();
            output.Theme = await uiCustomizer.GetUiSettings();

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper
                    .Map<TenantLoginInfoDto>(await TenantManager
                        .Tenants
                        .Include(t => t.Edition)
                        .FirstAsync(t => t.Id == AbpSession.GetTenantId()));
            }

            if (AbpSession.UserId.HasValue)
            {
                var user = await GetCurrentUserAsync();

                output.User = ObjectMapper.Map<UserLoginInfoDto>(user);

                var roles = await UserManager.GetRolesAsync(user);

                if(roles.Count > 0) 
                    output.User.Role = roles[0];
            }

            if (output.Tenant == null)
            {
                return output;
            }

            if (output.Tenant.Edition != null)
            {
                var lastPayment = await _subscriptionPaymentRepository.GetLastCompletedPaymentOrDefaultAsync(output.Tenant.Id, null, null);
                if (lastPayment != null)
                {
                    output.Tenant.Edition.IsHighestEdition = IsEditionHighest(output.Tenant.Edition.Id, lastPayment.GetPaymentPeriodType());
                }
            }

            output.Tenant.SubscriptionDateString = GetTenantSubscriptionDateString(output);
            output.Tenant.CreationTimeString = output.Tenant.CreationTime.ToString("d");

            return output;

        }

        private bool IsEditionHighest(int editionId, PaymentPeriodType paymentPeriodType)
        {
            var topEdition = GetHighestEditionOrNullByPaymentPeriodType(paymentPeriodType);
            if (topEdition == null)
            {
                return false;
            }

            return editionId == topEdition.Id;
        }

        private SubscribableEdition GetHighestEditionOrNullByPaymentPeriodType(PaymentPeriodType paymentPeriodType)
        {
            var editions = TenantManager.EditionManager.Editions;
            if (editions == null || !editions.Any())
            {
                return null;
            }

            var query = editions.Cast<SubscribableEdition>();

            switch (paymentPeriodType)
            {
                case PaymentPeriodType.Daily:
                    query = query.OrderByDescending(e => e.DailyPrice ?? 0); break;
                case PaymentPeriodType.Weekly:
                    query = query.OrderByDescending(e => e.WeeklyPrice ?? 0); break;
                case PaymentPeriodType.Monthly:
                    query = query.OrderByDescending(e => e.MonthlyPrice ?? 0); break;
                case PaymentPeriodType.Annual:
                    query = query.OrderByDescending(e => e.AnnualPrice ?? 0); break;
            }

            return query.FirstOrDefault();
        }

        private string GetTenantSubscriptionDateString(GetCurrentLoginInformationsOutput output)
        {
            return output.Tenant.SubscriptionEndDateUtc == null
                ? L("Unlimited")
                : output.Tenant.SubscriptionEndDateUtc?.ToString("d");
        }

        public async Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken()
        {
            if (AbpSession.UserId <= 0)
            {
                throw new Exception(L("ThereIsNoLoggedInUser"));
            }

            var user = await UserManager.GetUserAsync(AbpSession.ToUserIdentifier());
            user.SetSignInToken();
            return new UpdateUserSignInTokenOutput
            {
                SignInToken = user.SignInToken,
                EncodedUserId = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id.ToString())),
                EncodedTenantId = user.TenantId.HasValue
                    ? Convert.ToBase64String(Encoding.UTF8.GetBytes(user.TenantId.Value.ToString()))
                    : ""
            };
        }
    }
}