using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.SectorMeetSessions;
using Contable.Application.SectorMeetSessions.Dto;
using Contable.Application.Extensions;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using Contable.Application.Uploaders.Dto;
using NUglify.Helpers;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
    public class SectorMeetSessionAppService : ContableAppServiceBase, ISectorMeetSessionAppService
    {
        private readonly IRepository<SectorMeetSession> _sectorMeetSessionRepository;
        private readonly IRepository<SectorMeet> _sectorMeetRepository;
        private readonly IRepository<SectorMeetSessionAction> _sectorMeetSessionActionRepository;
        private readonly IRepository<SectorMeetSessionAgreement> _sectorMeetSessionAgreementRepository;
        private readonly IRepository<SectorMeetSessionCriticalAspect> _sectorMeetSessionCriticalAspectRepository;
        private readonly IRepository<SectorMeetSessionSchedule> _sectorMeetSessionScheduleRepository;
        private readonly IRepository<SectorMeetSessionSummary> _sectorMeetSessionSummaryRepository;
        private readonly IRepository<SectorMeetSessionResource> _sectorMeetSessionResourceRepository;
        private readonly IRepository<SectorMeetSessionLeader> _sectorMeetSessionLeaderRepository;
        private readonly IRepository<SectorMeetSessionTeam> _sectorMeetSessionTeamRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<DirectoryIndustry> _directoryIndustryRepository;
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;

        public SectorMeetSessionAppService(
            IRepository<SectorMeetSession> sectorMeetSessionRepository,
            IRepository<SectorMeet> sectorMeetRepository,
            IRepository<SectorMeetSessionAction> sectorMeetSessionActionRepository, 
            IRepository<SectorMeetSessionAgreement> sectorMeetSessionAgreementRepository,
            IRepository<SectorMeetSessionCriticalAspect> sectorMeetSessionCriticalAspectRepository,
            IRepository<SectorMeetSessionSchedule> sectorMeetSessionScheduleRepository,
            IRepository<SectorMeetSessionSummary> sectorMeetSessionSummaryRepository,
            IRepository<SectorMeetSessionResource> sectorMeetSessionResourceRepository,
            IRepository<SectorMeetSessionLeader> sectorMeetSessionLeaderRepository,
            IRepository<SectorMeetSessionTeam> sectorMeetSessionTeamRepository,
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<District> districtRepository,
            IRepository<Person> personRepository,
            IRepository<DirectoryIndustry> directoryIndustryRepository,
            IRepository<DirectoryGovernment> directoryGovernmentRepository)
        {
            _sectorMeetSessionRepository = sectorMeetSessionRepository;
            _sectorMeetRepository = sectorMeetRepository;
            _sectorMeetSessionActionRepository = sectorMeetSessionActionRepository;
            _sectorMeetSessionAgreementRepository = sectorMeetSessionAgreementRepository;
            _sectorMeetSessionCriticalAspectRepository = sectorMeetSessionCriticalAspectRepository;
            _sectorMeetSessionScheduleRepository = sectorMeetSessionScheduleRepository;
            _sectorMeetSessionSummaryRepository = sectorMeetSessionSummaryRepository;
            _sectorMeetSessionResourceRepository = sectorMeetSessionResourceRepository;
            _sectorMeetSessionLeaderRepository = sectorMeetSessionLeaderRepository;
            _sectorMeetSessionTeamRepository = sectorMeetSessionTeamRepository;
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _personRepository = personRepository;
            _directoryIndustryRepository = directoryIndustryRepository;
            _directoryGovernmentRepository = directoryGovernmentRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet_Create)]
        public async Task<EntityDto> Create(SectorMeetSessionCreateDto input)
        {
            var dbSectorMeetSession = await ValidateEntity(
                input: ObjectMapper.Map<SectorMeetSession>(input),
                sectorMeetId: input.SectorMeet == null ? -1 : input.SectorMeet.Id,
                personId: input.Person == null ? -1 : input.Person.Id,
                departmentId: input.Department == null ? -1 : input.Department.Id,
                provinceId: input.Province == null ? -1 : input.Province.Id,
                districtId: input.District == null ? -1 : input.District.Id,
                actions: input.Actions ?? new List<SectorMeetSessionActionRelationDto>(),
                agreements: input.Agreements ?? new List<SectorMeetSessionAgreementRelationDto>(),
                criticalAspects: input.CriticalAspects ?? new List<SectorMeetSessionCriticalAspectRelationDto>(),
                schedules: input.Schedules ?? new List<SectorMeetSessionScheduleRelationDto>(),
                summaries: input.Summaries ?? new List<SectorMeetSessionSummaryRelationDto>(),
                resources: input.Resources ?? new List<SectorMeetSessionResourceRelationDto>(),
                leaders: input.Leaders ?? new List<SectorMeetSessionLeaderRelationDto>(),
                uploadFiles: input.UploadFiles ?? new List<SectorMeetSessionAttachmentDto>(),
                uploadFilesPDF: input.UploadFilesPDF ?? new List<SectorMeetSessionAttachmentDto>());

            var sectorMeetSessionId = await _sectorMeetSessionRepository.InsertAndGetIdAsync(dbSectorMeetSession);

            return new EntityDto(sectorMeetSessionId);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _sectorMeetSessionRepository.CountAsync(p => p.Id == input.Id));

            await _sectorMeetSessionRepository.DeleteAsync(p => p.Id == input.Id);
            await _sectorMeetSessionActionRepository.DeleteAsync(p => p.SectorMeetSessionId == input.Id);
            await _sectorMeetSessionAgreementRepository.DeleteAsync(p => p.SectorMeetSessionId == input.Id);
            await _sectorMeetSessionCriticalAspectRepository.DeleteAsync(p => p.SectorMeetSessionId == input.Id);
            await _sectorMeetSessionScheduleRepository.DeleteAsync(p => p.SectorMeetSessionId == input.Id);
            await _sectorMeetSessionSummaryRepository.DeleteAsync(p => p.SectorMeetSessionId == input.Id);
            await _sectorMeetSessionResourceRepository.DeleteAsync(p => p.SectorMeetSessionId == input.Id);
            await _sectorMeetSessionLeaderRepository.DeleteAsync(p => p.SectorMeetSessionId == input.Id);            
            await _sectorMeetSessionTeamRepository.DeleteAsync(p => p.SectorMeetSessionLeader.SectorMeetSessionId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
        public async Task<SectorMeetSessionGetDataDto> Get(SectorMeetSessionGetInputDto input)
        {
            var output = new SectorMeetSessionGetDataDto();

            if(input.SectorMeetSessionId.HasValue)
            {
                VerifyCount(await _sectorMeetSessionRepository.CountAsync(p => p.Id == input.SectorMeetSessionId.Value && p.SectorMeetId == input.SectorMeetId));

                var dbSectorMeetSession = _sectorMeetSessionRepository
                    .GetAll()
                    .Include(p => p.SectorMeet)
                    .Include(p => p.Person)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Where(p => p.Id == input.SectorMeetSessionId.Value)
                    .First();

                output.SectorMeetSession = ObjectMapper.Map<SectorMeetSessionGetDto>(dbSectorMeetSession);

                output.SectorMeetSession.Actions = ObjectMapper.Map<List<SectorMeetSessionActionRelationDto>>(_sectorMeetSessionActionRepository
                    .GetAll()
                    .Where(p => p.SectorMeetSessionId == dbSectorMeetSession.Id)
                    .OrderBy(p => p.Index)
                    .ToList());

                output.SectorMeetSession.Agreements = ObjectMapper.Map<List<SectorMeetSessionAgreementRelationDto>>(_sectorMeetSessionAgreementRepository
                    .GetAll()
                    .Where(p => p.SectorMeetSessionId == dbSectorMeetSession.Id)
                    .OrderBy(p => p.Index)
                    .ToList());

                output.SectorMeetSession.CriticalAspects = ObjectMapper.Map<List<SectorMeetSessionCriticalAspectRelationDto>>(_sectorMeetSessionCriticalAspectRepository
                    .GetAll()
                    .Where(p => p.SectorMeetSessionId == dbSectorMeetSession.Id)
                    .OrderBy(p => p.Index)
                    .ToList());

                output.SectorMeetSession.Schedules = ObjectMapper.Map<List<SectorMeetSessionScheduleRelationDto>>(_sectorMeetSessionScheduleRepository
                    .GetAll()
                    .Where(p => p.SectorMeetSessionId == dbSectorMeetSession.Id)
                    .OrderBy(p => p.Index)
                    .ToList());

                output.SectorMeetSession.Summaries = ObjectMapper.Map<List<SectorMeetSessionSummaryRelationDto>>(_sectorMeetSessionSummaryRepository
                    .GetAll()
                    .Include(p => p.SectorMeetSessionLeader)
                    .ThenInclude(p => p.DirectoryGovernment)
                    .Include(p => p.SectorMeetSessionLeader)
                    .ThenInclude(p => p.DirectoryIndustry)
                    .Where(p => p.SectorMeetSessionId == dbSectorMeetSession.Id)
                    .OrderBy(p => p.Index)
                    .ToList());

                output.SectorMeetSession.Resources = ObjectMapper.Map<List<SectorMeetSessionResourceRelationDto>>(_sectorMeetSessionResourceRepository
                    .GetAll()
                    .Where(p => p.SectorMeetSessionId == dbSectorMeetSession.Id)
                    .ToList());

                output.SectorMeetSession.Leaders = ObjectMapper.Map<List<SectorMeetSessionLeaderRelationDto>>(_sectorMeetSessionLeaderRepository
                    .GetAll()
                    .Include(p => p.DirectoryGovernment)
                    .ThenInclude(p => p.District)
                    .ThenInclude(p => p.Province)
                    .ThenInclude(p => p.Department)
                    .Include(p => p.DirectoryIndustry)
                    .ThenInclude(p => p.District)
                    .ThenInclude(p => p.Province)
                    .ThenInclude(p => p.Department)
                    .Include(p => p.Teams)
                    .Where(p => p.SectorMeetSessionId == dbSectorMeetSession.Id)
                    .OrderBy(p => p.Index)
                    .ToList());
            }
            else
            {
                if (await _sectorMeetRepository.CountAsync(p => p.Id == input.SectorMeetId) == 0)
                    throw new UserFriendlyException("Aviso", "La reuniones multisectorial a la que hace referencia es inválida o ya no existe. Verifique la información antes de continuar");

                var dbSectorMeet = _sectorMeetRepository
                    .GetAll()
                    .Where(p => p.Id == input.SectorMeetId)
                    .First();

                output.SectorMeetSession = new SectorMeetSessionGetDto()
                {
                    SectorMeet = ObjectMapper.Map<SectorMeetSessionSectorMeetRelationDto>(dbSectorMeet)
                };

                var leaders = ObjectMapper.Map<List<SectorMeetSessionLeaderRelationDto>>(_sectorMeetSessionLeaderRepository
                    .GetAll()
                    .Include(p => p.DirectoryGovernment)
                    .ThenInclude(p => p.District)
                    .ThenInclude(p => p.Province)
                    .ThenInclude(p => p.Department)
                    .Include(p => p.DirectoryIndustry)
                    .ThenInclude(p => p.District)
                    .ThenInclude(p => p.Province)
                    .ThenInclude(p => p.Department)
                    .Include(p => p.Teams)
                    .Where(p => p.SectorMeetSession.SectorMeetId == dbSectorMeet.Id)
                    .ToList());

                var directorIndustries = new List<SectorMeetSessionLeaderRelationDto>();
                var directoryGovernments = new List<SectorMeetSessionLeaderRelationDto>();
                var civilSocieties = new List<SectorMeetSessionLeaderRelationDto>();
                var others = new List<SectorMeetSessionLeaderRelationDto>();

                foreach (var leader in leaders)
                {
                    leader.Id = 0;

                    foreach (var team in leader.Teams)
                        team.Id = 0;

                    if (leader.Type == SectorMeetSessionEntityType.COMPANY && leader.DirectoryIndustry != null)
                    {
                        var index = directorIndustries.FindIndex(p => p.DirectoryIndustry.Id == leader.DirectoryIndustry.Id);

                        if (index == -1)
                        {
                            directorIndustries.Add(leader);
                        }
                        else
                        {
                            foreach (var team in leader.Teams)
                                if (directorIndustries[index].Teams.Any(p => p.Document == team.Document && p.Name == team.Name && p.Surname == team.Surname) == false)
                                    directorIndustries[index].Teams.Add(team);
                        }
                    }
                    if (leader.Type == SectorMeetSessionEntityType.ESTATAL_ENTITY && leader.DirectoryGovernment != null)
                    {
                        var index = directoryGovernments.FindIndex(p => p.DirectoryGovernment.Id == leader.DirectoryGovernment.Id);

                        if (index == -1)
                        {
                            directoryGovernments.Add(leader);
                        }
                        else
                        {
                            foreach (var team in leader.Teams)
                                if (directoryGovernments[index].Teams.Any(p => p.Document == team.Document && p.Name == team.Name && p.Surname == team.Surname) == false)
                                    directoryGovernments[index].Teams.Add(team);
                        }

                    }
                    if (leader.Type == SectorMeetSessionEntityType.CIVIL_SOCIETY)
                    {
                        var index = civilSocieties.FindIndex(p => p.Role == leader.Role);

                        if (index == -1)
                        {
                            civilSocieties.Add(leader);
                        }
                        else
                        {
                            foreach (var team in leader.Teams)
                                if (civilSocieties[index].Teams.Any(p => p.Document == team.Document && p.Name == team.Name && p.Surname == team.Surname) == false)
                                    civilSocieties[index].Teams.Add(team);
                        }
                    }
                    if (leader.Type == SectorMeetSessionEntityType.OTHER)
                    {
                        var index = others.FindIndex(p => p.Role == leader.Role);

                        if (index == -1)
                        {
                            others.Add(leader);
                        }
                        else
                        {
                            foreach (var team in leader.Teams)
                                if (others[index].Teams.Any(p => p.Document == team.Document && p.Name == team.Name && p.Surname == team.Surname) == false)
                                    others[index].Teams.Add(team);
                        }
                    }
                }

                output.SectorMeetSession.Leaders = new List<SectorMeetSessionLeaderRelationDto>();

                foreach (var item in directorIndustries)
                    output.SectorMeetSession.Leaders.Add(item);
                foreach (var item in directoryGovernments)
                    output.SectorMeetSession.Leaders.Add(item);
                foreach (var civilSociety in civilSocieties)
                    output.SectorMeetSession.Leaders.Add(civilSociety);
                foreach (var other in others)
                    output.SectorMeetSession.Leaders.Add(other);

            }

            output.Departments = new List<SectorMeetSessionDepartmentRelationDto>();

            var departments = _departmentRepository
                .GetAll()
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var item in departments)
            {
                var department = new SectorMeetSessionDepartmentRelationDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Provinces = ObjectMapper.Map<List<SectorMeetSessionProvinceRelationDto>>(item.Provinces.OrderBy(p => p.Name))
                };

                foreach (var province in department.Provinces)
                    province.Districts = province.Districts.OrderBy(p => p.Name).ToList();

                output.Departments.Add(department);
            }

            output.Persons = ObjectMapper.Map<List<SectorMeetSessionPersonRelationDto>>(_personRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .Where(p => p.Type != PersonType.None)
                .ToList());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
        public async Task<PagedResultDto<SectorMeetSessionGetAllDto>> GetAll(SectorMeetSessionGetAllInputDto input)
        {
            var query = _sectorMeetSessionRepository
                .GetAll()
                .Include(p => p.Department)
                .Include(p => p.Province)
                .Include(p => p.District)
                .WhereIf(input.SectorMeetId.HasValue, p => p.SectorMeetId == input.SectorMeetId.Value);

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<SectorMeetSessionGetAllDto>(count, ObjectMapper.Map<List<SectorMeetSessionGetAllDto>>(result));
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
        public async Task<PagedResultDto<SectorMeetSessionLeaderGetAllDto>> GetAllLeaders(SectorMeetSessionLeaderGetAllInputDto input)
        {
            var query = _sectorMeetSessionLeaderRepository
                .GetAll()
                .Include(p => p.DirectoryGovernment)
                .ThenInclude(p => p.District)
                .ThenInclude(p => p.Province)
                .ThenInclude(p => p.Department)
                .Include(p => p.DirectoryIndustry)
                .ThenInclude(p => p.District)
                .ThenInclude(p => p.Province)
                .ThenInclude(p => p.Department)
                .Include(p => p.Teams)
                .Where(p => p.SectorMeetSessionId == input.SectorMeetSessionId)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<SectorMeetSessionLeader, bool>>)(expression =>
                        (expression.DirectoryGovernment == null || EF.Functions.Like(expression.DirectoryGovernment.Name, $"%{word}%")) ||
                        (expression.DirectoryIndustry == null || EF.Functions.Like(expression.DirectoryIndustry.Name, $"%{word}%")) ||
                        EF.Functions.Like(expression.Entity, $"%{word}%") ||
                        EF.Functions.Like(expression.Role, $"%{word}%")
                        ))
                    .ToArray());

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<SectorMeetSessionLeaderGetAllDto>(count, ObjectMapper.Map<List<SectorMeetSessionLeaderGetAllDto>>(result));
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet_Edit)]
        public async Task<EntityDto> Update(SectorMeetSessionUpdateDto input)
        {
            VerifyCount(await _sectorMeetSessionRepository.CountAsync(p => p.Id == input.Id));

            var dbSectorMeetSession = ObjectMapper.Map(input, await _sectorMeetSessionRepository.GetAsync(input.Id));

            var resultDbSectorMeetSession = await ValidateEntity(
                input: dbSectorMeetSession,
                sectorMeetId: dbSectorMeetSession.SectorMeetId,
                personId: input.Person == null ? -1 : input.Person.Id,
                departmentId: input.Department == null ? -1 : input.Department.Id,
                provinceId: input.Province == null ? -1 : input.Province.Id,
                districtId: input.District == null ? -1 : input.District.Id,
                actions: input.Actions ?? new List<SectorMeetSessionActionRelationDto>(),
                agreements: input.Agreements ?? new List<SectorMeetSessionAgreementRelationDto>(),
                criticalAspects: input.CriticalAspects ?? new List<SectorMeetSessionCriticalAspectRelationDto>(),
                schedules: input.Schedules ?? new List<SectorMeetSessionScheduleRelationDto>(),
                summaries: input.Summaries ?? new List<SectorMeetSessionSummaryRelationDto>(),
                resources: input.Resources ?? new List<SectorMeetSessionResourceRelationDto>(),
                leaders: input.Leaders ?? new List<SectorMeetSessionLeaderRelationDto>(),
                uploadFiles: input.UploadFiles ?? new List<SectorMeetSessionAttachmentDto>(),
                uploadFilesPDF: input.UploadFilesPDF ?? new List<SectorMeetSessionAttachmentDto>());

            await _sectorMeetSessionRepository.UpdateAsync(resultDbSectorMeetSession);

            return new EntityDto(resultDbSectorMeetSession.Id);
        }

        private async Task<SectorMeetSession> ValidateEntity(
            SectorMeetSession input, 
            int sectorMeetId, 
            int personId,
            int departmentId, 
            int provinceId, 
            int districtId,
            List<SectorMeetSessionActionRelationDto> actions,
            List<SectorMeetSessionAgreementRelationDto> agreements,
            List<SectorMeetSessionCriticalAspectRelationDto> criticalAspects,
            List<SectorMeetSessionScheduleRelationDto> schedules,
            List<SectorMeetSessionSummaryRelationDto> summaries,
            List<SectorMeetSessionResourceRelationDto> resources,
            List<SectorMeetSessionLeaderRelationDto> leaders,
            List<SectorMeetSessionAttachmentDto> uploadFiles,
            List<SectorMeetSessionAttachmentDto> uploadFilesPDF)
        {
            input.SessionTime = new DateTime(input.SessionTime.Year, input.SessionTime.Month, input.SessionTime.Day, input.SessionTime.Hour, input.SessionTime.Minute, 0);

            var sectorMeet = await _sectorMeetRepository
                .GetAll()
                .Where(p => p.Id == sectorMeetId)
                .FirstOrDefaultAsync();

            if (sectorMeet == null)
                throw new UserFriendlyException("Aviso", "La reuniones multisectorial a la que hace referencia es inválida o ya no existe. Verifique la información antes de continuar");

            input.SectorMeet = sectorMeet;
            input.SectorMeetId = sectorMeet.Id;

            if(personId > 0)
            {
                var dbPerson = _personRepository
                    .GetAll()
                    .Where(p => p.Id == personId)
                    .FirstOrDefault();

                input.Person = dbPerson;
                input.PersonId = dbPerson == null ? (int?)null : dbPerson.Id;
            }
            else
            {
                input.Person = null;
                input.PersonId = null;
            }

            if (input.Type == SectorMeetSessionType.PRESENTIAL)
            {
                var department = await _departmentRepository
                    .GetAll()
                    .Where(p => p.Id == departmentId)
                    .FirstOrDefaultAsync();

                if (department == null)
                    throw new UserFriendlyException("Aviso", "El departamento seleccionado es inválido o ya no existe. Por favor verifique la información antes de continuar");

                var province = await _provinceRepository
                    .GetAll()
                    .Where(p => p.Id == provinceId)
                    .FirstOrDefaultAsync();

                if (province == null)
                    throw new UserFriendlyException("Aviso", "La provincia seleccionada es inválida o ya no existe. Por favor verifique la información antes de continuar");

                var district = await _districtRepository
                    .GetAll()
                    .Where(p => p.Id == districtId)
                    .FirstOrDefaultAsync();

                if (district == null)
                    throw new UserFriendlyException("Aviso", "El distrito seleccionado es inválido o ya no existe. Por favor verifique la información antes de continuar");

                input.Department = department;
                input.DepartmentId = department.Id;

                input.Province = province;
                input.ProvinceId = province.Id;

                input.District = district;
                input.DistrictId = district.Id;
            }
            else
            {
                input.Department = null;
                input.DepartmentId = null;

                input.Province = null;
                input.ProvinceId = null;

                input.District = null;
                input.DistrictId = null;

                input.Location = null;
            }

            input.Actions = new List<SectorMeetSessionAction>();
            input.Agreements = new List<SectorMeetSessionAgreement>();
            input.CriticalAspects = new List<SectorMeetSessionCriticalAspect>();
            input.Schedules = new List<SectorMeetSessionSchedule>();
            input.Summaries = new List<SectorMeetSessionSummary>();
            input.Resources = new List<SectorMeetSessionResource>();
            input.Leaders = new List<SectorMeetSessionLeader>();

            var index = 0;

            foreach (var action in actions)
            {
                if (action.Remove)
                {
                    if (action.Id > 0 && input.Id > 0 && await _sectorMeetSessionActionRepository.CountAsync(p => p.Id == action.Id && p.SectorMeetSessionId == input.Id) > 0)
                    {
                        await _sectorMeetSessionActionRepository.DeleteAsync(action.Id);
                    }
                }
                else
                {
                    action.Description.IsValidOrException("Aviso", "La descripción de las próximas acciones son obligatorias");
                    action.Description.VerifyTableColumn(SectorMeetSessionActionConsts.DescriptionMinLength,
                        SectorMeetSessionActionConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de las acciones próximas \"{action.Description}\" no debe exceder los " +
                        $"{SectorMeetSessionActionConsts.DescriptionMaxLength} caracteres");

                    if (action.Id > 0)
                    {
                        if (await _sectorMeetSessionActionRepository.CountAsync(p => p.Id == action.Id && p.SectorMeetSessionId == input.Id) > 0)
                        {
                            var dbSectorMeetSessionAction = await _sectorMeetSessionActionRepository.GetAsync(action.Id);

                            dbSectorMeetSessionAction.Description = action.Description;
                            dbSectorMeetSessionAction.Index = index;

                            await _sectorMeetSessionActionRepository.UpdateAsync(dbSectorMeetSessionAction);
                        }
                    }
                    else
                    {
                        input.Actions.Add(new SectorMeetSessionAction()
                        {
                            Description = action.Description,
                            Index = index
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var agreement in agreements)
            {
                if (agreement.Remove)
                {
                    if (agreement.Id > 0 && input.Id > 0 && await _sectorMeetSessionAgreementRepository.CountAsync(p => p.Id == agreement.Id && p.SectorMeetSessionId == input.Id) > 0)
                    {
                        await _sectorMeetSessionAgreementRepository.DeleteAsync(agreement.Id);
                    }
                }
                else
                {
                    agreement.Description.IsValidOrException("Aviso", "La descripción de los acuerdos son obligatorias");
                    agreement.Description.VerifyTableColumn(SectorMeetSessionAgreementConsts.DescriptionMinLength,
                        SectorMeetSessionAgreementConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de los acuerdos \"{agreement.Description}\" no debe exceder los " +
                        $"{SectorMeetSessionAgreementConsts.DescriptionMaxLength} caracteres");

                    if (agreement.Id > 0)
                    {
                        if (await _sectorMeetSessionAgreementRepository.CountAsync(p => p.Id == agreement.Id && p.SectorMeetSessionId == input.Id) > 0)
                        {
                            var dbSectorMeetSessionAgreement = await _sectorMeetSessionAgreementRepository.GetAsync(agreement.Id);

                            dbSectorMeetSessionAgreement.Description = agreement.Description;
                            dbSectorMeetSessionAgreement.Index = index;

                            await _sectorMeetSessionAgreementRepository.UpdateAsync(dbSectorMeetSessionAgreement);
                        }
                    }
                    else
                    {
                        input.Agreements.Add(new SectorMeetSessionAgreement()
                        {
                            Description = agreement.Description,
                            Index = index
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var criticalAspect in criticalAspects)
            {
                if (criticalAspect.Remove)
                {
                    if (criticalAspect.Id > 0 && input.Id > 0 && await _sectorMeetSessionCriticalAspectRepository.CountAsync(p => p.Id == criticalAspect.Id && p.SectorMeetSessionId == input.Id) > 0)
                    {
                        await _sectorMeetSessionCriticalAspectRepository.DeleteAsync(criticalAspect.Id);
                    }
                }
                else
                {
                    criticalAspect.Description.IsValidOrException("Aviso", "La descripción de los aspectos críticos son obligatorias");
                    criticalAspect.Description.VerifyTableColumn(SectorMeetSessionCriticalAspectConsts.DescriptionMinLength,
                        SectorMeetSessionCriticalAspectConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de los aspectos críticos \"{criticalAspect.Description}\" no debe exceder los " +
                        $"{SectorMeetSessionCriticalAspectConsts.DescriptionMaxLength} caracteres");

                    if (criticalAspect.Id > 0)
                    {
                        if (await _sectorMeetSessionCriticalAspectRepository.CountAsync(p => p.Id == criticalAspect.Id && p.SectorMeetSessionId == input.Id) > 0)
                        {
                            var dbSectorMeetSessionCriticalAspect = await _sectorMeetSessionCriticalAspectRepository.GetAsync(criticalAspect.Id);

                            dbSectorMeetSessionCriticalAspect.Description = criticalAspect.Description;
                            dbSectorMeetSessionCriticalAspect.Index = index;

                            await _sectorMeetSessionCriticalAspectRepository.UpdateAsync(dbSectorMeetSessionCriticalAspect);
                        }
                    }
                    else
                    {
                        input.CriticalAspects.Add(new SectorMeetSessionCriticalAspect()
                        {
                            Description = criticalAspect.Description,
                            Index = index
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var schedule in schedules)
            {
                if (schedule.Remove)
                {
                    if (schedule.Id > 0 && input.Id > 0 && await _sectorMeetSessionScheduleRepository.CountAsync(p => p.Id == schedule.Id && p.SectorMeetSessionId == input.Id) > 0)
                    {
                        await _sectorMeetSessionScheduleRepository.DeleteAsync(schedule.Id);
                    }
                }
                else
                {
                    schedule.Description.IsValidOrException("Aviso", "La descripción de la agenda de reunión es obligatoria");
                    schedule.Description.VerifyTableColumn(SectorMeetSessionScheduleConsts.DescriptionMinLength,
                        SectorMeetSessionScheduleConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la agenda de reunión \"{schedule.Description}\" no debe exceder los " +
                        $"{SectorMeetSessionScheduleConsts.DescriptionMaxLength} caracteres");

                    if (schedule.Id > 0)
                    {
                        if (await _sectorMeetSessionScheduleRepository.CountAsync(p => p.Id == schedule.Id && p.SectorMeetSessionId == input.Id) > 0)
                        {
                            var dbSectorMeetSessionSchedule = await _sectorMeetSessionScheduleRepository.GetAsync(schedule.Id);

                            dbSectorMeetSessionSchedule.Description = schedule.Description;
                            dbSectorMeetSessionSchedule.Index = index;

                            await _sectorMeetSessionScheduleRepository.UpdateAsync(dbSectorMeetSessionSchedule);
                        }
                    }
                    else
                    {
                        input.Schedules.Add(new SectorMeetSessionSchedule()
                        {
                            Description = schedule.Description,
                            Index = index
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var summary in summaries)
            {
                if (summary.Remove)
                {
                    if (summary.Id > 0 && input.Id > 0 && await _sectorMeetSessionSummaryRepository.CountAsync(p => p.Id == summary.Id && p.SectorMeetSessionId == input.Id) > 0)
                    {
                        await _sectorMeetSessionSummaryRepository.DeleteAsync(summary.Id);
                    }
                }
                else
                {
                    summary.Description.IsValidOrException("Aviso", "La descripción del resumen de reunión es obligatorio");
                    summary.Description.VerifyTableColumn(SectorMeetSessionScheduleConsts.DescriptionMinLength,
                        SectorMeetSessionScheduleConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del resumen de reunión \"{summary.Description}\" no debe exceder los " +
                        $"{SectorMeetSessionScheduleConsts.DescriptionMaxLength} caracteres");

                    SectorMeetSessionLeader dbSectorMeetSessionLeader = null;

                    if(summary.SectorMeetSessionLeader != null)
                    {
                        dbSectorMeetSessionLeader = _sectorMeetSessionLeaderRepository
                            .GetAll()
                            .Where(p => p.Id == summary.SectorMeetSessionLeader.Id)
                            .FirstOrDefault();
                    }

                    if (summary.Id > 0)
                    {
                        if (await _sectorMeetSessionSummaryRepository.CountAsync(p => p.Id == summary.Id && p.SectorMeetSessionId == input.Id) > 0)
                        {
                            var dbSectorMeetSessionSummary = await _sectorMeetSessionSummaryRepository.GetAsync(summary.Id);

                            dbSectorMeetSessionSummary.Description = summary.Description;
                            dbSectorMeetSessionSummary.SectorMeetSessionLeaderId = dbSectorMeetSessionLeader == null ? (int?)null : dbSectorMeetSessionLeader.Id;
                            dbSectorMeetSessionSummary.Index = index;

                            await _sectorMeetSessionSummaryRepository.UpdateAsync(dbSectorMeetSessionSummary);
                        }
                    }
                    else
                    {
                        input.Summaries.Add(new SectorMeetSessionSummary()
                        {
                            Description = summary.Description,
                            SectorMeetSessionLeaderId = dbSectorMeetSessionLeader == null ? (int?)null : dbSectorMeetSessionLeader.Id,
                            Index = index
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var leader in leaders)
            {
                if (leader.Remove)
                {
                    if (leader.Id > 0 && input.Id > 0 && await _sectorMeetSessionLeaderRepository.CountAsync(p => p.Id == leader.Id && p.SectorMeetSessionId == input.Id) > 0)
                    {
                        await _sectorMeetSessionLeaderRepository.DeleteAsync(leader.Id);
                    }
                }
                else
                {
                    if (leader.Teams == null)
                        leader.Teams = new List<SectorMeetSessionTeamRelationDto>();

                    DirectoryGovernment dbDirectoryGovernment = null;
                    DirectoryIndustry dbDirectoryIndustry = null;

                    if(leader.Type == SectorMeetSessionEntityType.ESTATAL_ENTITY)
                    {
                        leader.Role = null;
                        dbDirectoryGovernment = _directoryGovernmentRepository
                            .GetAll()
                            .Where(p => p.Id == leader.DirectoryGovernment.Id)
                            .FirstOrDefault();
                    }

                    if (leader.Type == SectorMeetSessionEntityType.COMPANY)
                    {
                        leader.Role = null;
                        dbDirectoryIndustry = _directoryIndustryRepository
                          .GetAll()
                          .Where(p => p.Id == leader.DirectoryIndustry.Id)
                          .FirstOrDefault();
                    }

                    if (leader.Type == SectorMeetSessionEntityType.CIVIL_SOCIETY || leader.Type == SectorMeetSessionEntityType.OTHER)
                    {
                        leader.Entity = null;
                        leader.Role.IsValidOrException("Aviso", leader.Type == SectorMeetSessionEntityType.CIVIL_SOCIETY ?
                            "El nombre de la institución de la Sociedad Civil de los integrantes es obligatorio" :
                            "El nombre de la institución de los integrantes es obligatorio");
                        leader.Role.VerifyTableColumn(SectorMeetSessionLeaderConsts.RoleMinLength,
                            SectorMeetSessionLeaderConsts.RoleMaxLength,
                            DefaultTitleMessage,
                            leader.Type == SectorMeetSessionEntityType.CIVIL_SOCIETY ?
                            $"El nombre de la institución de la Sociedad Civil de los integrantes no debe exceder los {SectorMeetSessionLeaderConsts.RoleMaxLength} caracteres" :
                            $"El nombre de la institución de los integrantes no debe exceder los {SectorMeetSessionLeaderConsts.RoleMaxLength} caracteres");
                    }
                    else
                    {
                        leader.Entity.IsValidOrException("Aviso", "La descripción del Órgano/Área/Etc de los integrantes es obligatorio");
                        leader.Entity.VerifyTableColumn(SectorMeetSessionLeaderConsts.EntityMinLength,
                            SectorMeetSessionLeaderConsts.EntityMaxLength,
                            DefaultTitleMessage,
                            $"La descripción del Órgano/Área/Etc de los integrantes no debe exceder los " +
                            $"{SectorMeetSessionLeaderConsts.EntityMaxLength} caracteres");
                    }

                    foreach (var team in leader.Teams)
                    {
                        team.Document.VerifyTableColumn(SectorMeetSessionTeamConsts.DocumentMinLength,
                            SectorMeetSessionTeamConsts.DocumentMaxLength,
                            "Aviso",
                            $"El DNI de los participantes no debe exceder los {SectorMeetSessionTeamConsts.DocumentMaxLength} caracteres");

                        team.Name.IsValidOrException("Aviso", "El nombre de los participantes es obligatorio");
                        team.Name.VerifyTableColumn(SectorMeetSessionTeamConsts.NameMinLength,
                            SectorMeetSessionTeamConsts.NameMaxLength,
                            "Aviso",
                            $"El nombre de los participantes no debe exceder los {SectorMeetSessionTeamConsts.NameMaxLength} caracteres");
                        team.Name = team.Name.Trim().ToUpper();

                        team.Surname.IsValidOrException("Aviso", "El apellido paterno de los participantes es obligatorio");
                        team.Surname.VerifyTableColumn(SectorMeetSessionTeamConsts.NameMinLength,
                            SectorMeetSessionTeamConsts.NameMaxLength,
                            "Aviso",
                            $"El apellido paterno de los participantes no debe exceder los {SectorMeetSessionTeamConsts.NameMaxLength} caracteres");
                        team.Surname = team.Surname.Trim().ToUpper();

                        team.SecondSurname.VerifyTableColumn(SectorMeetSessionTeamConsts.SecondSurnameMinLength,
                            SectorMeetSessionTeamConsts.SecondSurnameMaxLength,
                            "Aviso",
                            $"El apellido materno de los participantes no debe exceder los {SectorMeetSessionTeamConsts.SecondSurnameMaxLength} caracteres");
                        team.SecondSurname = team.SecondSurname?.Trim().ToUpper();

                        team.Job.IsValidOrException("Aviso", "El cargo de los participantes es obligatorio");
                        team.Job.VerifyTableColumn(SectorMeetSessionTeamConsts.JobMinLength,
                            SectorMeetSessionTeamConsts.JobMaxLength,
                            "Aviso",
                            $"El cargo de los participantes no debe exceder los {SectorMeetSessionTeamConsts.JobMaxLength} caracteres");
                        team.Job = team.Job.Trim().ToUpper();

                        team.EmailAddress.VerifyTableColumn(SectorMeetSessionTeamConsts.EmailAddressMinLength,
                            SectorMeetSessionTeamConsts.EmailAddressMaxLength,
                            "Aviso",
                            $"El correo electrónico de los participantes no debe exceder los {SectorMeetSessionTeamConsts.EmailAddressMaxLength} caracteres");
                        team.PhoneNumber.VerifyTableColumn(SectorMeetSessionTeamConsts.PhoneNumberMinLength,
                            SectorMeetSessionTeamConsts.PhoneNumberMaxLength,
                            "Aviso",
                            $"El número de teléfono de los participantes no debe exceder los {SectorMeetSessionTeamConsts.PhoneNumberMaxLength} caracteres");
                    }

                    if (leader.Id > 0)
                    {
                        if (await _sectorMeetSessionLeaderRepository.CountAsync(p => p.Id == leader.Id && p.SectorMeetSessionId == input.Id) > 0)
                        {
                            var dbSectorMeetSessionLeader = await _sectorMeetSessionLeaderRepository.GetAsync(leader.Id);

                            dbSectorMeetSessionLeader.Type = leader.Type;
                            dbSectorMeetSessionLeader.DirectoryGovernment = dbDirectoryGovernment;
                            dbSectorMeetSessionLeader.DirectoryGovernmentId = dbDirectoryGovernment == null ? (int?)null : dbDirectoryGovernment.Id;
                            dbSectorMeetSessionLeader.DirectoryIndustry = dbDirectoryIndustry;
                            dbSectorMeetSessionLeader.DirectoryIndustryId = dbDirectoryIndustry == null ? (int?)null : dbDirectoryIndustry.Id;
                            dbSectorMeetSessionLeader.Entity = leader.Entity;
                            dbSectorMeetSessionLeader.Role = leader.Role;
                            dbSectorMeetSessionLeader.Index = index;
                            dbSectorMeetSessionLeader.Teams = new List<SectorMeetSessionTeam>();

                            foreach (var team in leader.Teams)
                            {
                                if (leader.Remove)
                                {
                                    if (leader.Id > 0 && input.Id > 0 && await _sectorMeetSessionTeamRepository.CountAsync(p => p.Id == leader.Id && p.SectorMeetSessionLeaderId == leader.Id) > 0)
                                    {
                                        await _sectorMeetSessionTeamRepository.DeleteAsync(team.Id);
                                    }
                                }
                                else
                                {
                                    if (team.Id > 0)
                                    {
                                        var dbSectorMeetSessionTeam = await _sectorMeetSessionTeamRepository.GetAsync(team.Id);

                                        dbSectorMeetSessionTeam.Document = team.Document;
                                        dbSectorMeetSessionTeam.Name = team.Name;
                                        dbSectorMeetSessionTeam.Surname = team.Surname;
                                        dbSectorMeetSessionTeam.SecondSurname = team.SecondSurname;
                                        dbSectorMeetSessionTeam.Job = team.Job;
                                        dbSectorMeetSessionTeam.EmailAddress = team.EmailAddress;
                                        dbSectorMeetSessionTeam.PhoneNumber = team.PhoneNumber;

                                        await _sectorMeetSessionTeamRepository.UpdateAsync(dbSectorMeetSessionTeam);
                                    }
                                    else
                                    {
                                        dbSectorMeetSessionLeader.Teams.Add(new SectorMeetSessionTeam()
                                        {
                                            Document = team.Document,
                                            Name = team.Name,
                                            Surname = team.Surname,
                                            SecondSurname = team.SecondSurname,
                                            Job = team.Job,
                                            EmailAddress = team.EmailAddress,
                                            PhoneNumber = team.PhoneNumber
                                        });
                                    }
                                }
                            }

                            await _sectorMeetSessionLeaderRepository.UpdateAsync(dbSectorMeetSessionLeader);
                        }
                    }
                    else
                    {
                        var dbSectorMeetSessionLeader = new SectorMeetSessionLeader()
                        {
                            Type = leader.Type,
                            DirectoryGovernment = dbDirectoryGovernment,
                            DirectoryGovernmentId = dbDirectoryGovernment == null ? (int?)null : dbDirectoryGovernment.Id,
                            DirectoryIndustry = dbDirectoryIndustry,
                            DirectoryIndustryId = dbDirectoryIndustry == null ? (int?)null : dbDirectoryIndustry.Id,
                            Entity = leader.Entity,
                            Role = leader.Role,
                            Index = index,
                            Teams = new List<SectorMeetSessionTeam>()
                        };

                        foreach (var team in leader.Teams)
                        {
                            dbSectorMeetSessionLeader.Teams.Add(new SectorMeetSessionTeam()
                            {
                                Document = team.Document,
                                Name = team.Name,
                                Surname = team.Surname,
                                SecondSurname = team.SecondSurname,
                                Job = team.Job,
                                EmailAddress = team.EmailAddress,
                                PhoneNumber = team.PhoneNumber
                            });

                        }
                        
                        input.Leaders.Add(dbSectorMeetSessionLeader);
                    }
                }

                index++;
            }

            foreach (var resource in resources)
            {
                if (resource.Remove)
                {
                    if (resource.Id > 0 && input.Id > 0 && await _sectorMeetSessionResourceRepository.CountAsync(p => p.Id == resource.Id && p.SectorMeetSessionId == input.Id) > 0)
                    {
                        await _sectorMeetSessionResourceRepository.DeleteAsync(resource.Id);
                    }
                }
            }

            foreach (var uploadFile in uploadFiles)
            {
                var dbResource = ObjectMapper.Map<SectorMeetSessionResource>(ResourceManager.Create(
                    resource: ObjectMapper.Map<UploadResourceInputDto>(uploadFile),
                    section: ResourceConsts.SectorMeetSession
                ));

                input.Resources.Add(dbResource);
            }

            foreach (var uploadFile in uploadFilesPDF)
            {
                var dbResource = ObjectMapper.Map<SectorMeetSessionFile>(ResourceManager.Create(
                    resource: ObjectMapper.Map<UploadResourceInputDto>(uploadFile),
                    section: ResourceConsts.SectorMeetSession
                ));

                input.ResourcesFiles.Add(dbResource);
            }

            return input;
        }
    }
}
