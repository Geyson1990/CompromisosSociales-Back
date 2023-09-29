using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Contable.Application.Utilities;
using Contable.Application.Utilities.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Contable.Application.Extensions;
using Contable.Authorization.Users;
using Abp.Collections.Extensions;
using System.Linq.Expressions;
using Abp.EntityFrameworkCore;
using Contable.EntityFrameworkCore;
using Contable.Repositories;
using NUglify.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Contable.Application
{
    [AbpAuthorize]
    public class UtilityAppService : ContableAppServiceBase, IUtilityAppService
    {
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<Compromise, long> _compromiseRepository;
        private readonly IRepository<Record, long> _recordRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<SocialConflictLocation> _socialConflictLocationRepository;
        private readonly IRepository<SocialConflictUser> _socialConflictUserRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Typology> _typologyRepository;
        private readonly IRepository<AlertResponsible> _alertResponsibleRepository;
        private readonly IRepository<AlertRisk> _alertRiskRepository;
        private readonly IRepository<AlertSeal> _alertSealRepository;
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;
        private readonly IRepository<DirectoryGovernmentSector> _directoryGovernmentSectorRepository;
        private readonly IRepository<DirectoryIndustry> _directoryIndustryRepository;
        private readonly IRepository<InterventionPlan> _interventionPlanRepository;
        private readonly IRepository<InterventionPlanLocation> _interventionPlanLocationRepository;
        private readonly IRepository<QuizState> _quizStateRepository;
        private readonly IRepository<DialogSpaceType> _dialogSpaceTypeRepository;
        private readonly IProcedureRepository _procedureRepository;
        private readonly IDbContextProvider<ContableDbContext> _dbContextProvider;

        private ContableDbContext _context
        {
            get => _dbContextProvider.GetDbContext();
        }

        public UtilityAppService(
            IDbContextProvider<ContableDbContext> dbContextProvider,
            IRepository<TerritorialUnit> territorialUnitRepository, 
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<Compromise, long> compromiseRepository,
            IRepository<Record, long> recordRepository,
            IRepository<User, long> userRepository,
            IRepository<SocialConflictLocation> socialConflictLocationRepository,
            IRepository<Department> departmentRepository,
            IRepository<Region> regionRepository,
            IRepository<Person> personRepository,
            IRepository<SocialConflictUser> socialConflictUserRepository,
            IRepository<Typology> typologyRepository,
            IRepository<AlertResponsible> alertResponsibleRepository,
            IRepository<AlertRisk> alertRiskRepository,
            IRepository<AlertSeal> alertSealRepository,
            IRepository<DirectoryGovernment> directoryGovernmentRepository,
            IRepository<DirectoryGovernmentSector> directoryGovernmentSectorRepository,
            IRepository<DirectoryIndustry> directoryIndustryRepository,
            IRepository<InterventionPlan> interventionPlanRepository,
            IRepository<InterventionPlanLocation> interventionPlanLocationRepository,
            IRepository<QuizState> quizStateRepository,
            IRepository<DialogSpaceType> dialogSpaceTypeRepository,
            IProcedureRepository procedureRepository)
        {
            _dbContextProvider = dbContextProvider;
            _territorialUnitRepository = territorialUnitRepository;
            _socialConflictRepository = socialConflictRepository;
            _compromiseRepository = compromiseRepository;
            _recordRepository = recordRepository;
            _userRepository = userRepository;
            _socialConflictLocationRepository = socialConflictLocationRepository;
            _departmentRepository = departmentRepository;
            _regionRepository = regionRepository;
            _personRepository = personRepository;
            _socialConflictUserRepository = socialConflictUserRepository;
            _typologyRepository = typologyRepository;
            _alertResponsibleRepository = alertResponsibleRepository;
            _alertRiskRepository = alertRiskRepository;
            _alertSealRepository = alertSealRepository;
            _directoryGovernmentRepository = directoryGovernmentRepository;
            _directoryGovernmentSectorRepository = directoryGovernmentSectorRepository;
            _directoryIndustryRepository = directoryIndustryRepository;
            _procedureRepository = procedureRepository;
            _interventionPlanRepository = interventionPlanRepository;
            _interventionPlanLocationRepository = interventionPlanLocationRepository;
            _quizStateRepository = quizStateRepository;
            _dialogSpaceTypeRepository = dialogSpaceTypeRepository;
        }

        public async Task<PagedResultDto<UtilityTerritorialUnitDto>> GetTerritorialUnits()
        {
            var query = _territorialUnitRepository
                .GetAll();

            var count = await query.CountAsync();
            var output = query.OrderBy(p => p.Name).PageBy(0, 1000);

            return new PagedResultDto<UtilityTerritorialUnitDto>(count, ObjectMapper.Map<List<UtilityTerritorialUnitDto>>(output));
        }

        public async Task<PagedResultDto<UtilitySocialConflictDto>> GetAllSocialConflicts(UtilitySocialConflicInputDto input)
        {
            var query = _socialConflictRepository
                .GetAll()
                .Include(p => p.Locations)
                .ThenInclude(p => p.TerritorialUnit)
                .Include(p => p.Locations)
                .ThenInclude(p => p.Department)
                .Include(p => p.Locations)
                .ThenInclude(p => p.Province)
                .Include(p => p.Locations)
                .ThenInclude(p => p.District)
                .Include(p => p.Typology)
                .WhereIf(input.TerritorialUnitId.HasValue, p => p.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId.Value))
                .WhereIf(input.DepartmentId.HasValue, p => p.Locations.Any(p => p.Department.Id == input.DepartmentId.Value))
                .WhereIf(input.ProvinceId.HasValue, p => p.Locations.Any(p => p.Province.Id == input.ProvinceId.Value))
                .WhereIf(input.DistrictId.HasValue, p => p.Locations.Any(p => p.District.Id == input.DistrictId.Value))
                .WhereIf(input.PersonId.HasValue, p => p.ManagerId == input.PersonId.Value)
                .WhereIf(input.TypologyId.HasValue, p => p.TypologyId == input.TypologyId.Value)
                .WhereIf(input.Condition.HasValue, p => p.LastCondition == input.Condition.Value)
                .LikeAllBidirectional(input.SocialConflictCode.SplitByLike(), nameof(SocialConflict.Code))
                .LikeAllBidirectional(input.SocialConflictDescription.SplitByLike(), nameof(SocialConflict.CaseName));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var result = new List<UtilitySocialConflictDto>();            

            foreach (var dialog in output)
            {
                var dialogItem = ObjectMapper.Map<UtilitySocialConflictDto>(dialog);

                dialogItem.WomanCompromise = _compromiseRepository
                     .GetAll()
                     .Where(p => p.Record.SocialConflict.Id == dialogItem.Id && p.WomanCompromise)
                     .Any();

                dialogItem.TerritorialUnits = dialog.Locations.Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(",");

                result.Add(dialogItem);
            }

            return new PagedResultDto<UtilitySocialConflictDto>(count, result);
        }

        public async Task<PagedResultDto<UtilitySocialConflictUserDto>> GetAllUserSocialConflicts(UtilitySocialConflictUserGetAllInputDto input)
        {
            if (input.UserId.HasValue == false)
                return new PagedResultDto<UtilitySocialConflictUserDto>(0, new List<UtilitySocialConflictUserDto>());

            var query = _socialConflictRepository
                .GetAll()
                .Include(p => p.Locations)
                .ThenInclude(p => p.TerritorialUnit)
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.Locations.Any(p => p.Department.TerritorialUnitDepartments.Any(f => f.TerritorialUnit.Id == input.TerritorialUnitId)))
                .WhereIf(input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflict.Filter));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var result = new List<UtilitySocialConflictUserDto>();

            foreach (var dialog in output)
            {
                var dialogItem = ObjectMapper.Map<UtilitySocialConflictUserDto>(dialog);
                dialogItem.Selected = await _socialConflictUserRepository.CountAsync(p => p.UserId == input.UserId.Value && p.SocialConflictId == dialog.Id) > 0;
                dialogItem.TerritorialUnits = dialog.Locations.Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(",");

                result.Add(dialogItem);
            }

            return new PagedResultDto<UtilitySocialConflictUserDto>(count, result);
        }

        public async Task<PagedResultDto<UtilityRecordDto>> GetAllRecords(UtilityRecordInputDto input)
        {
            var filter = input.Filter.SplitByLike();

            var query = _recordRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .ThenInclude(p => p.Locations)
                .ThenInclude(p => p.TerritorialUnit)
                .Where(p => !p.SocialConflict.IsDeleted)
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.SocialConflictCode.IsValid(), p => p.SocialConflict.Code.Contains(input.SocialConflictCode))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.SocialConflict.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(Record.Filter)); 

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var result = new List<UtilityRecordDto>();

            foreach (var record in output)
            {
                var recordItem = ObjectMapper.Map<UtilityRecordDto>(record);                
                recordItem.TerritorialUnits = record.SocialConflict.Locations.Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(",");
                result.Add(recordItem);
            }

            return new PagedResultDto<UtilityRecordDto>(count, result);
        }

        public async Task<PagedResultDto<UtilitySocialConflictLocationDto>> GetAllSocialConflictLocations(EntityDto input)
        {
            var query = _socialConflictLocationRepository
                .GetAll()
                .Include(p => p.TerritorialUnit)
                .Include(p => p.Department)
                .Include(p => p.Province)
                .Include(p => p.District)
                .Where(p => p.SocialConflict.Id == input.Id && !p.TerritorialUnit.IsDeleted && !p.Department.IsDeleted && !p.Province.IsDeleted && !p.District.IsDeleted);

            var count = await query.CountAsync();
            var output = query.OrderBy(p => p.Id).PageBy(0, 1000);

            return new PagedResultDto<UtilitySocialConflictLocationDto>(count, ObjectMapper.Map<List<UtilitySocialConflictLocationDto>>(output));
        }

        public async Task<PagedResultDto<UtilityRegionGetAllDto>> GetAllRegions(UtilityRegionGetAllInputDto input)
        {
            var query = _regionRepository
                .GetAll()
                .WhereIf(input.DistrictId.HasValue, p => p.DistrictId == input.DistrictId.Value)
                .LikeAnyBidirectional(input.Filter.SplitByLike(), nameof(Region.Filter));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<UtilityRegionGetAllDto>(count, ObjectMapper.Map<List<UtilityRegionGetAllDto>>(output));
        }

        public async Task<PagedResultDto<UtilityPersonGetAllDto>> GetAllPersons(UtilityPersonGetAllInputDto input)
        {
            var query = _personRepository
                .GetAll()
                .Include(p => p.User)
                .WhereIf(input.PersonType != PersonType.None, p => p.Type == input.PersonType)
                .LikeAnyBidirectional(input.Filter.SplitByLike(), nameof(Person.Name));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<UtilityPersonGetAllDto>(count, ObjectMapper.Map<List<UtilityPersonGetAllDto>>(output));
        }

        public async Task<PagedResultDto<UtilityDepartmentDataDto>> GetAllDepartments()
        {
            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            var output = new List<UtilityDepartmentDataDto>();

            foreach (var item in departments)
            {
                var department = new UtilityDepartmentDataDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => new EntityDto(p.TerritorialUnitId)).ToList(),
                    Provinces = ObjectMapper.Map<List<UtilityProvinceDataDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Add(department);
            }

            var count = output.Count;

            return new PagedResultDto<UtilityDepartmentDataDto>(count, output);
        }

        public async Task<UtilitySocialConflictReportFilterGetDto> GetAllSocialConflictFilters()
        {
            var output = new UtilitySocialConflictReportFilterGetDto();

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<UtilityDepartmentDataDto>();

            foreach (var item in departments)
            {
                var department = new UtilityDepartmentDataDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => new EntityDto(p.TerritorialUnitId)).ToList(),
                    Provinces = ObjectMapper.Map<List<UtilityProvinceDataDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.TerritorialUnits = ObjectMapper.Map<List<UtilityTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().OrderBy(p => p.Name).ToListAsync());

            output.Typologies = ObjectMapper.Map<List<UtilityTypologyDto>>(_typologyRepository
               .GetAll()
               .Include(p => p.SubTypologies)
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList());

            output.Persons = ObjectMapper.Map<List<UtilityPersonDto>>(_personRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .Where(p => p.Type == PersonType.Manager || p.Type == PersonType.Coordinator)
               .ToList());

            return output;
        }

        public async Task<UtilitySocialConflictAlertReportFilterGetDto> GetAllSocialConflictAlertFilters()
        {
            var output = new UtilitySocialConflictAlertReportFilterGetDto();

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<UtilityDepartmentDataDto>();

            foreach (var item in departments)
            {
                var department = new UtilityDepartmentDataDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => new EntityDto(p.TerritorialUnitId)).ToList(),
                    Provinces = ObjectMapper.Map<List<UtilityProvinceDataDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.TerritorialUnits = ObjectMapper.Map<List<UtilityTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().OrderBy(p => p.Name).ToListAsync());

            output.Typologies = ObjectMapper.Map<List<UtilityTypologyDto>>(_typologyRepository
               .GetAll()
               .Include(p => p.SubTypologies)
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList());

            output.Responsibles = ObjectMapper.Map<List<UtilitySocialConflictAlertResponsibleDto>>(_alertResponsibleRepository
              .GetAll()
              .Where(p => p.Enabled)
              .OrderBy(p => p.Name)
              .ToList());

            output.Persons = ObjectMapper.Map<List<UtilityPersonDto>>(_personRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .Where(p => p.Type == PersonType.Analyst)
               .ToList());

            output.Risks = ObjectMapper.Map<List<UtilityAlertRiskDto>>(_alertRiskRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Index)
                .ToList());

            output.Seals = ObjectMapper.Map<List<UtilitySealDto>>(_alertSealRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList());

            return output;
        }

        public async Task<UtilitySectorMeetReportFilterGetDto> GetAllSectorMeetFilters()
        {
            var output = new UtilitySectorMeetReportFilterGetDto();

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<UtilityDepartmentDataDto>();

            foreach (var item in departments)
            {
                var department = new UtilityDepartmentDataDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => new EntityDto(p.TerritorialUnitId)).ToList(),
                    Provinces = ObjectMapper.Map<List<UtilityProvinceDataDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.Persons = ObjectMapper.Map<List<UtilityPersonDto>>(_personRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .Where(p => p.Type == PersonType.Analyst)
               .ToList());

            return output;
        }

        public async Task<UtilityDirectoryGovernmentFilterGetDto> GetAllDirectoryGovermentFilters()
        {
            var output = new UtilityDirectoryGovernmentFilterGetDto();

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<UtilityDepartmentDataDto>();

            foreach (var item in departments)
            {
                var department = new UtilityDepartmentDataDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => new EntityDto(p.TerritorialUnitId)).ToList(),
                    Provinces = ObjectMapper.Map<List<UtilityProvinceDataDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.Sectors = ObjectMapper.Map<List<UtilityDirectoryGovernmentSectorDto>>(_directoryGovernmentSectorRepository
               .GetAll()
               .OrderBy(p => p.Name)
               .ToList());

            return output;
        }

        [HttpPost]
        public async Task<PagedResultDto<UtilityDirectoryGovernmentDto>> GetAllDirectoryGoverments(UtilityDirectoryGovernmentGetAllInputDto input)
        {
            var query = _directoryGovernmentRepository
                .GetAll()
                .Include(p => p.DirectoryGovernmentSector)
                .Include(p => p.District)
                .ThenInclude(p => p.Province)
                .ThenInclude(p => p.Department)
                .WhereIf(input.SkippedDirectoryGovernmentsIds != null && input.SkippedDirectoryGovernmentsIds.Length > 0, p => !input.SkippedDirectoryGovernmentsIds.Contains(p.Id))
                .WhereIf(input.DepartmentId.HasValue, p => p.District.Province.Department.Id == input.DepartmentId.Value)
                .WhereIf(input.ProvinceId.HasValue, p => p.District.Province.Id == input.ProvinceId.Value)
                .WhereIf(input.DistrictId.HasValue, p => p.DistrictId == input.DistrictId.Value)
                .WhereIf(input.SectorId.HasValue, p => p.DirectoryGovernmentSectorId == input.SectorId.Value)
                .LikeAnyBidirectional(input.Name.SplitByLike(), nameof(DirectoryGovernment.Name))
                .LikeAnyBidirectional(input.ShortName.SplitByLike(), nameof(DirectoryGovernment.ShortName));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<UtilityDirectoryGovernmentDto>(count, ObjectMapper.Map<List<UtilityDirectoryGovernmentDto>>(output));
        }

        public async Task<PagedResultDto<UtilityConflictListGetAllDto>> GetAllConflictList(UtilityConflictListGetAllInputDto input)
        {
            return await _procedureRepository.CallGetAllConflictList(
                code: input.Code,
                caseName: input.CaseName,
                site: input.Site,
                lastCondition: input.LastCondition,
                departmentId: input.DepartmentId.HasValue ? input.DepartmentId.Value : -1,
                provinceId: input.ProvinceId.HasValue ? input.ProvinceId.Value : -1,
                districtId: input.DistrictId.HasValue ? input.DistrictId.Value : -1,
                skipCount: input.SkipCount,
                maxResultCount: input.MaxResultCount);
        }

        public async Task<UtilityGetDataDto> GetReportFilters()
        {
            var output = new UtilityGetDataDto();

            var departments = await _departmentRepository
                            .GetAll()
                            .Include(p => p.Provinces)
                            .ThenInclude(p => p.Districts)
                            .ToListAsync();

            output.Departments = new List<UtilityDepartmentDataDto>();

            var territorialUnitAvaliables = await _context.EntityDtos.FromSqlRaw("EXECUTE report_zones @p0", "TU").ToListAsync();
            var departmentAvaliables = await _context.EntityDtos.FromSqlRaw("EXECUTE report_zones @p0", "DE").ToListAsync();
            var provinceAvaliables = await _context.EntityDtos.FromSqlRaw("EXECUTE report_zones @p0", "PR").ToListAsync();
            var districtAvaliables = await _context.EntityDtos.FromSqlRaw("EXECUTE report_zones @p0", "DI").ToListAsync();
            var socialConflictAvaliables = await _context.EntityDtos.FromSqlRaw("EXECUTE report_zones @p0", "SC").ToListAsync();

            foreach (var department in departments)
            {
                if (departmentAvaliables.Any(d => d.Id == department.Id))
                {
                    var insertDepartment = new UtilityDepartmentDataDto
                    {
                        Id = department.Id,
                        Name = department.Name,
                        TerritorialUnitIds = await _territorialUnitRepository.GetAll().Where(p => p.TerritorialUnitDepartments.Any(d => d.Department.Id == department.Id)).Select(p => new EntityDto(p.Id)).ToListAsync(),
                        Provinces = ObjectMapper.Map<List<UtilityProvinceDataDto>>(department.Provinces)
                    };

                    insertDepartment.Provinces = insertDepartment.Provinces.Where(p => p != null).Where(p => provinceAvaliables.Any(d => d.Id == p.Id)).ToList();

                    foreach(var province in insertDepartment.Provinces)
                    {
                        province.Districts = province.Districts.Where(p => p != null).Where(p => districtAvaliables.Any(d => d.Id == p.Id)).ToList();
                    }

                    output.Departments.Add(insertDepartment);
                }

            }

            output.TerritorialUnits = ObjectMapper.Map<List<UtilityTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().ToListAsync());
            output.TerritorialUnits = output.TerritorialUnits.Where(p => p != null).Where(p => territorialUnitAvaliables.Any(d => d.Id == p.Id)).ToList();

            var socialConflcits = await _socialConflictRepository
                 .GetAll()
                 .Include(p => p.Locations)
                     .ThenInclude(p => p.TerritorialUnit)
                 .Include(p => p.Locations)
                     .ThenInclude(p => p.Department)
                 .Include(p => p.Locations)
                     .ThenInclude(p => p.Province)
                 .Include(p => p.Locations)
                     .ThenInclude(p => p.District)
                 .ToListAsync();

            output.SocialConflicts = socialConflcits.Where(p => p != null).Where(p => socialConflictAvaliables.Any(d => d.Id == p.Id)).Select(p =>
            {
                return new UtilitySocialConflictDataDto()
                {
                    Id = p.Id,
                    CaseName = p.CaseName,
                    Code = p.Code,
                    CreationTime = p.CreationTime,
                    Description = p.Description,
                    Dialog = p.Dialog,
                    LastModificationTime = p.LastModificationTime,
                    Locations = ObjectMapper.Map<List<UtilitySocialConflictLocationDataDto>>(p.Locations)
                };
            }).ToList();

            return output;
        }

        public async Task<UtilityInterventionPlanFilterGetDto> GetAllInterventionPlanFilters()
        {
            var output = new UtilityInterventionPlanFilterGetDto();

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<UtilityDepartmentDataDto>();

            foreach (var item in departments)
            {
                var department = new UtilityDepartmentDataDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => new EntityDto(p.TerritorialUnitId)).ToList(),
                    Provinces = ObjectMapper.Map<List<UtilityProvinceDataDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.TerritorialUnits = ObjectMapper.Map<List<UtilityTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().OrderBy(p => p.Name).ToListAsync());

            output.Persons = ObjectMapper.Map<List<UtilityPersonDto>>(_personRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .Where(p => p.Enabled && (p.Type == PersonType.Coordinator || p.Type == PersonType.Manager))
               .ToList());

            return output;
        }

        public async Task<PagedResultDto<UtilityInterventionPlanGetAllDto>> GetAllInterventionPlans(UtilityInterventionPlanGetAllInputDto input)
        {
            var query = _interventionPlanRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.SocialConflictSensible)
                .WhereIf(input.ValidForTerritorialUnit(), p => p.Locations.Any(d => d.TerritorialUnitId == input.TerritorialUnitId.Value))
                .WhereIf(input.ValidForDepartment(), p => p.Locations.Any(d => d.DepartmentId == input.DepartmentId.Value))
                .WhereIf(input.ValidForProvince(), p => p.Locations.Any(d => d.DistrictId == input.DistrictId.Value))
                .WhereIf(input.PersonId.HasValue, p => p.PersonId == input.PersonId.Value)
                .LikeAllBidirectional(input.Code.SplitByLike(), nameof(InterventionPlan.Code))
                .LikeAllBidirectional(input.CaseName.SplitByLike(), nameof(InterventionPlan.CaseName));

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input).ToList();
            var output = new List<UtilityInterventionPlanGetAllDto>();

            foreach (var item in result)
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

                output.Add(new UtilityInterventionPlanGetAllDto()
                {
                    Id = item.Id,
                    Code = item.Code,
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

            return new PagedResultDto<UtilityInterventionPlanGetAllDto>(count, output);
        }

        public async Task<PagedResultDto<UtilityDirectoryIndustryGetAllDto>> GetAllDirectoryIndustries(UtilityDirectoryIndustryGetAllInputDto input)
        {
            var query = _directoryIndustryRepository
                .GetAll()
                .Include(p => p.DirectorySector)
                .Include(p => p.District)
                .ThenInclude(p => p.Province)
                .ThenInclude(p => p.Department)
                .WhereIf(input.ValidForDepartment(), p => p.District.Province.Department.Id == input.DepartmentId.Value)
                .WhereIf(input.ValidForProvince(), p => p.District.Province.Id == input.ProvinceId.Value)
                .WhereIf(input.DistrictId.HasValue, p => p.District.Id == input.DistrictId.Value)
                .WhereIf(input.SectorId.HasValue, p => p.DirectorySectorId == input.SectorId.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(DirectoryIndustry.Name))
                .LikeAllBidirectional(input.Address.SplitByLike(), nameof(DirectoryIndustry.Address));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<UtilityDirectoryIndustryGetAllDto>(count, ObjectMapper.Map<List<UtilityDirectoryIndustryGetAllDto>>(output));
        }

        public async Task<UtilityQuizFilterGetDto> GetAllQuizFilters()
        {
            var output = new UtilityQuizFilterGetDto();

            output.QuizStates = ObjectMapper.Map<List<UtilityQuizStateDto>>(await _quizStateRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToListAsync());

            return output;
        }

        public async Task<UtilityDialogSpaceFilterGetDto> GetAllDialogSpaceFilters()
        {
            var output = new UtilityDialogSpaceFilterGetDto();

            var departments = await _departmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.Departments = new List<UtilityDepartmentDataDto>();

            foreach (var item in departments)
            {
                var department = new UtilityDepartmentDataDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    TerritorialUnitIds = item.TerritorialUnitDepartments.Select(p => new EntityDto(p.TerritorialUnitId)).ToList(),
                    Provinces = ObjectMapper.Map<List<UtilityProvinceDataDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                {
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();
                }

                output.Departments.Add(department);
            }

            output.TerritorialUnits = ObjectMapper.Map<List<UtilityTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().OrderBy(p => p.Name).ToListAsync());

            output.DialogSpaceTypes = ObjectMapper.Map<List<UtilityDialogSpaceTypeDto>>(await _dialogSpaceTypeRepository.GetAll().Where(p => p.Enabled).OrderBy(p => p.Name).ToListAsync());

            return output;
        }
    }
}
