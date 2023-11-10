using Abp.Linq.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using System.Linq.Dynamic.Core;
using System.Linq;
using Contable.Application.SocialConflictSensibles;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using Contable.Application.SocialConflictSensibles.Dto;
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
using Contable.Dto;
using Contable.Application.Exporting;
using Contable.Application.SocialConflicts.Dto;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictSensible)]
    public class SocialConflictSensibleAppService : ContableAppServiceBase, ISocialConflictSensibleAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Region> _regionRepository; 
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<TerritorialUnitDepartment> _territorialUnitDepartmentRepository;
        private readonly IRepository<SocialConflictSensible> _socialConflictSensibleRepository;
        private readonly IRepository<SocialConflictSensibleLocation> _socialConflictSensibleLocationRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Record, long> _recordRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Typology> _typologyRepository;
        private readonly IRepository<Fact> _factRepository;
        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<ActorType> _actorTypeRepository;
        private readonly IRepository<ActorMovement> _actorMovementRepository;
        private readonly IRepository<Risk> _riskRepository;
        private readonly IRepository<Management> _managementRepository; 
        private readonly IRepository<SocialConflictActor> _socialConflictSensibleActorRepository;
        private readonly IRepository<SocialConflictSensibleRisk> _socialConflictSensibleRiskRepository;
        private readonly IRepository<SocialConflictSensibleGeneralFact> _socialConflictSensibleGeneralFactRepository;
        private readonly IRepository<SocialConflictSensibleSugerence> _socialConflictSensibleSugerenceRepository;
        private readonly IRepository<SocialConflictSensibleManagement> _socialConflictSensibleManagementRepository;
        private readonly IRepository<SocialConflictSensibleManagementResource> _socialConflictSensibleManagementResourceRepository;
        private readonly IRepository<SocialConflictSensibleState> _socialConflictSensibleStateRepository;
        private readonly IRepository<SocialConflictSensibleCondition> _socialConflictSensibleConditionRepository;
        private readonly IRepository<SocialConflictSensibleResource> _socialConflictSensibleResourceRepository;
        private readonly IRepository<SocialConflictSensibleNote> _socialConflictSensibleNoteRepository;
        private readonly ISocialConflictSensibleExcelExporter _socialConflictSensibleExcelExporter;

        public SocialConflictSensibleAppService(
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<District> districtRepository,
            IRepository<Region> regionRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<SocialConflictSensible> socialConflictSensibleRepository,
            IRepository<SocialConflictSensibleLocation> socialConflictSensibleLocationRepository,
            IRepository<User, long> userRepository,
            IRepository<Record, long> recordRepository,
            IRepository<TerritorialUnitDepartment> territorialUnitDepartmentRepository,
            IRepository<Person> personRepository,
            IRepository<Typology> typologyRepository,
            IRepository<Fact> factRepository,
            IRepository<Actor> actorRepository,
            IRepository<ActorType> actorTypeRepository,
            IRepository<ActorMovement> actorMovementRepository,
            IRepository<Risk> riskRepository,
            IRepository<Management> managementRepository,
            IRepository<SocialConflictActor> socialConflictSensibleActorRepository,
            IRepository<SocialConflictSensibleRisk> socialConflictSensibleRiskRepository,
            IRepository<SocialConflictSensibleGeneralFact> socialConflictSensibleGeneralFactRepository,
            IRepository<SocialConflictSensibleSugerence> socialConflictSensibleSugerenceRepository,
            IRepository<SocialConflictSensibleManagement> socialConflictSensibleManagementRepository,
            IRepository<SocialConflictSensibleManagementResource> socialConflictSensibleManagementResourceRepository,
            IRepository<SocialConflictSensibleState> socialConflictSensibleStateRepository,
            IRepository<SocialConflictSensibleCondition> socialConflictSensibleConditionRepository,
            IRepository<SocialConflictSensibleResource> socialConflictSensibleResourceRepository,
            IRepository<SocialConflictSensibleNote> socialConflictSensibleNoteRepository,
            ISocialConflictSensibleExcelExporter socialConflictSensibleExcelExporter)
        {
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _regionRepository = regionRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _socialConflictSensibleRepository = socialConflictSensibleRepository;
            _socialConflictSensibleLocationRepository = socialConflictSensibleLocationRepository;
            _userRepository = userRepository;
            _territorialUnitDepartmentRepository = territorialUnitDepartmentRepository;
            _recordRepository = recordRepository;
            _personRepository = personRepository;
            _typologyRepository = typologyRepository;
            _factRepository = factRepository;
            _actorTypeRepository = actorTypeRepository;
            _actorMovementRepository = actorMovementRepository;
            _riskRepository = riskRepository;
            _managementRepository = managementRepository;
            _socialConflictSensibleActorRepository = socialConflictSensibleActorRepository;
            _socialConflictSensibleRiskRepository = socialConflictSensibleRiskRepository;
            _socialConflictSensibleGeneralFactRepository = socialConflictSensibleGeneralFactRepository;
            _socialConflictSensibleSugerenceRepository = socialConflictSensibleSugerenceRepository;
            _socialConflictSensibleManagementRepository = socialConflictSensibleManagementRepository;
            _socialConflictSensibleManagementResourceRepository = socialConflictSensibleManagementResourceRepository;
            _socialConflictSensibleStateRepository = socialConflictSensibleStateRepository;
            _socialConflictSensibleConditionRepository = socialConflictSensibleConditionRepository;
            _socialConflictSensibleResourceRepository = socialConflictSensibleResourceRepository;
            _socialConflictSensibleNoteRepository = socialConflictSensibleNoteRepository;
            _socialConflictSensibleExcelExporter = socialConflictSensibleExcelExporter;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictSensible_Create)]
        public async Task<EntityDto> Create(SocialConflictSensibleCreateDto input)
        {
            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _socialConflictSensibleRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var socialConflictSensibleId = await _socialConflictSensibleRepository.InsertAndGetIdAsync(await ValidateEntity(
                    input: ObjectMapper.Map<SocialConflictSensible>(input),
                    verification: ObjectMapper.Map<SocialConflictSensibleVerificationRequestDto>(input),
                    analystId: input.Analyst == null ? -1 : input.Analyst.Id,
                    coordinatorId: input.Coordinator == null ? -1 : input.Coordinator.Id,
                    managerId: input.Manager == null ? -1 : input.Manager.Id,
                    typologyId: input.Typology == null ? -1 : input.Typology.Id,
                    locations: input.Locations,
                    actors: input.Actors,
                    risks: input.Risks,
                    generalFacts: input.GeneralFacts,
                    sugerences: input.Sugerences,
                    managements: input.Managements,
                    states: input.States,
                    conditions: input.Conditions,
                    resources: new List<SocialConflictSensibleResourceDto>(),
                    notes: new List<SocialConflictSensibleNoteLocationDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateSocialConflictSensibleCodeReplaceProcess(socialConflictSensibleId, input.ReplaceYear, input.ReplaceCount);
            else
                await FunctionManager.CallCreateSocialConflictSensibleCodeProcess(socialConflictSensibleId);

            await FunctionManager.CallSocialConflictSensibleStateProcess(socialConflictSensibleId);
            await FunctionManager.CallSocialConflictSensibleVerificationProccess(socialConflictSensibleId);

            return new EntityDto(socialConflictSensibleId);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictSensible_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _socialConflictSensibleRepository.CountAsync(p => p.Id == input.Id));
            await _socialConflictSensibleRepository.DeleteAsync(input.Id);
            await _socialConflictSensibleLocationRepository.DeleteAsync(p => p.SocialConflictSensible.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictSensible)]
        public async Task<SocialConflictSensibleGetDataDto> Get(NullableIdDto input)
        {
            var output = new SocialConflictSensibleGetDataDto
            {
                SocialConflictSensible = new SocialConflictSensibleGetDto()
            };

            if (input.Id.HasValue)
            {
                VerifyCount(await _socialConflictSensibleRepository.CountAsync(p => p.Id == input.Id));

                var socialConflictSensible = _socialConflictSensibleRepository
                    .GetAll()
                    .Include(p => p.Analyst)
                    .Include(p => p.Coordinator)
                    .Include(p => p.Manager)
                    .Include(p => p.Typology)
                    .Include(p => p.Resources)
                    .Include(p => p.Notes)
                    .Where(p => p.Id == input.Id)
                    .First();

                var socialConflictSensibleItem = ObjectMapper.Map<SocialConflictSensibleGetDto>(socialConflictSensible);

                socialConflictSensibleItem.Locations = ObjectMapper.Map<List<SocialConflictSensibleLocationDto>>(_socialConflictSensibleLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflictSensible.Id == input.Id)
                    .ToList());

                socialConflictSensibleItem.Actors = ObjectMapper.Map<List<SocialConflictSensibleActorLocationDto>>(_socialConflictSensibleActorRepository
                    .GetAll()
                    .Include(p => p.ActorMovement)
                    .Include(p => p.ActorType)
                    .Where(p => p.SocialConflictSensibleId == input.Id)
                    .ToList());

                socialConflictSensibleItem.Risks = ObjectMapper.Map<List<SocialConflictSensibleRiskLocationDto>>(_socialConflictSensibleRiskRepository
                    .GetAll()
                    .Include(p => p.Risk)
                    .Where(p => p.SocialConflictSensibleId == input.Id)
                    .ToList());

                socialConflictSensibleItem.GeneralFacts = ObjectMapper.Map<List<SocialConflictSensibleGeneralFactDto>>(_socialConflictSensibleGeneralFactRepository
                    .GetAll()
                    .Where(p => p.SocialConflictSensibleId == input.Id)
                    .ToList());

                socialConflictSensibleItem.Sugerences =
                    (from sugerence in _socialConflictSensibleSugerenceRepository.GetAll().Where(p => p.SocialConflictSensibleId == input.Id)
                     join userCreation in _userRepository.GetAll() on sugerence.CreatorUserId equals userCreation.Id
                     into userResult
                     from resultA in userResult.DefaultIfEmpty()
                     join userAccept in _userRepository.GetAll() on sugerence.AcceptedUserId equals userAccept.Id
                     into userAcceptResult
                     from resultB in userAcceptResult.DefaultIfEmpty()
                     select new SocialConflictSensibleSugerenceDto()
                     {
                         CreatorUser = new SocialConflictSensibleUserDto()
                         {
                             Name = resultA == null ? "N/A" : resultA.Name,
                             Surname = resultA == null ? "" : resultA.Surname,
                             EmailAddress = resultA == null ? "" : resultA.EmailAddress
                         },
                         Id = sugerence.Id,
                         Description = sugerence.Description,
                         CreationTime = sugerence.CreationTime,
                         Remove = false,
                         Accepted = sugerence.Accepted,
                         AcceptTime = sugerence.AcceptTime,
                         AcceptedUser = sugerence.Accepted == false ? null : new SocialConflictSensibleUserDto()
                         {
                             Name = resultB == null ? "N/A" : resultB.Name,
                             Surname = resultB == null ? "" : resultB.Surname,
                             EmailAddress = resultB == null ? "" : resultB.EmailAddress
                         }
                     }).ToList();

                var managements = _socialConflictSensibleManagementRepository
                    .GetAll()
                    .Include(p => p.Management)
                    .Include(p => p.Manager)
                    .Include(p => p.Resources)
                    .Where(p => p.SocialConflictSensibleId == input.Id)
                    .ToList();

                socialConflictSensibleItem.Managements = new List<SocialConflictSensibleManagementLocationDto>();

                foreach (var management in managements)
                {
                    var managementItem = ObjectMapper.Map<SocialConflictSensibleManagementLocationDto>(management);
                    managementItem.Resources = new List<SocialConflictSensibleManagementResourceDto>();

                    foreach (var resource in management.Resources)
                    {

                        var resourceItem = ObjectMapper.Map<SocialConflictSensibleManagementResourceDto>(resource);
                        var userResourceExists = resource.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == resource.CreatorUserId) > 0;

                        if (userResourceExists)
                        {
                            var user = await _userRepository.GetAsync(resource.CreatorUserId.Value);
                            resourceItem.CreatorUserName = (user.Name ?? "").Trim() + " " + (user.Surname ?? "").Trim();
                        }

                        managementItem.Resources.Add(resourceItem);
                    }

                    socialConflictSensibleItem.Managements.Add(managementItem);
                }

                socialConflictSensibleItem.States =
                    (from state in _socialConflictSensibleStateRepository.GetAll().Where(p => p.SocialConflictSensibleId == input.Id)
                     join manager in _personRepository.GetAll() on state.ManagerId equals manager.Id into managerResult
                     from manager in managerResult.DefaultIfEmpty()
                     join user in _userRepository.GetAll() on state.CreatorUserId equals user.Id into userResult
                     from user in userResult.DefaultIfEmpty()
                     select new SocialConflictSensibleStateDto()
                     {
                         CreatorUser = new SocialConflictSensibleUserDto()
                         {
                             Name = user == null ? "N/A" : user.Name,
                             Surname = user == null ? "" : user.Surname,
                             EmailAddress = user == null ? "" : user.EmailAddress
                         },
                         Id = state.Id,
                         State = state.State,
                         Description = state.Description,
                         CreationTime = state.CreationTime,
                         StateTime = state.StateTime,
                         VerificationChange = false,
                         VerificationState = state.Verification ? "true" : "false",
                         VerificationLocation = state.Verification,
                         Manager = manager == null ? null : new SocialConflictSensiblePersonDto()
                         {
                             Id = manager.Id,
                             Name = manager.Name
                         },
                         Remove = false
                     }).ToList();

                socialConflictSensibleItem.Conditions = ObjectMapper.Map<List<SocialConflictSensibleConditionDto>>(_socialConflictSensibleConditionRepository
                    .GetAll()
                    .Where(p => p.SocialConflictSensibleId == input.Id)
                    .ToList());

                socialConflictSensibleItem.Resources = ObjectMapper.Map<List<SocialConflictSensibleResourceDto>>(_socialConflictSensibleResourceRepository
                   .GetAll()
                   .Where(p => p.SocialConflictSensibleId == input.Id)
                   .ToList());

                var userCreateExits = socialConflictSensible.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == socialConflictSensible.CreatorUserId) > 0;
                var userEditExits = socialConflictSensible.LastModifierUserId.HasValue && await _userRepository.CountAsync(p => p.Id == socialConflictSensible.LastModifierUserId) > 0;

                socialConflictSensibleItem.CreatorUser = userCreateExits ?
                    ObjectMapper.Map<SocialConflictSensibleUserDto>(await _userRepository.GetAsync(socialConflictSensible.CreatorUserId.Value)) :
                    null;

                socialConflictSensibleItem.EditUser = userEditExits ?
                  ObjectMapper.Map<SocialConflictSensibleUserDto>(await _userRepository.GetAsync(socialConflictSensible.LastModifierUserId.Value)) :
                  null;

                output.SocialConflictSensible = socialConflictSensibleItem;
            }

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<SocialConflictSensibleDepartmentDto>();

            foreach(var item in departments)
            {
                var department = new SocialConflictSensibleDepartmentDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => p.TerritorialUnitId).ToArray(),
                    Provinces = ObjectMapper.Map<List<SocialConflictSensibleProvinceDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.TerritorialUnits = ObjectMapper.Map<List<SocialConflictSensibleTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().OrderBy(p => p.Name).ToListAsync());

            output.Persons = ObjectMapper.Map<List<SocialConflictSensiblePersonDto>>(_personRepository
                .GetAll()
                .Where(p => p.Enabled && p.Type != PersonType.None)
                .OrderBy(p => p.Name)
                .ToList());

            output.Typologies = ObjectMapper.Map<List<SocialConflictSensibleTypologyDto>>(_typologyRepository
                .GetAll()
                .Include(p => p.SubTypologies)
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.Facts = ObjectMapper.Map<List<SocialConflictSensibleFactDto>>(_factRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.ActorTypes = ObjectMapper.Map<List<SocialConflictSensibleActorTypeDto>>(_actorTypeRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.ActorMovements = ObjectMapper.Map<List<SocialConflictSensibleActorMovementDto>>(_actorMovementRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Index)
                .ToList());

            output.Risks = ObjectMapper.Map<List<SocialConflictSensibleRiskDto>>(_riskRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Index)
                .ToList());

            output.Managements = ObjectMapper.Map<List<SocialConflictSensibleManagementDto>>(_managementRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());
            
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictSensible)]
        public async Task<PagedResultDto<SocialConflictSensibleGetAllDto>> GetAll(SocialConflictSensibleGetAllInputDto input)
        {
            var query = _socialConflictSensibleRepository
                .GetAll()
                .Include(p => p.Locations)
                .ThenInclude(p => p.TerritorialUnit)
                .WhereIf(input.Verification.HasValue, p => p.Verification == input.Verification.Value)
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflictSensible.Filter));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var result = new List<SocialConflictSensibleGetAllDto>();

            foreach(var socialConflictSensible in output)
            {
                var socialConflictSensibleItem = ObjectMapper.Map<SocialConflictSensibleGetAllDto>(socialConflictSensible);

                var userCreateExits = socialConflictSensible.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == socialConflictSensible.CreatorUserId) > 0;
                var userEditExits = socialConflictSensible.LastModifierUserId.HasValue && await _userRepository.CountAsync(p => p.Id == socialConflictSensible.LastModifierUserId) > 0;

                socialConflictSensibleItem.CreatorUser = userCreateExits ?
                    ObjectMapper.Map<SocialConflictSensibleUserDto>(await _userRepository.GetAsync(socialConflictSensible.CreatorUserId.Value)) :
                    new SocialConflictSensibleUserDto() { Name = "N/A", Surname = "" };

                socialConflictSensibleItem.EditUser = userEditExits ?
                  ObjectMapper.Map<SocialConflictSensibleUserDto>(await _userRepository.GetAsync(socialConflictSensible.LastModifierUserId.Value)) :
                  new SocialConflictSensibleUserDto() { Name = "N/A", Surname = "" };
                                
                socialConflictSensibleItem.TerritorialUnits = socialConflictSensibleItem.Locations.Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(",");

                result.Add(socialConflictSensibleItem);
            }

            return new PagedResultDto<SocialConflictSensibleGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictSensible_Edit)]
        public async Task Update(SocialConflictSensibleUpdateDto input)
        {
            VerifyCount(await _socialConflictSensibleRepository.CountAsync(p => p.Id == input.Id));

            var dbSocialConflictSensible = await _socialConflictSensibleRepository.GetAsync(input.Id);

            if (dbSocialConflictSensible.CaseNameVerification)
                input.CaseName = dbSocialConflictSensible.CaseName;
            if (dbSocialConflictSensible.ProblemVerification)
                input.Problem = dbSocialConflictSensible.Problem;

            var socialConflictSensibleId = await _socialConflictSensibleRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                    input: ObjectMapper.Map(input, dbSocialConflictSensible),
                    verification: ObjectMapper.Map<SocialConflictSensibleVerificationRequestDto>(input),
                    analystId: input.Analyst == null ? -1 : input.Analyst.Id,
                    coordinatorId: input.Coordinator == null ? -1 : input.Coordinator.Id,
                    managerId: input.Manager == null ? -1 : input.Manager.Id,
                    typologyId: input.Typology == null ? -1 : input.Typology.Id,
                    locations: input.Locations,
                    actors: input.Actors,
                    risks: input.Risks,
                    generalFacts: input.GeneralFacts,
                    sugerences: input.Sugerences,
                    managements: input.Managements,
                    states: input.States,
                    conditions: input.Conditions,
                    resources: input.Resources ?? new List<SocialConflictSensibleResourceDto>(),
                    notes: input.Notes ?? new List<SocialConflictSensibleNoteLocationDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateSocialConflictSensibleCodeReplaceProcess(socialConflictSensibleId, input.ReplaceYear, input.ReplaceCount);

            await FunctionManager.CallSocialConflictSensibleStateProcess(socialConflictSensibleId);
            await FunctionManager.CallSocialConflictSensibleVerificationProccess(socialConflictSensibleId);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflictSensible_Resource)]
        public async Task<EntityDto> CreateNote(SocialConflictSensibleNoteCreateDto input)
        {
            input.Description.IsValidOrException(DefaultTitleMessage, "La descripción de la nota es obligatoria");
            input.Description.VerifyTableColumn(SocialConflictNoteConsts.DescriptionMinLength,
                SocialConflictNoteConsts.DescriptionMaxLength,
                DefaultTitleMessage,
                $"La descripción de la nota no debe exceder los {SocialConflictNoteConsts.DescriptionMaxLength} caracteres");

            if (await _socialConflictSensibleRepository.CountAsync(p => p.Id == input.SocialConflictSensibleId) == 0)
                    throw new UserFriendlyException("Aviso", "El caso de conflictividad que hace referencia ya no existe o fue eliminado. Verifique la información antes de continuar");


            var note = ObjectMapper.Map<SocialConflictSensibleNote>(input);

            var noteId = await _socialConflictSensibleNoteRepository.InsertAndGetIdAsync(note);

            return new EntityDto(noteId);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflictSensible_Resource)]
        public async Task<EntityDto> UpdateNote(SocialConflictSensibleNoteUpdateDto input)
        {
            input.Description.IsValidOrException(DefaultTitleMessage, "La descripción de la nota es obligatoria");
            input.Description.VerifyTableColumn(SocialConflictNoteConsts.DescriptionMinLength,
                SocialConflictNoteConsts.DescriptionMaxLength,
                DefaultTitleMessage,
                $"La descripción de la nota no debe exceder los {SocialConflictNoteConsts.DescriptionMaxLength} caracteres");

            if (await _socialConflictSensibleNoteRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "La nota a la que hace referencia ya no existe o fue eliminado. Verifique la información antes de continuar");

            var note = ObjectMapper.Map(input, await _socialConflictSensibleNoteRepository.GetAsync(input.Id));

            if (await _socialConflictSensibleRepository.CountAsync(p => p.Id == note.SocialConflictSensibleId) == 0)
                    throw new UserFriendlyException("Aviso", "El caso de conflictividad que hace referencia ya no existe o fue eliminado. Verifique la información antes de continuar");

            await _socialConflictSensibleNoteRepository.UpdateAsync(note);

            return new EntityDto(note.Id);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflictSensible_Resource)]
        public async Task DeleteNote(EntityDto input)
        {
            VerifyCount(await _socialConflictSensibleNoteRepository.CountAsync(p => p.Id == input.Id));

            await _socialConflictSensibleNoteRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflictSensible_Resource)]
        public async Task<EntityDto> CreateResource(SocialConflictSensibleCreateResourceDto input)
        {
            VerifyCount(await _socialConflictSensibleRepository.CountAsync(p => p.Id == input.SocialConflictId));

            if (ResourceManager.TokenIsValid(input.Resource.Token) == false)
                throw new UserFriendlyException("Aviso", "La validez de los archivos subidos a caducado, por favor intente nuevamente.");

            var resource = ObjectMapper.Map<SocialConflictSensibleResource>(ResourceManager.Create(input.Resource, ResourceConsts.SocialConflictSensible));
            resource.SocialConflictSensibleId = input.SocialConflictId;

            var resourceId = await _socialConflictSensibleResourceRepository.InsertAndGetIdAsync(resource);

            return new EntityDto(resourceId);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflictSensible_Resource)]
        public async Task DeleteResource(EntityDto input)
        {
            VerifyCount(await _socialConflictSensibleResourceRepository.CountAsync(p => p.SocialConflictSensibleId == input.Id));

            await _socialConflictSensibleResourceRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<FileDto> GetMatrizToExcel(SocialConflictSensibleGetAllInputDto input)
        {
            var query = _socialConflictSensibleRepository
                .GetAll()
                .Include(p => p.Analyst)
                .Include(p => p.Coordinator)
                .Include(p => p.Manager)
                .Include(p => p.Typology)
                .WhereIf(input.Verification.HasValue, p => p.Verification == input.Verification.Value)
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflictSensible.Filter));

            var data = new List<SocialConflictSensibleMatrizExportDto>();
            var result = await query.OrderBy(input.Sorting).ToListAsync();

            foreach (var dbSocialConflictSensible in result)
            {
                var item = new SocialConflictSensibleMatrizExportDto();

                item.Code = dbSocialConflictSensible.Code;
                item.CaseName = dbSocialConflictSensible.CaseName;
                item.Problem = dbSocialConflictSensible.Problem;

                item.CoordinatorName = dbSocialConflictSensible.Coordinator != null ? dbSocialConflictSensible.Coordinator.Name : "";
                item.ManagerName = dbSocialConflictSensible.Manager != null ? dbSocialConflictSensible.Manager.Name : "";
                item.AnalystName = dbSocialConflictSensible.Analyst != null ? dbSocialConflictSensible.Analyst.Name : "";

                item.TypologyDescription = dbSocialConflictSensible.Typology != null ? dbSocialConflictSensible.Typology.Name : "";

                item.CaseNameVerification = dbSocialConflictSensible.CaseNameVerification;
                item.ProblemVerification = dbSocialConflictSensible.ProblemVerification;
                item.RiskVerification = dbSocialConflictSensible.RiskVerification;
                item.ManagementVerification = dbSocialConflictSensible.ManagementVerification;
                item.StateVerification = dbSocialConflictSensible.StateVerification;
                item.ConditionVerification = dbSocialConflictSensible.ConditionVerification;

                var locations = _socialConflictSensibleLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflictSensible.Id == dbSocialConflictSensible.Id)
                    .ToList();

                if (locations.Count > 0)
                {
                    item.TerritorialUnits = locations.Where(p => p.TerritorialUnit != null).Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(", ");
                    item.Departments = locations.Where(p => p.Department != null).Select(p => p.Department.Name).Distinct().JoinAsString(", ");
                    item.Provinces = locations.Where(p => p.Province != null).Select(p => p.Province.Name).Distinct().JoinAsString(", ");
                    item.Districts = locations.Where(p => p.District != null).Select(p => p.District.Name).Distinct().JoinAsString(", ");
                    item.Regions = locations.Where(p => p.Region != null).Select(p => p.Region.Name).Distinct().JoinAsString(", ");
                    item.Ubications = locations.Select(p => (p.Ubication ?? "").Trim()).Where(p => p != "").Distinct().JoinAsString(", ");
                }

                if (dbSocialConflictSensible.LastSocialConflictSensibleRiskId.HasValue)
                {
                    var lastSocialConflictSensibleRisk = _socialConflictSensibleRiskRepository
                        .GetAll()
                        .Include(p => p.Risk)
                        .Where(p => p.Id == dbSocialConflictSensible.LastSocialConflictSensibleRiskId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictSensibleRisk != null && lastSocialConflictSensibleRisk.Risk != null)
                    {
                        item.LastRisk = lastSocialConflictSensibleRisk.Risk.Name ?? "";
                        item.LastRiskDescription = lastSocialConflictSensibleRisk.Description ?? "";
                        item.LastRiskTime = lastSocialConflictSensibleRisk.RiskTime;
                    }
                }

                if (dbSocialConflictSensible.LastSocialConflictSensibleConditionId.HasValue)
                {
                    var lastSocialConflictSensibleCondition = _socialConflictSensibleConditionRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictSensible.LastSocialConflictSensibleConditionId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictSensibleCondition != null)
                    {
                        item.LastCondition = lastSocialConflictSensibleCondition.Type == ConditionType.Open ? "Activo" : "Inactivo";
                        item.LastConditionDescription = lastSocialConflictSensibleCondition.Description ?? "";
                        item.LastConditionTime = lastSocialConflictSensibleCondition.ConditionTime;
                    }
                }

                var actors = _socialConflictSensibleActorRepository
                    .GetAll()
                    .Include(p => p.ActorMovement)
                    .Include(p => p.ActorType)
                    .Where(p => p.SocialConflictSensibleId == dbSocialConflictSensible.Id)
                    .ToList();

                if (actors.Count > 0)
                {
                    item.ActorDescriptions = actors
                        .Select(p => (p.Name ?? "").Trim() +
                                     (p.Job != null ? " - " + p.Job : "") +
                                     (p.ActorType != null ? " - " + p.ActorType.Name : "") +
                                     (p.ActorMovement != null ? " - " + p.ActorMovement.Name : ""))
                        .JoinAsString("/ ");

                    item.ActorPositions = actors
                        .Where(p => p.Position != null)
                        .Select(p => p.Position.Trim())
                        .JoinAsString("/ ");

                    item.ActorInterests = actors
                        .Where(p => p.Interest != null)
                        .Select(p => p.Interest.Trim())
                        .JoinAsString("/ ");
                }

                if (dbSocialConflictSensible.LastSocialConflictSensibleManagementId.HasValue)
                {
                    var lastSocialConflictSensibleManagement = _socialConflictSensibleManagementRepository
                        .GetAll()
                        .Include(p => p.Management)
                        .Include(p => p.Manager)
                        .Where(p => p.Id == dbSocialConflictSensible.LastSocialConflictSensibleManagementId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictSensibleManagement != null)
                    {
                        item.LastManagement = lastSocialConflictSensibleManagement.Management == null ? "" : lastSocialConflictSensibleManagement.Management.Name ?? "";
                        item.LastManagementDescription = lastSocialConflictSensibleManagement.Description ?? "";
                        item.LastManagementTime = lastSocialConflictSensibleManagement.ManagementTime;
                        item.LastManagementManager = lastSocialConflictSensibleManagement.Manager == null ? "" : lastSocialConflictSensibleManagement.Manager.Name ?? "";
                    }
                }

                if (dbSocialConflictSensible.LastSocialConflictSensibleStateId.HasValue)
                {
                    var lastSocialConflictSensibleState = _socialConflictSensibleStateRepository
                        .GetAll()
                        .Include(p => p.Manager)
                        .Where(p => p.Id == dbSocialConflictSensible.LastSocialConflictSensibleStateId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictSensibleState != null)
                    {
                        item.LastState = lastSocialConflictSensibleState.State ?? "";
                        item.LastStateDescription = lastSocialConflictSensibleState.Description ?? "";
                        item.LastStateTime = lastSocialConflictSensibleState.StateTime;
                        item.LastStateManager = lastSocialConflictSensibleState.Manager == null ? "" : lastSocialConflictSensibleState.Manager.Name ?? "";
                    }
                }

                if (dbSocialConflictSensible.CreatorUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictSensible.CreatorUserId.Value)
                        .FirstOrDefault();

                    item.CreatorUser = user?.GetNameSurname();
                    item.CreationTime = dbSocialConflictSensible.CreationTime;
                }

                if (dbSocialConflictSensible.LastModifierUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictSensible.LastModifierUserId.Value)
                        .FirstOrDefault();

                    item.LastModificationUser = user?.GetNameSurname();
                    item.LastModificationTime = dbSocialConflictSensible.LastModificationTime;
                }

                data.Add(item);
            }

            return _socialConflictSensibleExcelExporter.ExportMatrizToFile(data);
        }
       
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<FileDto> GetManagementToExcel(SocialConflictSensibleGetAllInputDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            var query = _socialConflictSensibleManagementRepository
                .GetAll()
                .Include(p => p.SocialConflictSensible)
                .Include(p => p.Manager)
                .Include(p => p.Management)
                .Where(p => p.SocialConflictSensible.IsDeleted == false)
                .WhereIf(input.Verification.HasValue, p => p.SocialConflictSensible.Verification == input.Verification.Value)
                .WhereIf(input.Code.IsValid(), p => p.SocialConflictSensible.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.SocialConflictSensible.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.SocialConflictSensible.CreationTime >= input.StartTime.Value && p.SocialConflictSensible.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflictSensible.Filter));

            var data = new List<SocialConflictSensibleManagementExportDto>();
            var result = query.OrderBy(input.Sorting).ToList();

            foreach (var dbSocialConflictSensibleManagement in result)
            {
                var item = new SocialConflictSensibleManagementExportDto();

                item.CaseCode = dbSocialConflictSensibleManagement.SocialConflictSensible.Code;
                item.CaseName = dbSocialConflictSensibleManagement.SocialConflictSensible.CaseName;

                item.ManagementTime = dbSocialConflictSensibleManagement.ManagementTime;
                item.ManagementDescription = dbSocialConflictSensibleManagement.Description;
                item.Management = dbSocialConflictSensibleManagement.Management == null ? "" : dbSocialConflictSensibleManagement.Management.Name ?? "";
                item.ManagementManager = dbSocialConflictSensibleManagement.Manager == null ? "" : dbSocialConflictSensibleManagement.Manager.Name ?? "";
                item.CivilMen = dbSocialConflictSensibleManagement.CivilMen;
                item.CivilWomen = dbSocialConflictSensibleManagement.CivilWomen;
                item.StateMen = dbSocialConflictSensibleManagement.StateMen;
                item.StateWomen = dbSocialConflictSensibleManagement.StateWomen;
                item.CompanyMen = dbSocialConflictSensibleManagement.CompanyMen;
                item.CompanyWomen = dbSocialConflictSensibleManagement.CompanyWomen;
                item.Verification = dbSocialConflictSensibleManagement.Verification;

                if (dbSocialConflictSensibleManagement.SocialConflictSensible.LastSocialConflictSensibleRiskId.HasValue)
                {
                    var lastSocialConflictSensibleRisk = _socialConflictSensibleRiskRepository
                        .GetAll()
                        .Include(p => p.Risk)
                        .Where(p => p.Id == dbSocialConflictSensibleManagement.SocialConflictSensible.LastSocialConflictSensibleRiskId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictSensibleRisk != null && lastSocialConflictSensibleRisk.Risk != null)
                    {
                        item.LastCaseRisk = lastSocialConflictSensibleRisk.Risk.Name ?? "";
                        item.LastCaseRiskDescription = lastSocialConflictSensibleRisk.Description ?? "";
                        item.LastCaseRiskTime = lastSocialConflictSensibleRisk.RiskTime;
                    }
                }

                if (dbSocialConflictSensibleManagement.SocialConflictSensible.LastSocialConflictSensibleConditionId.HasValue)
                {
                    var lastSocialConflictSensibleCondition = _socialConflictSensibleConditionRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictSensibleManagement.SocialConflictSensible.LastSocialConflictSensibleConditionId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictSensibleCondition != null)
                    {
                        item.LastCaseCondition = lastSocialConflictSensibleCondition.Type == ConditionType.Open ? "Activo" : "Inactivo";
                        item.LastCaseConditionDescription = lastSocialConflictSensibleCondition.Description ?? "";
                        item.LastCaseConditionTime = lastSocialConflictSensibleCondition.ConditionTime;
                    }
                }

                var locations = _socialConflictSensibleLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflictSensible.Id == dbSocialConflictSensibleManagement.SocialConflictSensibleId)
                    .ToList();

                if (locations.Count > 0)
                {
                    item.TerritorialUnits = locations.Where(p => p.TerritorialUnit != null).Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(", ");
                    item.Departments = locations.Where(p => p.Department != null).Select(p => p.Department.Name).Distinct().JoinAsString(", ");
                    item.Provinces = locations.Where(p => p.Province != null).Select(p => p.Province.Name).Distinct().JoinAsString(", ");
                    item.Districts = locations.Where(p => p.District != null).Select(p => p.District.Name).Distinct().JoinAsString(", ");
                    item.Regions = locations.Where(p => p.Region != null).Select(p => p.Region.Name).Distinct().JoinAsString(", ");
                    item.Ubications = locations.Select(p => (p.Ubication ?? "").Trim()).Where(p => p != "").Distinct().JoinAsString(", ");
                }

                data.Add(item);
            }

            return _socialConflictSensibleExcelExporter.ExportManagementToFile(data);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<FileDto> GetStateToExcel(SocialConflictSensibleGetAllInputDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            var query = _socialConflictSensibleStateRepository
                .GetAll()
                .Include(p => p.Manager)
                .Include(p => p.SocialConflictSensible)
                .Where(p => p.SocialConflictSensible.IsDeleted == false)
                .WhereIf(input.Verification.HasValue, p => p.SocialConflictSensible.Verification == input.Verification.Value)
                .WhereIf(input.Code.IsValid(), p => p.SocialConflictSensible.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.SocialConflictSensible.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.SocialConflictSensible.CreationTime >= input.StartTime.Value && p.SocialConflictSensible.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflictSensible.Filter));

            var data = new List<SocialConflictSensibleStateExportDto>();
            var result = query.OrderBy(input.Sorting).ToList();

            foreach (var dbSocialConflictSensibleState in result)
            {
                var item = new SocialConflictSensibleStateExportDto();

                item.CaseCode = dbSocialConflictSensibleState.SocialConflictSensible.Code;
                item.CaseName = dbSocialConflictSensibleState.SocialConflictSensible.CaseName;

                if (dbSocialConflictSensibleState.SocialConflictSensible.LastSocialConflictSensibleRiskId.HasValue)
                {
                    var lastSocialConflictSensibleRisk = _socialConflictSensibleRiskRepository
                        .GetAll()
                        .Include(p => p.Risk)
                        .Where(p => p.Id == dbSocialConflictSensibleState.SocialConflictSensible.LastSocialConflictSensibleRiskId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictSensibleRisk != null && lastSocialConflictSensibleRisk.Risk != null)
                    {
                        item.LastCaseRisk = lastSocialConflictSensibleRisk.Risk.Name ?? "";
                        item.LastCaseRiskDescription = lastSocialConflictSensibleRisk.Description ?? "";
                        item.LastCaseRiskTime = lastSocialConflictSensibleRisk.RiskTime;
                    }
                }

                if (dbSocialConflictSensibleState.SocialConflictSensible.LastSocialConflictSensibleConditionId.HasValue)
                {
                    var lastSocialConflictSensibleCondition = _socialConflictSensibleConditionRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictSensibleState.SocialConflictSensible.LastSocialConflictSensibleConditionId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictSensibleCondition != null)
                    {
                        item.LastCaseCondition = lastSocialConflictSensibleCondition.Type == ConditionType.Open ? "Activo" : "Inactivo";
                        item.LastCaseConditionDescription = lastSocialConflictSensibleCondition.Description ?? "";
                        item.LastCaseConditionTime = lastSocialConflictSensibleCondition.ConditionTime;
                    }
                }

                var locations = _socialConflictSensibleLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflictSensible.Id == dbSocialConflictSensibleState.SocialConflictSensibleId)
                    .ToList();

                if (locations.Count > 0)
                {
                    item.TerritorialUnits = locations.Where(p => p.TerritorialUnit != null).Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(", ");
                    item.Departments = locations.Where(p => p.Department != null).Select(p => p.Department.Name).Distinct().JoinAsString(", ");
                    item.Provinces = locations.Where(p => p.Province != null).Select(p => p.Province.Name).Distinct().JoinAsString(", ");
                    item.Districts = locations.Where(p => p.District != null).Select(p => p.District.Name).Distinct().JoinAsString(", ");
                    item.Regions = locations.Where(p => p.Region != null).Select(p => p.Region.Name).Distinct().JoinAsString(", ");
                    item.Ubications = locations.Select(p => (p.Ubication ?? "").Trim()).Where(p => p != "").Distinct().JoinAsString(", ");
                }

                item.State = dbSocialConflictSensibleState.State ?? "";
                item.StateDescription = dbSocialConflictSensibleState.Description ?? "";
                item.StateTime = dbSocialConflictSensibleState.StateTime;
                item.StateManager = dbSocialConflictSensibleState.Manager == null ? "" : dbSocialConflictSensibleState.Manager.Name;
                item.Verification = dbSocialConflictSensibleState.Verification;

                data.Add(item);
            }

            return _socialConflictSensibleExcelExporter.ExportStateToFile(data);
        }

        private async Task<SocialConflictSensible> ValidateEntity(
            SocialConflictSensible input,
            SocialConflictSensibleVerificationRequestDto verification,
            int analystId,
            int coordinatorId,
            int managerId,
            int typologyId,
            List<SocialConflictSensibleLocationDto> locations,
            List<SocialConflictSensibleActorLocationDto> actors,
            List<SocialConflictSensibleRiskLocationDto> risks,
            List<SocialConflictSensibleGeneralFactDto> generalFacts,
            List<SocialConflictSensibleSugerenceDto> sugerences,
            List<SocialConflictSensibleManagementLocationDto> managements,
            List<SocialConflictSensibleStateDto> states,
            List<SocialConflictSensibleConditionDto> conditions,
            List<SocialConflictSensibleResourceDto> resources,
            List<SocialConflictSensibleNoteLocationDto> notes)
        {
            input.CaseName.IsValidOrException(DefaultTitleMessage, "El nombre de la situación sensible al conflicto es obligatorio");
            input.CaseName.VerifyTableColumn(SocialConflictSensibleConsts.CaseNameMinLength, 
                SocialConflictSensibleConsts.CaseNameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre de la situación sensible al conflicto de debe exceder los {SocialConflictSensibleConsts.CaseNameMaxLength} caracteres");
                        
            input.Problem.VerifyTableColumn(SocialConflictSensibleConsts.ProblemMinLength, 
                SocialConflictSensibleConsts.ProblemMaxLength, 
                DefaultTitleMessage, 
                $"La descripción de la situación sensible al conflicto (problemática) no debe exceder los {SocialConflictSensibleConsts.ProblemMaxLength} caracteres");

            input.Filter = string.Concat(input.Code ?? "", " ", input.CaseName ?? "");
            input.Locations = new List<SocialConflictSensibleLocation>();
            input.Actors = new List<SocialConflictActor>();
            input.GeneralFacts = new List<SocialConflictSensibleGeneralFact>();
            input.Sugerences = new List<SocialConflictSensibleSugerence>();
            input.Managements = new List<SocialConflictSensibleManagement>();
            input.States = new List<SocialConflictSensibleState>();
            input.Risks = new List<SocialConflictSensibleRisk>();
            input.Conditions = new List<SocialConflictSensibleCondition>();
            input.VerificationHistories = new List<SocialConflictSensibleVerificationHistory>();

            var hasVerificationPermission = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflictSensible_Verification);

            if (hasVerificationPermission)
            {
                if (verification.CaseNameVerificationChange)
                {
                    var oldState = input.CaseNameVerification;
                    var newState = ((verification.CaseNameVerificationState ?? "false") == "true");

                    input.CaseNameVerification = newState;

                    if (oldState != newState)
                        input.VerificationHistories.Add(new SocialConflictSensibleVerificationHistory()
                        {
                            Site = SocialConflictVerificationSite.Name,
                            OldState = oldState,
                            NewState = newState
                        });
                }

                if (verification.ProblemVerificationChange)
                {
                    var oldState = input.ProblemVerification;
                    var newState = ((verification.ProblemVerificationState ?? "false") == "true");

                    input.ProblemVerification = newState;

                    if (oldState != newState)
                        input.VerificationHistories.Add(new SocialConflictSensibleVerificationHistory()
                        {
                            Site = SocialConflictVerificationSite.Problem,
                            OldState = oldState,
                            NewState = newState
                        });
                }
            }

            if (analystId > 0)
            {
                if (await _personRepository.CountAsync(p => p.Id == analystId) == 0)
                    throw new UserFriendlyException("Aviso", "El responsable de la SSPI ya no existe o fue eliminado. Verifique la información antes de continuar");

                var analyst = await _personRepository.GetAsync(analystId);

                input.Analyst = analyst;
                input.AnalystId = analyst.Id;
            }
            else
            {
                input.Analyst = null;
                input.AnalystId = null;
            }

            if (coordinatorId > 0)
            {
                if (await _personRepository.CountAsync(p => p.Id == coordinatorId) == 0)
                    throw new UserFriendlyException("Aviso", "El coordinador de la UT ya no existe o fue eliminado. Verifique la información antes de continuar");

                var coordinator = await _personRepository.GetAsync(coordinatorId);

                input.Coordinator = coordinator;
                input.CoordinatorId = coordinator.Id;
            }
            else
            {
                input.Coordinator = null;
                input.CoordinatorId = null;
            }

            if (managerId > 0)
            {
                if (await _personRepository.CountAsync(p => p.Id == managerId) == 0)
                    throw new UserFriendlyException("Aviso", "El gestor responsable de la situación sensible al conflicto ya no existe o fue eliminado. Verifique la información antes de continuar");

                var manager = await _personRepository.GetAsync(managerId);

                input.Manager = manager;
                input.ManagerId = manager.Id;
            }
            else
            {
                input.Manager = null;
                input.ManagerId = null;
            }

            if (typologyId > 0)
            {
                if (await _typologyRepository.CountAsync(p => p.Id == typologyId) == 0)
                    throw new UserFriendlyException("Aviso", "La tipología del conflicto ya no existe o fue eliminada. Verifique la información antes de continuar");

                var typology = await _typologyRepository.GetAsync(typologyId);

                input.Typology = typology;
                input.TypologyId = typology.Id;
            }
            else
            {
                input.Typology = null;
                input.TypologyId = null;
            }
            
            foreach (var location in locations)
            {
                if(location.Remove)
                {
                    if(location.Id > 0 && input.Id > 0 && await _socialConflictSensibleLocationRepository.CountAsync(p => p.Id == location.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                    {
                        await _socialConflictSensibleLocationRepository.DeleteAsync(location.Id);
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
                            location.Ubication.VerifyTableColumn(SocialConflictSensibleLocationConsts.UbicationMinLength,
                                SocialConflictSensibleLocationConsts.UbicationMaxLength, 
                                DefaultTitleMessage, 
                                $"La localidad - comunidad - Otros {location.Ubication} no debe exceder los {SocialConflictSensibleLocationConsts.UbicationMaxLength} caracteres");

                            input.Locations.Add(new SocialConflictSensibleLocation()
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
                    if (actor.Id > 0 && input.Id > 0 && await _socialConflictSensibleActorRepository.CountAsync(p => p.Id == actor.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                    {
                        await _socialConflictSensibleActorRepository.DeleteAsync(actor.Id);
                    }
                }
                else
                {
                    //actor.Name.IsValidOrException(DefaultTitleMessage, "El nombre del actor es obligatorio");

                    //actor.Name.VerifyTableColumn(SocialConflictActorConsts.NameMinLength,
                    //    SocialConflictActorConsts.NameMaxLength,
                    //    DefaultTitleMessage,
                    //    $"El nombre del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.NameMaxLength} caracteres");
                    //actor.Document.VerifyTableColumn(SocialConflictActorConsts.DocumentMinLength,
                    //    SocialConflictActorConsts.DocumentMaxLength,
                    //    DefaultTitleMessage,
                    //    $"El DNI del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.DocumentMaxLength} caracteres");
                    //actor.Job.VerifyTableColumn(SocialConflictActorConsts.JobMinLength,
                    //    SocialConflictActorConsts.JobMaxLength,
                    //    DefaultTitleMessage,
                    //    $"El cargo del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.JobMaxLength} caracteres");

                    //actor.Community.IsValidOrException(DefaultTitleMessage,  $"La institución del {actor.Name} es obligatoria");
                    //actor.Community.VerifyTableColumn(SocialConflictActorConsts.CommunityMinLength,
                    //    SocialConflictActorConsts.CommunityMaxLength,
                    //    DefaultTitleMessage,
                    //    $"La institución a la que pertenece el actor {actor.Name} no debe exceder los {SocialConflictActorConsts.CommunityMaxLength} caracteres");

                    //actor.PhoneNumber.VerifyTableColumn(SocialConflictActorConsts.PhoneNumberMinLength,
                    //    SocialConflictActorConsts.PhoneNumberMaxLength,
                    //    DefaultTitleMessage,
                    //    $"El número de teléfono del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.PhoneNumberMaxLength} caracteres");
                    //actor.EmailAddress.VerifyTableColumn(SocialConflictActorConsts.EmailAddressMinLength,
                    //    SocialConflictActorConsts.EmailAddressMaxLength,
                    //    DefaultTitleMessage,
                    //    $"El correo electrónico del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.EmailAddressMaxLength} caracteres");

                    //if(actor.IsPoliticalAssociation)
                    //{
                    //    actor.PoliticalAssociation.VerifyTableColumn(SocialConflictActorConsts.PoliticalAssociationMinLength,
                    //        SocialConflictActorConsts.PoliticalAssociationMaxLength,
                    //        DefaultTitleMessage,
                    //        $"El nombre del partido político al que pertenece el actor {actor.Name} no debe exceder los {SocialConflictActorConsts.PoliticalAssociationMaxLength} caracteres");
                    //}

                    //if (await _actorTypeRepository.CountAsync(p => p.Id == actor.ActorType.Id) == 0)
                    //    throw new UserFriendlyException(DefaultTitleMessage, $"El tipo de actor {actor.ActorType.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    //var dbActorType = await _actorTypeRepository.GetAsync(actor.ActorType.Id);
                    //ActorMovement dbActorMovement = null;

                    //if (dbActorType.ShowMovement)
                    //{
                    //    if (actor.ActorMovement.Id == -1)
                    //        throw new UserFriendlyException("Aviso", $"La capacidad de movilización del actor {actor.Name} es obligatoria");

                    //    if (await _actorMovementRepository.CountAsync(p => p.Id == actor.ActorMovement.Id) == 0)
                    //        throw new UserFriendlyException(DefaultTitleMessage, $"La capacidad de movilización {actor.ActorMovement.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    //    dbActorMovement = await _actorMovementRepository.GetAsync(actor.ActorMovement.Id);
                    //}

                    //if (dbActorType.ShowDetail)
                    //{
                    //    actor.Position.VerifyTableColumn(SocialConflictActorConsts.PositionMinLength,
                    //        SocialConflictActorConsts.PositionMaxLength,
                    //        DefaultTitleMessage,
                    //        $"La posición del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.PositionMaxLength} caracteres");
                    //    actor.Interest.VerifyTableColumn(SocialConflictActorConsts.InterestMinLength,
                    //        SocialConflictActorConsts.InterestMaxLength,
                    //        DefaultTitleMessage,
                    //        $"El interés del actor {actor.Name} no debe exceder los {SocialConflictActorConsts.InterestMaxLength} caracteres");
                    //}

                    if (actor.Id > 0)
                    {
                        if(await _socialConflictSensibleActorRepository.CountAsync(p => p.Id == actor.Id && p.SocialConflictSensibleId == input.Id) > 0)
                        {
                            var dbSocialConflictSensibleActor = await _socialConflictSensibleActorRepository.GetAsync(actor.Id);
                            var dbActor = await _actorRepository.GetAsync(actor.ActorId);
                            dbSocialConflictSensibleActor.ActorId = dbActor.Id;
                            dbSocialConflictSensibleActor.Name = dbActor.FullName;
                            dbSocialConflictSensibleActor.Document = dbActor.DocumentNumber;
                            dbSocialConflictSensibleActor.Job = dbActor.JobPosition;
                            dbSocialConflictSensibleActor.Community = dbActor.Institution;
                            dbSocialConflictSensibleActor.PhoneNumber = dbActor.PhoneNumber;
                            dbSocialConflictSensibleActor.EmailAddress = dbActor.EmailAddress;
                            dbSocialConflictSensibleActor.IsPoliticalAssociation = dbActor.IsPoliticalAssociation;
                            dbSocialConflictSensibleActor.PoliticalAssociation = dbActor.IsPoliticalAssociation ? dbActor.PoliticalAssociation : null;
                            dbSocialConflictSensibleActor.Position = dbActor.ActorType.ShowDetail ? dbActor.Position : null;
                            dbSocialConflictSensibleActor.Interest = dbActor.ActorType.ShowDetail ? dbActor.Interest : null;
                            dbSocialConflictSensibleActor.ActorTypeId = dbActor.ActorType.Id;
                            dbSocialConflictSensibleActor.ActorType = dbActor.ActorType;
                            dbSocialConflictSensibleActor.ActorMovementId = dbActor.ActorMovement == null ? (int?)null : dbActor.ActorMovement.Id;
                            dbSocialConflictSensibleActor.ActorMovement = dbActor.ActorMovement;
                            await _socialConflictSensibleActorRepository.UpdateAsync(dbSocialConflictSensibleActor);

                        }
                    }
                    else
                    {
                        var dbActor = await _actorRepository.GetAsync(actor.ActorId);

                        input.Actors.Add(new SocialConflictActor()
                        {
                            Name = dbActor.FullName,
                            Document = dbActor.DocumentNumber,
                            Job = dbActor.JobPosition,
                            Community = dbActor.Institution,
                            PhoneNumber = dbActor.PhoneNumber,
                            EmailAddress = dbActor.EmailAddress,
                            IsPoliticalAssociation = dbActor.IsPoliticalAssociation,
                            PoliticalAssociation = dbActor.IsPoliticalAssociation ? dbActor.PoliticalAssociation : null,
                            Position = dbActor.ActorType.ShowDetail ? dbActor.Position : null,
                            Interest = dbActor.ActorType.ShowDetail ? dbActor.Interest : null,
                            ActorTypeId = dbActor.ActorType.Id,
                            ActorType = dbActor.ActorType,
                            ActorMovementId = dbActor.ActorMovement == null ? (int?)null : dbActor.ActorMovement.Id,
                            ActorMovement = dbActor.ActorMovement,
                            ActorId = actor.ActorId,
                            Site = ActorSite.SocialConflictSensible
                        });
                    }
                }
            }

            foreach (var risk in risks)
            {
                if (risk.Remove)
                {
                    if (risk.Id > 0 && input.Id > 0 && await _socialConflictSensibleRiskRepository.CountAsync(p => p.Id == risk.Id && p.SocialConflictSensible.Id == input.Id && p.Verification == false) > 0)
                    {
                        await _socialConflictSensibleRiskRepository.DeleteAsync(risk.Id);
                    }
                }
                else
                {
                    risk.Description.VerifyTableColumn(SocialConflictSensibleRiskConsts.DescriptionMinLength,
                        SocialConflictSensibleRiskConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del nivel de riesgo {risk.Description} no debe exceder los " +
                        $"{SocialConflictSensibleRiskConsts.DescriptionMaxLength} caracteres");

                    var dbRisk = _riskRepository
                        .GetAll()
                        .Where(p => p.Id == risk.Risk.Id)
                        .FirstOrDefault();

                    if(dbRisk == null)
                        throw new UserFriendlyException("Aviso", $"El nivel de riesgo {risk.Risk.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (risk.Id > 0)
                    {
                        if (await _socialConflictSensibleRiskRepository.CountAsync(p => p.Id == risk.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                        {
                            var dbSocialConflictSensibleGeneralRisk = await _socialConflictSensibleRiskRepository.GetAsync(risk.Id);

                            if (dbSocialConflictSensibleGeneralRisk.Verification == false)
                            {
                                dbSocialConflictSensibleGeneralRisk.Description = risk.Description;
                                dbSocialConflictSensibleGeneralRisk.RiskTime = new DateTime(risk.RiskTime.Year, risk.RiskTime.Month, risk.RiskTime.Day);
                                dbSocialConflictSensibleGeneralRisk.RiskId = dbRisk.Id;
                                dbSocialConflictSensibleGeneralRisk.Risk = dbRisk;
                            }

                            if (hasVerificationPermission && risk.VerificationChange)
                            {
                                var oldState = dbSocialConflictSensibleGeneralRisk.Verification;
                                var newState = ((risk.VerificationState ?? "false") == "true");

                                dbSocialConflictSensibleGeneralRisk.Verification = newState;

                                if (oldState != newState)
                                    input.VerificationHistories.Add(new SocialConflictSensibleVerificationHistory()
                                    {
                                        Site = SocialConflictVerificationSite.Risk,
                                        OldState = oldState,
                                        NewState = newState,
                                        EntityId = risk.Id
                                    });
                            }

                            await _socialConflictSensibleRiskRepository.UpdateAsync(dbSocialConflictSensibleGeneralRisk);
                        }
                    }
                    else
                    {
                        input.Risks.Add(new SocialConflictSensibleRisk()
                        {
                            Description = risk.Description,
                            RiskTime = new DateTime(risk.RiskTime.Year, risk.RiskTime.Month, risk.RiskTime.Day),
                            Verification = risk.VerificationChange && hasVerificationPermission && ((risk.VerificationState ?? "false") == "true"),
                            RiskId = dbRisk.Id,
                            Risk = dbRisk
                        });
                    }
                }
            }

            foreach (var generalFact in generalFacts)
            {
                if (generalFact.Remove)
                {
                    if (generalFact.Id > 0 && input.Id > 0 && await _socialConflictSensibleGeneralFactRepository.CountAsync(p => p.Id == generalFact.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                    {
                        await _socialConflictSensibleGeneralFactRepository.DeleteAsync(generalFact.Id);
                    }
                }
                else
                {
                    generalFact.Description.IsValidOrException(DefaultTitleMessage,
                        "El detalle del hecho revelevante es obligatorio");

                    generalFact.Description.VerifyTableColumn(SocialConflictSensibleGeneralFactConsts.DescriptionMinLength, 
                        SocialConflictSensibleGeneralFactConsts.DescriptionMaxLength, 
                        DefaultTitleMessage, 
                        $"El detalle del hecho revelevante {generalFact.Description} no debe exceder los " +
                        $"{SocialConflictSensibleGeneralFactConsts.DescriptionMaxLength} caracteres");

                    if (generalFact.Id > 0)
                    {
                        if (await _socialConflictSensibleGeneralFactRepository.CountAsync(p => p.Id == generalFact.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                        {
                            var dbSocialConflictSensibleGeneralFact = await _socialConflictSensibleGeneralFactRepository.GetAsync(generalFact.Id);
                            dbSocialConflictSensibleGeneralFact.Description = generalFact.Description;
                            dbSocialConflictSensibleGeneralFact.FactTime = new DateTime(generalFact.FactTime.Year, generalFact.FactTime.Month, generalFact.FactTime.Day);

                            await _socialConflictSensibleGeneralFactRepository.UpdateAsync(dbSocialConflictSensibleGeneralFact);
                        }
                    }
                    else
                    {
                        input.GeneralFacts.Add(new SocialConflictSensibleGeneralFact()
                        {
                            Description = generalFact.Description,
                            FactTime = new DateTime(generalFact.FactTime.Year, generalFact.FactTime.Month, generalFact.FactTime.Day)
                        });
                    }
                }
            }

            var hasCreatePermissionSugerence = false;
            var hasEditPermissionSugerence = false;
            var hasDeletePermissionSugerence = false;
            var hasAcceptPermissionSugerence = false;

            if (PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence))
            {
                hasCreatePermissionSugerence = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Create);
                hasEditPermissionSugerence = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Edit);
                hasDeletePermissionSugerence = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Delete);
                hasAcceptPermissionSugerence = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Accept);
            }
            else
            {
                sugerences = new List<SocialConflictSensibleSugerenceDto>();
            }

            foreach (var sugerence in sugerences)
            {
                if (sugerence.Remove)
                {
                    if (hasDeletePermissionSugerence && sugerence.Id > 0 && input.Id > 0 && await _socialConflictSensibleSugerenceRepository.CountAsync(p => p.Id == sugerence.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                    {
                        await _socialConflictSensibleSugerenceRepository.DeleteAsync(sugerence.Id);
                    }
                }
                else
                {
                    sugerence.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción de las recomendaciones son obligatorias");

                    sugerence.Description.VerifyTableColumn(SocialConflictSensibleSugerenceConsts.DescriptionMinLength,
                        SocialConflictSensibleSugerenceConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la recomendación {sugerence.Description} no debe exceder los " +
                        $"{SocialConflictSensibleSugerenceConsts.DescriptionMaxLength} caracteres");

                    if (sugerence.Id > 0)
                    {
                        if (hasEditPermissionSugerence && await _socialConflictSensibleSugerenceRepository.CountAsync(p => p.Id == sugerence.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                        {
                            var dbSocialConflictSensibleSugerence = await _socialConflictSensibleSugerenceRepository.GetAsync(sugerence.Id);
                            dbSocialConflictSensibleSugerence.Description = sugerence.Description;

                            if(dbSocialConflictSensibleSugerence.Accepted == false && sugerence.Accepted && hasAcceptPermissionSugerence)
                            {
                                dbSocialConflictSensibleSugerence.Accepted = true;
                                dbSocialConflictSensibleSugerence.AcceptTime = DateTime.Now;
                                dbSocialConflictSensibleSugerence.AcceptedUserId = AbpSession.UserId;
                            }

                            await _socialConflictSensibleSugerenceRepository.UpdateAsync(dbSocialConflictSensibleSugerence);
                        }
                    }
                    else
                    {
                        if(hasCreatePermissionSugerence)
                        {
                            input.Sugerences.Add(new SocialConflictSensibleSugerence()
                            {
                                Description = sugerence.Description,
                                Accepted = sugerence.Accepted,
                                AcceptTime = sugerence.Accepted && hasAcceptPermissionSugerence ? DateTime.Now : (DateTime?)null,
                                AcceptedUserId = sugerence.Accepted && hasAcceptPermissionSugerence ? AbpSession.UserId : (long?)null
                            });
                        }
                    }
                }
            }
            
            foreach (var state in states)
            {

                if (state.Remove)
                {
                    if (state.Id > 0 && input.Id > 0 && await _socialConflictSensibleStateRepository.CountAsync(p => p.Id == state.Id && p.SocialConflictSensibleId == input.Id && p.Verification == false) > 0)
                    {
                        await _socialConflictSensibleStateRepository.DeleteAsync(state.Id);
                    }
                }
                else
                {
                    state.State.IsValidOrException(DefaultTitleMessage,
                        "La descripción de la situación actual (interna) son obligatorias");

                    state.State.VerifyTableColumn(SocialConflictSensibleStateConsts.StateMinLength,
                        SocialConflictSensibleStateConsts.StateMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la situación actual (interna) {state.Description} no debe exceder los " +
                        $"{SocialConflictSensibleStateConsts.StateMaxLength} caracteres");

                    state.Description.IsValidOrException(DefaultTitleMessage,
                        "La proyección y acciones propuestas de la situación actual son obligatorias");

                    state.Description.VerifyTableColumn(SocialConflictSensibleStateConsts.DescriptionMinLength,
                        SocialConflictSensibleStateConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La proyección y acciones propuestas de la situación actual {state.Description} no debe exceder los " +
                        $"{SocialConflictSensibleStateConsts.DescriptionMaxLength} caracteres");

                    var dbManager = _personRepository
                       .GetAll()
                       .Where(p => p.Id == state.Manager.Id)
                       .FirstOrDefault();

                    if (dbManager == null)
                        throw new UserFriendlyException("Aviso", $"El gestor que registra {state.Manager.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (state.Id > 0)
                    {
                        if (await _socialConflictSensibleStateRepository.CountAsync(p => p.Id == state.Id && p.SocialConflictSensible.Id == input.Id && p.Verification == false) > 0)
                        {
                            var dbSocialConflictSensibleState = await _socialConflictSensibleStateRepository.GetAsync(state.Id);

                            if(dbSocialConflictSensibleState.Verification == false)
                            {
                                dbSocialConflictSensibleState.Manager = dbManager;
                                dbSocialConflictSensibleState.ManagerId = dbManager.Id;
                                dbSocialConflictSensibleState.StateTime = new DateTime(state.StateTime.Year, state.StateTime.Month, state.StateTime.Day);
                                dbSocialConflictSensibleState.State = state.State;
                                dbSocialConflictSensibleState.Description = state.Description;
                            }

                            if (hasVerificationPermission && state.VerificationChange)
                            {
                                var oldState = dbSocialConflictSensibleState.Verification;
                                var newState = ((state.VerificationState ?? "false") == "true");

                                dbSocialConflictSensibleState.Verification = newState;

                                if (oldState != newState)
                                    input.VerificationHistories.Add(new SocialConflictSensibleVerificationHistory()
                                    {
                                        Site = SocialConflictVerificationSite.State,
                                        OldState = oldState,
                                        NewState = newState,
                                        EntityId = state.Id
                                    });
                            }

                            await _socialConflictSensibleStateRepository.UpdateAsync(dbSocialConflictSensibleState);
                        }
                    }
                    else
                    {
                        input.States.Add(new SocialConflictSensibleState()
                        {
                            ManagerId = dbManager.Id,
                            Manager = dbManager,
                            StateTime = new DateTime(state.StateTime.Year, state.StateTime.Month, state.StateTime.Day),
                            State = state.State,
                            Verification = state.VerificationChange && hasVerificationPermission && ((state.VerificationState ?? "false") == "true"),
                            Description = state.Description
                        });
                    }
                }
            }

            foreach (var condition in conditions)
            {
                if (condition.Remove)
                {
                    if (condition.Id > 0 && input.Id > 0 && await _socialConflictSensibleConditionRepository.CountAsync(p => p.Id == condition.Id && p.SocialConflictSensible.Id == input.Id && p.Verification == false) > 0)
                    {
                        await _socialConflictSensibleConditionRepository.DeleteAsync(condition.Id);
                    }
                }
                else
                {
                    condition.Description.VerifyTableColumn(SocialConflictSensibleConditionConsts.DescriptionMinLength,
                        SocialConflictSensibleConditionConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción el estado del caso {condition.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictSensibleConditionConsts.DescriptionMaxLength} caracteres");

                    if (condition.Id > 0)
                    {
                        if (await _socialConflictSensibleConditionRepository.CountAsync(p => p.Id == condition.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                        {
                            var dbSocialConflictSensibleCondition = await _socialConflictSensibleConditionRepository.GetAsync(condition.Id);

                            if(dbSocialConflictSensibleCondition.Verification == false)
                            {
                                dbSocialConflictSensibleCondition.Description = condition.Description;
                                dbSocialConflictSensibleCondition.Type = condition.Type;
                                dbSocialConflictSensibleCondition.ConditionTime = condition.ConditionTime;
                            }

                            if (hasVerificationPermission && condition.VerificationChange)
                            {
                                var oldState = dbSocialConflictSensibleCondition.Verification;
                                var newState = ((condition.VerificationState ?? "false") == "true");

                                dbSocialConflictSensibleCondition.Verification = newState;

                                if (oldState != newState)
                                    input.VerificationHistories.Add(new SocialConflictSensibleVerificationHistory()
                                    {
                                        Site = SocialConflictVerificationSite.Condition,
                                        OldState = oldState,
                                        NewState = newState,
                                        EntityId = condition.Id
                                    });
                            }

                            await _socialConflictSensibleConditionRepository.UpdateAsync(dbSocialConflictSensibleCondition);
                        }
                    }
                    else
                    {
                        input.Conditions.Add(new SocialConflictSensibleCondition()
                        {
                            Description = condition.Description,
                            Type = condition.Type,
                            Verification = condition.VerificationChange && hasVerificationPermission && ((condition.VerificationState ?? "false") == "true"),
                            ConditionTime = condition.ConditionTime
                        });
                    }
                }
            }

            foreach (var management in managements)
            {

                if (management.Remove)
                {
                    if (management.Id > 0 && input.Id > 0 && await _socialConflictSensibleManagementRepository.CountAsync(p => p.Id == management.Id && p.SocialConflictSensible.Id == input.Id && p.Verification == false) > 0)
                    {
                        await _socialConflictSensibleManagementRepository.DeleteAsync(management.Id);
                        await _socialConflictSensibleManagementResourceRepository.DeleteAsync(p => p.SocialConflictSensibleManagementId == management.Id);
                    }
                }
                else
                {
                    management.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción de las gestiones del conflicto social son obligatorias");

                    management.Description.VerifyTableColumn(SocialConflictSensibleManagementConsts.DescriptionMinLength,
                        SocialConflictSensibleManagementConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la gestión {management.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictSensibleManagementConsts.DescriptionMaxLength} caracteres");

                    var dbManagement = _managementRepository
                       .GetAll()
                       .Where(p => p.Id == management.Management.Id)
                       .FirstOrDefault();

                    if (dbManagement == null)
                        throw new UserFriendlyException("Aviso", $"El tipo de gestión {management.Management.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    var dbManager = _personRepository
                       .GetAll()
                       .Where(p => p.Id == management.Manager.Id)
                       .FirstOrDefault();

                    if (dbManager == null)
                        throw new UserFriendlyException("Aviso", $"El gestor {management.Manager.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    SocialConflictSensibleManagement dbSocialConflictSensibleManagement = null;

                    if (management.Id > 0)
                    {
                        if (await _socialConflictSensibleManagementRepository.CountAsync(p => p.Id == management.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                        {
                            dbSocialConflictSensibleManagement = await _socialConflictSensibleManagementRepository.GetAsync(management.Id);

                            if(dbSocialConflictSensibleManagement.Verification == false)
                            {
                                dbSocialConflictSensibleManagement.Description = management.Description;
                                dbSocialConflictSensibleManagement.ManagementTime = new DateTime(management.ManagementTime.Year, management.ManagementTime.Month, management.ManagementTime.Day);
                                dbSocialConflictSensibleManagement.Management = dbManagement;
                                dbSocialConflictSensibleManagement.ManagementId = dbManagement.Id;
                                dbSocialConflictSensibleManagement.Manager = dbManager;
                                dbSocialConflictSensibleManagement.ManagerId = dbManager.Id;
                                dbSocialConflictSensibleManagement.CivilMen = dbManagement.ShowDetail ? management.CivilMen : (int?)null;
                                dbSocialConflictSensibleManagement.CivilWomen = dbManagement.ShowDetail ? management.CivilWomen : (int?)null;
                                dbSocialConflictSensibleManagement.StateMen = dbManagement.ShowDetail ? management.StateMen : (int?)null;
                                dbSocialConflictSensibleManagement.StateWomen = dbManagement.ShowDetail ? management.StateWomen : (int?)null;
                                dbSocialConflictSensibleManagement.CompanyMen = dbManagement.ShowDetail ? management.CompanyMen : (int?)null;
                                dbSocialConflictSensibleManagement.CompanyWomen = dbManagement.ShowDetail ? management.CompanyWomen : (int?)null;
                            }

                            dbSocialConflictSensibleManagement.Resources = new List<SocialConflictSensibleManagementResource>();

                            if (hasVerificationPermission && management.VerificationChange)
                            {
                                var oldState = dbSocialConflictSensibleManagement.Verification;
                                var newState = ((management.VerificationState ?? "false") == "true");

                                dbSocialConflictSensibleManagement.Verification = newState;

                                if (oldState != newState)
                                    input.VerificationHistories.Add(new SocialConflictSensibleVerificationHistory()
                                    {
                                        Site = SocialConflictVerificationSite.Management,
                                        OldState = oldState,
                                        NewState = newState,
                                        EntityId = management.Id
                                    });
                            }
                        }
                    }
                    else
                    {
                        dbSocialConflictSensibleManagement = new SocialConflictSensibleManagement()
                        {
                            ManagementId = dbManagement.Id,
                            Management = dbManagement,
                            ManagerId = dbManager.Id,
                            Manager = dbManager,
                            Description = management.Description,
                            ManagementTime = new DateTime(management.ManagementTime.Year, management.ManagementTime.Month, management.ManagementTime.Day),
                            CivilMen = dbManagement.ShowDetail ? management.CivilMen : (int?)null,
                            CivilWomen = dbManagement.ShowDetail ? management.CivilWomen : (int?)null,
                            StateMen = dbManagement.ShowDetail ? management.StateMen : (int?)null,
                            StateWomen = dbManagement.ShowDetail ? management.StateWomen : (int?)null,
                            CompanyMen = dbManagement.ShowDetail ? management.CompanyMen : (int?)null,
                            CompanyWomen = dbManagement.ShowDetail ? management.CompanyWomen : (int?)null,
                            Verification = management.VerificationChange && hasVerificationPermission && ((management.VerificationState ?? "false") == "true"),
                            Resources = new List<SocialConflictSensibleManagementResource>()
                        };
                    }

                    foreach (var resource in management.Resources)
                    {
                        if (resource.Remove)
                        {
                            if (resource.Id > 0 && management.Id > 0 && input.Id > 0 && await _socialConflictSensibleManagementResourceRepository.CountAsync(p => p.Id == resource.Id && p.SocialConflictSensibleManagementId == management.Id) > 0)
                            {
                                await _socialConflictSensibleManagementResourceRepository.DeleteAsync(resource.Id);
                            }
                        }
                    }

                    foreach (var resource in management.UploadFiles)
                    {
                        dbSocialConflictSensibleManagement.Resources.Add(ObjectMapper.Map<SocialConflictSensibleManagementResource>(ResourceManager.Create(resource, ResourceConsts.SocialConflictSensibleManagement)));
                    }

                    if (dbSocialConflictSensibleManagement != null)
                    {
                        if (dbSocialConflictSensibleManagement.Id > 0)
                        {
                            await _socialConflictSensibleManagementRepository.UpdateAsync(dbSocialConflictSensibleManagement);
                        }
                        else
                        {
                            input.Managements.Add(dbSocialConflictSensibleManagement);
                        }
                    }
                }
            }

            foreach (var resource in resources)
            {
                if (resource.Remove)
                {
                    if (resource.Id > 0 && input.Id > 0 && await _socialConflictSensibleResourceRepository.CountAsync(p => p.Id == resource.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                    {
                        await _socialConflictSensibleResourceRepository.DeleteAsync(resource.Id);
                    }
                }
            }

            foreach (var note in notes)
            {
                if (note.Remove)
                {
                    if (note.Id > 0 && input.Id > 0 && await _socialConflictSensibleNoteRepository.CountAsync(p => p.Id == note.Id && p.SocialConflictSensible.Id == input.Id) > 0)
                    {
                        await _socialConflictSensibleNoteRepository.DeleteAsync(note.Id);
                    }
                }
            }

            return input;
        }
    }
}
