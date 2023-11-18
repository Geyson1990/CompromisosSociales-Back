using Abp.Application.Services.Dto;
using Contable.Application;
using Contable.Application.SocialConflictTaskManagements.Dto;
using Contable.Application.Utilities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Repositories
{
    public interface IProcedureRepository
    {        
        Task<int> CallCreateRecordCodeProcess(int socialConflictId, long recordId);
        Task<int> CallCreateCompromiseCodeProcess(long compromiseId);
        Task<int> CallUpdateCompromiseCodeProcess(long compromiseId);
        Task<int> CallProspectiveRiskProcess(long userId);
        Task<int> CallProjectRiskProcess(long userId);
        Task<int> CallCreateSocialConflictCodeProcess(int socialConflictId);
        Task<int> CallCreateSocialConflictCodeReplaceProcess(int socialConflictId, int year, int count); 
        Task<int> CallSocialConflictStateProcess(int socialConflictId);
        Task<int> CallSocialConflictVerificationProccess(int socialConflictId); 
        Task<int> CallCreateSocialConflictAlertCodeProcess(int socialConflictAlertId);
        Task<int> CallSocialConflictAlertStateProcess(int socialConflictAlertId);
        Task<int> CallCreateSocialConflictSensibleCodeProcess(int socialConflictSensibleId);
        Task<int> CallCreateSocialConflictSensibleCodeReplaceProcess(int socialConflictSensibleId, int year, int count);
        Task<int> CallSocialConflictSensibleStateProcess(int socialConflictSensibleId);
        Task<int> CallSocialConflictSensibleVerificationProccess(int socialConflictSensibleId);
        Task<int> CallCreateHelpMemoryCodeProcess(int helpMemoryId);
        Task<List<UtilityPersonListDto>> CallTaskManagementGetAllPersons(long taskManagementId);
        Task<List<UtilityPersonListDto>> CallSocialConflictTaskManagementGetAllPersons(long socialConflictTaskManagementId);
        Task<int> CallCreateInterventionPlanCodeProcess(int interventionPlanId);
        Task<int> CallCreateInterventionPlanCodeReplaceProcess(int interventionPlanId, int year, int count);
        Task<int> CallCreateInterventionPlanStateProcess(int interventionPlanId);
        Task<int> CallCreateCrisisCommitteeCodeProcess(int crisisCommitteenId);
        Task<int> CallCreateCrisisCommitteeCodeReplaceProcess(int crisisCommitteenId, int year, int count);
        Task<PagedResultDto<UtilityConflictListGetAllDto>> CallGetAllConflictList(
            string code,
            string caseName,
            ConflictSite site,
            ConditionType lastCondition,
            int departmentId,
            int provinceId,
            int districtId,
            int skipCount,
            int maxResultCount);
        Task<PagedResultDto<SocialConflictTaskManagementConflictGetAllDto>> CallGetAllConflictTaskManagements(
            string names, 
            string codes, 
            DateTime? startDate, 
            DateTime? endDate,
            ConflictSite site,
            int skipCount, 
            int maxResultCount);
        Task<int> CallCreateSectorMeetCodeReplaceProcess(int sectorMeetId, int year, int count);
        Task<int> CallCreateSectorMeetCodeProcess(int sectorMeetId);
        Task<int> CallCreateDialogSpaceCodeProcess(int dialogSpaceId);
        Task<int> CallCreateDialogSpaceCodeReplaceProcess(int dialogSpaceId, int year, int count);
        Task<int> CallCreateDialogSpaceStateProcess(int dialogSpaceId);
        Task<int> CallGenerateMeetProcess(int sectorMeetId);
    }
}
