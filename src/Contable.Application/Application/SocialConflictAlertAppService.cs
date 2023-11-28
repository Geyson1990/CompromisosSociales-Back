using Abp.Domain.Repositories;
using Abp.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contable.Application.Extensions;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Collections.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using System;
using System.Linq.Expressions;
using Contable.Application.SocialConflictAlerts.Dto;
using Contable.Application.SocialConflictAlerts;
using Abp.BackgroundJobs;
using Contable.Gdpr;
using Abp.Runtime.Session;
using Contable.Authorization.Users;
using Contable.Application.Uploaders.Dto;
using Contable.Net.Emailing;
using System.ComponentModel.DataAnnotations;
using Contable.Application.Reports;
using Microsoft.AspNetCore.Mvc;
using Contable.Application.Reports.Dto;
using Contable.Configuration;
using Contable.Application.Exporting;
using Contable.Application.Exporting.Dto;
using Contable.Application.SocialConflicts.Dto;
using Contable.Dto;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert)]
    public class SocialConflictAlertAppService : ContableAppServiceBase, ISocialConflictAlertAppService
    {
        private readonly IRepository<SocialConflictAlert> _socialConflictAlertRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SocialConflictAlertLocation> _socialConflictAlertLocationRepository;
        private readonly IRepository<SocialConflictAlertRisk> _socialConflictAlertRiskRepository;
        private readonly IRepository<SocialConflictAlertSector> _socialConflictAlertSectorRepository;
        private readonly IRepository<SocialConflictAlertState> _socialConflictAlertStateRepository;
        private readonly IRepository<SocialConflictAlertSeal> _socialConflictAlertSealRepository;
        private readonly IRepository<SocialConflictActor> _socialConflictAlertActorRepository;
        private readonly IRepository<SocialConflictAlertHistory> _socialConflictAlertHistoryRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<TerritorialUnitDepartment> _territorialUnitDepartmentRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<AlertRisk> _alertRiskRepository;
        private readonly IRepository<AlertSector> _alertSectorRepository;
        private readonly IRepository<AlertSeal> _alertSealRepository;
        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<ActorType> _actorTypeRepository;
        private readonly IRepository<ActorMovement> _actorMovementRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Typology> _typologyRepository;
        private readonly IRepository<SubTypology> _subTypologyRepository;
        private readonly IRepository<AlertDemand> _alertDemandRepository;
        private readonly IRepository<AlertResponsible> _alertResponsibleRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<SocialConflictAlertResource> _socialConflictAlertResourceRepository;
        private readonly ISocialConflictAlertExcelExporter _socialConflictAlertExcelExporter;
        private readonly IAppEmailSender _appEmailSender;

        public SocialConflictAlertAppService(
            IRepository<SocialConflictAlert> socialConflictAlertRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<SocialConflictAlertLocation> socialConflictAlertLocationRepository,
            IRepository<SocialConflictAlertRisk> socialConflictAlertRiskRepository,
            IRepository<SocialConflictAlertSector> socialConflictAlertSectorRepository,
            IRepository<SocialConflictAlertState> socialConflictAlertStateRepository,
            IRepository<SocialConflictAlertSeal> socialConflictAlertSealRepository,
            IRepository<SocialConflictActor> socialConflictAlertActorRepository,
            IRepository<SocialConflictAlertHistory> socialConflictAlertHistoryRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<TerritorialUnitDepartment> territorialUnitDepartmentRepository,
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<District> districtRepository,
            IRepository<Region> regionRepository,
            IRepository<AlertRisk> alertRiskRepository,
            IRepository<AlertSector> alertSectorRepository,
            IRepository<AlertSeal> alertSealRepository,
            IRepository<Actor> actorRepository,
            IRepository<ActorType> actorTypeRepository,
            IRepository<ActorMovement> actorMovementRepository,
            IRepository<Person> personRepository,
            IRepository<Typology> typologyRepository,
            IRepository<SubTypology> subTypologyRepository,
            IRepository<AlertDemand> alertDemandRepository,
            IRepository<AlertResponsible> alertResponsibleRepository,
            IRepository<User, long> userRepository,
            IRepository<SocialConflictAlertResource> socialConflictAlertResourceRepository,
            ISocialConflictAlertExcelExporter socialConflictAlertExcelExporter,
            IAppEmailSender appEmailSender)
        {
            _socialConflictAlertRepository = socialConflictAlertRepository;
            _socialConflictRepository = socialConflictRepository;
            _socialConflictAlertLocationRepository = socialConflictAlertLocationRepository;
            _socialConflictAlertRiskRepository = socialConflictAlertRiskRepository;
            _socialConflictAlertSectorRepository = socialConflictAlertSectorRepository;
            _socialConflictAlertStateRepository = socialConflictAlertStateRepository;
            _socialConflictAlertSealRepository = socialConflictAlertSealRepository;
            _socialConflictAlertActorRepository = socialConflictAlertActorRepository;
            _socialConflictAlertHistoryRepository = socialConflictAlertHistoryRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _territorialUnitDepartmentRepository = territorialUnitDepartmentRepository;
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _regionRepository = regionRepository;
            _alertRiskRepository = alertRiskRepository;
            _alertSectorRepository = alertSectorRepository;
            _alertSealRepository = alertSealRepository;
            _actorTypeRepository = actorTypeRepository;
            _actorMovementRepository = actorMovementRepository;
            _personRepository = personRepository;
            _typologyRepository = typologyRepository;
            _subTypologyRepository = subTypologyRepository;
            _alertDemandRepository = alertDemandRepository;
            _alertResponsibleRepository = alertResponsibleRepository;
            _userRepository = userRepository;
            _socialConflictAlertResourceRepository = socialConflictAlertResourceRepository;
            _socialConflictAlertExcelExporter = socialConflictAlertExcelExporter;
            _appEmailSender = appEmailSender;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_Create)]
        public async Task<EntityDto> Create(SocialConflictAlertCreateDto input)
        {
            var socialConflictAlertId = await _socialConflictAlertRepository.InsertAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map<SocialConflictAlert>(input),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                territorialUnitId: input.TerritorialUnit == null ? -1 : input.TerritorialUnit.Id,
                alertDemandId: input.AlertDemand == null ? -1 : input.AlertDemand.Id,
                alertResponsibleId: input.AlertResponsible == null ? -1 : input.AlertResponsible.Id,
                typologyId: input.Typology == null ? -1 : input.Typology.Id,
                subTypologyId: input.SubTypology == null ? -1 : input.SubTypology.Id,
                analystId: input.Analyst == null ? -1 : input.Analyst.Id,
                managerId: input.Manager == null ? -1 : input.Manager.Id,
                coordinatorId: input.Coordinator == null ? -1 : input.Coordinator.Id,
                locations: input.Locations,
                actors: input.Actors,
                risks: input.Risks,
                sectors: input.Sectors,
                states: input.States,
                seals: input.Seals,
                resources: input.Resources,
                uploads: input.UploadFiles));

            await CurrentUnitOfWork.SaveChangesAsync();

            await FunctionManager.CallCreateSocialConflictAlertCodeProcess(socialConflictAlertId);
            await FunctionManager.CallSocialConflictAlertStateProcess(socialConflictAlertId);

            return new EntityDto(socialConflictAlertId);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert)]
        public async Task<SocialConflictAlertGetDataDto> Get(NullableIdDto input)
        {
            var output = new SocialConflictAlertGetDataDto
            {
                SocialConflictAlert = new SocialConflictAlertGetDto()
            };

            if (input.Id.HasValue)
            {
                VerifyCount(await _socialConflictAlertRepository.CountAsync(p => p.Id == input.Id));

                var socialConflictAlert = _socialConflictAlertRepository
                    .GetAll()
                    .Include(p => p.SocialConflict)
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Analyst)
                    .Include(p => p.Manager)
                    .Include(p => p.Coordinator)
                    .Include(p => p.Typology)
                    .Include(p => p.SubTypology)
                    .Include(p => p.AlertDemand)
                    .Include(p => p.AlertResponsible)
                    .Where(p => p.Id == input.Id)
                    .First();

                output.SocialConflictAlert = ObjectMapper.Map<SocialConflictAlertGetDto>(socialConflictAlert);

                output.SocialConflictAlert.CreatorUser = socialConflictAlert.CreatorUserId.HasValue == false ? null : ObjectMapper.Map<SocialConflictAlertUserDto>(_userRepository
                    .GetAll()
                    .Where(p => p.Id == socialConflictAlert.CreatorUserId.Value)
                    .FirstOrDefault());

                output.SocialConflictAlert.EditionUser = socialConflictAlert.LastModifierUserId.HasValue == false ? null : ObjectMapper.Map<SocialConflictAlertUserDto>(_userRepository
                    .GetAll()
                    .Where(p => p.Id == socialConflictAlert.LastModifierUserId.Value)
                    .FirstOrDefault());

                output.SocialConflictAlert.Locations = ObjectMapper.Map<List<SocialConflictAlertLocationDto>>(_socialConflictAlertLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflictAlert.Id == input.Id)
                    .ToList());

                output.SocialConflictAlert.Risks = ObjectMapper.Map<List<SocialConflictAlertRiskLocationDto>>(_socialConflictAlertRiskRepository
                    .GetAll()
                    .Include(p => p.AlertRisk)
                    .Where(p => p.SocialConflictAlertId == input.Id)
                    .ToList());
                
                output.SocialConflictAlert.Sectors = ObjectMapper.Map<List<SocialConflictAlertSectorLocationDto>>(_socialConflictAlertSectorRepository
                    .GetAll()
                    .Include(p => p.AlertSector)
                    .Where(p => p.SocialConflictAlertId == input.Id)
                    .ToList());

                output.SocialConflictAlert.States = ObjectMapper.Map<List<SocialConflictAlertStateLocationDto>>(_socialConflictAlertStateRepository
                    .GetAll()
                    .Where(p => p.SocialConflictAlertId == input.Id)
                    .ToList());

                output.SocialConflictAlert.Seals = ObjectMapper.Map<List<SocialConflictAlertSealLocationDto>>(_socialConflictAlertSealRepository
                    .GetAll()
                    .Include(p => p.AlertSeal)
                    .Where(p => p.SocialConflictAlertId == input.Id)
                    .ToList());

                output.SocialConflictAlert.Actors = ObjectMapper.Map<List<SocialConflictAlertActorLocationDto>>(_socialConflictAlertActorRepository
                    .GetAll()
                    .Include(p => p.ActorType)
                    .Include(p => p.ActorMovement)
                    .Where(p => p.SocialConflictAlertId == input.Id)
                    .ToList());

                output.SocialConflictAlert.Resources = ObjectMapper.Map<List<SocialConflictAlertResourceDto>>(_socialConflictAlertResourceRepository
                    .GetAll()
                    .Where(p => p.SocialConflictAlertId == input.Id)
                    .ToList());
            }

            output.Departments = ObjectMapper.Map<List<SocialConflictAlertDepartmentDto>>(_territorialUnitDepartmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnit)
                .Include(p => p.Department)
                .ThenInclude(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .Where(p => p.TerritorialUnit != null && p.Department != null)
                .OrderBy(p => p.Department.Name)
                .ToList());

            output.TerritorialUnits = ObjectMapper.Map<List<SocialConflictAlertTerritorialUnitDto>>(_territorialUnitRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .ToList());

            output.Risks = ObjectMapper.Map<List<SocialConflictAlertRiskDto>>(_alertRiskRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Index)
                .ToList());

            output.Sectors = ObjectMapper.Map<List<SocialConflictAlertSectorDto>>(_alertSectorRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Index)
                .ToList());

            output.Seals = ObjectMapper.Map<List<SocialConflictAlertSealDto>>(_alertSealRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList());

            output.ActorTypes = ObjectMapper.Map<List<SocialConflictAlertActorTypeDto>>(_actorTypeRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList());

            output.ActorMovements = ObjectMapper.Map<List<SocialConflictAlertActorMovementDto>>(_actorMovementRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList());

            output.Persons = ObjectMapper.Map<List<SocialConflictAlertPersonDto>>(_personRepository
               .GetAll()
               .Where(p => p.Enabled && p.Type != PersonType.None)
               .OrderBy(p => p.Name)
               .ToList());

            output.Typologies = ObjectMapper.Map<List<SocialConflictAlertTypologyDto>>(_typologyRepository
               .GetAll()
               .Include(p => p.SubTypologies)
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList()).Select(p => {
                   p.SubTypologies = p.SubTypologies.Where(p => p.Enabled).ToList();
                   return p;
               }).ToList();

            output.Demands = ObjectMapper.Map<List<SocialConflictAlertDemandDto>>(_alertDemandRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList());

            output.Responsibles = ObjectMapper.Map<List<SocialConflictAlertResponsibleDto>>(_alertResponsibleRepository
               .GetAll()
               .Where(p => p.Enabled)
               .OrderBy(p => p.Name)
               .ToList());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert)]
        public async Task<PagedResultDto<SocialConflictAlertGetAllDto>> GetAll(SocialConflictAlertGetAllInputDto input)
        {
            var query = _socialConflictAlertRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.TerritorialUnit)
                .Include(p => p.Locations)
                .ThenInclude(p => p.Department)
                .Include(p => p.Locations)
                .ThenInclude(p => p.Province)
                .Include(p => p.Locations)
                .ThenInclude(p => p.District)
                .Include(p => p.AlertResponsible)
                .Include(p => p.Analyst)
                .Include(p => p.Actors)
                .ThenInclude(p => p.ActorType)
                .Include(p => p.Actors)
                .ThenInclude(p => p.ActorMovement)
                .WhereIf(input.TerritorialUnitId.HasValue, p => p.TerritorialUnitId == input.TerritorialUnitId || p.Locations.Any(p => p.TerritorialUnitId == input.TerritorialUnitId))
                .WhereIf(input.DepartmentId.HasValue, p => p.Locations.Any(p => p.DepartmentId == input.DepartmentId))
                .WhereIf(input.ProvinceId.HasValue, p => p.Locations.Any(p => p.ProvinceId == input.ProvinceId))
                .WhereIf(input.DistrictId.HasValue, p => p.Locations.Any(p => p.DistrictId == input.DistrictId))
                .WhereIf(input.PersonId.HasValue, p => p.AnalystId == input.PersonId)
                .WhereIf(input.ResponsibleId.HasValue, p => p.AlertResponsibleId == input.ResponsibleId)
                .WhereIf(input.TypologyId.HasValue, p => p.TypologyId == input.TypologyId)
                .WhereIf(input.RiskId.HasValue, p => p.Risks.Any(d => d.Id == p.LastAlertRiskId.Value && d.AlertRiskId == input.RiskId))
                .WhereIf(input.SealId.HasValue, p => p.LastSealId == input.SealId)
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.AlertTime >= input.StartTime.Value && p.AlertTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.SocialConflictAlertCode.SplitByLike(), nameof(SocialConflictAlert.Code))
                .LikeAllBidirectional(input.SocialConflictAlertDescription.SplitByLike(), nameof(SocialConflictAlert.Description))
                .LikeAllBidirectional(input.SocialConflictAlertInformation.SplitByLike(), nameof(SocialConflictAlert.Information));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);
            var result = ObjectMapper.Map<List<SocialConflictAlertGetAllDto>>(output);

            foreach (var item in result)
            {
                item.Risk = ObjectMapper.Map<SocialConflictAlertRiskLocationGetAllDto>(_socialConflictAlertRiskRepository
                    .GetAll()
                    .Include(p => p.AlertRisk)
                    .Where(p => p.SocialConflictAlertId == item.Id)
                    .OrderByDescending(p => p.RiskTime)
                    .ThenByDescending(p => p.CreationTime)
                    .FirstOrDefault());
            }

            return new PagedResultDto<SocialConflictAlertGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_Edit)]
        public async Task Update(SocialConflictAlertUpdateDto input)
        {
            VerifyCount(await _socialConflictAlertRepository.CountAsync(p => p.Id == input.Id));

            var socialConflictAlertId = await _socialConflictAlertRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map(input, await _socialConflictAlertRepository.GetAsync(input.Id)),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                territorialUnitId: input.TerritorialUnit == null ? -1 : input.TerritorialUnit.Id,
                alertDemandId: input.AlertDemand == null ? -1 : input.AlertDemand.Id,
                alertResponsibleId: input.AlertResponsible == null ? -1 : input.AlertResponsible.Id,
                typologyId: input.Typology == null ? -1 : input.Typology.Id,
                subTypologyId: input.SubTypology == null ? -1 : input.SubTypology.Id,
                analystId: input.Analyst == null ? -1 : input.Analyst.Id,
                managerId: input.Manager == null ? -1 : input.Manager.Id,
                coordinatorId: input.Coordinator == null ? -1 : input.Coordinator.Id,
                locations: input.Locations,
                actors: input.Actors,
                risks: input.Risks,
                sectors: input.Sectors,
                states: input.States,
                seals: input.Seals,
                resources: input.Resources,
                uploads: input.UploadFiles));

            await CurrentUnitOfWork.SaveChangesAsync();

            await FunctionManager.CallSocialConflictAlertStateProcess(socialConflictAlertId);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_Send)]
        public async Task<SocialConflictAlertEmailConfigurationDto> GetEmailConfiguration(EntityDto input)
        {
            VerifyCount(await _socialConflictAlertRepository.CountAsync(p => p.Id == input.Id));

            var alert = _socialConflictAlertRepository
                .GetAll()
                .Where(p => p.Id == input.Id)
                .First();

            var subject = string.Format(await SettingManager
                    .GetSettingValueForTenantAsync(
                        name: AppSettings.Template.SocialConflictAlertSubject, 
                        tenantId: AbpSession.GetTenantId()), 
                alert.Code, 
                alert.Description)
                .Trim();

            if ((subject ?? "").Length >= 256)
                subject = subject.Substring(0, 256).Trim();

            return new SocialConflictAlertEmailConfigurationDto()
            {
                Subject = subject,
                Template = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.SocialConflictAlertTemplate, AbpSession.GetTenantId())
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_Send)]
        public async Task SendAlert(SocialConflictAlertSendNotificationDto input) 
        {
            VerifyCount(await _socialConflictAlertRepository.CountAsync(p => p.Id == input.Id));

            if (string.IsNullOrWhiteSpace(input.Subject))
                throw new UserFriendlyException("Aviso", "El asunto del correo electrónico es obligatorio");

            var alert = _socialConflictAlertRepository
                .GetAll()
                .Include(p => p.Resources)
                .Where(p => p.Id == input.Id)
                .First();

            var emailValidator = new EmailAddressAttribute();

            foreach(var emailAddress in input.To)
                if(emailValidator.IsValid(emailAddress) == false)
                    throw new UserFriendlyException("Aviso", $"La dirección de correo electrónico en destinatarios \"Para\": {emailAddress} es inválida");
            foreach (var emailAddress in input.Copy)
                if (emailValidator.IsValid(emailAddress) == false)
                    throw new UserFriendlyException("Aviso", $"La dirección de correo electrónico en destinatarios \"Copia a\": {emailAddress} es inválida");

            var attachments = new List<EmailAttachment>();

            var request = await ReportManager.Create(new JasperReportRequest()
            {
                Name = ReportNames.SocialConflictAlert,
                Type = ReportManager.GetType(ReportType.PDF),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "AlertId",
                        Value = $"{alert.Id}"
                    }
                }
            });

            if (request.Success == false)
                throw new UserFriendlyException(request.Exception.Error.Title, request.Exception.Error.Message);

            attachments.Add(new EmailAttachment()
            {
                Name = ReportManager.CreateAlertReportName(alert, ReportType.PDF),
                Content = request.Report
            });

            var index = 1;

            foreach (var resource in alert.Resources)
            {
                var resourceContent = ResourceManager.Get(
                    commonFolder: resource.CommonFolder,
                    resourceFolder: resource.ResourceFolder,
                    sectionFolder: resource.SectionFolder,
                    fileName: resource.FileName);

                if (resourceContent != null)
                {
                    attachments.Add(new EmailAttachment()
                    {
                        Name = ReportManager.CreateAlertResourceName(alert, resource.Extension, index),
                        Content = resourceContent
                    });

                    index++;
                }
            }

            await _socialConflictAlertHistoryRepository.InsertAsync(new SocialConflictAlertHistory()
            {
                SocialConflictAlertId = alert.Id,
                SocialConflictAlert = alert,
                Code = alert.Code,
                Subject = input.Subject,
                Template = input.Template,
                To = string.Join(";", input.To),
                Copy = string.Join(";", input.Copy),
                Files = string.Join(";", attachments.Select(p => p.Name))
            });

            await _appEmailSender.SendEmail(
                to: input.To.ToArray(),
                cc: input.Copy.ToArray(),
                subject: input.Subject,
                body: input.Template, 
                attachments: attachments.ToArray());
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_Send)]
        public async Task<FileDto> GetMatrizToExcel(SocialConflictAlertGetAllInputDto input)
        {
            var query = _socialConflictAlertRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.TerritorialUnit)
                .Include(p => p.AlertDemand)
                .Include(p => p.Typology)
                .Include(p => p.SubTypology)
                .Include(p => p.Analyst)
                .Include(p => p.Coordinator)
                .Include(p => p.Manager)
                .Include(p => p.AlertResponsible)
                .WhereIf(input.TerritorialUnitId.HasValue, p => p.TerritorialUnitId == input.TerritorialUnitId || p.Locations.Any(p => p.TerritorialUnitId == input.TerritorialUnitId))
                .WhereIf(input.DepartmentId.HasValue, p => p.Locations.Any(p => p.DepartmentId == input.DepartmentId))
                .WhereIf(input.ProvinceId.HasValue, p => p.Locations.Any(p => p.ProvinceId == input.ProvinceId))
                .WhereIf(input.DistrictId.HasValue, p => p.Locations.Any(p => p.DistrictId == input.DistrictId))
                .WhereIf(input.PersonId.HasValue, p => p.AnalystId == input.PersonId)
                .WhereIf(input.ResponsibleId.HasValue, p => p.AlertResponsibleId == input.ResponsibleId)
                .WhereIf(input.TypologyId.HasValue, p => p.TypologyId == input.TypologyId)
                .WhereIf(input.RiskId.HasValue, p => p.Risks.Any(d => d.Id == p.LastAlertRiskId.Value && d.AlertRiskId == input.RiskId))
                .WhereIf(input.SealId.HasValue, p => p.LastSealId == input.SealId)
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.AlertTime >= input.StartTime.Value && p.AlertTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.SocialConflictAlertCode.SplitByLike(), nameof(SocialConflictAlert.Code))
                .LikeAllBidirectional(input.SocialConflictAlertDescription.SplitByLike(), nameof(SocialConflictAlert.Description))
                .LikeAllBidirectional(input.SocialConflictAlertInformation.SplitByLike(), nameof(SocialConflictAlert.Information));

            var data = new List<SocialConflictAlertMatrizExportDto>();
            var result = await query.OrderBy(input.Sorting).ToListAsync();

            foreach(var dbSocialConflictAlert in result)
            {
                var item = new SocialConflictAlertMatrizExportDto();

                item.AlertCode = dbSocialConflictAlert.Code;
                item.AlertName = dbSocialConflictAlert.Description;
                item.AlertTime = dbSocialConflictAlert.AlertTime;

                item.CaseName = dbSocialConflictAlert.SocialConflict == null ? "" : dbSocialConflictAlert.SocialConflict.CaseName ?? "";

                item.Information = dbSocialConflictAlert.Information ?? "";
                item.TerritorialUnit = dbSocialConflictAlert.TerritorialUnit == null ? "" : dbSocialConflictAlert.TerritorialUnit.Name ?? "";

                item.DemandType = dbSocialConflictAlert.AlertDemand == null ? "" : dbSocialConflictAlert.AlertDemand.Name ?? "";
                item.Demand = dbSocialConflictAlert.Demand ?? "";

                item.TypologyDescription = dbSocialConflictAlert.Typology != null ? dbSocialConflictAlert.Typology.Name : "";
                item.SubTypologyDescription = dbSocialConflictAlert.SubTypology != null ? dbSocialConflictAlert.SubTypology.Name : "";

                item.AlertResponsible = dbSocialConflictAlert.AlertResponsible != null ? dbSocialConflictAlert.AlertResponsible.Name : "";
                item.CoordinatorName = dbSocialConflictAlert.Coordinator != null ? dbSocialConflictAlert.Coordinator.Name : "";
                item.ManagerName = dbSocialConflictAlert.Manager != null ? dbSocialConflictAlert.Manager.Name : "";
                item.AnalystName = dbSocialConflictAlert.Analyst != null ? dbSocialConflictAlert.Analyst.Name : "";

                item.Actions = dbSocialConflictAlert.Actions ?? "";
                item.Recommendations = dbSocialConflictAlert.Recommendations ?? "";
                item.AditionalInformation = dbSocialConflictAlert.AditionalInformation ?? "";
                item.Source = dbSocialConflictAlert.Source ?? "";
                item.SourceType = dbSocialConflictAlert.SourceType ?? "";
                item.Link = dbSocialConflictAlert.Link ?? "";

                if (dbSocialConflictAlert.LastAlertRiskId.HasValue)
                {
                    var lastSocialConflictRisk = _socialConflictAlertRiskRepository
                        .GetAll()
                        .Include(p => p.AlertRisk)
                        .Where(p => p.Id == dbSocialConflictAlert.LastAlertRiskId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictRisk != null && lastSocialConflictRisk.AlertRisk != null)
                    {
                        item.LastCaseRisk = lastSocialConflictRisk.AlertRisk.Name ?? "";
                        item.LastCaseRiskDescription = lastSocialConflictRisk.Description ?? "";
                        item.LastCaseRiskTime = lastSocialConflictRisk.RiskTime;
                    }
                }

                var locations = _socialConflictAlertLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Include(p => p.Region)
                    .Where(p => p.SocialConflictAlert.Id == dbSocialConflictAlert.Id)
                    .ToList();

                if (locations.Count > 0)
                {
                    var textTerritorialUnits = new List<string>();

                    if (dbSocialConflictAlert.TerritorialUnit != null)
                        textTerritorialUnits.Add(dbSocialConflictAlert.TerritorialUnit.Name);

                    item.TerritorialUnit = textTerritorialUnits
                        .Union(locations.Where(p => p.TerritorialUnit != null).Select(p => p.TerritorialUnit.Name))
                        .Where(p => !string.IsNullOrWhiteSpace(p))
                        .Distinct()
                        .JoinAsString(", ");

                    item.Departments = locations.Where(p => p.Department != null).Select(p => p.Department.Name).Distinct().JoinAsString(", ");
                    item.Provinces = locations.Where(p => p.Province != null).Select(p => p.Province.Name).Distinct().JoinAsString(", ");
                    item.Districts = locations.Where(p => p.District != null).Select(p => p.District.Name).Distinct().JoinAsString(", ");
                    item.Regions = locations.Where(p => p.Region != null).Select(p => p.Region.Name).Distinct().JoinAsString(", ");
                    item.Ubications = locations.Select(p => (p.Ubication ?? "").Trim()).Where(p => p != "").Distinct().JoinAsString(", ");
                }

                var actors = _socialConflictAlertActorRepository
                    .GetAll()
                    .Include(p => p.ActorMovement)
                    .Include(p => p.ActorType)
                    .Where(p => p.SocialConflictAlertId == dbSocialConflictAlert.Id)
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

                var attention = _socialConflictAlertSectorRepository
                    .GetAll()
                    .Include(p => p.AlertSector)
                    .Where(p => p.SocialConflictAlertId == dbSocialConflictAlert.Id)
                    .OrderBy(p => p.AlertSector.Index)
                    .ThenByDescending(p => p.SectorTime)
                    .ThenByDescending(p => p.CreationTime)
                    .FirstOrDefault();

                if(attention != null)
                {
                    item.Attention = attention.AlertSector == null ? "" : $"{attention.AlertSector.Index}. {attention.AlertSector.Name}";
                    item.AttentionDescription = attention.Description;
                    item.AttentionTime = attention.SectorTime;
                }

                if (dbSocialConflictAlert.LastStateId.HasValue)
                {
                    var lastSocialConflictAlertState = _socialConflictAlertStateRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictAlert.LastStateId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictAlertState != null)
                    {
                        item.LastStateDescription = lastSocialConflictAlertState.Description ?? "";
                        item.LastStateTime = lastSocialConflictAlertState.StateTime;
                    }
                }

                if (dbSocialConflictAlert.LastSealId.HasValue)
                {
                    var lastSocialConflictAlertSeal = _socialConflictAlertSealRepository
                        .GetAll()
                        .Include(p => p.AlertSeal)
                        .Where(p => p.Id == dbSocialConflictAlert.LastSealId.Value)
                        .FirstOrDefault();

                    if (lastSocialConflictAlertSeal != null)
                    {
                        item.LastSeal = lastSocialConflictAlertSeal.AlertSeal == null ? "" : lastSocialConflictAlertSeal.AlertSeal.Name ?? "";
                        item.LastSealDescription = lastSocialConflictAlertSeal.Description ?? "";
                        item.LastSealTime = lastSocialConflictAlertSeal.SealTime;
                    }
                }

                if (dbSocialConflictAlert.CreatorUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictAlert.CreatorUserId.Value)
                        .FirstOrDefault();

                    item.CreatorUser = user?.GetNameSurname();
                    item.CreationTime = dbSocialConflictAlert.CreationTime;
                }

                if (dbSocialConflictAlert.LastModifierUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == dbSocialConflictAlert.LastModifierUserId.Value)
                        .FirstOrDefault();

                    item.LastModificationUser = user?.GetNameSurname();
                    item.LastModificationTime = dbSocialConflictAlert.LastModificationTime;
                }

                data.Add(item);
            }

            return _socialConflictAlertExcelExporter.ExportMatrizToFile(data);
        }

        private async Task<SocialConflictAlert> ValidateEntity(
            SocialConflictAlert input,
            int socialConflictId,
            int territorialUnitId,
            int alertDemandId,
            int alertResponsibleId,
            int typologyId,
            int subTypologyId,
            int analystId,
            int managerId,
            int coordinatorId,
            List<SocialConflictAlertLocationDto> locations,
            List<SocialConflictAlertActorLocationDto> actors,
            List<SocialConflictAlertRiskLocationDto> risks,
            List<SocialConflictAlertSectorLocationDto> sectors,
            List<SocialConflictAlertStateLocationDto> states,
            List<SocialConflictAlertSealLocationDto> seals,
            List<SocialConflictAlertResourceDto> resources,
            List<UploadResourceInputDto> uploads)
        {
            input.Description.IsValidOrException(DefaultTitleMessage, "El nombre de la alerta es obligatoria");
            input.Description.VerifyTableColumn(SocialConflictAlertConsts.DescriptionMinLength, 
                SocialConflictAlertConsts.DescriptionMaxLength, 
                DefaultTitleMessage, $"El nombre del alerta " +
                $"no debe exceder los {SocialConflictAlertConsts.DescriptionMaxLength} caracteres");

            input.Information.VerifyTableColumn(SocialConflictAlertConsts.InformationMinLength,
                SocialConflictAlertConsts.InformationMaxLength,
                DefaultTitleMessage, $"La información principal del alerta " +
                $"no debe exceder los {SocialConflictAlertConsts.InformationMaxLength} caracteres");

            input.Locations = new List<SocialConflictAlertLocation>();
            input.Risks = new List<SocialConflictAlertRisk>();
            input.Sectors = new List<SocialConflictAlertSector>();
            input.States = new List<SocialConflictAlertState>();
            input.Seals = new List<SocialConflictAlertSeal>();
            input.Actors = new List<SocialConflictActor>();
            input.Resources = new List<SocialConflictAlertResource>();

            if (uploads.Count > 5)
                throw new UserFriendlyException("Aviso", "El límite de archivos adjuntos es de 5");

            if(input.Id > 0)
            {
                var currentUploadCount = await _socialConflictAlertResourceRepository.CountAsync(p => p.SocialConflictAlertId == input.Id);

                if((currentUploadCount + uploads.Count) > 5)
                    throw new UserFriendlyException("Aviso", $"El límite de archivos adjuntos es de 5, el uso actual son {currentUploadCount} archivos subidos");
            }

            if (socialConflictId > 0)
            {
                var socialConflict = _socialConflictRepository
                    .GetAll()
                    .Where(p => p.Id == socialConflictId)
                    .FirstOrDefault();

                if(socialConflict == null)
                    throw new UserFriendlyException(DefaultTitleMessage, $"El caso de conflictividad social {input.SocialConflict.CaseName} ya no existe o fue eliminado. Verifique la información antes de continuar");

                input.SocialConflictId = socialConflict.Id;
                input.SocialConflict = socialConflict;
            }  
            else
            {
                input.SocialConflictId = null;
                input.SocialConflict = null;
            }

            if (territorialUnitId > 0)
            {
                if (await _territorialUnitRepository.CountAsync(p => p.Id == territorialUnitId) == 0)
                    throw new UserFriendlyException("Aviso", "La unidad territorial (Aspectos generales) no existe o fue eliminado. Verifique la información antes de continuar");

                var territorialUnit = await _territorialUnitRepository.GetAsync(territorialUnitId);

                input.TerritorialUnit = territorialUnit;
                input.TerritorialUnitId = territorialUnit.Id;
            }
            else
            {
                input.TerritorialUnit = null;
                input.TerritorialUnitId = null;
            }

            if (alertDemandId > 0)
            {
                if (await _alertDemandRepository.CountAsync(p => p.Id == alertDemandId) == 0)
                    throw new UserFriendlyException("Aviso", $"El tipo de demanda seleccionada no existe o fue eliminada. Verifique la información antes de continuar");

                var alertDemand = await _alertDemandRepository.GetAsync(alertDemandId);

                input.AlertDemand = alertDemand;
                input.AlertDemandId = alertDemand.Id;
            }
            else
            {
                input.AlertDemand = null;
                input.AlertDemandId = null;
            }

            if (typologyId > 0)
            {
                if (await _typologyRepository.CountAsync(p => p.Id == typologyId) == 0)
                    throw new UserFriendlyException("Aviso", $"La tipología del conflicto seleccionada no existe o fue eliminada. Verifique la información antes de continuar");

                var typology = await _typologyRepository.GetAsync(typologyId);

                input.Typology = typology;
                input.TypologyId = typology.Id;
            }
            else
            {
                input.Typology = null;
                input.TypologyId = null;
            }

            if (subTypologyId > 0)
            {
                if (await _subTypologyRepository.CountAsync(p => p.Id == subTypologyId) == 0)
                    throw new UserFriendlyException("Aviso", $"La tipología detallada seleccionada no existe o fue eliminada. Verifique la información antes de continuar");

                var subTypology = await _subTypologyRepository.GetAsync(subTypologyId);

                input.SubTypology = subTypology;
                input.SubTypologyId = subTypology.Id;
            }
            else
            {
                input.SubTypology = null;
                input.SubTypologyId = null;
            }

            if (alertResponsibleId > 0)
            {
                if (await _alertResponsibleRepository.CountAsync(p => p.Id == alertResponsibleId) == 0)
                    throw new UserFriendlyException("Aviso", $"La subsecretaría responsable seleccionada no existe o fue eliminada. Verifique la información antes de continuar");

                var alertResponsible = await _alertResponsibleRepository.GetAsync(alertResponsibleId);

                input.AlertResponsible = alertResponsible;
                input.AlertResponsibleId = alertResponsible.Id;
            }
            else
            {
                input.AlertResponsible = null;
                input.AlertResponsibleId = null;
            }

            if (analystId > 0)
            {
                if (await _personRepository.CountAsync(p => p.Id == analystId) == 0)
                    throw new UserFriendlyException("Aviso", $"El responsable de la alerta seleccionado no existe o fue eliminado. Verifique la información antes de continuar");

                var analyst = await _personRepository.GetAsync(analystId);

                input.Analyst = analyst;
                input.AnalystId = analyst.Id;
            }
            else
            {
                input.Analyst = null;
                input.AnalystId = null;
            }

            if (managerId > 0)
            {
                if (await _personRepository.CountAsync(p => p.Id == managerId) == 0)
                    throw new UserFriendlyException("Aviso", $"El responsable de la intervención seleccionado no existe o fue eliminado. Verifique la información antes de continuar");

                var manager = await _personRepository.GetAsync(managerId);

                input.Manager = manager;
                input.ManagerId = manager.Id;
            }
            else
            {
                input.Manager = null;
                input.ManagerId = null;
            }

            if (coordinatorId > 0)
            {
                if (await _personRepository.CountAsync(p => p.Id == coordinatorId) == 0)
                    throw new UserFriendlyException("Aviso", $"El coordinador seleccionado no existe o fue eliminado. Verifique la información antes de continuar");

                var coordinator = await _personRepository.GetAsync(coordinatorId);

                input.Coordinator = coordinator;
                input.CoordinatorId = coordinator.Id;
            }
            else
            {
                input.Coordinator = null;
                input.CoordinatorId = null;
            }

            foreach (var location in locations)
            {
                if (location.Remove)
                {
                    if (location.Id > 0 && input.Id > 0 && await _socialConflictAlertLocationRepository.CountAsync(p => p.Id == location.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                    {
                        await _socialConflictAlertLocationRepository.DeleteAsync(location.Id);
                    }
                }
                else { 

                    if(location.Id <= 0)
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

                        location.Ubication.VerifyTableColumn(SocialConflictAlertLocationConsts.UbicationMinLength,
                            SocialConflictAlertLocationConsts.UbicationMaxLength,
                            DefaultTitleMessage,
                            $"La localidad - comunidad - Otros {location.Ubication} no debe exceder los {SocialConflictAlertLocationConsts.UbicationMaxLength} caracteres");

                        input.Locations.Add(new SocialConflictAlertLocation()
                        {
                            TerritorialUnit = territorialUnit,
                            Department = department,
                            Province = province,
                            District = district,
                            Region = region,
                            Ubication = location.Ubication,
                            Id = 0
                        });
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
                    if (actor.Id > 0 && input.Id > 0 && await _socialConflictAlertActorRepository.CountAsync(p => p.Id == actor.Id && p.SocialConflictAlertId == input.Id) > 0)
                    {
                        await _socialConflictAlertActorRepository.DeleteAsync(actor.Id);
                        continue;
                    }
                }
                else
                {
                    if (actor.ActorId > 0)
                    {
                        if (await _socialConflictAlertActorRepository.CountAsync(p => p.ActorId == actor.ActorId && p.SocialConflictAlertId == input.Id) == 0)
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
                                ActorMovementId = actor.ActorMovement.Id < 0 ? (int?)null : actor.ActorMovement.Id,
                                ActorId = actor.ActorId,
                                Site = ActorSite.SocialConflictAlert
                            });
                        }
                    }
                }
            }

            foreach (var risk in risks)
            {
                if (risk.Remove)
                {
                    if (risk.Id > 0 && input.Id > 0 && await _socialConflictAlertRiskRepository.CountAsync(p => p.Id == risk.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                    {
                        await _socialConflictAlertRiskRepository.DeleteAsync(risk.Id);
                    }
                }
                else
                {
                    risk.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción de los niveles de riesgo son obligatorias");
                    risk.Description.VerifyTableColumn(SocialConflictAlertRiskConsts.DescriptionMinLength,
                        SocialConflictAlertRiskConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del nivel de riesgo {risk.Description} no debe exceder los " +
                        $"{SocialConflictAlertRiskConsts.DescriptionMaxLength} caracteres");

                    risk.Observation.VerifyTableColumn(SocialConflictAlertRiskConsts.ObservationMinLength,
                        SocialConflictAlertRiskConsts.ObservationMaxLength,
                        DefaultTitleMessage,
                        $"La observación del nivel de riesgo {risk.Description} no debe exceder los " +
                        $"{SocialConflictAlertRiskConsts.ObservationMaxLength} caracteres");

                    var dbRisk = _alertRiskRepository
                        .GetAll()
                        .Where(p => p.Id == risk.AlertRisk.Id)
                        .FirstOrDefault();

                    if (dbRisk == null)
                        throw new UserFriendlyException("Aviso", $"El nivel de riesgo {risk.AlertRisk.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (risk.Id > 0)
                    {
                        if (await _socialConflictAlertRiskRepository.CountAsync(p => p.Id == risk.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                        {
                            var dbSocialConflictAlertRisk = await _socialConflictAlertRiskRepository.GetAsync(risk.Id);
                            dbSocialConflictAlertRisk.Description = risk.Description;
                            dbSocialConflictAlertRisk.Observation = risk.Observation;
                            dbSocialConflictAlertRisk.RiskTime = risk.RiskTime;
                            dbSocialConflictAlertRisk.AlertRiskId = dbRisk.Id;
                            dbSocialConflictAlertRisk.AlertRisk = dbRisk;

                            await _socialConflictAlertRiskRepository.UpdateAsync(dbSocialConflictAlertRisk);
                        }
                    }
                    else
                    {
                        input.Risks.Add(new SocialConflictAlertRisk()
                        {
                            Description = risk.Description,
                            Observation = risk.Observation,
                            RiskTime = risk.RiskTime,
                            AlertRiskId = dbRisk.Id,
                            AlertRisk = dbRisk
                        });
                    }
                }
            }

            foreach (var sector in sectors)
            {
                if (sector.Remove)
                {
                    if (sector.Id > 0 && input.Id > 0 && await _socialConflictAlertSectorRepository.CountAsync(p => p.Id == sector.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                    {
                        await _socialConflictAlertSectorRepository.DeleteAsync(sector.Id);
                    }
                }
                else
                {
                    sector.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción de la atención de los sectores son obligatorias");
                    sector.Description.VerifyTableColumn(SocialConflictAlertSectorConsts.DescriptionMinLength,
                        SocialConflictAlertSectorConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la atención de los sectores {sector.Description} no debe exceder los " +
                        $"{SocialConflictAlertSectorConsts.DescriptionMaxLength} caracteres");

                    var dbSector = _alertSectorRepository
                        .GetAll()
                        .Where(p => p.Id == sector.AlertSector.Id)
                        .FirstOrDefault();

                    if (dbSector == null)
                        throw new UserFriendlyException("Aviso", $"La atención de los sectores {sector.AlertSector.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (sector.Id > 0)
                    {
                        if (await _socialConflictAlertSectorRepository.CountAsync(p => p.Id == sector.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                        {
                            var dbSocialConflictAlertSector = await _socialConflictAlertSectorRepository.GetAsync(sector.Id);
                            dbSocialConflictAlertSector.SectorTime = sector.SectorTime;
                            dbSocialConflictAlertSector.Description = sector.Description;
                            dbSocialConflictAlertSector.AlertSectorId = dbSector.Id;
                            dbSocialConflictAlertSector.AlertSector = dbSector;

                            await _socialConflictAlertSectorRepository.UpdateAsync(dbSocialConflictAlertSector);
                        }
                    }
                    else
                    {
                        input.Sectors.Add(new SocialConflictAlertSector()
                        {
                            SectorTime = sector.SectorTime,
                            Description = sector.Description,
                            AlertSectorId = dbSector.Id,
                            AlertSector = dbSector
                        });
                    }
                }
            }

            foreach (var state in states)
            {
                if (state.Remove)
                {
                    if (state.Id > 0 && input.Id > 0 && await _socialConflictAlertStateRepository.CountAsync(p => p.Id == state.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                    {
                        await _socialConflictAlertStateRepository.DeleteAsync(state.Id);
                    }
                }
                else
                {
                    state.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción de la actualización de las alertas son obligatorias");
                    state.Description.VerifyTableColumn(SocialConflictAlertStateConsts.DescriptionMinLength,
                        SocialConflictAlertStateConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la actualización del alerta {state.Description} no debe exceder los " +
                        $"{SocialConflictAlertStateConsts.DescriptionMaxLength} caracteres");

                    if (state.Id > 0)
                    {
                        if (await _socialConflictAlertStateRepository.CountAsync(p => p.Id == state.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                        {
                            var dbSocialConflictAlertState = await _socialConflictAlertStateRepository.GetAsync(state.Id);
                            dbSocialConflictAlertState.StateTime = state.StateTime;
                            dbSocialConflictAlertState.Description = state.Description;

                            await _socialConflictAlertStateRepository.UpdateAsync(dbSocialConflictAlertState);
                        }
                    }
                    else
                    {
                        input.States.Add(new SocialConflictAlertState()
                        {
                            StateTime = state.StateTime,
                            Description = state.Description
                        });
                    }
                }
            }

            foreach (var seal in seals)
            {
                if (seal.Remove)
                {
                    if (seal.Id > 0 && input.Id > 0 && await _socialConflictAlertSealRepository.CountAsync(p => p.Id == seal.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                    {
                        await _socialConflictAlertSealRepository.DeleteAsync(seal.Id);
                    }
                }
                else
                {
                    seal.Description.IsValidOrException(DefaultTitleMessage,
                        "La descripción de los cierres de alerta son obligatorias");
                    seal.Description.VerifyTableColumn(SocialConflictAlertSealConsts.DescriptionMinLength,
                        SocialConflictAlertSealConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del cierre de alerta {seal.Description} no debe exceder los " +
                        $"{SocialConflictAlertSealConsts.DescriptionMaxLength} caracteres");

                    var dbSeal = _alertSealRepository
                        .GetAll()
                        .Where(p => p.Id == seal.AlertSeal.Id)
                        .FirstOrDefault();

                    if (dbSeal == null)
                        throw new UserFriendlyException("Aviso", $"El tipo de cierre de alerta {seal.AlertSeal.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (seal.Id > 0)
                    {
                        if (await _socialConflictAlertSealRepository.CountAsync(p => p.Id == seal.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                        {
                            var dbSocialConflictAlertSeal = await _socialConflictAlertSealRepository.GetAsync(seal.Id);
                            dbSocialConflictAlertSeal.SealTime = seal.SealTime;
                            dbSocialConflictAlertSeal.Description = seal.Description;
                            dbSocialConflictAlertSeal.AlertSealId = dbSeal.Id;
                            dbSocialConflictAlertSeal.AlertSeal = dbSeal;

                            await _socialConflictAlertSealRepository.UpdateAsync(dbSocialConflictAlertSeal);
                        }
                    }
                    else
                    {
                        input.Seals.Add(new SocialConflictAlertSeal()
                        {
                            SealTime = seal.SealTime,
                            Description = seal.Description,
                            AlertSealId = dbSeal.Id,
                            AlertSeal = dbSeal
                        });
                    }
                }
            }

            foreach (var resource in resources)
            {
                if (resource.Remove)
                {
                    if (resource.Id > 0 && input.Id > 0 && await _socialConflictAlertResourceRepository.CountAsync(p => p.Id == resource.Id && p.SocialConflictAlert.Id == input.Id) > 0)
                    {
                        await _socialConflictAlertResourceRepository.DeleteAsync(resource.Id);
                    }
                }
            }
            
            foreach (var upload in uploads)
            {
                if (ResourceManager.TokenIsValid(upload.Token) == false)
                    throw new UserFriendlyException("Aviso", "La validez de los archivos subidos a caducado, por favor intente nuevamente.");
            }

            foreach (var upload in uploads)
            {
                input.Resources.Add(ObjectMapper.Map<SocialConflictAlertResource>(ResourceManager.Create(upload, ResourceConsts.SocialConflictAlert)));
            }
            
            return input;
        }
    }
}
