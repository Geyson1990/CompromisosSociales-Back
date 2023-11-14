using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Contable.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contable.Repositories;
using Abp;
using Contable.Application.Utilities.Dto;

namespace Contable.Manager.Base
{
    public class FunctionManagerBase : IDomainService, ITransientDependency
    {
        private readonly IRepository<Parameter> _parameterRepository;
        private readonly IProcedureRepository _procedureRepository;

        public FunctionManagerBase(IRepository<Parameter> parameterRepository, IProcedureRepository procedureRepository)
        {
            _parameterRepository = parameterRepository;
            _procedureRepository = procedureRepository;
        }

        public async Task<List<Parameter>> GetParameterValues(string categoryCode)
        {
            return await _parameterRepository
                .GetAll()
                .Include(e => e.ParameterCategory)
                .Where(e => e.ParameterCategory.Code == categoryCode)     
                .OrderBy(e => e.Order)
                .ToListAsync();
        }

        public async Task CallCreateRecordCodeProcess(int socialConflictId, long recordId)
        {
            await _procedureRepository.CallCreateRecordCodeProcess(socialConflictId, recordId);
        }
        public async Task CallCreateCompromiseCodeProcess(long compromiseId)
        {
            await _procedureRepository.CallCreateCompromiseCodeProcess(compromiseId);
        }

        public async Task CallUpdateCompromiseCodeProcess(long compromiseId)
        {
            await _procedureRepository.CallUpdateCompromiseCodeProcess(compromiseId);
        }

        public async Task CallProspectiveRiskProcess(UserIdentifier userIdentifier)
        {
            await _procedureRepository.CallProspectiveRiskProcess(userIdentifier.UserId);
        }

        public async Task CallProjectRiskProcess(UserIdentifier userIdentifier)
        {
            await _procedureRepository.CallProjectRiskProcess(userIdentifier.UserId);
        }

        public async Task CallSocialConflictStateProcess(int socialConflictId)
        {
            await _procedureRepository.CallSocialConflictStateProcess(socialConflictId);
        }

        public async Task CallSocialConflictVerificationProccess(int socialConflictId)
        {
            await _procedureRepository.CallSocialConflictVerificationProccess(socialConflictId);
        }

        public async Task CallCreateSocialConflictCodeProcess(int socialConflictId)
        {
            await _procedureRepository.CallCreateSocialConflictCodeProcess(socialConflictId);
        }

        public async Task CallCreateSocialConflictCodeReplaceProcess(int socialConflictId, int year, int count)
        {
            await _procedureRepository.CallCreateSocialConflictCodeReplaceProcess(socialConflictId, year, count);
        }

        public async Task CallCreateSocialConflictAlertCodeProcess(int socialConflictAlertId)
        {
            await _procedureRepository.CallCreateSocialConflictAlertCodeProcess(socialConflictAlertId);
        }

        public async Task CallSocialConflictAlertStateProcess(int socialConflictAlertId)
        {
            await _procedureRepository.CallSocialConflictAlertStateProcess(socialConflictAlertId);
        }

        public async Task CallCreateSocialConflictSensibleCodeProcess(int socialConflictSensibleId)
        {
            await _procedureRepository.CallCreateSocialConflictSensibleCodeProcess(socialConflictSensibleId);
        }

        public async Task CallCreateSocialConflictSensibleCodeReplaceProcess(int socialConflictSensibleId, int year, int count)
        {
            await _procedureRepository.CallCreateSocialConflictSensibleCodeReplaceProcess(socialConflictSensibleId, year, count);
        }

        public async Task CallSocialConflictSensibleStateProcess(int socialConflictSensibleId)
        {
            await _procedureRepository.CallSocialConflictSensibleStateProcess(socialConflictSensibleId);
        }

        public async Task CallSocialConflictSensibleVerificationProccess(int socialConflictSensibleId)
        {
            await _procedureRepository.CallSocialConflictSensibleVerificationProccess(socialConflictSensibleId);
        }

        public async Task CallCreateHelpMemoryCodeProcess(int helpMemoryId)
        {
            await _procedureRepository.CallCreateHelpMemoryCodeProcess(helpMemoryId);
        }

        public async Task CallCreateInterventionPlanCodeReplaceProcess(int interventionPlanId, int year, int count)
        {
            await _procedureRepository.CallCreateInterventionPlanCodeReplaceProcess(interventionPlanId, year, count);
        }

        public async Task CallCreateInterventionPlanCodeProcess(int interventionPlanId)
        {
            await _procedureRepository.CallCreateInterventionPlanCodeProcess(interventionPlanId);
        }

        public async Task CallCreateCrisisCommitteeCodeReplaceProcess(int crisisCommitteenId, int year, int count)
        {
            await _procedureRepository.CallCreateCrisisCommitteeCodeReplaceProcess(crisisCommitteenId, year, count);
        }

        public async Task CallCreateInterventionPlanStateProcess(int interventionPlanId)
        {
            await _procedureRepository.CallCreateInterventionPlanStateProcess(interventionPlanId);
        }

        public async Task CallCreateCrisisCommitteeCodeProcess(int crisisCommitteenId)
        {
            await _procedureRepository.CallCreateCrisisCommitteeCodeProcess(crisisCommitteenId);
        }

        public async Task<List<UtilityPersonListDto>> CallSocialConflictTaskManagementGetAllPersons(int socialConflictTaskManagementId)
        {
            return await _procedureRepository.CallSocialConflictTaskManagementGetAllPersons(socialConflictTaskManagementId);
        }

        public async Task<List<UtilityPersonListDto>> CallTaskManagementGetAllPersons(long taskManagementId)
        {
            return await _procedureRepository.CallTaskManagementGetAllPersons(taskManagementId);
        }

        public async Task CallCreateSectorMeetCodeReplaceProcess(int sectorMeetId, int year, int count)
        {
            await _procedureRepository.CallCreateSectorMeetCodeReplaceProcess(sectorMeetId, year, count);
        }

        public async Task CallCreateSectorMeetCodeProcess(int sectorMeetId)
        {
            await _procedureRepository.CallCreateSectorMeetCodeProcess(sectorMeetId);
        }

        public async Task CallCreateDialogSpaceCodeProcess(int dialogSpaceId)
        {
            await _procedureRepository.CallCreateDialogSpaceCodeProcess(dialogSpaceId);
        }

        public async Task CallCreateDialogSpaceCodeReplaceProcess(int dialogSpaceId, int year, int count)
        {
            await _procedureRepository.CallCreateDialogSpaceCodeReplaceProcess(dialogSpaceId, year, count);
        }

        public async Task CallCreateDialogSpaceStateProcess(int dialogSpaceId)
        {
            await _procedureRepository.CallCreateDialogSpaceStateProcess(dialogSpaceId);
        }
    }
}
