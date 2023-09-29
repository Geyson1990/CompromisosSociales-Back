using Abp.Linq.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using Contable.Application.DialogSpaces;
using Contable.Application.DialogSpaces.Dto;
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
using Contable.Application.InterventionPlans.Dto;
using Contable.Migrations;
using Contable.Repositories;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace)]
    public class DialogSpaceAppService : ContableAppServiceBase, IDialogSpaceAppService
    {
        private readonly IRepository<DialogSpace> _dialogSpaceRepository;
        private readonly IRepository<DialogSpaceLocation> _dialogSpaceLocationRepository;
        private readonly IRepository<DialogSpaceLeader> _dialogSpaceLeaderRepository;
        private readonly IRepository<DialogSpaceDocument> _dialogSpaceDocumentRepository;
        private readonly IRepository<DialogSpaceTeam> _dialogSpaceTeamRepository;
        private readonly IRepository<DialogSpaceType> _dialogSpaceTypeRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SocialConflictLocation> _socialConflictLocationRepository;
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;
        private readonly IRepository<Person> _personRepository;

        public DialogSpaceAppService(
            IRepository<DialogSpace> dialogSpaceRepository, 
            IRepository<DialogSpaceLocation> dialogSpaceLocationRepository,
            IRepository<DialogSpaceLeader> dialogSpaceLeaderRepository,
            IRepository<DialogSpaceTeam> dialogSpaceTeamRepository,
            IRepository<DialogSpaceDocument> dialogSpaceDocumentRepository,
            IRepository<DialogSpaceType> dialogSpaceTypeRepository,
            IRepository<TerritorialUnit> territorialUnitRepository, 
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository, 
            IRepository<District> districtRepository, 
            IRepository<Region> regionRepository,
            IRepository<User, long> userRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<SocialConflictLocation> socialConflictLocationRepository,
            IRepository<DirectoryGovernment> directoryGovernmentRepository,
            IRepository<Person> personRepository)
        {
            _dialogSpaceRepository = dialogSpaceRepository;
            _dialogSpaceLocationRepository = dialogSpaceLocationRepository;
            _dialogSpaceLeaderRepository = dialogSpaceLeaderRepository;
            _dialogSpaceDocumentRepository = dialogSpaceDocumentRepository;
            _dialogSpaceTeamRepository = dialogSpaceTeamRepository;
            _dialogSpaceTypeRepository = dialogSpaceTypeRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _regionRepository = regionRepository;
            _userRepository = userRepository;
            _socialConflictRepository = socialConflictRepository;
            _socialConflictLocationRepository = socialConflictLocationRepository;
            _directoryGovernmentRepository = directoryGovernmentRepository;
            _personRepository = personRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace_Create)]
        public async Task<EntityDto> Create(DialogSpaceCreateDto input)
        {
            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _dialogSpaceRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var dialogSpaceId = await _dialogSpaceRepository.InsertAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map<DialogSpace>(input),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                dialogSpaceTypeId: input.DialogSpaceType == null ? -1 : input.DialogSpaceType.Id,
                personId: input.Person == null ? -1 : input.Person.Id,
                leaders: input.Leaders ?? new List<DialogSpaceLeaderRelationDto>(),
                locations: input.Locations ?? new List<DialogSpaceLocationRelationDto>()
            ));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateDialogSpaceCodeReplaceProcess(dialogSpaceId, input.ReplaceYear, input.ReplaceCount);
            else
                await FunctionManager.CallCreateDialogSpaceCodeProcess(dialogSpaceId);

            await FunctionManager.CallCreateDialogSpaceStateProcess(dialogSpaceId);

            return new EntityDto(dialogSpaceId);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _dialogSpaceRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceRepository.DeleteAsync(p => p.Id == input.Id);
            await _dialogSpaceLocationRepository.DeleteAsync(p => p.DialogSpaceId == input.Id);
            await _dialogSpaceLeaderRepository.DeleteAsync(p => p.DialogSpaceId == input.Id);
            await _dialogSpaceDocumentRepository.DeleteAsync(p => p.DialogSpaceId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace)]
        public async Task<DialogSpaceGetDataDto> Get(NullableIdDto input)
        {
            var output = new DialogSpaceGetDataDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _dialogSpaceRepository.CountAsync(p => p.Id == input.Id));

                var dbDialogSpace = _dialogSpaceRepository
                    .GetAll()
                    .Include(p => p.SocialConflict)
                    .Include(p => p.DialogSpaceType)
                    .Include(p => p.Person)
                    .Where(p => p.Id == input.Id.Value)
                    .FirstOrDefault();

                output.DialogSpace = ObjectMapper.Map<DialogSpaceGetDto>(dbDialogSpace);

                output.DialogSpace.Locations = ObjectMapper.Map<List<DialogSpaceLocationRelationDto>>(_dialogSpaceLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.DialogSpaceId == input.Id)
                    .ToList());

                output.DialogSpace.Leaders = ObjectMapper.Map<List<DialogSpaceLeaderRelationDto>>(_dialogSpaceLeaderRepository
                    .GetAll()
                    .Include(p => p.DirectoryGovernment)
                    .ThenInclude(p => p.District)
                    .ThenInclude(p => p.Province)
                    .ThenInclude(p => p.Department)
                    .Include(p => p.Teams)
                    .Where(p => p.DialogSpaceId == dbDialogSpace.Id)
                    .OrderBy(p => p.Index)
                    .ToList());

                var creatorUser = dbDialogSpace.CreatorUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbDialogSpace.CreatorUserId.Value)
                    .FirstOrDefault() : null;

                var editUser = dbDialogSpace.LastModifierUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbDialogSpace.LastModifierUserId.Value)
                    .FirstOrDefault() : null;

                output.DialogSpace.CreatorUser = creatorUser == null ? null : ObjectMapper.Map<DialogSpaceUserDto>(creatorUser);
                output.DialogSpace.EditUser = editUser == null ? null : ObjectMapper.Map<DialogSpaceUserDto>(editUser);
            }

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<DialogSpaceDepartmentDto>();

            foreach (var item in departments)
            {
                var department = new DialogSpaceDepartmentDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => p.TerritorialUnitId).ToArray(),
                    Provinces = ObjectMapper.Map<List<DialogSpaceProvinceDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.TerritorialUnits = ObjectMapper.Map<List<DialogSpaceTerritorialUnitDto>>(await _territorialUnitRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .ToListAsync());

            output.Types = ObjectMapper.Map<List<DialogSpaceDialogSpaceTypeRelatioDto>>(await _dialogSpaceTypeRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .Where(p => p.Enabled)
                .ToListAsync());

            output.Persons = ObjectMapper.Map<List<DialogSpacePersonRelationDto>>(await _personRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .Where(p => p.Enabled && p.Type != PersonType.None)
                .ToListAsync());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace)]
        public async Task<PagedResultDto<DialogSpaceGetAllDto>> GetAll(DialogSpaceGetAllInputDto input)
        {
            var query = _dialogSpaceRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.DialogSpaceType)
                .Include(p => p.Person)
                .WhereIf(input.DialogSpaceTypeId.HasValue, p => p.DialogSpaceTypeId == input.DialogSpaceTypeId.Value)
                .WhereIf(input.ValidForTerritorialUnit, p => p.Locations.Any(d => d.TerritorialUnitId == input.TerritorialUnitId.Value))
                .WhereIf(input.ValidForDepartment, p => p.Locations.Any(d => d.DepartmentId == input.DepartmentId.Value))
                .WhereIf(input.ValidForProvince, p => p.Locations.Any(d => d.DistrictId == input.DistrictId.Value))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Code.SplitByLike(), nameof(DialogSpace.Code))
                .LikeAllBidirectional(input.CaseName.SplitByLike(), nameof(DialogSpace.CaseName));

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input).ToList();
            var output = new List<DialogSpaceGetAllDto>();

            foreach (var item in result)
            {
                var newItem = new DialogSpaceGetAllDto()
                {
                    Id = item.Id,
                    Code = item.Code,
                    Year = item.Year,
                    Count = item.Count,
                    CaseName = item.CaseName,
                    SocialConflictCaseName = item.SocialConflict?.CaseName,
                    Person = item.Person?.Name,
                    Type = item.DialogSpaceType.Name
                };

                var dbLocations = _dialogSpaceLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.DialogSpaceId == item.Id)
                    .ToList()
                    .Where(p => p.TerritorialUnit != null &&
                                p.Department != null &&
                                p.Province != null &&
                                p.District != null)
                    .DistinctBy(p => p.DistrictId)
                    .ToList();

                var locations = string.Join(", ", dbLocations.Select(p => $"{p.TerritorialUnit.Name} - {p.Department.Name} - {p.Province.Name} - {p.District.Name}").ToList());
                var territorialUnits = string.Join(", ", dbLocations.DistinctBy(p => p.TerritorialUnitId).Select(p => p.TerritorialUnit.Name).ToList());


                newItem.Locations = locations;
                newItem.TerritorialUnits = territorialUnits;

                if (item.LastDialogSpaceDocumentId.HasValue)
                {
                    var dialogSpaceDocument = _dialogSpaceDocumentRepository
                        .GetAll()
                        .Include(p => p.DialogSpaceDocumentSituation)
                        .Where(p => p.Id == item.LastDialogSpaceDocumentId.Value && p.DialogSpaceId == item.Id)
                        .FirstOrDefault();

                    if(dialogSpaceDocument != null)
                    {
                        newItem.Document = dialogSpaceDocument.Document;
                        newItem.DocumentTime = dialogSpaceDocument.DocumentTime;
                        newItem.DocumentType = dialogSpaceDocument.Type;
                        newItem.DocumentSituation = dialogSpaceDocument.DialogSpaceDocumentSituation?.Name;
                        newItem.DocumentObservation = dialogSpaceDocument.Observation;
                    }
                }

                output.Add(newItem);
            }

            return new PagedResultDto<DialogSpaceGetAllDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace)]
        public async Task<PagedResultDto<DialogSpaceLocationReferenceDto>> GetAllLocations(InterventionPlanLocationGetAllInputDto input)
        {
            var query = _socialConflictLocationRepository
                .GetAll()
                .Include(p => p.TerritorialUnit)
                .Include(p => p.Department)
                .Include(p => p.Province)
                .Include(p => p.District)
                .Include(p => p.Region)
                .Where(p => p.SocialConflict.Id == input.ConflictId);

            var count = await query.CountAsync();
            var result = query
                .OrderBy(p => p.Department.Name)
                .ThenBy(p => p.Province.Name)
                .ThenBy(p => p.District.Name)
                .PageBy(0, 1000);

            return new PagedResultDto<DialogSpaceLocationReferenceDto>(count, ObjectMapper.Map<List<DialogSpaceLocationReferenceDto>>(result));
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace_Edit)]
        public async Task<EntityDto> Update(DialogSpaceUpdateDto input)
        {
            VerifyCount(await _dialogSpaceRepository.CountAsync(p => p.Id == input.Id));

            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _dialogSpaceRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var dialogSpaceId = await _dialogSpaceRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map(input, await _dialogSpaceRepository.GetAsync(input.Id)),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                dialogSpaceTypeId: input.DialogSpaceType == null ? -1 : input.DialogSpaceType.Id,
                personId: input.Person == null ? -1 : input.Person.Id,
                leaders: input.Leaders ?? new List<DialogSpaceLeaderRelationDto>(),
                locations: input.Locations ?? new List<DialogSpaceLocationRelationDto>()
            ));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateDialogSpaceCodeReplaceProcess(dialogSpaceId, input.ReplaceYear, input.ReplaceCount);

            await FunctionManager.CallCreateDialogSpaceStateProcess(dialogSpaceId);

            return new EntityDto(dialogSpaceId);
        }

        private async Task<DialogSpace> ValidateEntity(
            DialogSpace input, 
            int socialConflictId, 
            int dialogSpaceTypeId, 
            int personId,
            List<DialogSpaceLeaderRelationDto> leaders, 
            List<DialogSpaceLocationRelationDto> locations)
        {
            input.CaseName.IsValidOrException(DefaultTitleMessage,
                "La denominación del espacio de diálogo es obligatoria");
            input.CaseName.VerifyTableColumn(
                DialogSpaceConsts.CaseNameMinLength,
                DialogSpaceConsts.CaseNameMaxLength,
                DefaultTitleMessage,
                $"La denominación del espacio de diálogo no debe exceder los {DialogSpaceConsts.CaseNameMaxLength} caracteres");

            var dbDialogSpaceType = _dialogSpaceTypeRepository
                .GetAll()
                .Where(p => p.Id == dialogSpaceTypeId)
                .FirstOrDefault();

            if (dbDialogSpaceType == null)
                throw new UserFriendlyException("Aviso", "El tipo de espacio de diálogo seleccionado ya no existe o fue eliminado. Por favor verifique la información antes de continuar");

            input.DialogSpaceType = dbDialogSpaceType;
            input.DialogSpaceTypeId = dbDialogSpaceType.Id;

            var dbSocialConflict = _socialConflictRepository
                .GetAll()
                .Where(p => p.Id == socialConflictId)
                .FirstOrDefault();

            if (dbSocialConflict == null)
                throw new UserFriendlyException("Aviso", "El conflicto social seleccionado ya no existe o fue eliminado. Por favor verifique la información antes de continuar");

            input.SocialConflict = dbSocialConflict;
            input.SocialConflictId = dbSocialConflict.Id;

            var dbPerson = _personRepository
                .GetAll()
                .Where(p => p.Id == personId)
                .FirstOrDefault();

            if (dbPerson == null)
                throw new UserFriendlyException("Aviso", "El encargado de registrar el espacio de diálogo seleccionado ya no existe o fue eliminado. Por favor verifique la información antes de continuar");

            input.Person = dbPerson;
            input.PersonId = dbPerson.Id;

            input.Locations = new List<DialogSpaceLocation>();
            input.Leaders = new List<DialogSpaceLeader>();

            var index = 0;
            foreach (var location in locations)
            {
                if (location.Remove)
                {
                    if (location.Id > 0 && input.Id > 0 && await _dialogSpaceLocationRepository.CountAsync(p => p.Id == location.Id && p.DialogSpaceId == input.Id) > 0)
                    {
                        await _dialogSpaceLocationRepository.DeleteAsync(location.Id);
                    }
                }
                else
                {
                    if (location.Id <= 0)
                    {
                        var dbDistrict = _districtRepository
                            .GetAll()
                            .Where(p => p.Id == location.District.Id)
                            .FirstOrDefault();

                        if (dbDistrict == null)
                            throw new UserFriendlyException(DefaultTitleMessage, $"El distrito {location.District.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        var dbProvince = _provinceRepository
                            .GetAll()
                            .Where(p => p.Id == location.Province.Id)
                            .FirstOrDefault();

                        if (dbProvince == null)
                            throw new UserFriendlyException(DefaultTitleMessage, $"La provincia {location.Province.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        var dbDepartment = _departmentRepository
                            .GetAll()
                            .Where(p => p.Id == location.Department.Id)
                            .FirstOrDefault();

                        if (dbDepartment == null)
                            throw new UserFriendlyException(DefaultTitleMessage, $"El departamento {location.Department.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        var dbTerritorialUnit = _territorialUnitRepository
                            .GetAll()
                            .Where(p => p.Id == location.TerritorialUnit.Id)
                            .FirstOrDefault();

                        if (dbTerritorialUnit == null)
                            throw new UserFriendlyException(DefaultTitleMessage, $"La unidad territorial {location.TerritorialUnit.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                        Region region = null;

                        if (location.Region != null)
                            region = await _regionRepository.GetAsync(location.Region.Id);

                        if (input.Locations.Where(p => p.District.Id == location.District.Id).Count() == 0)
                        {
                            location.Ubication.VerifyTableColumn(DialogSpaceLocationConsts.UbicationMinLength,
                                DialogSpaceLocationConsts.UbicationMaxLength,
                                DefaultTitleMessage,
                                $"La localidad - comunidad - Otros {location.Ubication} no debe exceder los {DialogSpaceLocationConsts.UbicationMaxLength} caracteres");

                            input.Locations.Add(new DialogSpaceLocation()
                            {
                                TerritorialUnit = dbTerritorialUnit,
                                Department = dbDepartment,
                                Province = dbProvince,
                                District = dbDistrict,
                                Region = region,
                                RegionId = region == null ? (int?)null : region.Id,
                                Ubication = location.Ubication,
                                Id = 0
                            });
                        }
                    }
                }
            }

            index = 0;

            foreach (var leader in leaders)
            {
                if (leader.Remove)
                {
                    if (leader.Id > 0 && input.Id > 0 && await _dialogSpaceLeaderRepository.CountAsync(p => p.Id == leader.Id && p.DialogSpaceId == input.Id) > 0)
                    {
                        await _dialogSpaceLeaderRepository.DeleteAsync(leader.Id);
                    }
                }
                else
                {
                    leader.Teams ??= new List<DialogSpaceTeamRelationDto>();

                    var dbDirectoryGovernment = _directoryGovernmentRepository
                            .GetAll()
                            .Where(p => p.Id == leader.DirectoryGovernment.Id)
                            .FirstOrDefault();

                    if (dbDirectoryGovernment == null)
                        throw new UserFriendlyException("Aviso", $"La Entidad Gubernamental {leader.DirectoryGovernment.Name} ya no existe o fue eliminada. Por favor verifique la información antes de continuar");

                    foreach (var team in leader.Teams)
                    {
                        team.Name.IsValidOrException("Aviso", "El nombre de los participantes es obligatorio");
                        team.Name.VerifyTableColumn(DialogSpaceTeamConsts.NameMinLength,
                            DialogSpaceTeamConsts.NameMaxLength,
                            "Aviso",
                            $"El nombre de los participantes no debe exceder los {DialogSpaceTeamConsts.NameMaxLength} caracteres");

                        team.Name = team.Name.Trim().ToUpper();
                    }

                    if (leader.Id > 0)
                    {
                        if (await _dialogSpaceLeaderRepository.CountAsync(p => p.Id == leader.Id && p.DialogSpaceId == input.Id) > 0)
                        {
                            var dbDialogSpaceLeader = await _dialogSpaceLeaderRepository.GetAsync(leader.Id);

                            dbDialogSpaceLeader.DirectoryGovernment = dbDirectoryGovernment;
                            dbDialogSpaceLeader.DirectoryGovernmentId = dbDirectoryGovernment.Id;
                            dbDialogSpaceLeader.Index = index;
                            dbDialogSpaceLeader.Teams = new List<DialogSpaceTeam>();

                            foreach (var team in leader.Teams)
                            {
                                if (leader.Remove)
                                {
                                    if (leader.Id > 0 && input.Id > 0 && await _dialogSpaceTeamRepository.CountAsync(p => p.Id == leader.Id && p.DialogSpaceLeaderId == leader.Id) > 0)
                                    {
                                        await _dialogSpaceTeamRepository.DeleteAsync(team.Id);
                                    }
                                }
                                else
                                {
                                    if (team.Id > 0)
                                    {
                                        var dbDialogSpaceTeam = await _dialogSpaceTeamRepository.GetAsync(team.Id);

                                        dbDialogSpaceTeam.Name = team.Name;

                                        await _dialogSpaceTeamRepository.UpdateAsync(dbDialogSpaceTeam);
                                    }
                                    else
                                    {
                                        dbDialogSpaceLeader.Teams.Add(new DialogSpaceTeam()
                                        {
                                            Name = team.Name
                                        });
                                    }
                                }
                            }

                            await _dialogSpaceLeaderRepository.UpdateAsync(dbDialogSpaceLeader);
                        }
                    }
                    else
                    {
                        var dbDialogSpaceLeader = new DialogSpaceLeader()
                        {
                            DirectoryGovernment = dbDirectoryGovernment,
                            DirectoryGovernmentId = dbDirectoryGovernment.Id,
                            Index = index,
                            Teams = new List<DialogSpaceTeam>()
                        };

                        foreach (var team in leader.Teams)
                        {
                            dbDialogSpaceLeader.Teams.Add(new DialogSpaceTeam()
                            {
                                Name = team.Name
                            });

                        }

                        input.Leaders.Add(dbDialogSpaceLeader);
                    }
                }

                index++;
            }

            return input;
        }
    }
}
