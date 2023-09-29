using System.Threading.Tasks;
using Abp.Application.Services;
using Contable.Editions.Dto;
using Contable.MultiTenancy.Dto;

namespace Contable.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}