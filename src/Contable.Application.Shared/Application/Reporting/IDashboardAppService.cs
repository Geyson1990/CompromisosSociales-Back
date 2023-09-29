using Abp.Application.Services;
using Contable.Application.Reporting.Dto;
using System.Threading.Tasks;

namespace Contable.Application.Reporting
{
    public interface IDashboardAppService : IApplicationService
    {
        Task<DashboardGetAllDto> GetAll(DashboardGetAllInputDto input);
    }
}
