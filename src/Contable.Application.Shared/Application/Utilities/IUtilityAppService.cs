using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Utilities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Utilities
{
    public interface IUtilityAppService : IApplicationService
    {
        Task<PagedResultDto<UtilityTerritorialUnitDto>> GetTerritorialUnits();
        Task<PagedResultDto<UtilitySocialConflictDto>> GetAllSocialConflicts(UtilitySocialConflicInputDto input);
        Task<PagedResultDto<UtilitySocialConflictUserDto>> GetAllUserSocialConflicts(UtilitySocialConflictUserGetAllInputDto input);
        Task<PagedResultDto<UtilityRecordDto>> GetAllRecords(UtilityRecordInputDto input);
        Task<PagedResultDto<UtilitySocialConflictLocationDto>> GetAllSocialConflictLocations(EntityDto input);
        Task<PagedResultDto<UtilityRegionGetAllDto>> GetAllRegions(UtilityRegionGetAllInputDto input);
        Task<PagedResultDto<UtilityPersonGetAllDto>> GetAllPersons(UtilityPersonGetAllInputDto input);
        Task<PagedResultDto<UtilityDepartmentDataDto>> GetAllDepartments();
        Task<UtilitySocialConflictReportFilterGetDto> GetAllSocialConflictFilters();
        Task<UtilitySocialConflictAlertReportFilterGetDto> GetAllSocialConflictAlertFilters();
        Task<UtilitySectorMeetReportFilterGetDto> GetAllSectorMeetFilters();
        Task<UtilityDirectoryGovernmentFilterGetDto> GetAllDirectoryGovermentFilters();
        Task<PagedResultDto<UtilityDirectoryGovernmentDto>> GetAllDirectoryGoverments(UtilityDirectoryGovernmentGetAllInputDto input);
        Task<PagedResultDto<UtilityConflictListGetAllDto>> GetAllConflictList(UtilityConflictListGetAllInputDto input);
        Task<PagedResultDto<UtilityInterventionPlanGetAllDto>> GetAllInterventionPlans(UtilityInterventionPlanGetAllInputDto input);
        Task<PagedResultDto<UtilityDirectoryIndustryGetAllDto>> GetAllDirectoryIndustries(UtilityDirectoryIndustryGetAllInputDto input);
        Task<UtilityGetDataDto> GetReportFilters();
        Task<UtilityInterventionPlanFilterGetDto> GetAllInterventionPlanFilters();
        Task<UtilityQuizFilterGetDto> GetAllQuizFilters();
        Task<UtilityDialogSpaceFilterGetDto> GetAllDialogSpaceFilters();
    }
}
