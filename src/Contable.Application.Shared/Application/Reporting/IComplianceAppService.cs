using Abp.Application.Services;
using Contable.Application.Reporting.Dto;
using System.Threading.Tasks;

namespace Contable.Application.Reporting
{
    public interface IComplianceAppService : IApplicationService
    {
        Task<ComplianceGetAllDto> GetAll(ComplianceGetAllInputDto input);
    }
}
