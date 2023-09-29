using Abp.Linq.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using Contable.Application.InterventionPlans;
using Contable.Application.InterventionPlans.Dto;
using System.Collections.Generic;
using Abp.UI;
using Abp.Application.Services.Dto;
using Contable.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Contable.Authorization.Users;
using Abp.Collections.Extensions;
using System;
using System.Linq.Expressions;
using NUglify.Helpers;
using Contable.Application.Uploaders.Dto;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_ConflictTools_InterventionPlan)]
    public class InterventionPlanAppService : ContableAppServiceBase, IInterventionPlanAppService
    {
        private readonly IRepository<InterventionPlan> _interventionPlanRepository;
        private readonly IRepository<InterventionPlanLocation> _interventionPlanLocationRepository;
        private readonly IRepository<InterventionPlanActor> _interventionPlanActorRepository;
        private readonly IRepository<InterventionPlanState> _interventionPlanStateRepository;
        private readonly IRepository<InterventionPlanRisk> _interventionPlanRiskRepository;
        private readonly IRepository<InterventionPlanMethodology> _interventionPlanMethodologyRepository;
        private readonly IRepository<InterventionPlanOption> _interventionPlanOptionRepository;        
        private readonly IRepository<InterventionPlanSolution> _interventionPlanSolutionRepository;
        private readonly IRepository<InterventionPlanSchedule> _interventionPlanScheduleRepository;
        private readonly IRepository<InterventionPlanActivity> _interventionPlanActivityRepository;
        private readonly IRepository<InterventionPlanEntity> _interventionPlanEntityRepository;
        private readonly IRepository<InterventionPlanTeam> _interventionPlanTeamRepository;
        private readonly IRepository<InterventionPlanRole> _interventionPlanRoleRepository;
        private readonly IRepository<CrisisCommittee> _crisisCommitteeRepository;
        private readonly IRepository<CrisisCommitteeTeam> _crisisCommitteeTeamRepository;
        private readonly IRepository<CrisisCommitteePlan> _crisisCommitteePlanRepository;
        private readonly IRepository<CrisisCommitteeAction> _crisisCommitteeActionRepository;
        private readonly IRepository<CrisisCommitteeMessage> _crisisCommitteeMessageRepository;
        private readonly IRepository<CrisisCommitteeChannel> _crisisCommitteeChannelRepository;
        private readonly IRepository<CrisisCommitteeSector> _crisisCommitteeSectorRepository;
        private readonly IRepository<CrisisCommitteeTask> _crisisCommitteeTaskRepository;
        private readonly IRepository<CrisisCommitteeAgreement> _crisisCommitteeAgreementRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SocialConflictLocation> _socialConflictLocationRepository;
        private readonly IRepository<SocialConflictActor> _socialConflictActorRepository;
        private readonly IRepository<SocialConflictSensible> _socialConflictSensibleRepository;
        private readonly IRepository<SocialConflictSensibleLocation> _socialConflictSensibleLocationRepository;
        private readonly IRepository<ActorType> _actorTypeRepository;
        private readonly IRepository<ActorMovement> _actorMovementRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<Risk> _riskRepository;
        private readonly IRepository<AlertResponsible> _alertResponsibleRepository;
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;
        private readonly IRepository<User, long> _userRepository;

        public InterventionPlanAppService(
            IRepository<InterventionPlan> interventionPlanRepository, 
            IRepository<InterventionPlanLocation> interventionPlanLocationRepository,
            IRepository<InterventionPlanActor> interventionPlanActorRepository,
            IRepository<InterventionPlanState> interventionPlanStateRepository,
            IRepository<InterventionPlanRisk> interventionPlanRiskRepository,
            IRepository<InterventionPlanMethodology> interventionPlanMethodologyRepository,
            IRepository<InterventionPlanOption> interventionPlanOptionRepository,
            IRepository<InterventionPlanSolution> interventionPlanSolutionRepository,
            IRepository<InterventionPlanSchedule> interventionPlanScheduleRepository,
            IRepository<InterventionPlanActivity> interventionPlanActivityRepository,
            IRepository<InterventionPlanEntity> interventionPlanEntityRepository,
            IRepository<InterventionPlanTeam> interventionPlanTeamRepository,
            IRepository<InterventionPlanRole> interventionPlanRoleRepository,
            IRepository<CrisisCommittee> crisisCommitteeRepository,
            IRepository<CrisisCommitteeTeam> crisisCommitteeTeamRepository,
            IRepository<CrisisCommitteePlan> crisisCommitteePlanRepository,
            IRepository<CrisisCommitteeAction> crisisCommitteeActionRepository,
            IRepository<CrisisCommitteeMessage> crisisCommitteeMessageRepository,
            IRepository<CrisisCommitteeChannel> crisisCommitteeChannelRepository,
            IRepository<CrisisCommitteeSector> crisisCommitteeSectorRepository,
            IRepository<CrisisCommitteeTask> crisisCommitteeTaskRepository,
            IRepository<CrisisCommitteeAgreement> crisisCommitteeAgreementRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<SocialConflictLocation> socialConflictLocationRepository,
            IRepository<SocialConflictActor> socialConflictActorRepository,
            IRepository<SocialConflictSensible> socialConflictSensibleRepository,
            IRepository<SocialConflictSensibleLocation> socialConflictSensibleLocationRepository,
            IRepository<ActorType> actorTypeRepository,
            IRepository<ActorMovement> actorMovementRepository,
            IRepository<Person> personRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<District> districtRepository,
            IRepository<Region> regionRepository,
            IRepository<Risk> riskRepository,
            IRepository<AlertResponsible> alertResponsibleRepository,
            IRepository<DirectoryGovernment> directoryGovernmentRepository,
            IRepository<User, long> userRepository)
        {
            _interventionPlanRepository = interventionPlanRepository;
            _interventionPlanLocationRepository = interventionPlanLocationRepository;
            _interventionPlanActorRepository = interventionPlanActorRepository;
            _interventionPlanStateRepository = interventionPlanStateRepository;
            _interventionPlanRiskRepository = interventionPlanRiskRepository;
            _interventionPlanMethodologyRepository = interventionPlanMethodologyRepository;
            _interventionPlanOptionRepository = interventionPlanOptionRepository;
            _interventionPlanSolutionRepository = interventionPlanSolutionRepository;
            _interventionPlanScheduleRepository = interventionPlanScheduleRepository;
            _interventionPlanActivityRepository = interventionPlanActivityRepository;
            _interventionPlanEntityRepository = interventionPlanEntityRepository;
            _interventionPlanTeamRepository = interventionPlanTeamRepository;
            _interventionPlanRoleRepository = interventionPlanRoleRepository;
            _crisisCommitteeRepository = crisisCommitteeRepository;
            _crisisCommitteeTeamRepository = crisisCommitteeTeamRepository;
            _crisisCommitteePlanRepository = crisisCommitteePlanRepository;
            _crisisCommitteeActionRepository = crisisCommitteeActionRepository;
            _crisisCommitteeMessageRepository = crisisCommitteeMessageRepository;
            _crisisCommitteeChannelRepository = crisisCommitteeChannelRepository;
            _crisisCommitteeSectorRepository = crisisCommitteeSectorRepository;
            _crisisCommitteeTaskRepository = crisisCommitteeTaskRepository;
            _crisisCommitteeAgreementRepository = crisisCommitteeAgreementRepository;
            _socialConflictRepository = socialConflictRepository;
            _socialConflictLocationRepository = socialConflictLocationRepository;
            _socialConflictActorRepository = socialConflictActorRepository;
            _socialConflictSensibleRepository = socialConflictSensibleRepository;
            _socialConflictSensibleLocationRepository = socialConflictSensibleLocationRepository;
            _actorTypeRepository = actorTypeRepository;
            _actorMovementRepository = actorMovementRepository;
            _personRepository = personRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _regionRepository = regionRepository;
            _riskRepository = riskRepository;
            _alertResponsibleRepository = alertResponsibleRepository;
            _directoryGovernmentRepository = directoryGovernmentRepository;
            _userRepository = userRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_InterventionPlan_Create)]
        public async Task<EntityDto> Create(InterventionPlanCreateDto input)
        {
            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _interventionPlanRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var interventionPlanId = await _interventionPlanRepository.InsertAndGetIdAsync(await ValidateEntity(
                    input: ObjectMapper.Map<InterventionPlan>(input),
                    socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                    socialConflictSensibleId: input.SocialConflictSensible == null ? -1 : input.SocialConflictSensible.Id,
                    personId: input.Person == null ? -1 : input.Person.Id,
                    locations: input.Locations ?? new List<InterventionPlanLocationRelationDto>(),
                    actors: input.Actors ?? new List<InterventionPlanActorRelationDto>(),
                    states: input.States ?? new List<InterventionPlanStateRelationDto>(),
                    methodologies: input.Methodologies ?? new List<InterventionPlanMethodologyRelationDto>(),
                    risks: input.Risks ?? new List<InterventionPlanRiskRelationDto>(),
                    schedules: input.Schedules ?? new List<InterventionPlanScheduleRelationDto>(),
                    teams: input.Teams ?? new List<InterventionPlanTeamRelationDto>(),
                    solutions: input.Solutions ?? new List<InterventionPlanSolutionRelationDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateInterventionPlanCodeReplaceProcess(interventionPlanId, input.ReplaceYear, input.ReplaceCount);
            else
               await FunctionManager.CallCreateInterventionPlanCodeProcess(interventionPlanId);

            await FunctionManager.CallCreateInterventionPlanStateProcess(interventionPlanId);

            return new EntityDto(interventionPlanId);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_InterventionPlan_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _interventionPlanRepository.CountAsync(p => p.Id == input.Id));
            
            await _interventionPlanRepository.DeleteAsync(p => p.Id == input.Id);
            await _interventionPlanLocationRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);
            await _interventionPlanActorRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);
            await _interventionPlanStateRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);
            await _interventionPlanMethodologyRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);
            await _interventionPlanRiskRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);
            await _interventionPlanScheduleRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);
            await _interventionPlanTeamRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);
            await _interventionPlanSolutionRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);

            await _crisisCommitteeRepository.DeleteAsync(p => p.InterventionPlanId == input.Id);
            await _crisisCommitteeTeamRepository.DeleteAsync(p => p.CrisisCommittee.InterventionPlanId == input.Id);
            await _crisisCommitteePlanRepository.DeleteAsync(p => p.CrisisCommittee.InterventionPlanId == input.Id);
            await _crisisCommitteeActionRepository.DeleteAsync(p => p.CrisisCommittee.InterventionPlanId == input.Id);
            await _crisisCommitteeMessageRepository.DeleteAsync(p => p.CrisisCommittee.InterventionPlanId == input.Id);
            await _crisisCommitteeChannelRepository.DeleteAsync(p => p.CrisisCommittee.InterventionPlanId == input.Id);
            await _crisisCommitteeSectorRepository.DeleteAsync(p => p.CrisisCommittee.InterventionPlanId == input.Id);
            await _crisisCommitteeTaskRepository.DeleteAsync(p => p.CrisisCommittee.InterventionPlanId == input.Id);
            await _crisisCommitteeAgreementRepository.DeleteAsync(p => p.CrisisCommittee.InterventionPlanId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_InterventionPlan)]
        public async Task<InterventionPlanGetDataDto> Get(NullableIdDto input)
        {
            var output = new InterventionPlanGetDataDto();

            if(input.Id.HasValue)
            {
                VerifyCount(await _interventionPlanRepository.CountAsync(p => p.Id == input.Id));

                var dbInterventionPlan = _interventionPlanRepository
                    .GetAll()
                    .Include(p => p.SocialConflict)
                    .Include(p => p.SocialConflictSensible)
                    .Include(p => p.Person)
                    .Where(p => p.Id == input.Id)
                    .First();

                output.InterventionPlan = ObjectMapper.Map<InterventionPlanGetDto>(dbInterventionPlan);

                output.InterventionPlan.Locations = ObjectMapper.Map<List<InterventionPlanLocationRelationDto>>(_interventionPlanLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.InterventionPlanId == input.Id)
                    .ToList());

                output.InterventionPlan.Actors = ObjectMapper.Map<List<InterventionPlanActorRelationDto>>(_interventionPlanActorRepository
                    .GetAll()
                    .Include(p => p.ActorMovement)
                    .Include(p => p.ActorType)
                    .Where(p => p.InterventionPlanId == input.Id)
                    .ToList());

                output.InterventionPlan.States = ObjectMapper.Map<List<InterventionPlanStateRelationDto>>(_interventionPlanStateRepository
                    .GetAll()
                    .Where(p => p.InterventionPlanId == input.Id)
                    .ToList());

                output.InterventionPlan.Methodologies = ObjectMapper.Map<List<InterventionPlanMethodologyRelationDto>>(_interventionPlanMethodologyRepository
                    .GetAll()
                    .Include(p => p.InterventionPlanOption)
                    .Where(p => p.InterventionPlanId == input.Id)
                    .ToList());

                output.InterventionPlan.Risks = ObjectMapper.Map<List<InterventionPlanRiskRelationDto>>(_interventionPlanRiskRepository
                    .GetAll()
                    .Include(p => p.Risk)
                    .Where(p => p.InterventionPlanId == input.Id)
                    .ToList());

                output.InterventionPlan.Schedules = ObjectMapper.Map<List<InterventionPlanScheduleRelationDto>>(_interventionPlanScheduleRepository
                    .GetAll()
                    .Include(p => p.InterventionPlanActivity)
                    .Include(p => p.InterventionPlanEntity)
                    .Include(p => p.AlertResponsible)
                    .Include(p => p.DirectoryGovernment)
                    .Include(p => p.InterventionPlanMethodology)
                    .Where(p => p.InterventionPlanId == input.Id)
                    .ToList());

                output.InterventionPlan.Teams = ObjectMapper.Map<List<InterventionPlanTeamRelationDto>>(_interventionPlanTeamRepository
                    .GetAll()
                    .Include(p => p.InterventionPlanRole)
                    .Include(p => p.InterventionPlanEntity)
                    .Include(p => p.AlertResponsible)
                    .Include(p => p.DirectoryGovernment)
                    .Where(p => p.InterventionPlanId == input.Id)
                    .ToList());

                output.InterventionPlan.Solutions = ObjectMapper.Map<List<InterventionPlanSolutionRelationDto>>(_interventionPlanSolutionRepository
                    .GetAll()
                    .Where(p => p.InterventionPlanId == input.Id)
                    .ToList());

                var creatorUser = dbInterventionPlan.CreatorUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbInterventionPlan.CreatorUserId.Value)
                    .FirstOrDefault() : null;

                var editUser = dbInterventionPlan.LastModifierUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbInterventionPlan.LastModifierUserId.Value)
                    .FirstOrDefault() : null;

                output.InterventionPlan.CreatorUser = creatorUser == null ? null : ObjectMapper.Map<InterventionPlanUserDto>(creatorUser);
                output.InterventionPlan.EditUser = editUser == null ? null : ObjectMapper.Map<InterventionPlanUserDto>(editUser);
            }

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<InterventionPlanDepartmentDto>();

            foreach (var item in departments)
            {
                var department = new InterventionPlanDepartmentDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => p.TerritorialUnitId).ToArray(),
                    Provinces = ObjectMapper.Map<List<InterventionPlanProvinceDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.ActorMovements = ObjectMapper.Map<List<InterventionPlanActorMovementRelationDto>>(_actorMovementRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Index)
                .ToList());

            output.ActorTypes = ObjectMapper.Map<List<InterventionPlanActorTypeRelationDto>>(_actorTypeRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.Persons = ObjectMapper.Map<List<InterventionPlanPersonRelationDto>>(_personRepository
                .GetAll()
                .Where(p => p.Enabled && (p.Type == PersonType.Coordinator || p.Type == PersonType.Manager))
                .OrderBy(p => p.Name)
                .ToList());

            output.Activities = ObjectMapper.Map<List<InterventionPlanActivityRelationDto>>(_interventionPlanActivityRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.Entities = ObjectMapper.Map<List<InterventionPlanEntityRelationDto>>(_interventionPlanEntityRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.TerritorialUnits = ObjectMapper.Map<List<InterventionPlanTerritorialUnitRelationDto>>(await _territorialUnitRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .ToListAsync());

            output.Options = ObjectMapper.Map<List<InterventionPlanOptionRelationDto>>(await _interventionPlanOptionRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .ToListAsync());

            output.Risks = ObjectMapper.Map<List<InterventionPlanRiskLevelRelationDto>>(await _riskRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToListAsync());

            output.AlertResponsibles = ObjectMapper.Map<List<InterventionPlanAlertResponsibleRelationDto>>(await _alertResponsibleRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToListAsync());

            output.Roles = ObjectMapper.Map<List<InterventionPlanRoleRelationDto>>(await _interventionPlanRoleRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToListAsync());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_InterventionPlan)]
        public async Task<PagedResultDto<InterventionPlanGetAllDto>> GetAll(InterventionPlanGetAllInputDto input)
        {
            var query = _interventionPlanRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.SocialConflictSensible)
                .WhereIf(input.ValidForTerritorialUnit(), p => p.Locations.Any(d => d.TerritorialUnitId == input.TerritorialUnitId.Value))
                .WhereIf(input.ValidForDepartment(), p => p.Locations.Any(d => d.DepartmentId == input.DepartmentId.Value))
                .WhereIf(input.ValidForProvince(), p => p.Locations.Any(d => d.DistrictId == input.DistrictId.Value))
                .WhereIf(input.PersonId.HasValue, p => p.PersonId == input.PersonId.Value)
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.InterventionPlanTime >= input.StartTime.Value && p.InterventionPlanTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Code.SplitByLike(), nameof(InterventionPlan.Code))
                .LikeAllBidirectional(input.CaseName.SplitByLike(), nameof(InterventionPlan.CaseName));

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input).ToList();
            var output = new List<InterventionPlanGetAllDto>();

            foreach(var item in result)
            {
                var dbLocations = _interventionPlanLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.InterventionPlanId == item.Id)
                    .ToList()
                    .Where(p => p.TerritorialUnit != null &&
                                p.Department != null &&
                                p.Province != null &&
                                p.District != null)
                    .DistinctBy(p => p.DistrictId)
                    .ToList();

                var locations = string.Join(", ", dbLocations.Select(p => $"{p.TerritorialUnit.Name} - {p.Department.Name} - {p.Province.Name} - {p.District.Name}").ToList());
                var territorialUnits = string.Join(", ", dbLocations.DistinctBy(p => p.TerritorialUnitId).Select(p => p.TerritorialUnit.Name).ToList());

                output.Add(new InterventionPlanGetAllDto()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Year = item.Year,
                    Count = item.Count,
                    CaseName = item.CaseName,
                    InterventionPlanTime = item.InterventionPlanTime,
                    Locations = locations,
                    TerritorialUnits = territorialUnits,
                    ConflictCode = item.Site == ConflictSite.All ? null : 
                        item.Site == ConflictSite.SocialConflict ? (item.SocialConflict?.Code) :
                        item.Site == ConflictSite.SocialConflictSensible ? (item.SocialConflictSensible?.Code) : null,
                    ConflictCaseName = item.Site == ConflictSite.All ? null :
                        item.Site == ConflictSite.SocialConflict ? (item.SocialConflict?.CaseName) :
                        item.Site == ConflictSite.SocialConflictSensible ? (item.SocialConflictSensible?.CaseName) : null
                });
            }

            return new PagedResultDto<InterventionPlanGetAllDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_InterventionPlan)]
        public async Task<PagedResultDto<InterventionPlanActorRelationDto>> GetAllActorByConflict(InterventionPlanActorGetAllInputDto input)
        {
            var output = new List<InterventionPlanActorRelationDto>();

            if(input.Site == ConflictSite.SocialConflict || input.Site == ConflictSite.SocialConflictSensible)
            {
                var query = await _socialConflictActorRepository
                    .GetAll()
                    .Include(p => p.ActorMovement)
                    .Include(p => p.ActorType)
                    .WhereIf(input.Site == ConflictSite.SocialConflict, p => p.SocialConflict.Id == input.ConflictId)
                    .WhereIf(input.Site == ConflictSite.SocialConflictSensible, p => p.SocialConflictSensible.Id == input.ConflictId)
                    .ToListAsync();

                output = ObjectMapper.Map<List<InterventionPlanActorRelationDto>>(query);
            }

            return new PagedResultDto<InterventionPlanActorRelationDto>(output.Count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_InterventionPlan)]
        public async Task<PagedResultDto<InterventionPlanLocationReferenceDto>> GetAllLocationByConflict(InterventionPlanLocationGetAllInputDto input)
        {
            var output = new List<InterventionPlanLocationReferenceDto>();

            if (input.Site == ConflictSite.SocialConflict)
            {
                var query = await _socialConflictLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflict.Id == input.ConflictId)
                    .ToListAsync();

                output = ObjectMapper.Map<List<InterventionPlanLocationReferenceDto>>(query);
            }

            if (input.Site == ConflictSite.SocialConflictSensible)
            {
                var query = await _socialConflictSensibleLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflictSensible.Id == input.ConflictId)
                    .ToListAsync();

                output = ObjectMapper.Map<List<InterventionPlanLocationReferenceDto>>(query);
            }

            return new PagedResultDto<InterventionPlanLocationReferenceDto>(output.Count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_InterventionPlan_Edit)]
        public async Task Update(InterventionPlanUpdateDto input)
        {
            VerifyCount(await _interventionPlanRepository.CountAsync(p => p.Id == input.Id));

            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _interventionPlanRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var interventionPlanId = await _interventionPlanRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                    input: ObjectMapper.Map(input, await _interventionPlanRepository.GetAsync(input.Id)),
                    socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                    socialConflictSensibleId: input.SocialConflictSensible == null ? -1 : input.SocialConflictSensible.Id,
                    personId: input.Person == null ? -1 : input.Person.Id,
                    locations: input.Locations ?? new List<InterventionPlanLocationRelationDto>(),
                    actors: input.Actors ?? new List<InterventionPlanActorRelationDto>(),
                    states: input.States ?? new List<InterventionPlanStateRelationDto>(),
                    methodologies: input.Methodologies ?? new List<InterventionPlanMethodologyRelationDto>(),
                    risks: input.Risks ?? new List<InterventionPlanRiskRelationDto>(),
                    schedules: input.Schedules ?? new List<InterventionPlanScheduleRelationDto>(),
                    teams: input.Teams ?? new List<InterventionPlanTeamRelationDto>(),
                    solutions: input.Solutions ?? new List<InterventionPlanSolutionRelationDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateInterventionPlanCodeReplaceProcess(interventionPlanId, input.ReplaceYear, input.ReplaceCount);

            await FunctionManager.CallCreateInterventionPlanStateProcess(interventionPlanId);
        }

        private async Task<InterventionPlan> ValidateEntity(
            InterventionPlan input,
            int socialConflictId,
            int socialConflictSensibleId,
            int personId,
            List<InterventionPlanLocationRelationDto> locations,
            List<InterventionPlanActorRelationDto> actors,
            List<InterventionPlanStateRelationDto> states,
            List<InterventionPlanMethodologyRelationDto> methodologies,
            List<InterventionPlanRiskRelationDto> risks,
            List<InterventionPlanScheduleRelationDto> schedules,
            List<InterventionPlanTeamRelationDto> teams,
            List<InterventionPlanSolutionRelationDto> solutions)
        {
            input.CaseName.IsValidOrException(DefaultTitleMessage,
                "La denominación del plan de intervención es obligatoria");
            input.CaseName.VerifyTableColumn(
                InterventionPlanConsts.CaseNameMinLength,
                InterventionPlanConsts.CaseNameMaxLength, 
                DefaultTitleMessage, 
                $"La denominación del plan de intervención no debe exceder los {SocialConflictConsts.CaseNameMaxLength} caracteres");

            if (personId > 0)
            {
                if (await _personRepository.CountAsync(p => p.Id == personId) == 0)
                    throw new UserFriendlyException("Aviso", "La persona que elabora el plan de intervención ya no existe o fue eliminado. Verifique la información antes de continuar");

                var person = await _personRepository.GetAsync(personId);

                input.Person = person;
                input.PersonId = person.Id;
            }
            else
            {
                input.Person = null;
                input.PersonId = null;
            }

            if (socialConflictId > 0)
            {
                if (await _socialConflictRepository.CountAsync(p => p.Id == socialConflictId) == 0)
                    throw new UserFriendlyException("Aviso", "El conflicto social seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

                var socialConflict = await _socialConflictRepository.GetAsync(socialConflictId);

                input.SocialConflict = socialConflict;
                input.SocialConflictId = socialConflict.Id;
                input.Site = ConflictSite.SocialConflict;
            }
            else
            {
                input.SocialConflict = null;
                input.SocialConflictId = null;
                input.Site = ConflictSite.All;
            }

            if (socialConflictSensibleId > 0)
            {
                if (await _socialConflictRepository.CountAsync(p => p.Id == socialConflictSensibleId) == 0)
                    throw new UserFriendlyException("Aviso", "La situación sensible ya no existe o fue eliminado. Verifique la información antes de continuar");

                var socialConflictSensible = await _socialConflictSensibleRepository.GetAsync(socialConflictSensibleId);

                input.SocialConflictSensible = socialConflictSensible;
                input.SocialConflictSensibleId = socialConflictSensible.Id;
                input.Site = ConflictSite.SocialConflictSensible;
            }
            else
            {
                if (socialConflictId <= 0 && socialConflictSensibleId <= 0)
                {
                    input.SocialConflictSensible = null;
                    input.SocialConflictSensibleId = null;
                    input.Site = ConflictSite.All;
                }
            }

            input.Locations = new List<InterventionPlanLocation>();
            input.Actors = new List<InterventionPlanActor>();
            input.States = new List<InterventionPlanState>();
            input.Methodologies = new List<InterventionPlanMethodology>();
            input.Risks = new List<InterventionPlanRisk>();
            input.Schedules = new List<InterventionPlanSchedule>();
            input.Teams = new List<InterventionPlanTeam>();
            input.Solutions = new List<InterventionPlanSolution>();

            foreach (var location in locations)
            {
                if (location.Remove)
                {
                    if (location.Id > 0 && input.Id > 0 && await _interventionPlanLocationRepository.CountAsync(p => p.Id == location.Id && p.InterventionPlanId == input.Id) > 0)
                    {
                        await _interventionPlanLocationRepository.DeleteAsync(location.Id);
                    }
                }
                else
                {
                    if (location.Id <= 0)
                    {
                        if (await _districtRepository.CountAsync(p => p.Id == location.District.Id) == 0)
                            throw new UserFriendlyException(DefaultTitleMessage, $"El distrito {location.District.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        if (await _provinceRepository.CountAsync(p => p.Id == location.Province.Id) == 0)
                            throw new UserFriendlyException(DefaultTitleMessage, $"La provincia {location.Province.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        if (await _departmentRepository.CountAsync(p => p.Id == location.Department.Id) == 0)
                            throw new UserFriendlyException(DefaultTitleMessage, $"El departamento {location.Department.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        if (await _territorialUnitRepository.CountAsync(p => p.Id == location.TerritorialUnit.Id) == 0)
                            throw new UserFriendlyException(DefaultTitleMessage, $"La unidad territorial {location.TerritorialUnit.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        var territorialUnit = await _territorialUnitRepository.GetAsync(location.TerritorialUnit.Id);
                        var department = await _departmentRepository.GetAsync(location.Department.Id);
                        var province = await _provinceRepository.GetAsync(location.Province.Id);
                        var district = await _districtRepository.GetAsync(location.District.Id);
                        Region region = null;

                        if (location.Region != null)
                            region = await _regionRepository.GetAsync(location.Region.Id);

                        if (input.Locations.Where(p => p.District.Id == location.District.Id).Count() == 0)
                        {
                            location.Ubication.VerifyTableColumn(SocialConflictLocationConsts.UbicationMinLength,
                                SocialConflictLocationConsts.UbicationMaxLength,
                                DefaultTitleMessage,
                                $"La localidad - comunidad - Otros {location.Ubication} no debe exceder los {SocialConflictLocationConsts.UbicationMaxLength} caracteres");

                            input.Locations.Add(new InterventionPlanLocation()
                            {
                                TerritorialUnit = territorialUnit,
                                Department = department,
                                Province = province,
                                District = district,
                                Region = region,
                                RegionId = region == null ? (int?)null : region.Id,
                                Ubication = location.Ubication,
                                Id = 0
                            });
                        }
                    }
                }
            }

            foreach (var actor in actors)
            {
                if (actor.Remove)
                {
                    if (actor.Id > 0 && input.Id > 0 && await _interventionPlanActorRepository.CountAsync(p => p.Id == actor.Id && p.InterventionPlanId == input.Id) > 0)
                    {
                        await _interventionPlanActorRepository.DeleteAsync(actor.Id);
                    }
                }
                else
                {
                    actor.Name.IsValidOrException(DefaultTitleMessage, "El nombre del actor es obligatorio");

                    actor.Name.VerifyTableColumn(SocialConflictActorConsts.NameMinLength,
                        SocialConflictActorConsts.NameMaxLength,
                        DefaultTitleMessage,
                        $"El nombre del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.NameMaxLength} caracteres");
                    actor.Document.VerifyTableColumn(SocialConflictActorConsts.DocumentMinLength,
                        SocialConflictActorConsts.DocumentMaxLength,
                        DefaultTitleMessage,
                        $"El DNI del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.DocumentMaxLength} caracteres");
                    actor.Job.VerifyTableColumn(SocialConflictActorConsts.JobMinLength,
                        SocialConflictActorConsts.JobMaxLength,
                        DefaultTitleMessage,
                        $"El cargo del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.JobMaxLength} caracteres");

                    actor.Community.IsValidOrException(DefaultTitleMessage, $"La institución del {actor.Name} es obligatoria");
                    actor.Community.VerifyTableColumn(SocialConflictActorConsts.CommunityMinLength,
                        SocialConflictActorConsts.CommunityMaxLength,
                        DefaultTitleMessage,
                        $"La institución a la que pertenece el actor {actor.Name} no debe exceder los {SocialConflictActorConsts.CommunityMaxLength} caracteres");

                    actor.PhoneNumber.VerifyTableColumn(SocialConflictActorConsts.PhoneNumberMinLength,
                        SocialConflictActorConsts.PhoneNumberMaxLength,
                        DefaultTitleMessage,
                        $"El número de teléfono del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.PhoneNumberMaxLength} caracteres");
                    actor.EmailAddress.VerifyTableColumn(SocialConflictActorConsts.EmailAddressMinLength,
                        SocialConflictActorConsts.EmailAddressMaxLength,
                        DefaultTitleMessage,
                        $"El correo electrónico del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.EmailAddressMaxLength} caracteres");

                    if (actor.IsPoliticalAssociation)
                    {
                        actor.PoliticalAssociation.VerifyTableColumn(SocialConflictActorConsts.PoliticalAssociationMinLength,
                            SocialConflictActorConsts.PoliticalAssociationMaxLength,
                            DefaultTitleMessage,
                            $"El nombre del partido político al que pertenece el actor {actor.Name} no debe exceder los {SocialConflictActorConsts.PoliticalAssociationMaxLength} caracteres");
                    }

                    if (await _actorTypeRepository.CountAsync(p => p.Id == actor.ActorType.Id) == 0)
                        throw new UserFriendlyException(DefaultTitleMessage, $"El tipo de actor {actor.ActorType.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    var dbActorType = await _actorTypeRepository.GetAsync(actor.ActorType.Id);
                    ActorMovement dbActorMovement = null;

                    if (dbActorType.ShowMovement)
                    {
                        if (actor.ActorMovement.Id == -1)
                            throw new UserFriendlyException("Aviso", $"La capacidad de movilización del actor {actor.Name} es obligatoria");

                        if (await _actorMovementRepository.CountAsync(p => p.Id == actor.ActorMovement.Id) == 0)
                            throw new UserFriendlyException(DefaultTitleMessage, $"La capacidad de movilización {actor.ActorMovement.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        dbActorMovement = await _actorMovementRepository.GetAsync(actor.ActorMovement.Id);
                    }

                    if (dbActorType.ShowDetail)
                    {
                        actor.Position.VerifyTableColumn(SocialConflictActorConsts.PositionMinLength,
                            SocialConflictActorConsts.PositionMaxLength,
                            DefaultTitleMessage,
                            $"La posición del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.PositionMaxLength} caracteres");
                        actor.Interest.VerifyTableColumn(SocialConflictActorConsts.InterestMinLength,
                            SocialConflictActorConsts.InterestMaxLength,
                            DefaultTitleMessage,
                            $"El interés del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.InterestMaxLength} caracteres");
                    }

                    if (actor.Id > 0)
                    {
                        if (await _interventionPlanActorRepository.CountAsync(p => p.Id == actor.Id && p.InterventionPlanId == input.Id) > 0)
                        {
                            var dbActor = await _interventionPlanActorRepository.GetAsync(actor.Id);
                            dbActor.Name = actor.Name;
                            dbActor.Document = actor.Document;
                            dbActor.Job = actor.Job;
                            dbActor.Community = actor.Community;
                            dbActor.PhoneNumber = actor.PhoneNumber;
                            dbActor.EmailAddress = actor.EmailAddress;
                            dbActor.IsPoliticalAssociation = actor.IsPoliticalAssociation;
                            dbActor.PoliticalAssociation = actor.IsPoliticalAssociation ? actor.PoliticalAssociation : null;
                            dbActor.Position = dbActorType.ShowDetail ? actor.Position : null;
                            dbActor.Interest = dbActorType.ShowDetail ? actor.Interest : null;
                            dbActor.ActorTypeId = dbActorType.Id;
                            dbActor.ActorType = dbActorType;
                            dbActor.ActorMovementId = dbActorMovement == null ? (int?)null : dbActorMovement.Id;
                            dbActor.ActorMovement = dbActorMovement;

                            await _interventionPlanActorRepository.UpdateAsync(dbActor);
                        }
                    }
                    else
                    {
                        input.Actors.Add(new InterventionPlanActor()
                        {
                            Name = actor.Name,
                            Document = actor.Document,
                            Job = actor.Job,
                            Community = actor.Community,
                            PhoneNumber = actor.PhoneNumber,
                            EmailAddress = actor.EmailAddress,
                            IsPoliticalAssociation = actor.IsPoliticalAssociation,
                            PoliticalAssociation = actor.IsPoliticalAssociation ? actor.PoliticalAssociation : null,
                            Position = dbActorType.ShowDetail ? actor.Position : null,
                            Interest = dbActorType.ShowDetail ? actor.Interest : null,
                            ActorTypeId = dbActorType.Id,
                            ActorType = dbActorType,
                            ActorMovementId = dbActorMovement == null ? (int?)null : dbActorMovement.Id,
                            ActorMovement = dbActorMovement,
                            Imported = actor.Imported,
                            ImportedId = actor.ImportedId
                        });
                    }
                }
            }

            foreach (var state in states)
            {
                if (state.Remove)
                {
                    if (state.Id > 0 && input.Id > 0 && await _interventionPlanStateRepository.CountAsync(p => p.Id == state.Id && p.InterventionPlanId == input.Id) > 0)
                    {
                        await _interventionPlanStateRepository.DeleteAsync(state.Id);
                    }
                }
                else
                {
                    state.Description.IsValidOrException("Aviso", "La descripción de las situaciones actuales son obligatiorias");
                    state.Description.VerifyTableColumn(InterventionPlanStateConsts.DescriptionMinLength,
                        InterventionPlanStateConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la situación actual {state.Description} no debe exceder los " +
                        $"{InterventionPlanStateConsts.DescriptionMaxLength} caracteres");

                    if (state.Id > 0)
                    {
                        if (await _interventionPlanStateRepository.CountAsync(p => p.Id == state.Id && p.InterventionPlanId == input.Id) > 0)
                        {
                            var dbState = await _interventionPlanStateRepository.GetAsync(state.Id);

                            dbState.Description = state.Description;

                            await _interventionPlanStateRepository.UpdateAsync(dbState);
                        }
                    }
                    else
                    {
                        input.States.Add(new InterventionPlanState()
                        {
                            Description = state.Description
                        });
                    }
                }
            }

            foreach (var methodology in methodologies)
            {
                if (methodology.Remove)
                {
                    if (methodology.Id > 0 && input.Id > 0 && await _interventionPlanMethodologyRepository.CountAsync(p => p.Id == methodology.Id && p.InterventionPlanId == input.Id) > 0)
                    {
                        await _interventionPlanMethodologyRepository.DeleteAsync(methodology.Id);
                    }
                }
                else
                {
                    methodology.Methodology.IsValidOrException("Aviso", "El objetivo de los objetivos son obligatiorios");
                    methodology.Methodology.VerifyTableColumn(InterventionPlanMethodologyConsts.MethodologyMinLength,
                        InterventionPlanMethodologyConsts.MethodologyMaxLength,
                        DefaultTitleMessage,
                        $"El objetivo {methodology.Methodology} no debe exceder los " +
                        $"{InterventionPlanMethodologyConsts.MethodologyMaxLength} caracteres");

                    methodology.Description.VerifyTableColumn(InterventionPlanMethodologyConsts.DescriptionMinLength,
                        InterventionPlanMethodologyConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"El resultado esperado {methodology.Methodology} de los objetivosb no debe exceder los " +
                        $"{InterventionPlanMethodologyConsts.DescriptionMaxLength} caracteres");

                    var option = _interventionPlanOptionRepository
                        .GetAll()
                        .Where(p => p.Id == methodology.InterventionPlanOption.Id)
                        .FirstOrDefault();

                    if(option != null)
                    {
                        if (methodology.Id > 0)
                        {
                            if (await _interventionPlanMethodologyRepository.CountAsync(p => p.Id == methodology.Id && p.InterventionPlanId == input.Id) > 0)
                            {
                                var dbMethodology = await _interventionPlanMethodologyRepository.GetAsync(methodology.Id);

                                dbMethodology.Description = methodology.Description;
                                dbMethodology.Methodology = methodology.Methodology;
                                dbMethodology.InterventionPlanOption = option;
                                dbMethodology.InterventionPlanOptionId = option.Id;

                                await _interventionPlanMethodologyRepository.UpdateAsync(dbMethodology);
                            }
                        }
                        else
                        {
                            input.Methodologies.Add(new InterventionPlanMethodology()
                            {
                                Description = methodology.Description,
                                Methodology = methodology.Methodology,
                                InterventionPlanOption = option,
                                InterventionPlanOptionId = option.Id
                            });
                        }
                    }
                }
            }

            foreach (var risk in risks)
            {
                if (risk.Remove)
                {
                    if (risk.Id > 0 && input.Id > 0 && await _interventionPlanRiskRepository.CountAsync(p => p.Id == risk.Id && p.InterventionPlanId == input.Id) > 0)
                    {
                        await _interventionPlanRiskRepository.DeleteAsync(risk.Id);
                    }
                }
                else
                {
                    var dbRisk = _riskRepository
                        .GetAll()
                        .Where(p => p.Id == risk.Risk.Id)
                        .FirstOrDefault();

                    if (dbRisk == null)
                        throw new UserFriendlyException("Aviso", $"El nivel de riesgo {risk.Risk.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (risk.Id > 0)
                    {
                        if (await _interventionPlanRiskRepository.CountAsync(p => p.Id == risk.Id && p.InterventionPlanId == input.Id) > 0)
                        {
                            var dbInterventionPlanRisk = await _interventionPlanRiskRepository.GetAsync(risk.Id);

                            dbInterventionPlanRisk.RiskId = dbRisk.Id;
                            dbInterventionPlanRisk.Risk = dbRisk;

                            await _interventionPlanRiskRepository.UpdateAsync(dbInterventionPlanRisk);
                        }
                    }
                    else
                    {
                        input.Risks.Add(new InterventionPlanRisk()
                        {
                            RiskId = dbRisk.Id,
                            Risk = dbRisk
                        });
                    }
                }
            }

            foreach (var schedule in schedules)
            {

                if (schedule.Remove)
                {
                    if (schedule.Id > 0 && input.Id > 0 && await _interventionPlanScheduleRepository.CountAsync(p => p.Id == schedule.Id && p.InterventionPlanId == input.Id) > 0)
                    {
                        await _interventionPlanScheduleRepository.DeleteAsync(schedule.Id);
                    }
                }
                else
                {
                    var dbInterventionPlanActivity = _interventionPlanActivityRepository
                       .GetAll()
                       .Where(p => p.Id == schedule.InterventionPlanActivity.Id)
                       .FirstOrDefault();

                    if (dbInterventionPlanActivity == null)
                        throw new UserFriendlyException("Aviso", $"El tipo de actividad {schedule.InterventionPlanActivity.Name} de las actividades ya no existe o fue eliminado. Verifique la información antes de continuar");

                    var dbInterventionPlanEntity = _interventionPlanEntityRepository
                       .GetAll()
                       .Where(p => p.Id == schedule.InterventionPlanEntity.Id)
                       .FirstOrDefault();

                    if (dbInterventionPlanEntity == null)
                        throw new UserFriendlyException("Aviso", $"El tipo de entidad {schedule.InterventionPlanEntity.Name} de las actividades ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (dbInterventionPlanActivity.ShowDescription)
                    {
                        schedule.Activity.IsValidOrException(DefaultTitleMessage, $"La descripción del tipo de actividad {schedule.InterventionPlanActivity.Name} de las actividades es obligatoria");

                        schedule.Activity.VerifyTableColumn(InterventionPlanScheduleConsts.ActivityMinLength,
                            InterventionPlanScheduleConsts.ActivityMaxLength,
                            DefaultTitleMessage,
                            $"La descripción de tipo de actividad {schedule.Activity} de las actividades no debe exceder los " +
                            $"{InterventionPlanScheduleConsts.ActivityMaxLength} caracteres");
                    }
                    else
                    {
                        schedule.Activity = null;
                    }

                    if (dbInterventionPlanEntity.Type == InterventionPlanEntityType.None)
                        throw new UserFriendlyException("Aviso", $"El tipo de entidad {dbInterventionPlanEntity.Name} de las actividades es inválido, por favor seleccione otro");

                    DirectoryGovernment dbDirectoryGovernment = null;

                    if (dbInterventionPlanEntity.Type == InterventionPlanEntityType.Sector)
                    {
                        dbDirectoryGovernment = _directoryGovernmentRepository
                            .GetAll()
                            .Where(p => p.Id == schedule.DirectoryGovernment.Id)
                            .FirstOrDefault();

                        if (dbDirectoryGovernment == null)
                            throw new UserFriendlyException("Aviso", $"La entidad responsable {schedule.DirectoryGovernment.Name} de las actividades ya no existe o fue eliminado. Verifique la información antes de continuar");
                        
                        schedule.Entity = null;
                    }

                    AlertResponsible dbAlertResponsible = null;

                    if (dbInterventionPlanEntity.Type == InterventionPlanEntityType.Responsible)
                    {
                        dbAlertResponsible = _alertResponsibleRepository
                            .GetAll()
                            .Where(p => p.Id == schedule.AlertResponsible.Id)
                            .FirstOrDefault();

                        if (dbAlertResponsible == null)
                            throw new UserFriendlyException("Aviso", $"La entidad responsable {schedule.AlertResponsible.Name} de las actividades ya no existe o fue eliminado. Verifique la información antes de continuar");

                        schedule.Entity = null;
                    }

                    if (dbInterventionPlanEntity.Type == InterventionPlanEntityType.Other)
                    {
                        schedule.Entity.IsValidOrException(DefaultTitleMessage, $"La descripción del tipo de entidad {schedule.InterventionPlanEntity.Name} de las actividades es obligatoria");

                        schedule.Entity.VerifyTableColumn(InterventionPlanScheduleConsts.EntityMinLength,
                            InterventionPlanScheduleConsts.EntityMaxLength,
                            DefaultTitleMessage,
                            $"La descripción de tipo de entidad {schedule.Entity} de las actividades no debe exceder los " +
                            $"{InterventionPlanScheduleConsts.EntityMaxLength} caracteres");
                    }

                    schedule.Product.IsValidOrException(DefaultTitleMessage, $"La descripción del producto de las actividades {schedule.InterventionPlanEntity.Name} es obligatoria");

                    schedule.Product.VerifyTableColumn(InterventionPlanScheduleConsts.ProductMinLength,
                        InterventionPlanScheduleConsts.ProductMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del producto de las actividades {schedule.InterventionPlanEntity.Name} no debe exceder los " +
                        $"{InterventionPlanScheduleConsts.ProductMaxLength} caracteres");

                    var dbMetholody = _interventionPlanMethodologyRepository
                        .GetAll()
                        .Where(p => p.Id == schedule.InterventionPlanMethodology.Id)
                        .FirstOrDefault();

                    if (schedule.Id > 0)
                    {
                        if (await _interventionPlanScheduleRepository.CountAsync(p => p.Id == schedule.Id && p.InterventionPlanId == input.Id) > 0)
                        {
                            var dbInterventionPlanSchedule = await _interventionPlanScheduleRepository.GetAsync(schedule.Id);

                            dbInterventionPlanSchedule.Schedule = schedule.Schedule;
                            dbInterventionPlanSchedule.ScheduleTime = schedule.ScheduleTime;
                            dbInterventionPlanSchedule.InterventionPlanActivityId = dbInterventionPlanActivity.Id;
                            dbInterventionPlanSchedule.InterventionPlanActivity = dbInterventionPlanActivity;
                            dbInterventionPlanSchedule.Activity = schedule.Activity;
                            dbInterventionPlanSchedule.InterventionPlanEntityId = dbInterventionPlanEntity.Id;
                            dbInterventionPlanSchedule.InterventionPlanEntity = dbInterventionPlanEntity;
                            dbInterventionPlanSchedule.AlertResponsibleId = dbAlertResponsible == null ? (int?)null : dbAlertResponsible.Id;
                            dbInterventionPlanSchedule.AlertResponsible = dbAlertResponsible;
                            dbInterventionPlanSchedule.DirectoryGovernmentId = dbDirectoryGovernment == null ? (int?)null : dbDirectoryGovernment.Id;
                            dbInterventionPlanSchedule.DirectoryGovernment = dbDirectoryGovernment;
                            dbInterventionPlanSchedule.Entity = schedule.Entity;
                            dbInterventionPlanSchedule.Product = schedule.Product;
                            dbInterventionPlanSchedule.InterventionPlanMethodologyId = dbMetholody == null ? (int?)null : dbMetholody.Id;

                            await _interventionPlanScheduleRepository.UpdateAsync(dbInterventionPlanSchedule);
                        }
                    }
                    else
                    {
                        input.Schedules.Add(new InterventionPlanSchedule()
                        {
                            Schedule = schedule.Schedule,
                            ScheduleTime = schedule.ScheduleTime,
                            InterventionPlanActivityId = dbInterventionPlanActivity.Id,
                            InterventionPlanActivity = dbInterventionPlanActivity,
                            Activity = schedule.Activity,
                            InterventionPlanEntityId = dbInterventionPlanEntity.Id,
                            InterventionPlanEntity = dbInterventionPlanEntity,
                            AlertResponsibleId = dbAlertResponsible == null ? (int?)null : dbAlertResponsible.Id,
                            AlertResponsible = dbAlertResponsible,
                            DirectoryGovernmentId = dbDirectoryGovernment == null ? (int?)null : dbDirectoryGovernment.Id,
                            DirectoryGovernment = dbDirectoryGovernment,
                            Entity = schedule.Entity,
                            Product = schedule.Product,
                            InterventionPlanMethodologyId = dbMetholody == null ? (int?)null : dbMetholody.Id
                        });
                    }
                }
            }

            foreach (var team in teams)
            {

                if (team.Remove)
                {
                    if (team.Id > 0 && input.Id > 0 && await _interventionPlanTeamRepository.CountAsync(p => p.Id == team.Id && p.InterventionPlanId == input.Id) > 0)
                    {
                        await _interventionPlanTeamRepository.DeleteAsync(team.Id);
                    }
                }
                else
                {
                    team.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de todos los registros del equipo de intervención es obligatorio");
                    team.Name.VerifyTableColumn(InterventionPlanTeamConsts.NameMinLength,
                        InterventionPlanTeamConsts.NameMaxLength,
                        DefaultTitleMessage,
                        $"El nombre {team.Name} del equipo de intervención no debe exceder los " +
                        $"{InterventionPlanTeamConsts.NameMaxLength} caracteres");

                    team.Surname.IsValidOrException(DefaultTitleMessage, $"El apellido paterno de todos los registros del equipo de intervención es obligatorio");
                    team.Surname.VerifyTableColumn(InterventionPlanTeamConsts.SurnameMinLength,
                        InterventionPlanTeamConsts.SurnameMaxLength,
                        DefaultTitleMessage,
                        $"El apellido paterno {team.Name} del equipo de intervención no debe exceder los " +
                        $"{InterventionPlanTeamConsts.SurnameMaxLength} caracteres");

                    team.SecondSurname.VerifyTableColumn(InterventionPlanTeamConsts.SecondSurnameMinLength,
                        InterventionPlanTeamConsts.SecondSurnameMaxLength,
                        DefaultTitleMessage,
                        $"El apellido materno {team.Name} del equipo de intervención no debe exceder los " +
                        $"{InterventionPlanTeamConsts.SecondSurnameMaxLength} caracteres");

                    team.Job.IsValidOrException(DefaultTitleMessage, $"El cargo de todos los registros del equipo de intervención es obligatorio");
                    team.Job.VerifyTableColumn(InterventionPlanTeamConsts.JobMinLength,
                        InterventionPlanTeamConsts.JobMaxLength,
                        DefaultTitleMessage,
                        $"El cargo del integrante {team.Name} del equipo de intervención no debe exceder los " +
                        $"{InterventionPlanTeamConsts.JobMaxLength} caracteres");

                    var dbInterventionPlanRole = _interventionPlanRoleRepository
                       .GetAll()
                       .Where(p => p.Id == team.InterventionPlanRole.Id)
                       .FirstOrDefault();

                    if (dbInterventionPlanRole == null)
                        throw new UserFriendlyException("Aviso", $"El rol {team.InterventionPlanRole.Name} seleccionado en el equipo de intervención ya no existe o fue eliminado. Verifique la información antes de continuar");

                    var dbInterventionPlanEntity = _interventionPlanEntityRepository
                       .GetAll()
                       .Where(p => p.Id == team.InterventionPlanEntity.Id)
                       .FirstOrDefault();

                    if (dbInterventionPlanEntity == null)
                        throw new UserFriendlyException("Aviso", $"El tipo de entidad {team.InterventionPlanEntity.Name} del equipo de intervención ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (dbInterventionPlanRole.ShowDescription)
                    {
                        team.Role.IsValidOrException(DefaultTitleMessage, $"La descripción del rol {team.InterventionPlanRole.Name} del equipo de intervención es obligatoria");

                        team.Role.VerifyTableColumn(InterventionPlanTeamConsts.RoleMinLength,
                            InterventionPlanTeamConsts.RoleMaxLength,
                            DefaultTitleMessage,
                            $"La descripción del rol {team.Role} del equipo de intervención no debe exceder los " +
                            $"{InterventionPlanTeamConsts.RoleMaxLength} caracteres");
                    }
                    else
                    {
                        team.Role = null;
                    }

                    if (dbInterventionPlanEntity.Type == InterventionPlanEntityType.None)
                        throw new UserFriendlyException("Aviso", $"El tipo de entidad {dbInterventionPlanEntity.Name} del equipo de intervención es inválido, por favor seleccione otro");

                    DirectoryGovernment dbDirectoryGovernment = null;

                    if (dbInterventionPlanEntity.Type == InterventionPlanEntityType.Sector)
                    {
                        dbDirectoryGovernment = _directoryGovernmentRepository
                            .GetAll()
                            .Where(p => p.Id == team.DirectoryGovernment.Id)
                            .FirstOrDefault();

                        if (dbDirectoryGovernment == null)
                            throw new UserFriendlyException("Aviso", $"La entidad responsable {team.DirectoryGovernment.Name} del equipo de intervención ya no existe o fue eliminado. Verifique la información antes de continuar");

                        team.Entity = null;
                    }

                    AlertResponsible dbAlertResponsible = null;

                    if (dbInterventionPlanEntity.Type == InterventionPlanEntityType.Responsible)
                    {
                        dbAlertResponsible = _alertResponsibleRepository
                            .GetAll()
                            .Where(p => p.Id == team.AlertResponsible.Id)
                            .FirstOrDefault();

                        if (dbAlertResponsible == null)
                            throw new UserFriendlyException("Aviso", $"La entidad responsable {team.AlertResponsible.Name} del equipo de intervención ya no existe o fue eliminado. Verifique la información antes de continuar");

                        team.Entity = null;
                    }

                    if (dbInterventionPlanEntity.Type == InterventionPlanEntityType.Other)
                    {
                        team.Entity.IsValidOrException(DefaultTitleMessage, $"La descripción del tipo de entidad {team.InterventionPlanEntity.Name} del equipo de intervención de actividades es obligatoria");

                        team.Entity.VerifyTableColumn(InterventionPlanTeamConsts.EntityMinLength,
                            InterventionPlanTeamConsts.EntityMaxLength,
                            DefaultTitleMessage,
                            $"La descripción de tipo de entidad {team.Entity} del equipo de intervención no debe exceder los " +
                            $"{InterventionPlanTeamConsts.EntityMaxLength} caracteres");
                    }

                    if (team.Id > 0)
                    {
                        if (await _interventionPlanTeamRepository.CountAsync(p => p.Id == team.Id && p.InterventionPlanId == input.Id) > 0)
                        {
                            var dbInterventionPlanTeam = await _interventionPlanTeamRepository.GetAsync(team.Id);

                            dbInterventionPlanTeam.Document = team.Document;
                            dbInterventionPlanTeam.Name = team.Name;
                            dbInterventionPlanTeam.Surname = team.Surname;
                            dbInterventionPlanTeam.SecondSurname = team.SecondSurname;
                            dbInterventionPlanTeam.InterventionPlanEntityId = dbInterventionPlanEntity.Id;
                            dbInterventionPlanTeam.InterventionPlanEntity = dbInterventionPlanEntity;
                            dbInterventionPlanTeam.AlertResponsibleId = dbAlertResponsible == null ? (int?)null : dbAlertResponsible.Id;
                            dbInterventionPlanTeam.AlertResponsible = dbAlertResponsible;
                            dbInterventionPlanTeam.DirectoryGovernmentId = dbDirectoryGovernment == null ? (int?)null : dbDirectoryGovernment.Id;
                            dbInterventionPlanTeam.DirectoryGovernment = dbDirectoryGovernment;
                            dbInterventionPlanTeam.Entity = team.Entity;
                            dbInterventionPlanTeam.Job = team.Job;
                            dbInterventionPlanTeam.InterventionPlanRoleId = dbInterventionPlanRole.Id;
                            dbInterventionPlanTeam.InterventionPlanRole = dbInterventionPlanRole;
                            dbInterventionPlanTeam.Role = team.Role;

                            await _interventionPlanTeamRepository.UpdateAsync(dbInterventionPlanTeam);
                        }
                    }
                    else
                    {
                        input.Teams.Add(new InterventionPlanTeam()
                        {
                            Document = team.Document,
                            Name = team.Name,
                            Surname = team.Surname,
                            SecondSurname = team.SecondSurname,
                            InterventionPlanEntityId = dbInterventionPlanEntity.Id,
                            InterventionPlanEntity = dbInterventionPlanEntity,
                            AlertResponsibleId = dbAlertResponsible == null ? (int?)null : dbAlertResponsible.Id,
                            AlertResponsible = dbAlertResponsible,
                            DirectoryGovernmentId = dbDirectoryGovernment == null ? (int?)null : dbDirectoryGovernment.Id,
                            DirectoryGovernment = dbDirectoryGovernment,
                            Entity = team.Entity,
                            Job = team.Job,
                            InterventionPlanRoleId = dbInterventionPlanRole.Id,
                            InterventionPlanRole = dbInterventionPlanRole,
                            Role = team.Role
                        });
                    }
                }
            }

            foreach (var solution in solutions)
            {
                if (solution.Remove)
                {
                    if (solution.Id > 0 && input.Id > 0 && await _interventionPlanSolutionRepository.CountAsync(p => p.Id == solution.Id && p.InterventionPlanId == input.Id) > 0)
                    {
                        await _interventionPlanSolutionRepository.DeleteAsync(solution.Id);
                    }
                }
                else
                {
                    solution.Description.IsValidOrException("Aviso", "La descripción de las evaluaciones de resultados son obligatiorias");
                    solution.Description.VerifyTableColumn(InterventionPlanSolutionConsts.DescriptionMinLength,
                        InterventionPlanSolutionConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de las evaluaciones de resultados {solution.Description} no debe exceder los " +
                        $"{InterventionPlanSolutionConsts.DescriptionMaxLength} caracteres");

                    if (solution.Id > 0)
                    {
                        if (await _interventionPlanSolutionRepository.CountAsync(p => p.Id == solution.Id && p.InterventionPlanId == input.Id) > 0)
                        {
                            var dbSolution = await _interventionPlanSolutionRepository.GetAsync(solution.Id);

                            dbSolution.Description = solution.Description;

                            await _interventionPlanSolutionRepository.UpdateAsync(dbSolution);
                        }
                    }
                    else
                    {
                        input.Solutions.Add(new InterventionPlanSolution()
                        {
                            Description = solution.Description
                        });
                    }
                }
            }

            return input;
        }
    }
}
