using System.Threading.Tasks;
using Abp.Application.Services;
using Contable.Configuration.Tenants.Dto;

namespace Contable.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
