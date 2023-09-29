using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Portal.Dto;
using System.Threading.Tasks;

namespace Contable.Application.Portal
{
    public interface IPortalAppService : IApplicationService
    {
        Task<PagedResultDto<PortalQuizFormDto>> GetQuestions();
        Task CreateQuiz(PortalQuizCreateDto input);
        Task<PortalPipMefDto> GetPipMef(PortalGetAllInputDto input);
        Task<PortalGetAllDto> GetAll(PortalGetAllInputDto input);
        Task<PortalSocialConflictDataDto> GetAllSocialConflicts(PortalGetAllSocialConflictInputDto input);
        Task<PortalSocialConflictAlertDataDto> GetAllSocialConflictAlerts(PortalGetAllInputDto input);
        Task<PortalSocialConflictSensibleDataDto> GetAllSocialConflictSensibles(PortalGetAllSocialConflictSensibleInputDto input);
        Task<PortaReportDataDto> GetReportFilters();
    }
}
