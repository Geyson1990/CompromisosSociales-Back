using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Common.Dto;
using Contable.Editions.Dto;

namespace Contable.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}