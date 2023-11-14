using Abp.Linq.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using System.Linq.Dynamic.Core;
using System.Linq;
using Contable.Application.SocialConflicts;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using Contable.Application.SocialConflicts.Dto;
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
using Contable.Application.Exporting;
using Contable.Dto;
using Contable.Application.Actors.Dto;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict, AppPermissions.App_SocialConflict, RequireAllPermissions = false)]
    public class SocialConflictAppService : ContableAppServiceBase, ISocialConflictAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Region> _regionRepository; 
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SocialConflictLocation> _socialConflictLocationRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Typology> _typologyRepository;
        private readonly IRepository<SubTypology> _subTypologyRepository;
        private readonly IRepository<Fact> _factRepository;
        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<ActorType> _actorTypeRepository;
        private readonly IRepository<ActorMovement> _actorMovementRepository;
        private readonly IRepository<Sector> _sectorRepository;
        private readonly IRepository<Risk> _riskRepository;
        private readonly IRepository<Management> _managementRepository; 
        private readonly IRepository<SocialConflictActor> _socialConflictActorRepository;
        private readonly IRepository<SocialConflictRisk> _socialConflictRiskRepository;
        private readonly IRepository<SocialConflictGeneralFact> _socialConflictGeneralFactRepository;
        private readonly IRepository<SocialConflictSugerence> _socialConflictSugerenceRepository;
        private readonly IRepository<Compromise, long> _compromiseRepository;
        private readonly IRepository<SocialConflictManagement> _socialConflictManagementRepository;
        private readonly IRepository<SocialConflictManagementResource> _socialConflictManagementResourceRepository;
        private readonly IRepository<SocialConflictState> _socialConflictStateRepository;
        private readonly IRepository<SocialConflictViolenceFact> _socialConflictViolenceFactRepository;
        private readonly IRepository<SocialConflictViolenceFactLocation> _socialConflictViolenceFactLocationRepository;
        private readonly IRepository<SocialConflictCondition> _socialConflictConditionRepository;
        private readonly IRepository<SocialConflictNote> _socialConflictNoteRepository;
        private readonly IRepository<SocialConflictResource> _socialConflictResourceRepository;
        private readonly ISocialConflictExcelExporter _socialConflictExporter;

        public SocialConflictAppService(
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<District> districtRepository,
            IRepository<Region> regionRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<SocialConflictLocation> socialConflictLocationRepository,
            IRepository<User, long> userRepository,
            IRepository<Person> personRepository,
            IRepository<Typology> typologyRepository,
            IRepository<SubTypology> subTypologyRepository,
            IRepository<Fact> factRepository,
            IRepository<Actor> actorRepository,
            IRepository<ActorType> actorTypeRepository,
            IRepository<ActorMovement> actorMovementRepository,
            IRepository<Sector> sectorRepository,
            IRepository<Risk> riskRepository,
            IRepository<Management> managementRepository,
            IRepository<SocialConflictActor> socialConflictActorRepository,
            IRepository<SocialConflictRisk> socialConflictRiskRepository,
            IRepository<SocialConflictGeneralFact> socialConflictGeneralFactRepository,
            IRepository<SocialConflictSugerence> socialConflictSugerenceRepository,
            IRepository<Compromise, long> compromiseRepository,
            IRepository<SocialConflictManagement> socialConflictManagementRepository,
            IRepository<SocialConflictManagementResource> socialConflictManagementResourceRepository,
            IRepository<SocialConflictState> socialConflictStateRepository,
            IRepository<SocialConflictViolenceFact> socialConflictViolenceFactRepository,
            IRepository<SocialConflictViolenceFactLocation> socialConflictViolenceFactLocationRepository,
            IRepository<SocialConflictCondition> socialConflictConditionRepository,
            IRepository<SocialConflictNote> socialConflictNoteRepository,
            IRepository<SocialConflictResource> socialConflictResourceRepository,
            ISocialConflictExcelExporter socialConflictExporter)
        {
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _regionRepository = regionRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _socialConflictRepository = socialConflictRepository;
            _socialConflictLocationRepository = socialConflictLocationRepository;
            _userRepository = userRepository;
            _personRepository = personRepository;
            _typologyRepository = typologyRepository;
            _subTypologyRepository = subTypologyRepository;
            _factRepository = factRepository;
            _actorTypeRepository = actorTypeRepository;
            _actorMovementRepository = actorMovementRepository;
            _sectorRepository = sectorRepository;
            _riskRepository = riskRepository;
            _managementRepository = managementRepository;
            _socialConflictActorRepository = socialConflictActorRepository;
            _socialConflictRiskRepository = socialConflictRiskRepository;
            _socialConflictGeneralFactRepository = socialConflictGeneralFactRepository;
            _socialConflictSugerenceRepository = socialConflictSugerenceRepository;
            _compromiseRepository = compromiseRepository;
            _socialConflictManagementRepository = socialConflictManagementRepository;
            _socialConflictManagementResourceRepository = socialConflictManagementResourceRepository;
            _socialConflictStateRepository = socialConflictStateRepository;
            _socialConflictViolenceFactRepository = socialConflictViolenceFactRepository;
            _socialConflictViolenceFactLocationRepository = socialConflictViolenceFactLocationRepository;
            _socialConflictConditionRepository = socialConflictConditionRepository;
            _socialConflictNoteRepository = socialConflictNoteRepository;
            _socialConflictResourceRepository = socialConflictResourceRepository;
            _socialConflictExporter = socialConflictExporter;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict_Create)]
        public async Task<EntityDto> Create(SocialConflictCreateDto input)
        {
            if(input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _socialConflictRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
                if (await _socialConflictRepository.CountAsync(p => p.Code == $"{input.ReplaceYear} - {input.ReplaceCount}") > 0)
                    throw new UserFriendlyException(DefaultTitleMessage, "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var socialConflictId = await _socialConflictRepository.InsertAndGetIdAsync(await ValidateEntity(
                    input: ObjectMapper.Map<SocialConflict>(input),
                    verification: ObjectMapper.Map<SocialConflictVerificationRequestDto>(input),
                    analystId: input.Analyst == null ? -1 : input.Analyst.Id,
                    coordinatorId: input.Coordinator == null ? -1 : input.Coordinator.Id,
                    managerId: input.Manager == null ? -1 : input.Manager.Id,
                    typologyId: input.Typology == null ? -1 : input.Typology.Id,
                    subTypologyId: input.SubTypology == null ? -1 : input.SubTypology.Id,
                    sectorId: input.Sector == null ? -1 : input.Sector.Id,
                    locations: input.Locations ?? new List<SocialConflictLocationDto>(),
                    actors: input.Actors ?? new List<SocialConflictActorLocationDto>(),
                    risks: input.Risks ?? new List<SocialConflictRiskLocationDto>(),
                    generalFacts: input.GeneralFacts ?? new List<SocialConflictGeneralFactDto>(),
                    sugerences: input.Sugerences ?? new List<SocialConflictSugerenceDto>(),
                    managements: input.Managements ?? new List<SocialConflictManagementLocationDto>(),
                    states: input.States ?? new List<SocialConflictStateDto>(),
                    violenceFacts: input.ViolenceFacts ?? new List<SocialConflictViolenceFactDto>(),
                    conditions: input.Conditions ?? new List<SocialConflictConditionDto>(),
                    resources: new List<SocialConflictResourceDto>(),
                    notes: new List<SocialConflictNoteLocationDto>())); 

            await CurrentUnitOfWork.SaveChangesAsync();

            await FunctionManager.CallSocialConflictStateProcess(socialConflictId);
            await FunctionManager.CallSocialConflictVerificationProccess(socialConflictId);

            if(input.ReplaceCode)
                await FunctionManager.CallCreateSocialConflictCodeReplaceProcess(socialConflictId, input.ReplaceYear, input.ReplaceCount);
            else
                await FunctionManager.CallCreateSocialConflictCodeProcess(socialConflictId);
            
            return new EntityDto(socialConflictId);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _socialConflictRepository.CountAsync(p => p.Id == input.Id));
            await _socialConflictRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<SocialConflictGetDataDto> Get(NullableIdDto input)
        {
            var output = new SocialConflictGetDataDto
            {
                SocialConflict = new SocialConflictGetDto()
            };

            if (input.Id.HasValue)
            {
                var dbUser = await GetCurrentUserAsync();

                VerifyCount(await _socialConflictRepository.CountAsync(p => p.Id == input.Id));

                if (dbUser.Type != PersonType.None && await _socialConflictRepository.CountAsync(p => p.Id == input.Id && p.SocialConflictUsers.Any(p => p.UserId == dbUser.Id)) == 0)
                    throw new UserFriendlyException("Aviso", "Estimado usuario, usted no posee permisos para acceder al conflicto seleccionado");

                var socialConflict = _socialConflictRepository
                    .GetAll()
                    .Include(p => p.Analyst)
                    .Include(p => p.Coordinator)
                    .Include(p => p.Manager)
                    .Include(p => p.Typology)
                    .Include(p => p.SubTypology)
                    .Include(p => p.Sector)
                    .Where(p => p.Id == input.Id)
                    .First();

                var socialConflictItem = ObjectMapper.Map<SocialConflictGetDto>(socialConflict);

                socialConflictItem.Locations = ObjectMapper.Map<List<SocialConflictLocationDto>>(_socialConflictLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflict.Id == input.Id)
                    .ToList());

                socialConflictItem.Actors = ObjectMapper.Map<List<SocialConflictActorLocationDto>>(_socialConflictActorRepository
                    .GetAll()
                    .Include(p => p.ActorMovement)
                    .Include(p => p.ActorType)
                    .Where(p => p.SocialConflictId == input.Id)
                    .ToList());

                socialConflictItem.Risks = ObjectMapper.Map<List<SocialConflictRiskLocationDto>>(_socialConflictRiskRepository
                    .GetAll()
                    .Include(p => p.Risk)
                    .Where(p => p.SocialConflictId == input.Id)
                    .ToList());

                socialConflictItem.GeneralFacts = ObjectMapper.Map<List<SocialConflictGeneralFactDto>>(_socialConflictGeneralFactRepository
                    .GetAll()
                    .Where(p => p.SocialConflictId == input.Id)
                    .ToList());

                socialConflictItem.Sugerences =
                    (from sugerence in _socialConflictSugerenceRepository.GetAll().Where(p => p.SocialConflictId == input.Id)
                     join userCreation in _userRepository.GetAll() on sugerence.CreatorUserId equals userCreation.Id
                     into userResult
                     from resultA in userResult.DefaultIfEmpty()
                     join userAccept in _userRepository.GetAll() on sugerence.AcceptedUserId equals userAccept.Id
                     into userAcceptResult
                     from resultB in userAcceptResult.DefaultIfEmpty()
                     select new SocialConflictSugerenceDto()
                     {
                         CreatorUser = new SocialConflictUserDto()
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
                         AcceptedUser = sugerence.Accepted == false ? null : new SocialConflictUserDto()
                         {
                             Name = resultB == null ? "N/A" : resultB.Name,
                             Surname = resultB == null ? "" : resultB.Surname,
                             EmailAddress = resultB == null ? "" : resultB.EmailAddress
                         }
                     }).ToList();

                var managements = _socialConflictManagementRepository
                    .GetAll()
                    .Include(p => p.Management)
                    .Include(p => p.Manager)
                    .Include(p => p.Resources)
                    .Where(p => p.SocialConflictId == input.Id)
                    .ToList();

                socialConflictItem.Managements = new List<SocialConflictManagementLocationDto>();

                foreach (var management in managements)
                {
                    var managementItem = ObjectMapper.Map<SocialConflictManagementLocationDto>(management);
                    managementItem.Resources = new List<SocialConflictManagementResourceDto>();

                    foreach (var resource in management.Resources)
                    {

                        var resourceItem = ObjectMapper.Map<SocialConflictManagementResourceDto>(resource);
                        var userResourceExists = resource.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == resource.CreatorUserId) > 0;

                        if (userResourceExists)
                        {
                            var user = await _userRepository.GetAsync(resource.CreatorUserId.Value);
                            resourceItem.CreatorUserName = (user.Name ?? "").Trim() + " " + (user.Surname ?? "").Trim();
                        }

                        managementItem.Resources.Add(resourceItem);
                    }

                    socialConflictItem.Managements.Add(managementItem);
                }

                socialConflictItem.States =
                    (from state in _socialConflictStateRepository.GetAll().Where(p => p.SocialConflictId == input.Id)
                     join manager in _personRepository.GetAll() on state.ManagerId equals manager.Id into managerResult
                     from manager in managerResult.DefaultIfEmpty()
                     join user in _userRepository.GetAll() on state.CreatorUserId equals user.Id into userResult
                     from user in userResult.DefaultIfEmpty()
                     select new SocialConflictStateDto()
                     {
                         CreatorUser = new SocialConflictUserDto()
                         {
                             Name = user == null ? "N/A" : user.Name,
                             Surname = user == null ? "" : user.Surname,
                             EmailAddress = user == null ? "" : user.EmailAddress
                         },
                         Id = state.Id,
                         State = state.State,
                         Description = state.Description,
                         CreationTime = state.CreationTime,
                         VerificationChange = false,
                         VerificationState = state.Verification ? "true" : "false",
                         VerificationLocation = state.Verification,
                         StateTime = state.StateTime,
                         Manager = manager == null ? null : new SocialConflictPersonDto()
                         {
                             Id = manager.Id,
                             Name = manager.Name
                         },
                         Remove = false
                     }).ToList();

                socialConflictItem.ViolenceFacts = ObjectMapper.Map<List<SocialConflictViolenceFactDto>>(_socialConflictViolenceFactRepository
                    .GetAll()
                    .Include(p => p.Fact)
                    .Include(p => p.Manager)
                    .Include(p => p.Locations)
                    .ThenInclude(p => p.Department)
                    .Include(p => p.Locations)
                    .ThenInclude(p => p.Province)
                    .Include(p => p.Locations)
                    .ThenInclude(p => p.District)
                    .Include(p => p.Locations)
                    .ThenInclude(p => p.Region)
                    .Where(p => p.SocialConflictId == input.Id)
                    .ToList());

                socialConflictItem.Conditions = ObjectMapper.Map<List<SocialConflictConditionDto>>(_socialConflictConditionRepository
                    .GetAll()
                    .Where(p => p.SocialConflictId == input.Id)
                    .ToList());

                socialConflictItem.Notes = ObjectMapper.Map<List<SocialConflictNoteLocationDto>>(_socialConflictNoteRepository
                    .GetAll()
                    .Where(p => p.SocialConflictId == input.Id)
                    .ToList());

                socialConflictItem.Resources = ObjectMapper.Map<List<SocialConflictResourceDto>>(_socialConflictResourceRepository
                   .GetAll()
                   .Where(p => p.SocialConflictId == input.Id)
                   .ToList());                

                var userCreateExits = socialConflict.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == socialConflict.CreatorUserId) > 0;
                var userEditExits = socialConflict.LastModifierUserId.HasValue && await _userRepository.CountAsync(p => p.Id == socialConflict.LastModifierUserId) > 0;

                socialConflictItem.CreatorUser = userCreateExits ?
                    ObjectMapper.Map<SocialConflictUserDto>(await _userRepository.GetAsync(socialConflict.CreatorUserId.Value)) :
                    null;

                socialConflictItem.EditUser = userEditExits ?
                  ObjectMapper.Map<SocialConflictUserDto>(await _userRepository.GetAsync(socialConflict.LastModifierUserId.Value)) :
                  null;

                output.SocialConflict = socialConflictItem;
            }

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<SocialConflictDepartmentDto>();

            foreach(var item in departments)
            {
                var department = new SocialConflictDepartmentDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => p.TerritorialUnitId).ToArray(),
                    Provinces = ObjectMapper.Map<List<SocialConflictProvinceDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.TerritorialUnits = ObjectMapper.Map<List<SocialConflictTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().OrderBy(p => p.Name).ToListAsync());

            output.Persons = ObjectMapper.Map<List<SocialConflictPersonDto>>(_personRepository
                .GetAll()
                .Where(p => p.Enabled && p.Type != PersonType.None)
                .OrderBy(p => p.Name)
                .ToList());

            output.Typologies = ObjectMapper.Map<List<SocialConflictTypologyDto>>(_typologyRepository
                .GetAll()
                .Include(p => p.SubTypologies)
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList())
                .Select(p =>
                {
                    p.SubTypologies = p.SubTypologies.OrderBy(p => p.Name).Where(p => p.Enabled).ToList();
                    return p;
                }).ToList();

            output.Facts = ObjectMapper.Map<List<SocialConflictFactDto>>(_factRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.ActorTypes = ObjectMapper.Map<List<SocialConflictActorTypeDto>>(_actorTypeRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.ActorMovements = ObjectMapper.Map<List<SocialConflictActorMovementDto>>(_actorMovementRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Index)
                .ToList());

            output.Sectors = ObjectMapper.Map<List<SocialConflictSectorDto>>(_sectorRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());

            output.Risks = ObjectMapper.Map<List<SocialConflictRiskDto>>(_riskRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Index)
                .ToList());

            output.Managements = ObjectMapper.Map<List<SocialConflictManagementDto>>(_managementRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToList());
            
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<PagedResultDto<SocialConflictGetAllDto>> GetAll(SocialConflictGetAllInputDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            var query = _socialConflictRepository
                .GetAll()
                .Include(p => p.Locations)
                .ThenInclude(p => p.TerritorialUnit)
                .WhereIf(input.Verification.HasValue, p => p.Verification == input.Verification.Value)
                .WhereIf(dbUser.Type != PersonType.None, p => p.SocialConflictUsers.Any(p => p.UserId == dbUser.Id))
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflict.Filter));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var result = new List<SocialConflictGetAllDto>();

            foreach(var socialConflict in output)
            {
                var socialConflictItem = ObjectMapper.Map<SocialConflictGetAllDto>(socialConflict);

                var userCreateExits = socialConflict.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == socialConflict.CreatorUserId) > 0;
                var userEditExits = socialConflict.LastModifierUserId.HasValue && await _userRepository.CountAsync(p => p.Id == socialConflict.LastModifierUserId) > 0;

                socialConflictItem.CreatorUser = userCreateExits ?
                    ObjectMapper.Map<SocialConflictUserDto>(await _userRepository.GetAsync(socialConflict.CreatorUserId.Value)) :
                    new SocialConflictUserDto() { Name = "N/A", Surname = "" };

                socialConflictItem.EditUser = userEditExits ?
                  ObjectMapper.Map<SocialConflictUserDto>(await _userRepository.GetAsync(socialConflict.LastModifierUserId.Value)) :
                  new SocialConflictUserDto() { Name = "N/A", Surname = "" };
                                
                socialConflictItem.TerritorialUnits = socialConflictItem.Locations.Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(",");

                result.Add(socialConflictItem);
            }

            return new PagedResultDto<SocialConflictGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict_Geo)]
        public async Task<ListResultDto<SocialConflictGeoDto>> GetAllSocialConflictIntegration()
        {
            var query = _socialConflictRepository
                .GetAll()
                .Include(p => p.Typology)
                .Include(p => p.SubTypology)
                .Include(p => p.Sector)
                .Include(p => p.Locations)
                    .ThenInclude(p => p.TerritorialUnit)
                .Include(p => p.Locations)
                    .ThenInclude(p => p.Department)
                .Include(p => p.Locations)
                    .ThenInclude(p => p.Province)
                .Include(p => p.Locations)
                    .ThenInclude(p => p.District)
                .Include(p => p.Locations)
                    .ThenInclude(p => p.Region)
                .Where(p => p.Published);

            var result = await query.ToListAsync();

            var output = new List<SocialConflictGeoDto>();

            foreach(var item in result)
            {
                output.Add(new SocialConflictGeoDto()
                {
                    Id = item.Id,
                    CaseName = item.CaseName,
                    Description = item.Description,
                    TypologyName = item.Typology == null ? "" : item.Typology.Name,
                    SubTypologyName = item.Typology == null || item.SubTypology == null ? "" : item.SubTypology.Name,
                    SectorName = item.Sector == null ? "" : item.Sector.Name,
                    GovernmentLevelName = item.GovernmentLevel == GovernmentLevel.National ? "Nacional" : item.GovernmentLevel == GovernmentLevel.Location ? "Multiregional" : "Regional",
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Locations = ObjectMapper.Map<List<SocialConflictLocationGeoDto>>(item.Locations)
                });
            }

            return new ListResultDto<SocialConflictGeoDto>(output);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict_Actor)]
        public async Task<PagedResultDto<SocialConflictActorGetAllDto>> GetAllActors(SocialConflictActorGetAllInputDto input)
        {
            var query = _socialConflictActorRepository
                .GetAll()
                .Include(p => p.ActorType)
                .Include(p => p.ActorMovement)
                .Include(p => p.SocialConflict)
                .ThenInclude(p => p.Locations)
                .ThenInclude(p => p.Department)
                .Include(p => p.SocialConflictAlert)
                .ThenInclude(p => p.Locations)
                .ThenInclude(p => p.Department)
                .Include(p => p.SocialConflictSensible)
                .ThenInclude(p => p.Locations)
                .ThenInclude(p => p.Department)
                .Where(p => (p.SocialConflictId.HasValue == false || p.SocialConflict.IsDeleted == false) && 
                            (p.SocialConflictAlertId.HasValue == false || p.SocialConflictAlert.IsDeleted == false) && 
                            (p.SocialConflictSensibleId.HasValue == false || p.SocialConflictSensible.IsDeleted == false))
                .LikeAllBidirectional(input.NameSurname.SplitByLike(), nameof(SocialConflictActor.Name))
                .LikeAllBidirectional(input.Document.SplitByLike(), nameof(SocialConflictActor.Document));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input).ToList();
            var result = new List<SocialConflictActorGetAllDto>();

            foreach(var actor in output) {

                var item = ObjectMapper.Map<SocialConflictActorGetAllDto>(actor);

                if(actor.SocialConflictId.HasValue && actor.SocialConflict != null)
                {
                    item.ConflictId = actor.SocialConflict.Id;
                    item.Code = actor.SocialConflict.Code;
                    item.CaseName = actor.SocialConflict.CaseName;
                    item.Regions = String.Join(", ", actor.SocialConflict.Locations
                                    .Where(p => p.Department != null)
                                    .DistinctBy(p => p.Department.Id)
                                    .Select(p => p.Department.Name));

                    result.Add(item);
                }

                if(actor.SocialConflictAlertId.HasValue && actor.SocialConflictAlert != null)
                {
                    item.ConflictId = actor.SocialConflictAlert.Id;
                    item.Code = actor.SocialConflictAlert.Code;
                    item.CaseName = actor.SocialConflictAlert.Description;
                    item.Regions = String.Join(", ", actor.SocialConflictAlert.Locations
                                    .Where(p => p.Department != null)
                                    .DistinctBy(p => p.Department.Id)
                                    .Select(p => p.Department.Name));

                    result.Add(item);
                }

                if (actor.SocialConflictSensibleId.HasValue && actor.SocialConflictSensible != null)
                {
                    item.ConflictId = actor.SocialConflictSensible.Id;
                    item.Code = actor.SocialConflictSensible.Code;
                    item.CaseName = actor.SocialConflictSensible.CaseName;
                    item.Regions = String.Join(", ", actor.SocialConflictSensible.Locations
                                    .Where(p => p.Department != null)
                                    .DistinctBy(p => p.Department.Id)
                                    .Select(p => p.Department.Name));

                    result.Add(item);
                }
            }

            return new PagedResultDto<SocialConflictActorGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<PagedResultDto<SocialConflictCompromiseGetAllDto>> GetAllCompromises(SocialConflictCompromiseGetAllInputDto input)
        {
            var query = _compromiseRepository
                .GetAll()
                .Include(p => p.Status)
                .Where(p => p.Record.SocialConflict.Id == input.SocialConflictId.Value)
                .WhereIf(input.OnlyPriority, p => p.Type == CompromiseType.Activity && p.IsPriority)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(Compromise.Name));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<SocialConflictCompromiseGetAllDto>(count, ObjectMapper.Map<List<SocialConflictCompromiseGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict_Edit)]
        public async Task Update(SocialConflictUpdateDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            VerifyCount(await _socialConflictRepository.CountAsync(p => p.Id == input.Id));

            if (dbUser.Type != PersonType.None && await _socialConflictRepository.CountAsync(p => p.Id == input.Id && p.SocialConflictUsers.Any(p => p.UserId == dbUser.Id)) == 0)
                throw new UserFriendlyException("Aviso", "Estimado usuario, usted no posee permisos para modificar la información del conflicto seleccionado");

            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _socialConflictRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var dbSocialConflict = await _socialConflictRepository.GetAsync(input.Id);

            if (dbSocialConflict.CaseNameVerification)
                input.CaseName = dbSocialConflict.CaseName;
            if (dbSocialConflict.DescriptionVerification)
                input.Description = dbSocialConflict.Description;
            if(dbSocialConflict.ProblemVerification)
                input.Problem = dbSocialConflict.Problem;

            var socialConflictId = await _socialConflictRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                    input: ObjectMapper.Map(input, dbSocialConflict),
                    verification: ObjectMapper.Map<SocialConflictVerificationRequestDto>(input),
                    analystId: input.Analyst == null ? -1 : input.Analyst.Id,
                    coordinatorId: input.Coordinator == null ? -1 : input.Coordinator.Id,
                    managerId: input.Manager == null ? -1 : input.Manager.Id,
                    typologyId: input.Typology == null ? -1 : input.Typology.Id,
                    subTypologyId: input.SubTypology == null ? -1 : input.SubTypology.Id,
                    sectorId: input.Sector == null ? -1 : input.Sector.Id,
                    locations: input.Locations ?? new List<SocialConflictLocationDto>(),
                    actors: input.Actors ?? new List<SocialConflictActorLocationDto>(),
                    risks: input.Risks ?? new List<SocialConflictRiskLocationDto>(),
                    generalFacts: input.GeneralFacts ?? new List<SocialConflictGeneralFactDto>(),
                    sugerences: input.Sugerences ?? new List<SocialConflictSugerenceDto>(),
                    managements: input.Managements ?? new List<SocialConflictManagementLocationDto>(),
                    states: input.States ?? new List<SocialConflictStateDto>(),
                    violenceFacts: input.ViolenceFacts ?? new List<SocialConflictViolenceFactDto>(),
                    conditions: input.Conditions ?? new List<SocialConflictConditionDto>(),
                    resources: input.Resources ?? new List<SocialConflictResourceDto>(),
                    notes: input.Notes ?? new List<SocialConflictNoteLocationDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateSocialConflictCodeReplaceProcess(socialConflictId, input.ReplaceYear, input.ReplaceCount);

            await FunctionManager.CallSocialConflictStateProcess(socialConflictId);
            await FunctionManager.CallSocialConflictVerificationProccess(socialConflictId);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflict_Resource)]
        public async Task<EntityDto> CreateNote(SocialConflictNoteCreateDto input)
        {
            input.Description.IsValidOrException(DefaultTitleMessage, "La descripción de la nota es obligatoria");
            input.Description.VerifyTableColumn(SocialConflictNoteConsts.DescriptionMinLength,
                SocialConflictNoteConsts.DescriptionMaxLength,
                DefaultTitleMessage,
                $"La descripción de la nota no debe exceder los {SocialConflictNoteConsts.DescriptionMaxLength} caracteres");

            var dbUser = await GetCurrentUserAsync();

            if (dbUser.Type != PersonType.None)
            {
                if (await _socialConflictRepository.CountAsync(p => p.Id == input.SocialConflictId && p.SocialConflictUsers.Any(p => p.UserId == dbUser.Id)) == 0)
                    throw new UserFriendlyException("Aviso", "Estimado usuario, usted no posee permisos para acceder al conflicto seleccionado");
            }
            else
            {
                if (await _socialConflictRepository.CountAsync(p => p.Id == input.SocialConflictId) == 0)
                    throw new UserFriendlyException("Aviso", "El caso de conflictividad que hace referencia ya no existe o fue eliminado. Verifique la información antes de continuar");
            }

            var note = ObjectMapper.Map<SocialConflictNote>(input);

            var noteId = await _socialConflictNoteRepository.InsertAndGetIdAsync(note);

            return new EntityDto(noteId);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflict_Resource)]
        public async Task<EntityDto> UpdateNote(SocialConflictNoteUpdateDto input)
        {
            input.Description.IsValidOrException(DefaultTitleMessage, "La descripción de la nota es obligatoria");
            input.Description.VerifyTableColumn(SocialConflictNoteConsts.DescriptionMinLength,
                SocialConflictNoteConsts.DescriptionMaxLength,
                DefaultTitleMessage,
                $"La descripción de la nota no debe exceder los {SocialConflictNoteConsts.DescriptionMaxLength} caracteres");

            if (await _socialConflictNoteRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "La nota a la que hace referencia ya no existe o fue eliminado. Verifique la información antes de continuar");

            var note = ObjectMapper.Map(input, await _socialConflictNoteRepository.GetAsync(input.Id));

            var dbUser = await GetCurrentUserAsync();

            if (dbUser.Type != PersonType.None)
            {
                if (await _socialConflictRepository.CountAsync(p => p.Id == note.SocialConflictId && p.SocialConflictUsers.Any(p => p.UserId == dbUser.Id)) == 0)
                    throw new UserFriendlyException("Aviso", "Estimado usuario, usted no posee permisos para acceder al conflicto seleccionado");
            }
            else
            {
                if (await _socialConflictRepository.CountAsync(p => p.Id == note.SocialConflictId) == 0)
                    throw new UserFriendlyException("Aviso", "El caso de conflictividad que hace referencia ya no existe o fue eliminado. Verifique la información antes de continuar");
            }

            await _socialConflictNoteRepository.UpdateAsync(note);

            return new EntityDto(note.Id);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflict_Resource)]
        public async Task DeleteNote(EntityDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            if(dbUser.Type != PersonType.None)
                if (await _socialConflictNoteRepository.CountAsync(p => p.Id == input.Id && p.SocialConflict.SocialConflictUsers.Any(p => p.UserId == dbUser.Id)) == 0)
                    throw new UserFriendlyException("Aviso", "Estimado usuario, usted no posee permisos para acceder al conflicto seleccionado");
            else
                VerifyCount(await _socialConflictNoteRepository.CountAsync(p => p.Id == input.Id));

            await _socialConflictNoteRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflict_Resource)]
        public async Task<EntityDto> CreateResource(SocialConflictCreateResourceDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            if (dbUser.Type != PersonType.None)
                if (await _socialConflictRepository.CountAsync(p => p.Id == input.SocialConflictId && p.SocialConflictUsers.Any(p => p.UserId == dbUser.Id)) == 0)
                    throw new UserFriendlyException("Aviso", "Estimado usuario, usted no posee permisos para acceder al conflicto seleccionado");
            
            VerifyCount(await _socialConflictRepository.CountAsync(p => p.Id == input.SocialConflictId));

            if (ResourceManager.TokenIsValid(input.Resource.Token) == false)
                throw new UserFriendlyException("Aviso", "La validez de los archivos subidos a caducado, por favor intente nuevamente.");

            var resource = ObjectMapper.Map<SocialConflictResource>(ResourceManager.Create(input.Resource, ResourceConsts.SocialConflict));
            resource.SocialConflictId = input.SocialConflictId;

            var resourceId = await _socialConflictResourceRepository.InsertAndGetIdAsync(resource);

            return new EntityDto(resourceId);
        }

        [AbpAuthorize(AppPermissions.App_SocialConflict_Resource)]
        public async Task DeleteResource(EntityDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            if (dbUser.Type != PersonType.None)
                if (await _socialConflictResourceRepository.CountAsync(p => p.Id == input.Id && p.SocialConflict.SocialConflictUsers.Any(p => p.UserId == dbUser.Id)) == 0)
                    throw new UserFriendlyException("Aviso", "Estimado usuario, usted no posee permisos para acceder al conflicto seleccionado");
                else
                    VerifyCount(await _socialConflictResourceRepository.CountAsync(p => p.Id == input.Id));

            await _socialConflictResourceRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<FileDto> GetMatrizToExcel(SocialConflictGetAllInputDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            var query = _socialConflictRepository
                .GetAll()
                .Include(p => p.Analyst)
                .Include(p => p.Coordinator)
                .Include(p => p.Manager)
                .Include(p => p.Typology)
                .Include(p => p.SubTypology)
                .Include(p => p.Sector)
                .WhereIf(input.Verification.HasValue, p => p.Verification == input.Verification.Value)
                .WhereIf(dbUser.Type != PersonType.None, p => p.SocialConflictUsers.Any(p => p.UserId == dbUser.Id))
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflict.Filter));

            var data = new List<SocialConflictMatrizExportDto>();
            var result = query.OrderBy(input.Sorting).ToList();

            foreach (var dbSocialConflict in result)
            {
                var item = new SocialConflictMatrizExportDto();

                item.Code = dbSocialConflict.Code;
                item.CaseName = dbSocialConflict.CaseName;
                item.Description = dbSocialConflict.Description;
                item.Problem = dbSocialConflict.Problem;

                item.CoordinatorName = dbSocialConflict.Coordinator != null ? dbSocialConflict.Coordinator.Name : "";
                item.ManagerName = dbSocialConflict.Manager != null ? dbSocialConflict.Manager.Name : "";
                item.AnalystName = dbSocialConflict.Analyst != null ? dbSocialConflict.Analyst.Name : "";

                item.TypologyDescription = dbSocialConflict.Typology != null ? dbSocialConflict.Typology.Name : "";
                item.SubTypologyDescription = dbSocialConflict.SubTypology != null ? dbSocialConflict.SubTypology.Name : "";

                item.SectorDescription = dbSocialConflict.Sector != null ? dbSocialConflict.Sector.Name : "";

                item.CaseNameVerification = dbSocialConflict.CaseNameVerification;
                item.ProblemVerification = dbSocialConflict.ProblemVerification;
                item.DescriptionVerification = dbSocialConflict.DescriptionVerification;
                item.RiskVerification = dbSocialConflict.RiskVerification;
                item.ManagementVerification = dbSocialConflict.ManagementVerification;
                item.StateVerification = dbSocialConflict.StateVerification;
                item.ConditionVerification = dbSocialConflict.ConditionVerification;

                var locations = _socialConflictLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflict.Id == dbSocialConflict.Id)
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

                if (dbSocialConflict.LastSocialConflictRiskId.HasValue)
                {
                    var lastSocialConflictRisk = _socialConflictRiskRepository
                        .GetAll()
                        .Include(p => p.Risk)
                        .Where(p => p.Id == dbSocialConflict.LastSocialConflictRiskId.Value)
                        .FirstOrDefault();

                    if(lastSocialConflictRisk != null && lastSocialConflictRisk.Risk != null)
                    {
                        item.LastRisk = lastSocialConflictRisk.Risk.Name ?? "";
                        item.LastRiskDescription = lastSocialConflictRisk.Description ?? "";
                        item.LastRiskTime = lastSocialConflictRisk.RiskTime;
                    }
                }

                if (dbSocialConflict.LastSocialConflictConditionId.HasValue)
                {
                    var lastSocialConflictCondition = _socialConflictConditionRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflict.LastSocialConflictConditionId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictCondition != null)
                    {
                        item.LastCondition = lastSocialConflictCondition.Type == ConditionType.Open ? "Activo" : "Inactivo";
                        item.LastConditionDescription = lastSocialConflictCondition.Description ?? "";
                        item.LastConditionTime = lastSocialConflictCondition.ConditionTime;
                    }
                }

                var actors = _socialConflictActorRepository
                    .GetAll()
                    .Include(p => p.ActorMovement)
                    .Include(p => p.ActorType)
                    .Where(p => p.SocialConflictId == dbSocialConflict.Id)
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

                if(dbSocialConflict.LastSocialConflictManagementId.HasValue)
                {
                    var lastSocialConflictManagement = _socialConflictManagementRepository
                        .GetAll()
                        .Include(p => p.Management)
                        .Include(p => p.Manager)
                        .Where(p => p.Id == dbSocialConflict.LastSocialConflictManagementId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictManagement != null)
                    {
                        item.LastManagement = lastSocialConflictManagement.Management == null ? "" : lastSocialConflictManagement.Management.Name ?? "";
                        item.LastManagementDescription = lastSocialConflictManagement.Description ?? "";
                        item.LastManagementTime = lastSocialConflictManagement.ManagementTime;
                        item.LastManagementManager = lastSocialConflictManagement.Manager == null ? "" : lastSocialConflictManagement.Manager.Name ?? "";
                    }
                }

                if (dbSocialConflict.LastSocialConflictStateId.HasValue)
                {
                    var lastSocialConflictState = _socialConflictStateRepository
                        .GetAll()
                        .Include(p => p.Manager)
                        .Where(p => p.Id == dbSocialConflict.LastSocialConflictStateId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictState != null)
                    {
                        item.LastState = lastSocialConflictState.State ?? "";
                        item.LastStateDescription = lastSocialConflictState.Description ?? "";
                        item.LastStateTime = lastSocialConflictState.StateTime;
                        item.LastStateManager = lastSocialConflictState.Manager == null ? "" : lastSocialConflictState.Manager.Name ?? "";
                    }
                }

                if(dbSocialConflict.CreatorUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflict.CreatorUserId.Value)
                        .FirstOrDefault();

                    item.CreatorUser = user?.GetNameSurname();
                    item.CreationTime = dbSocialConflict.CreationTime;
                }

                if (dbSocialConflict.LastModifierUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflict.LastModifierUserId.Value)
                        .FirstOrDefault();

                    item.LastModificationUser = user?.GetNameSurname();
                    item.LastModificationTime = dbSocialConflict.LastModificationTime;
                }

                data.Add(item);
            }

            return _socialConflictExporter.ExportMatrizToFile(data);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<FileDto> GetManagementToExcel(SocialConflictGetAllInputDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            var query = _socialConflictManagementRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.Manager)
                .Include(p => p.Management)
                .Where(p => p.SocialConflict.IsDeleted == false)
                .WhereIf(input.Verification.HasValue, p => p.SocialConflict.Verification == input.Verification.Value)
                .WhereIf(dbUser.Type != PersonType.None, p => p.SocialConflict.SocialConflictUsers.Any(p => p.UserId == dbUser.Id))
                .WhereIf(input.Code.IsValid(), p => p.SocialConflict.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.SocialConflict.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.SocialConflict.CreationTime >= input.StartTime.Value && p.SocialConflict.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflict.Filter));

            var data = new List<SocialConflictManagementExportDto>();
            var result = query.OrderBy(input.Sorting).ToList();

            foreach (var dbSocialConflictManagement in result)
            {
                var item = new SocialConflictManagementExportDto();

                item.CaseCode = dbSocialConflictManagement.SocialConflict.Code;
                item.CaseName = dbSocialConflictManagement.SocialConflict.CaseName;

                item.ManagementTime = dbSocialConflictManagement.ManagementTime;
                item.ManagementDescription = dbSocialConflictManagement.Description;
                item.Management = dbSocialConflictManagement.Management == null ? "" : dbSocialConflictManagement.Management.Name ?? "";
                item.ManagementManager = dbSocialConflictManagement.Manager == null ? "" : dbSocialConflictManagement.Manager.Name ?? "";
                item.CivilMen = dbSocialConflictManagement.CivilMen;
                item.CivilWomen = dbSocialConflictManagement.CivilWomen;
                item.StateMen = dbSocialConflictManagement.StateMen;
                item.StateWomen = dbSocialConflictManagement.StateWomen;
                item.CompanyMen = dbSocialConflictManagement.CompanyMen;
                item.CompanyWomen = dbSocialConflictManagement.CompanyWomen;
                item.Verification = dbSocialConflictManagement.Verification;

                if (dbSocialConflictManagement.SocialConflict.LastSocialConflictRiskId.HasValue)
                {
                    var lastSocialConflictRisk = _socialConflictRiskRepository
                        .GetAll()
                        .Include(p => p.Risk)
                        .Where(p => p.Id == dbSocialConflictManagement.SocialConflict.LastSocialConflictRiskId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictRisk != null && lastSocialConflictRisk.Risk != null)
                    {
                        item.LastCaseRisk = lastSocialConflictRisk.Risk.Name ?? "";
                        item.LastCaseRiskDescription = lastSocialConflictRisk.Description ?? "";
                        item.LastCaseRiskTime = lastSocialConflictRisk.RiskTime;
                    }
                }

                if (dbSocialConflictManagement.SocialConflict.LastSocialConflictConditionId.HasValue)
                {
                    var lastSocialConflictCondition = _socialConflictConditionRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictManagement.SocialConflict.LastSocialConflictConditionId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictCondition != null)
                    {
                        item.LastCaseCondition = lastSocialConflictCondition.Type == ConditionType.Open ? "Activo" : "Inactivo";
                        item.LastCaseConditionDescription = lastSocialConflictCondition.Description ?? "";
                        item.LastCaseConditionTime = lastSocialConflictCondition.ConditionTime;
                    }
                }

                var locations = _socialConflictLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflict.Id == dbSocialConflictManagement.SocialConflictId)
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

            return _socialConflictExporter.ExportManagementToFile(data);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public async Task<FileDto> GetStateToExcel(SocialConflictGetAllInputDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            var query = _socialConflictStateRepository
                .GetAll()
                .Include(p => p.Manager)
                .Include(p => p.SocialConflict)
                .Where(p => p.SocialConflict.IsDeleted == false)
                .WhereIf(input.Verification.HasValue, p => p.SocialConflict.Verification == input.Verification.Value)
                .WhereIf(dbUser.Type != PersonType.None, p => p.SocialConflict.SocialConflictUsers.Any(p => p.UserId == dbUser.Id))
                .WhereIf(input.Code.IsValid(), p => p.SocialConflict.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.SocialConflict.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.SocialConflict.CreationTime >= input.StartTime.Value && p.SocialConflict.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflict.Filter));

            var data = new List<SocialConflictStateExportDto>();
            var result = query.OrderBy(input.Sorting).ToList();

            foreach (var dbSocialConflictState in result)
            {
                var item = new SocialConflictStateExportDto();

                item.CaseCode = dbSocialConflictState.SocialConflict.Code;
                item.CaseName = dbSocialConflictState.SocialConflict.CaseName;

                if (dbSocialConflictState.SocialConflict.LastSocialConflictRiskId.HasValue)
                {
                    var lastSocialConflictRisk = _socialConflictRiskRepository
                        .GetAll()
                        .Include(p => p.Risk)
                        .Where(p => p.Id == dbSocialConflictState.SocialConflict.LastSocialConflictRiskId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictRisk != null && lastSocialConflictRisk.Risk != null)
                    {
                        item.LastCaseRisk = lastSocialConflictRisk.Risk.Name ?? "";
                        item.LastCaseRiskDescription = lastSocialConflictRisk.Description ?? "";
                        item.LastCaseRiskTime = lastSocialConflictRisk.RiskTime;
                    }
                }

                if (dbSocialConflictState.SocialConflict.LastSocialConflictConditionId.HasValue)
                {
                    var lastSocialConflictCondition = _socialConflictConditionRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictState.SocialConflict.LastSocialConflictConditionId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictCondition != null)
                    {
                        item.LastCaseCondition = lastSocialConflictCondition.Type == ConditionType.Open ? "Activo" : "Inactivo";
                        item.LastCaseConditionDescription = lastSocialConflictCondition.Description ?? "";
                        item.LastCaseConditionTime = lastSocialConflictCondition.ConditionTime;
                    }
                }

                var locations = _socialConflictLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflict.Id == dbSocialConflictState.SocialConflictId)
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

                item.State = dbSocialConflictState.State ?? "";
                item.StateDescription = dbSocialConflictState.Description ?? "";
                item.StateTime = dbSocialConflictState.StateTime;
                item.StateManager = dbSocialConflictState.Manager == null ? "" : dbSocialConflictState.Manager.Name;
                item.Verification = dbSocialConflictState.Verification;

                data.Add(item);
            }

            return _socialConflictExporter.ExportStateToFile(data);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict_Actor)]
        public async Task<FileDto> GetActorMatrizToExcel(SocialConflictActorGetAllInputDto input)
        {
            var query = _socialConflictActorRepository
                .GetAll()
                .Include(p => p.ActorType)
                .Include(p => p.ActorMovement)
                .Include(p => p.SocialConflict)
                .ThenInclude(p => p.Locations)
                .ThenInclude(p => p.Department)
                .Include(p => p.SocialConflictAlert)
                .ThenInclude(p => p.Locations)
                .ThenInclude(p => p.Department)
                .Include(p => p.SocialConflictSensible)
                .ThenInclude(p => p.Locations)
                .ThenInclude(p => p.Department)
                .Where(p => (p.SocialConflictId.HasValue == false || p.SocialConflict.IsDeleted == false) &&
                            (p.SocialConflictAlertId.HasValue == false || p.SocialConflictAlert.IsDeleted == false) &&
                            (p.SocialConflictSensibleId.HasValue == false || p.SocialConflictSensible.IsDeleted == false))
                .LikeAllBidirectional(input.NameSurname.SplitByLike(), nameof(SocialConflictActor.Name))
                .LikeAllBidirectional(input.Document.SplitByLike(), nameof(SocialConflictActor.Document));

            var output = await query.OrderBy(input.Sorting).ToListAsync();
            var data = new List<SocialConflictActorExcelExportDto>();

            foreach (var actor in output)
            {
                var item = new SocialConflictActorExcelExportDto();

                item.Name = (actor.Name ?? "").Trim();
                item.Document = (actor.Document ?? "").Trim();
                item.Job = (actor.Job ?? "").Trim();
                item.Community = (actor.Community ?? "").Trim();
                item.PhoneNumber = (actor.PhoneNumber ?? "").Trim();
                item.EmailAddress = (actor.EmailAddress ?? "").Trim();
                item.IsPoliticalAssociation = actor.IsPoliticalAssociation ? "Si" : "No";
                item.Position = (actor.Position ?? "").Trim();
                item.Interest = (actor.Interest ?? "").Trim();
                item.Site = actor.Site == ActorSite.SocialConflict ? "Caso" : actor.Site == ActorSite.SocialConflictSensible ? "Situación Sensible" : actor.Site == ActorSite.SocialConflictAlert ? "Alerta" : "";
                item.ActorType = actor.ActorType == null ? "" : actor.ActorType.Name ?? "";
                item.ActorMovement = actor.ActorMovement == null ? "" : actor.ActorMovement.Name ?? "";

                if (actor.SocialConflictId.HasValue && actor.SocialConflict != null)
                {
                    item.CaseCode = actor.SocialConflict.Code;
                    item.CaseName = actor.SocialConflict.CaseName;
                    item.Regions = String.Join(", ", actor.SocialConflict.Locations
                                    .Where(p => p.Department != null)
                                    .DistinctBy(p => p.Department.Id)
                                    .Select(p => p.Department.Name));

                    data.Add(item);
                }

                if (actor.SocialConflictAlertId.HasValue && actor.SocialConflictAlert != null)
                {
                    item.CaseCode = actor.SocialConflictAlert.Code;
                    item.CaseName = actor.SocialConflictAlert.Description;
                    item.Regions = String.Join(", ", actor.SocialConflictAlert.Locations
                                    .Where(p => p.Department != null)
                                    .DistinctBy(p => p.Department.Id)
                                    .Select(p => p.Department.Name));

                    data.Add(item);
                }

                if (actor.SocialConflictSensibleId.HasValue && actor.SocialConflictSensible != null)
                {
                    item.CaseCode = actor.SocialConflictSensible.Code;
                    item.CaseName = actor.SocialConflictSensible.CaseName;
                    item.Regions = String.Join(", ", actor.SocialConflictSensible.Locations
                                    .Where(p => p.Department != null)
                                    .DistinctBy(p => p.Department.Id)
                                    .Select(p => p.Department.Name));

                    data.Add(item);
                }
            }

            return _socialConflictExporter.ExportActorToFile(data);
        }

        async Task<SocialConflict> ValidateEntity(
            SocialConflict input,
            SocialConflictVerificationRequestDto verification,
            int analystId,
            int coordinatorId,
            int managerId,
            int typologyId,
            int subTypologyId,
            int sectorId,
            List<SocialConflictLocationDto> locations,
            List<SocialConflictActorLocationDto> actors,
            List<SocialConflictRiskLocationDto> risks,
            List<SocialConflictGeneralFactDto> generalFacts,
            List<SocialConflictSugerenceDto> sugerences,
            List<SocialConflictManagementLocationDto> managements,
            List<SocialConflictStateDto> states,
            List<SocialConflictViolenceFactDto> violenceFacts,
            List<SocialConflictConditionDto> conditions,
            List<SocialConflictResourceDto> resources,
            List<SocialConflictNoteLocationDto> notes)
        {
            input.CaseName.IsValidOrException(DefaultTitleMessage, "La denominación del caso (mesa de diálogo) es obligatoria");
            input.CaseName.VerifyTableColumn(SocialConflictConsts.CaseNameMinLength, SocialConflictConsts.CaseNameMaxLength, DefaultTitleMessage, $"La denominación del caso (mesa de diálogo) no debe exceder los {SocialConflictConsts.CaseNameMaxLength} caracteres");
                        
            input.Description.VerifyTableColumn(SocialConflictConsts.DescriptionMinLength, SocialConflictConsts.DescriptionMaxLength, DefaultTitleMessage, $"La descripción del caso del conflicto social no debe exceder los {SocialConflictConsts.DescriptionMaxLength} caracteres");
            input.Dialog.VerifyTableColumn(SocialConflictConsts.DialogMinLength, SocialConflictConsts.DialogMaxLength, DefaultTitleMessage, $"El espacio de diálogo no debe exceder los {SocialConflictConsts.DialogMaxLength} caracteres");
            input.Problem.VerifyTableColumn(SocialConflictConsts.ProblemMinLength, SocialConflictConsts.ProblemMaxLength, DefaultTitleMessage, $"La problemática no debe exceder los {SocialConflictConsts.ProblemMaxLength} caracteres");
            input.Plaint.VerifyTableColumn(SocialConflictConsts.PlaintMinLength, SocialConflictConsts.PlaintMaxLength, DefaultTitleMessage, $"La demanda en el nivel de riesgo no debe exceder los {SocialConflictConsts.PlaintMaxLength} caracteres");
            input.FactorContext.VerifyTableColumn(SocialConflictConsts.FactorContextMinLength, SocialConflictConsts.FactorContextMaxLength, DefaultTitleMessage, $"Los factores de contexto en el nivel de riesgo no debe exceder los {SocialConflictConsts.FactorContextMaxLength} caracteres");
            input.Strategy.VerifyTableColumn(SocialConflictConsts.StrategyMinLength, SocialConflictConsts.StrategyMaxLength, DefaultTitleMessage, $"La estrategia y proceso de abordaje en el nivel de riesgo no debe exceder los {SocialConflictConsts.StrategyMaxLength} caracteres");

            input.Filter = string.Concat(input.Code ?? "", " ", input.CaseName ?? "", " ", input.Description ?? "");
            input.Locations = new List<SocialConflictLocation>();
            input.Actors = new List<SocialConflictActor>();
            input.GeneralFacts = new List<SocialConflictGeneralFact>();
            input.Sugerences = new List<SocialConflictSugerence>();
            input.Managements = new List<SocialConflictManagement>();
            input.States = new List<SocialConflictState>();
            input.ViolenceFacts = new List<SocialConflictViolenceFact>();
            input.Risks = new List<SocialConflictRisk>();
            input.Conditions = new List<SocialConflictCondition>();
            input.VerificationHistories = new List<SocialConflictVerificationHistory>();

            var hasVerificationPermission = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflict_Verification);

            if (hasVerificationPermission)
            {
                if (verification.CaseNameVerificationChange)
                {
                    var oldState = input.CaseNameVerification;
                    var newState = ((verification.CaseNameVerificationState ?? "false") == "true");

                    input.CaseNameVerification = newState;

                    if (oldState != newState)
                        input.VerificationHistories.Add(new SocialConflictVerificationHistory()
                        {
                            Site = SocialConflictVerificationSite.Name,
                            OldState = oldState,
                            NewState = newState
                        });
                }
                    

                if (verification.DescriptionVerificationChange)
                {
                    var oldState = input.DescriptionVerification;
                    var newState = ((verification.DescriptionVerificationState ?? "false") == "true");

                    input.DescriptionVerification = newState;

                    if (oldState != newState)
                        input.VerificationHistories.Add(new SocialConflictVerificationHistory()
                        {
                            Site = SocialConflictVerificationSite.Description,
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
                        input.VerificationHistories.Add(new SocialConflictVerificationHistory()
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
                    throw new UserFriendlyException("Aviso", "El gestor del cargo ya no existe o fue eliminado. Verifique la información antes de continuar");

                var manager = await _personRepository.GetAsync(managerId);

                input.Manager = manager;
                input.ManagerId = manager.Id;
            }
            else
            {
                input.Manager = null;
                input.ManagerId = null;
            }

            if (sectorId > 0)
            {
                if (await _sectorRepository.CountAsync(p => p.Id == sectorId) == 0)
                    throw new UserFriendlyException("Aviso", "El sector responsable ya no existe o fue eliminado. Verifique la información antes de continuar");

                var sector = await _sectorRepository.GetAsync(sectorId);

                input.Sector = sector;
                input.SectorId = sector.Id;
            } 
            else
            {
                input.Sector = null;
                input.SectorId = null;
            }

            if (typologyId > 0)
            {
                if (await _typologyRepository.CountAsync(p => p.Id == typologyId) == 0)
                    throw new UserFriendlyException("Aviso", "La tipología general ya no existe o fue eliminada. Verifique la información antes de continuar");

                var typology = await _typologyRepository.GetAsync(typologyId);

                input.Typology = typology;
                input.TypologyId = typology.Id;

                if (subTypologyId > 0)
                {
                    if (await _subTypologyRepository.CountAsync(p => p.Id == subTypologyId) == 0)
                        throw new UserFriendlyException("Aviso", "La tipología detallada ya no existe o fue eliminada. Verifique la información antes de continuar");

                    var subTypology = await _subTypologyRepository.GetAsync(subTypologyId);

                    input.SubTypology = subTypology;
                    input.SubTypologyId = subTypology.Id;
                }
                else
                {
                    input.SubTypology = null;
                    input.SubTypologyId = null;
                }
            }
            else
            {
                input.Typology = null;
                input.TypologyId = null;
                input.SubTypology = null;
                input.SubTypologyId = null;
            }

            if (input.GeographicType == GeographycType.None)
                throw new UserFriendlyException("Aviso", "El tipo de cobertura geográfica es obligatorio");
            
            foreach (var location in locations)
            {
                if(location.Remove)
                {
                    if(location.Id > 0 && input.Id > 0 && await _socialConflictLocationRepository.CountAsync(p => p.Id == location.Id && p.SocialConflict.Id == input.Id) > 0)
                    {
                        await _socialConflictLocationRepository.DeleteAsync(location.Id);
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

                            input.Locations.Add(new SocialConflictLocation()
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
                var records = actors.Where(p => p.ActorId == actor.ActorId && p.Remove == false).Count();

                if (records > 1)
                {
                    throw new UserFriendlyException("Aviso", $"El actor {actor.Name} ya fue agregado a este registro. Verifique la información antes de continuar");
                }
                if (actor.Remove)
                {
                    if (actor.Id > 0 && input.Id > 0 && await _socialConflictActorRepository.CountAsync(p => p.Id == actor.Id && p.SocialConflict.Id == input.Id) > 0)
                    {
                        await _socialConflictActorRepository.DeleteAsync(actor.Id);
                        continue;
                    }
                }
                else
                {
                    if (actor.ActorId > 0)
                    {
                        if (await _socialConflictActorRepository.CountAsync(p => p.ActorId == actor.ActorId && p.SocialConflictId == input.Id) == 0)
                        {
                            input.Actors.Add(new SocialConflictActor()
                            {
                                Name = actor.Name,
                                Document = actor.Document,
                                Job = actor.Job,
                                Community = actor.Community,
                                PhoneNumber = actor.PhoneNumber,
                                EmailAddress = actor.EmailAddress,
                                IsPoliticalAssociation = actor.IsPoliticalAssociation,
                                PoliticalAssociation = actor.IsPoliticalAssociation ? actor.PoliticalAssociation : null,
                                Position = actor.ActorType.ShowDetail ? actor.Position : null,
                                Interest = actor.ActorType.ShowDetail ? actor.Interest : null,
                                ActorTypeId = actor.ActorType.Id,
                                ActorMovementId = actor.ActorMovement == null ? (int?)null : actor.ActorMovement.Id,
                                ActorId = actor.ActorId,
                                Site = ActorSite.SocialConflict
                            });
                        }
                    }
                }
            }

            foreach (var risk in risks)
            {
                if (risk.Remove)
                {
                    if (risk.Id > 0 && input.Id > 0 && await _socialConflictRiskRepository.CountAsync(p => p.Id == risk.Id && p.SocialConflict.Id == input.Id && p.Verification == false) > 0)
                    {
                        await _socialConflictRiskRepository.DeleteAsync(risk.Id);
                    }
                }
                else
                {
                    risk.Description.VerifyTableColumn(SocialConflictRiskConsts.DescriptionMinLength,
                        SocialConflictRiskConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del nivel de riesgo {risk.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictRiskConsts.DescriptionMaxLength} caracteres");

                    var dbRisk = _riskRepository
                        .GetAll()
                        .Where(p => p.Id == risk.Risk.Id)
                        .FirstOrDefault();

                    if(dbRisk == null)
                        throw new UserFriendlyException("Aviso", $"El nivel de riesgo {risk.Risk.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (risk.Id > 0)
                    {
                        if (await _socialConflictRiskRepository.CountAsync(p => p.Id == risk.Id && p.SocialConflict.Id == input.Id) > 0)
                        {
                            var dbSocialConflictGeneralRisk = await _socialConflictRiskRepository.GetAsync(risk.Id);

                            if(dbSocialConflictGeneralRisk.Verification == false)
                            {
                                dbSocialConflictGeneralRisk.Description = risk.Description;
                                dbSocialConflictGeneralRisk.RiskTime = new DateTime(risk.RiskTime.Year, risk.RiskTime.Month, risk.RiskTime.Day);
                                dbSocialConflictGeneralRisk.RiskId = dbRisk.Id;
                                dbSocialConflictGeneralRisk.Risk = dbRisk;
                            }

                            if (hasVerificationPermission && risk.VerificationChange)
                            {
                                var oldState = dbSocialConflictGeneralRisk.Verification;
                                var newState = ((risk.VerificationState ?? "false") == "true");

                                dbSocialConflictGeneralRisk.Verification = newState;

                                if (oldState != newState)
                                    input.VerificationHistories.Add(new SocialConflictVerificationHistory()
                                    {
                                        Site = SocialConflictVerificationSite.Risk,
                                        OldState = oldState,
                                        NewState = newState,
                                        EntityId = risk.Id
                                    });
                            }

                            await _socialConflictRiskRepository.UpdateAsync(dbSocialConflictGeneralRisk);
                        }
                    }
                    else
                    {
                        input.Risks.Add(new SocialConflictRisk()
                        {
                            Description = risk.Description,
                            RiskTime = new DateTime(risk.RiskTime.Year, risk.RiskTime.Month, risk.RiskTime.Day),
                            RiskId = dbRisk.Id,
                            Verification = risk.VerificationChange && hasVerificationPermission && ((risk.VerificationState ?? "false") == "true"),
                            Risk = dbRisk
                        });
                    }
                }
            }

            foreach (var generalFact in generalFacts)
            {
                if (generalFact.Remove)
                {
                    if (generalFact.Id > 0 && input.Id > 0 && await _socialConflictGeneralFactRepository.CountAsync(p => p.Id == generalFact.Id && p.SocialConflict.Id == input.Id) > 0)
                    {
                        await _socialConflictGeneralFactRepository.DeleteAsync(generalFact.Id);
                    }
                }
                else
                {
                    generalFact.Description.IsValidOrException(DefaultTitleMessage,
                        "El detalle del hecho revelevante del conflicto social es obligatorio");

                    generalFact.Description.VerifyTableColumn(SocialConflictGeneralFactConsts.DescriptionMinLength, 
                        SocialConflictGeneralFactConsts.DescriptionMaxLength, 
                        DefaultTitleMessage, 
                        $"El detalle del hecho revelevante {generalFact.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictGeneralFactConsts.DescriptionMaxLength} caracteres");

                    if (generalFact.Id > 0)
                    {
                        if (await _socialConflictGeneralFactRepository.CountAsync(p => p.Id == generalFact.Id && p.SocialConflict.Id == input.Id) > 0)
                        {
                            var dbSocialConflictGeneralFact = await _socialConflictGeneralFactRepository.GetAsync(generalFact.Id);
                            dbSocialConflictGeneralFact.Description = generalFact.Description;
                            dbSocialConflictGeneralFact.FactTime = new DateTime(generalFact.FactTime.Year, generalFact.FactTime.Month, generalFact.FactTime.Day);

                            await _socialConflictGeneralFactRepository.UpdateAsync(dbSocialConflictGeneralFact);
                        }
                    }
                    else
                    {
                        input.GeneralFacts.Add(new SocialConflictGeneralFact()
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

            if (PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflict_Sugerence))
            {
                hasCreatePermissionSugerence = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflict_Sugerence_Create);
                hasEditPermissionSugerence = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflict_Sugerence_Edit);
                hasDeletePermissionSugerence = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflict_Sugerence_Delete);
                hasAcceptPermissionSugerence = PermissionChecker.IsGranted(AppPermissions.Pages_Application_SocialConflict_Sugerence_Accept);
            }
            else
            {
                sugerences = new List<SocialConflictSugerenceDto>();
            }

            foreach (var sugerence in sugerences)
            {
                if (sugerence.Remove)
                {
                    if (hasDeletePermissionSugerence && sugerence.Id > 0 && input.Id > 0 && await _socialConflictSugerenceRepository.CountAsync(p => p.Id == sugerence.Id && p.SocialConflict.Id == input.Id) > 0)
                    {
                        await _socialConflictSugerenceRepository.DeleteAsync(sugerence.Id);
                    }
                }
                else
                {
                    sugerence.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción de las recomendaciones del conflicto social son obligatorias");

                    sugerence.Description.VerifyTableColumn(SocialConflictSugerenceConsts.DescriptionMinLength,
                        SocialConflictSugerenceConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la recomendación {sugerence.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictSugerenceConsts.DescriptionMaxLength} caracteres");

                    if (sugerence.Id > 0)
                    {
                        if (hasEditPermissionSugerence && await _socialConflictSugerenceRepository.CountAsync(p => p.Id == sugerence.Id && p.SocialConflict.Id == input.Id) > 0)
                        {
                            var dbSocialConflictSugerence = await _socialConflictSugerenceRepository.GetAsync(sugerence.Id);
                            dbSocialConflictSugerence.Description = sugerence.Description;

                            if(dbSocialConflictSugerence.Accepted == false && sugerence.Accepted && hasAcceptPermissionSugerence)
                            {
                                dbSocialConflictSugerence.Accepted = true;
                                dbSocialConflictSugerence.AcceptTime = DateTime.Now;
                                dbSocialConflictSugerence.AcceptedUserId = AbpSession.UserId;
                            }

                            await _socialConflictSugerenceRepository.UpdateAsync(dbSocialConflictSugerence);
                        }
                    }
                    else
                    {
                        if(hasCreatePermissionSugerence)
                        {
                            input.Sugerences.Add(new SocialConflictSugerence()
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
                    if (state.Id > 0 && input.Id > 0 && await _socialConflictStateRepository.CountAsync(p => p.Id == state.Id && p.SocialConflictId == input.Id && p.Verification == false) > 0)
                    {
                        await _socialConflictStateRepository.DeleteAsync(state.Id);
                    }
                }
                else
                {
                    state.State.IsValidOrException(DefaultTitleMessage,
                        "La descripción de la situación actual (interna) del conflicto social son obligatorias");

                    state.State.VerifyTableColumn(SocialConflictStateConsts.StateMinLength,
                        SocialConflictStateConsts.StateMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la situación actual (interna) {state.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictStateConsts.StateMaxLength} caracteres");

                    state.Description.IsValidOrException(DefaultTitleMessage,
                        "La proyección y acciones propuestas de la situación actual del conflicto social son obligatorias");

                    state.Description.VerifyTableColumn(SocialConflictStateConsts.DescriptionMinLength,
                        SocialConflictStateConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La proyección y acciones propuestas de la situación actual {state.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictStateConsts.DescriptionMaxLength} caracteres");

                    var dbManager = _personRepository
                       .GetAll()
                       .Where(p => p.Id == state.Manager.Id)
                       .FirstOrDefault();

                    if (dbManager == null)
                        throw new UserFriendlyException("Aviso", $"El gestor que registra {state.Manager.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (state.Id > 0)
                    {
                        if (await _socialConflictStateRepository.CountAsync(p => p.Id == state.Id && p.SocialConflict.Id == input.Id) > 0)
                        {
                            var dbSocialConflictState = await _socialConflictStateRepository.GetAsync(state.Id);

                            if(dbSocialConflictState.Verification == false)
                            {
                                dbSocialConflictState.Manager = dbManager;
                                dbSocialConflictState.ManagerId = dbManager.Id;
                                dbSocialConflictState.StateTime = new DateTime(state.StateTime.Year, state.StateTime.Month, state.StateTime.Day);
                                dbSocialConflictState.State = state.State;
                                dbSocialConflictState.Description = state.Description;
                            }

                            if (hasVerificationPermission && state.VerificationChange)
                            {
                                var oldState = dbSocialConflictState.Verification;
                                var newState = ((state.VerificationState ?? "false") == "true");

                                dbSocialConflictState.Verification = newState;

                                if (oldState != newState)
                                    input.VerificationHistories.Add(new SocialConflictVerificationHistory()
                                    {
                                        Site = SocialConflictVerificationSite.State,
                                        OldState = oldState,
                                        NewState = newState,
                                        EntityId = state.Id
                                    });
                            }

                            await _socialConflictStateRepository.UpdateAsync(dbSocialConflictState);
                        }
                    }
                    else
                    {
                        input.States.Add(new SocialConflictState()
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

            foreach (var violenceFact in violenceFacts)
            {

                if (violenceFact.Remove)
                {
                    if (violenceFact.Id > 0 && input.Id > 0 && await _socialConflictViolenceFactRepository.CountAsync(p => p.Id == violenceFact.Id && p.SocialConflict.Id == input.Id) > 0)
                    {
                        await _socialConflictViolenceFactRepository.DeleteAsync(violenceFact.Id);
                        await _socialConflictViolenceFactLocationRepository.DeleteAsync(p => p.SocialConflictViolenceFactId == violenceFact.Id);
                    }
                }
                else
                {
                    violenceFact.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción del hecho de violencia del conflicto social es obligatorio");

                    violenceFact.Description.VerifyTableColumn(SocialConflictViolenceFactConsts.DescriptionMinLength,
                        SocialConflictViolenceFactConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del hecho de violencia de violencia {violenceFact.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictViolenceFactConsts.DescriptionMaxLength} caracteres");

                    violenceFact.Responsible.VerifyTableColumn(SocialConflictViolenceFactConsts.ResponsibleMinLength,
                        SocialConflictViolenceFactConsts.ResponsibleMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del responsable del hecho de violencia {violenceFact.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictViolenceFactConsts.ResponsibleMaxLength} caracteres");

                    violenceFact.Actions.VerifyTableColumn(SocialConflictViolenceFactConsts.ActionsMinLength,
                        SocialConflictViolenceFactConsts.ActionsMaxLength,
                        DefaultTitleMessage,
                        $"Las acciones realizadas para la atención del hecho de violencia del hecho de violencia {violenceFact.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictViolenceFactConsts.ActionsMaxLength} caracteres");

                    var dbManager = violenceFact.Manager == null || violenceFact.Manager.Id == -1 ? null : _personRepository
                       .GetAll()
                       .Where(p => p.Id == violenceFact.Manager.Id)
                       .FirstOrDefault();

                    if (violenceFact.Manager != null && violenceFact.Manager.Id != -1 &&  dbManager == null)
                        throw new UserFriendlyException("Aviso", $"El responsable de la información (PCM) {violenceFact.Manager.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    var dbFact = _factRepository
                       .GetAll()
                       .Where(p => p.Id == violenceFact.Fact.Id)
                       .FirstOrDefault();

                    if (dbFact == null)
                        throw new UserFriendlyException("Aviso", $"El tipo de hecho {violenceFact.Fact.Name} del hecho de violencia {violenceFact.Manager.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (violenceFact.Id > 0)
                    {
                        if (await _socialConflictViolenceFactRepository.CountAsync(p => p.Id == violenceFact.Id && p.SocialConflict.Id == input.Id) > 0)
                        {
                            var dbSocialConflictViolenceFact = await _socialConflictViolenceFactRepository.GetAsync(violenceFact.Id);
                            dbSocialConflictViolenceFact.Manager = dbManager;
                            dbSocialConflictViolenceFact.ManagerId = dbManager == null ? (int?)null : dbManager.Id;
                            dbSocialConflictViolenceFact.Fact = dbFact;
                            dbSocialConflictViolenceFact.FactId = dbFact.Id;
                            dbSocialConflictViolenceFact.StartTime = violenceFact.StartTime;
                            dbSocialConflictViolenceFact.EndTime = violenceFact.EndTime;
                            dbSocialConflictViolenceFact.Description = violenceFact.Description;
                            dbSocialConflictViolenceFact.Responsible = (violenceFact.Responsible ?? "").ToUpperInvariant();
                            dbSocialConflictViolenceFact.Actions = violenceFact.Actions;
                            dbSocialConflictViolenceFact.InjuredMen = violenceFact.InjuredMen;
                            dbSocialConflictViolenceFact.InjuredWomen = violenceFact.InjuredWomen;
                            dbSocialConflictViolenceFact.DeadMen = violenceFact.DeadMen;
                            dbSocialConflictViolenceFact.DeadWomen = violenceFact.DeadWomen;

                            await _socialConflictViolenceFactRepository.UpdateAsync(dbSocialConflictViolenceFact);

                            foreach (var location in violenceFact.Locations ?? new List<SocialConflictViolenceFactLocationDto>())
                            {
                                if (location.Remove)
                                {
                                    if (location.Id > 0 && input.Id > 0 && await _socialConflictViolenceFactLocationRepository.CountAsync(p => p.Id == violenceFact.Id && p.SocialConflictViolenceFactId == violenceFact.Id) > 0)
                                    {
                                        await _socialConflictViolenceFactLocationRepository.DeleteAsync(location.Id);
                                    }
                                }
                                else
                                {
                                    if(location.Id <= 0)
                                    {
                                        var dbDeparment = _departmentRepository
                                            .GetAll()
                                            .Where(p => p.Id == location.Department.Id)
                                            .FirstOrDefault();
                                        var dbProvince = _provinceRepository
                                            .GetAll()
                                            .Where(p => p.Id == location.Province.Id)
                                            .FirstOrDefault();
                                        var dbDistrict = _districtRepository
                                            .GetAll()
                                            .Where(p => p.Id == location.District.Id)
                                            .FirstOrDefault();

                                        Region dbRegion = null;

                                        if (location.Region != null)
                                            dbRegion = _regionRepository
                                                .GetAll()
                                                .Where(p => p.Id == location.Region.Id)
                                                .FirstOrDefault();

                                        location.Ubication.VerifyTableColumn(SocialConflictViolenceFactLocationConsts.UbicationMinLength,
                                            SocialConflictViolenceFactLocationConsts.UbicationMaxLength,
                                            DefaultTitleMessage,
                                            $"La localidad - comunidad - Otros {location.Ubication} del hecho violento {violenceFact.Description} " +
                                            $"no debe exceder los {SocialConflictViolenceFactLocationConsts.UbicationMaxLength} caracteres");

                                        if (dbDeparment != null && dbProvince != null && dbDistrict != null)
                                        {
                                            await _socialConflictViolenceFactLocationRepository.InsertAsync(new SocialConflictViolenceFactLocation()
                                            {
                                                SocialConflictViolenceFact = dbSocialConflictViolenceFact,
                                                SocialConflictViolenceFactId = dbSocialConflictViolenceFact.Id,
                                                Department = dbDeparment,
                                                DepartmentId = dbDeparment.Id,
                                                Province = dbProvince,
                                                ProvinceId = dbProvince.Id,
                                                District = dbDistrict,
                                                DistrictId = dbDistrict.Id,
                                                Region = dbRegion,
                                                RegionId = dbRegion == null ? (int?)null : dbRegion.Id,
                                                Ubication = (location.Ubication ?? "").ToUpperInvariant()
                                            });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var dbViolenceFact = new SocialConflictViolenceFact()
                        {
                            Manager = dbManager,
                            ManagerId = dbManager == null ? (int?)null : dbManager.Id,
                            Fact = dbFact,
                            FactId = dbFact.Id,
                            StartTime = violenceFact.StartTime,
                            EndTime = violenceFact.EndTime,
                            Description = violenceFact.Description,
                            Responsible = (violenceFact.Responsible ?? "").ToUpperInvariant(),
                            Actions = violenceFact.Actions,
                            InjuredMen = violenceFact.InjuredMen,
                            InjuredWomen = violenceFact.InjuredWomen,
                            DeadMen = violenceFact.DeadMen,
                            DeadWomen = violenceFact.DeadWomen,
                            Locations = new List<SocialConflictViolenceFactLocation>()
                        };

                        foreach (var location in violenceFact.Locations ?? new List<SocialConflictViolenceFactLocationDto>())
                        {
                            if (!location.Remove && location.Id <= 0)
                            {
                                var dbDeparment = _departmentRepository
                                    .GetAll()
                                    .Where(p => p.Id == location.Department.Id)
                                    .FirstOrDefault();
                                var dbProvince = _provinceRepository
                                    .GetAll()
                                    .Where(p => p.Id == location.Province.Id)
                                    .FirstOrDefault();
                                var dbDistrict = _districtRepository
                                    .GetAll()
                                    .Where(p => p.Id == location.District.Id)
                                    .FirstOrDefault();

                                Region dbRegion = null;

                                if (location.Region != null)
                                    dbRegion = _regionRepository
                                        .GetAll()
                                        .Where(p => p.Id == location.Region.Id)
                                        .FirstOrDefault();

                                location.Ubication.VerifyTableColumn(SocialConflictViolenceFactLocationConsts.UbicationMinLength,
                                    SocialConflictViolenceFactLocationConsts.UbicationMaxLength,
                                    DefaultTitleMessage,
                                    $"La localidad - comunidad - Otros {location.Ubication} del hecho violento {violenceFact.Description} " +
                                    $"no debe exceder los {SocialConflictViolenceFactLocationConsts.UbicationMaxLength} caracteres");

                                if (dbDeparment != null && dbProvince != null && dbDistrict != null)
                                {
                                    dbViolenceFact.Locations.Add(new SocialConflictViolenceFactLocation()
                                    {
                                        Department = dbDeparment,
                                        DepartmentId = dbDeparment.Id,
                                        Province = dbProvince,
                                        ProvinceId = dbProvince.Id,
                                        District = dbDistrict,
                                        DistrictId = dbDistrict.Id,
                                        Region = dbRegion,
                                        RegionId = dbRegion == null ? (int?)null : dbRegion.Id,
                                        Ubication = (location.Ubication ?? "").ToUpperInvariant()
                                    });
                                }
                            }
                        }

                        input.ViolenceFacts.Add(dbViolenceFact);
                    }
                }
            }

            foreach (var condition in conditions)
            {
                if (condition.Remove)
                {
                    if (condition.Id > 0 && input.Id > 0 && await _socialConflictConditionRepository.CountAsync(p => p.Id == condition.Id && p.SocialConflict.Id == input.Id && p.Verification == false) > 0)
                    {
                        await _socialConflictConditionRepository.DeleteAsync(condition.Id);
                    }
                }
                else
                {
                    condition.Description.VerifyTableColumn(SocialConflictConditionConsts.DescriptionMinLength,
                        SocialConflictConditionConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción el estado del caso {condition.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictConditionConsts.DescriptionMaxLength} caracteres");

                    if (condition.Id > 0)
                    {
                        if (await _socialConflictConditionRepository.CountAsync(p => p.Id == condition.Id && p.SocialConflict.Id == input.Id) > 0)
                        {
                            var dbSocialConflictCondition = await _socialConflictConditionRepository.GetAsync(condition.Id);

                            if(dbSocialConflictCondition.Verification == false)
                            {
                                dbSocialConflictCondition.Description = condition.Description;
                                dbSocialConflictCondition.Type = condition.Type;
                                dbSocialConflictCondition.ConditionTime = condition.ConditionTime;
                            }

                            if (hasVerificationPermission && condition.VerificationChange)
                            {
                                var oldState = dbSocialConflictCondition.Verification;
                                var newState = ((condition.VerificationState ?? "false") == "true");

                                dbSocialConflictCondition.Verification = newState;

                                if (oldState != newState)
                                    input.VerificationHistories.Add(new SocialConflictVerificationHistory()
                                    {
                                        Site = SocialConflictVerificationSite.Condition,
                                        OldState = oldState,
                                        NewState = newState,
                                        EntityId = condition.Id
                                    });
                            }

                            await _socialConflictConditionRepository.UpdateAsync(dbSocialConflictCondition);
                        }
                    }
                    else
                    {
                        input.Conditions.Add(new SocialConflictCondition()
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
                    if (management.Id > 0 && input.Id > 0 && await _socialConflictManagementRepository.CountAsync(p => p.Id == management.Id && p.SocialConflict.Id == input.Id) > 0)
                    {
                        await _socialConflictManagementRepository.DeleteAsync(management.Id);
                        await _socialConflictManagementResourceRepository.DeleteAsync(p => p.SocialConflictManagementId == management.Id);
                    }
                }
                else
                {
                    management.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción de las gestiones del conflicto social son obligatorias");

                    management.Description.VerifyTableColumn(SocialConflictManagementConsts.DescriptionMinLength,
                        SocialConflictManagementConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la gestión {management.Description} del conflicto social no debe exceder los " +
                        $"{SocialConflictManagementConsts.DescriptionMaxLength} caracteres");

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

                    SocialConflictManagement dbSocialConflictManagement = null;

                    if (management.Id > 0)
                    {
                        if (await _socialConflictManagementRepository.CountAsync(p => p.Id == management.Id && p.SocialConflict.Id == input.Id) > 0)
                        {
                            dbSocialConflictManagement = await _socialConflictManagementRepository.GetAsync(management.Id);

                            if(dbSocialConflictManagement.Verification == false)
                            {
                                dbSocialConflictManagement.Description = management.Description;
                                dbSocialConflictManagement.ManagementTime = new DateTime(management.ManagementTime.Year, management.ManagementTime.Month, management.ManagementTime.Day);
                                dbSocialConflictManagement.Management = dbManagement;
                                dbSocialConflictManagement.ManagementId = dbManagement.Id;
                                dbSocialConflictManagement.Manager = dbManager;
                                dbSocialConflictManagement.ManagerId = dbManager.Id;
                                dbSocialConflictManagement.CivilMen = dbManagement.ShowDetail ? management.CivilMen : (int?)null;
                                dbSocialConflictManagement.CivilWomen = dbManagement.ShowDetail ? management.CivilWomen : (int?)null;
                                dbSocialConflictManagement.StateMen = dbManagement.ShowDetail ? management.StateMen : (int?)null;
                                dbSocialConflictManagement.StateWomen = dbManagement.ShowDetail ? management.StateWomen : (int?)null;
                                dbSocialConflictManagement.CompanyMen = dbManagement.ShowDetail ? management.CompanyMen : (int?)null;
                                dbSocialConflictManagement.CompanyWomen = dbManagement.ShowDetail ? management.CompanyWomen : (int?)null;
                            }

                            dbSocialConflictManagement.Resources = new List<SocialConflictManagementResource>();

                            if (hasVerificationPermission && management.VerificationChange)
                            {
                                var oldState = dbSocialConflictManagement.Verification;
                                var newState = ((management.VerificationState ?? "false") == "true");

                                dbSocialConflictManagement.Verification = newState;

                                if (oldState != newState)
                                    input.VerificationHistories.Add(new SocialConflictVerificationHistory()
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
                        dbSocialConflictManagement = new SocialConflictManagement()
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
                            Resources = new List<SocialConflictManagementResource>()
                        };
                    }

                    foreach (var resource in management.Resources)
                    {
                        if (resource.Remove)
                        {
                            if (resource.Id > 0 && management.Id > 0 && input.Id > 0 && await _socialConflictManagementResourceRepository.CountAsync(p => p.Id == resource.Id && p.SocialConflictManagementId == management.Id) > 0)
                            {
                                await _socialConflictManagementResourceRepository.DeleteAsync(resource.Id);
                            }
                        }
                    }

                    foreach (var resource in management.UploadFiles)
                    {
                        dbSocialConflictManagement.Resources.Add(ObjectMapper.Map<SocialConflictManagementResource>(ResourceManager.Create(resource, ResourceConsts.SocialConflictManagement)));
                    }

                    if (dbSocialConflictManagement != null)
                    {
                        if (dbSocialConflictManagement.Id > 0)
                        {
                            await _socialConflictManagementRepository.UpdateAsync(dbSocialConflictManagement);
                        }
                        else
                        {
                            input.Managements.Add(dbSocialConflictManagement);
                        }
                    }
                }
            }

            foreach (var resource in resources)
            {

                if (resource.Remove)
                {
                    if (resource.Id > 0 && input.Id > 0 && await _socialConflictResourceRepository.CountAsync(p => p.Id == resource.Id && p.SocialConflict.Id == input.Id) > 0)
                    {
                        await _socialConflictResourceRepository.DeleteAsync(resource.Id);
                    }
                }
            }

            foreach (var note in notes)
            {

                if (note.Remove)
                {
                    if (note.Id > 0 && input.Id > 0 && await _socialConflictNoteRepository.CountAsync(p => p.Id == note.Id && p.SocialConflict.Id == input.Id) > 0)
                    {
                        await _socialConflictNoteRepository.DeleteAsync(note.Id);
                    }
                }
            }
            
            return input;
        }
    }
}
