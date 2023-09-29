using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Contable.Application.SocialConflictTaskManagements;
using Contable.Application.SocialConflictTaskManagements.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Contable.Application.Extensions;
using Abp.Domain.Uow;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;
using Contable.Dto;
using Contable.Application.Exporting;
using Abp.Collections.Extensions;
using Contable.Application.External.Dto;
using Abp.Authorization;
using Contable.Authorization;
using System.Linq.Expressions;
using NUglify.Helpers;
using Abp.EntityFrameworkCore;
using Contable.EntityFrameworkCore;
using Contable.Repositories;
using Abp.Authorization.Users;
using Contable.Configuration;
using Contable.Net.Emailing;
using System.ComponentModel.DataAnnotations;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
    public class SocialConflictTaskManagementAppService : ContableAppServiceBase, ISocialConflictTaskManagementAppService
    {
        private readonly IRepository<SocialConflictTaskManagement> _taskManagementRepository;
        private readonly IRepository<SocialConflictTaskManagementPerson> _taskManagementPersonRepository;
        private readonly IRepository<SocialConflictTaskManagementExtend> _taskManagementExtendRepository;
        private readonly IRepository<SocialConflictTaskManagementComment> _taskManagementCommentRepository;
        private readonly IRepository<SocialConflictTaskManagementResource> _taskManagementResourceRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SocialConflictAlert> _socialConflictAlertRepository;
        private readonly IRepository<SocialConflictSensible> _socialConflictSensibleRepository;
        private readonly IRepository<SocialConflictTaskManagementHistory> _socialConflictTaskManagementHistoryRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IProcedureRepository _procedureRepository;
        private readonly ITaskExcelExporter _taskExcelExporter;
        private readonly IAppEmailSender _appEmailSender;

        public SocialConflictTaskManagementAppService(
            IRepository<SocialConflictTaskManagement> taskMagementRepository,
            IRepository<SocialConflictTaskManagementPerson> taskManagementPersonRepository,
            IRepository<SocialConflictTaskManagementExtend> taskMagementExtendRepository,
            IRepository<SocialConflictTaskManagementComment> taskMagementCommentRepository,
            IRepository<SocialConflictTaskManagementResource> taskManagementResourceRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<SocialConflictAlert> socialConflictAlertRepository,
            IRepository<SocialConflictSensible> socialConflictSensibleRepository,
            IRepository<SocialConflictTaskManagementHistory> socialConflictTaskManagementHistoryRepository,
            IRepository<Person> personRepository,
            IProcedureRepository procedureRepository,
            ITaskExcelExporter taskExcelExporter,
            IAppEmailSender appEmailSender)
        {
            _taskManagementRepository = taskMagementRepository;
            _taskManagementPersonRepository = taskManagementPersonRepository;
            _taskManagementExtendRepository = taskMagementExtendRepository;
            _taskManagementCommentRepository = taskMagementCommentRepository;
            _taskManagementResourceRepository = taskManagementResourceRepository;
            _socialConflictRepository = socialConflictRepository;
            _socialConflictAlertRepository = socialConflictAlertRepository;
            _socialConflictSensibleRepository = socialConflictSensibleRepository;
            _socialConflictTaskManagementHistoryRepository = socialConflictTaskManagementHistoryRepository;
            _personRepository = personRepository;
            _procedureRepository = procedureRepository;
            _taskExcelExporter = taskExcelExporter;
            _appEmailSender = appEmailSender;
        }

        #region Tasks 
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<PagedResultDto<SocialConflictTaskManagementConflictGetAllDto>> GetAllConflicts(SocialConflictTaskManagementConflictGetAllInputDto input)
        {
            return await _procedureRepository.CallGetAllConflictTaskManagements(
                names: input.Name,
                codes: input.Code,
                startDate: input.StartDate,
                endDate: input.EndDate,
                site: input.Site,
                skipCount: input.SkipCount,
                maxResultCount: input.MaxResultCount);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<SocialConflictTaskManagementConflictGetDto> GetConflict(SocialConflictTaskManagementConflictGetInputDto input)
        {
            if (input.Site == ConflictSite.SocialConflict)
            {
                VerifyCount(await _socialConflictRepository.CountAsync(p => p.Id == input.Id));

                var socialConflict = await _socialConflictRepository.GetAsync(input.Id);
                var taskCount = await _taskManagementRepository.CountAsync(p => p.SocialConflictId == input.Id && p.Status != TaskStatus.Completed);

                return new SocialConflictTaskManagementConflictGetDto()
                {
                    Id = socialConflict.Id,
                    CreationTime = socialConflict.CreationTime,
                    Code = socialConflict.Code,
                    Name = socialConflict.CaseName,
                    Tasks = taskCount,
                    Type = ConflictSite.SocialConflict
                };
            }

            if (input.Site == ConflictSite.SocialConflictAlert)
            {
                VerifyCount(await _socialConflictAlertRepository.CountAsync(p => p.Id == input.Id));

                var socialConflictAlert = await _socialConflictAlertRepository.GetAsync(input.Id);
                var taskCount = await _taskManagementRepository.CountAsync(p => p.SocialConflictAlertId == input.Id && p.Status != TaskStatus.Completed);

                return new SocialConflictTaskManagementConflictGetDto()
                {
                    Id = socialConflictAlert.Id,
                    CreationTime = socialConflictAlert.CreationTime,
                    Code = socialConflictAlert.Code,
                    Name = socialConflictAlert.Description,
                    Tasks = taskCount,
                    Type = ConflictSite.SocialConflictAlert
                };
            }

            if (input.Site == ConflictSite.SocialConflictSensible)
            {
                VerifyCount(await _socialConflictSensibleRepository.CountAsync(p => p.Id == input.Id));

                var socialConflictSensible = await _socialConflictSensibleRepository.GetAsync(input.Id);
                var taskCount = await _taskManagementRepository.CountAsync(p => p.SocialConflictSensibleId == input.Id && p.Status != TaskStatus.Completed);

                return new SocialConflictTaskManagementConflictGetDto()
                {
                    Id = socialConflictSensible.Id,
                    CreationTime = socialConflictSensible.CreationTime,
                    Code = socialConflictSensible.Code,
                    Name = socialConflictSensible.CaseName,
                    Tasks = taskCount,
                    Type = ConflictSite.SocialConflictSensible
                };
            }

            throw new UserFriendlyException("Aviso", "El tipo de conflicto solicitado es inválido");
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<PagedResultDto<SocialConflictTaskManagementPersonGetAllDto>> GetAllPersons(SocialConflictTaskManagementPersonGetAllInputDto input)
        {
            var dbUser = await GetCurrentUserAsync();

            if (dbUser.UserName != AbpUserBase.AdminUserName && dbUser.PersonId.HasValue == false)
                throw new UserFriendlyException("Aviso", "Estimado usuario, usted debe pertenecer a un tipo de perfil: Coordinador o Gestor, para utilizar esta funcionalidad");

            Person dbPerson = dbUser.PersonId.HasValue ? await _personRepository.GetAsync(dbUser.PersonId.Value) : null;

            if(dbUser.UserName != AbpUserBase.AdminUserName && (dbPerson.Type == PersonType.Analyst || dbPerson.Type == PersonType.None))
                return new PagedResultDto<SocialConflictTaskManagementPersonGetAllDto>(0, new List<SocialConflictTaskManagementPersonGetAllDto>());

            var query =
                (from person in _personRepository
                     .GetAll()
                     .WhereIf(dbPerson != null && dbPerson.Type == PersonType.Manager, p => p.Id == dbPerson.Id)
                     .WhereIf(dbPerson != null && dbPerson.Type == PersonType.Coordinator, p => p.Id == dbPerson.Id || p.TerritorialUnit.Coordinators.Any(p => p.PersonId == dbPerson.Id))
                 join task in _taskManagementPersonRepository.GetAll().Where(p => p.SocialConflictTaskManagementId == input.SocialConflictTaskManagementId)
                 on person.Id equals task.PersonId into TaskData
                 from result in TaskData.DefaultIfEmpty()
                 select new SocialConflictTaskManagementPersonGetAllDto()
                 {
                     Id = person.Id,
                     Document = person.Document,
                     Name = person.Name,
                     EmailAddress = person.EmailAddress,
                     Type = person.Type,
                     Selected = result != null
                 }).LikeAnyBidirectional(input.Document.SplitByLike(), nameof(SocialConflictTaskManagementPersonGetAllDto.Document))
                 .LikeAnyBidirectional(input.Names.SplitByLike(), nameof(SocialConflictTaskManagementPersonGetAllDto.Name))
                 .LikeAnyBidirectional(input.EmailAddress.SplitByLike(), nameof(SocialConflictTaskManagementPersonGetAllDto.EmailAddress));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<SocialConflictTaskManagementPersonGetAllDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<SocialConflictTaskManagementPersonChangeOutputDto> PersonChanges(SocialConflictTaskManagementPersonChangeInputDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskManagementRepository.GetAsync(input.Id);

            foreach (var change in input.Changes.ToArray().DistinctBy(p => p.PersonId))
            {
                if (change.Checked)
                {
                    if (await _taskManagementPersonRepository.CountAsync(p => p.SocialConflictTaskManagementId == task.Id && p.PersonId == change.PersonId) == 0)
                    {
                        var dbPerson = _personRepository
                            .GetAll()
                            .Where(p => p.Id == change.PersonId)
                            .FirstOrDefault();

                        if (dbPerson != null)
                        {
                            await _taskManagementPersonRepository.InsertAsync(new SocialConflictTaskManagementPerson()
                            {
                                Person = dbPerson,
                                PersonId = dbPerson.Id,
                                SocialConflictTaskManagement = task,
                                SocialConflictTaskManagementId = task.Id
                            });
                        }
                    }
                }
                else
                {
                    await _taskManagementPersonRepository.DeleteAsync(p => p.SocialConflictTaskManagementId == task.Id && p.PersonId == change.PersonId);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return new SocialConflictTaskManagementPersonChangeOutputDto()
            {
                Persons = ObjectMapper.Map<List<SocialConflictTaskManagementPersonRelationDto>>(_taskManagementPersonRepository
                    .GetAll()
                    .Include(p => p.Person)
                    .Where(p => p.SocialConflictTaskManagementId == task.Id))
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<SocialConflictTaskManagementGetDto> CreateTask(SocialConflictTaskManagementCreateDto input)
        {
            var taskManagement = await ValidateEntityTask(
                input: ObjectMapper.Map<SocialConflictTaskManagement>(input), 
                conflictId: input.ConflictId, 
                site: input.Site,
                persons: input.Persons ?? new List<SocialConflictTaskManagementPersonRelationDto>(),
                resources: new List<SocialConflictTaskManagementResourceGetDto>());

            var taskId = await _taskManagementRepository.InsertAndGetIdAsync(taskManagement);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<SocialConflictTaskManagementGetDto>(_taskManagementRepository
                .GetAll()
                .Include(p => p.SocialConflictAlert)
                .Include(p => p.SocialConflictSensible)
                .Include(p => p.SocialConflict)
                .Include(p => p.Comments)
                .ThenInclude(p => p.User)
                .Include(p => p.Persons)
                .ThenInclude(p => p.Person)
                .Where(p => p.Id == taskId)
                .First());
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task DeleteTask(EntityDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));
            await _taskManagementRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<SocialConflictTaskManagementGetDto> GetTask(NullableIdDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<SocialConflictTaskManagementGetDto>(_taskManagementRepository
                .GetAll()
                .Include(p => p.SocialConflictAlert)
                .Include(p => p.SocialConflictSensible)
                .Include(p => p.SocialConflict)
                .Include(p => p.Comments)
                .ThenInclude(p => p.User)
                .Include(p => p.Persons)
                .ThenInclude(p => p.Person)
                .Include(p => p.Resources)
                .Where(p => p.Id == input.Id)
                .First());
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<PagedResultDto<SocialConflictTaskManagementGetAllDto>> GetAllTask(SocialConflictTaskManagementGetAllInputDto input)
        {
            var query = _taskManagementRepository
               .GetAll()
               .Include(p => p.SocialConflict)
               .ThenInclude(p => p.Locations)
               .ThenInclude(p => p.TerritorialUnit)
               .Include(p => p.SocialConflictAlert)
               .ThenInclude(p => p.Locations)
               .ThenInclude(p => p.TerritorialUnit)
               .Include(p => p.SocialConflictSensible)
               .ThenInclude(p => p.Locations)
               .ThenInclude(p => p.TerritorialUnit)
               .Where(p => (p.SocialConflictId.HasValue && p.SocialConflict.IsDeleted == false) ||
                           (p.SocialConflictAlertId.HasValue && p.SocialConflictAlert.IsDeleted == false) ||
                           (p.SocialConflictSensibleId.HasValue && p.SocialConflictSensible.IsDeleted == false))
               .LikeAllBidirectional(input
                    .ConflictCode
                    .SplitByLike()
                    .Select(word => (Expression<Func<SocialConflictTaskManagement, bool>>)(expression => 
                        (expression.SocialConflict == null || EF.Functions.Like(expression.SocialConflict.Code, $"%{word}%")) ||
                        (expression.SocialConflictAlert == null || EF.Functions.Like(expression.SocialConflictAlert.Code, $"%{word}%")) ||
                        (expression.SocialConflictSensible == null || EF.Functions.Like(expression.SocialConflictSensible.Code, $"%{word}%"))
                        ))
                    .ToArray())
               .LikeAllBidirectional(input
                    .ConflictName
                    .SplitByLike()
                    .Select(word => (Expression<Func<SocialConflictTaskManagement, bool>>)(expression =>
                        (expression.SocialConflict == null || EF.Functions.Like(expression.SocialConflict.CaseName, $"%{word}%")) ||
                        (expression.SocialConflictAlert == null || EF.Functions.Like(expression.SocialConflictAlert.Description, $"%{word}%")) ||
                        (expression.SocialConflictSensible == null || EF.Functions.Like(expression.SocialConflictSensible.CaseName, $"%{word}%"))
                        ))
                    .ToArray())
               .LikeAllBidirectional(input.TaskTitle.SplitByLike(), nameof(SocialConflictTaskManagement.Title))
               .WhereIf(input.TaskStatus != TaskStatus.None, p => p.Status == input.TaskStatus)
               .WhereIf(input.ConflictType == ConflictSite.SocialConflict, p => p.SocialConflictId.HasValue)
               .WhereIf(input.ConflictType == ConflictSite.SocialConflictAlert, p => p.SocialConflictAlertId.HasValue)
               .WhereIf(input.ConflictType == ConflictSite.SocialConflictSensible, p => p.SocialConflictSensibleId.HasValue)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflict, p => p.SocialConflictId == input.ConflictId)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflict, p => p.SocialConflictId == input.ConflictId)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflict, p => p.SocialConflictId == input.ConflictId)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflictAlert, p => p.SocialConflictAlertId == input.ConflictId)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflictSensible, p => p.SocialConflictSensibleId == input.ConflictId);

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var result = new List<SocialConflictTaskManagementGetAllDto>();

            foreach (var item in output)
            {
                var taskItem = new SocialConflictTaskManagementGetAllDto();

                if(item.SocialConflict != null)
                {
                    taskItem.ConflictId = item.SocialConflict.Id;
                    taskItem.ConflictCode = item.SocialConflict.Code;
                    taskItem.ConflictName = item.SocialConflict.CaseName;
                    taskItem.ConflictSite = ConflictSite.SocialConflict;
                    taskItem.ConflictTerritorialUnits = string.Join(", ", item
                        .SocialConflict
                        .Locations
                        .Where(p => p.TerritorialUnit != null)
                        .DistinctBy(p => p.TerritorialUnit.Id)
                        .Select(p => p.TerritorialUnit.Name));                        
                }

                if(item.SocialConflictAlert != null)
                {
                    taskItem.ConflictId = item.SocialConflictAlert.Id;
                    taskItem.ConflictCode = item.SocialConflictAlert.Code;
                    taskItem.ConflictName = item.SocialConflictAlert.Description;
                    taskItem.ConflictSite = ConflictSite.SocialConflictAlert;
                    taskItem.ConflictTerritorialUnits = string.Join(", ", item
                        .SocialConflictAlert
                        .Locations
                        .Where(p => p.TerritorialUnit != null)
                        .DistinctBy(p => p.TerritorialUnit.Id)
                        .Select(p => p.TerritorialUnit.Name));
                }

                if(item.SocialConflictSensible != null)
                {
                    taskItem.ConflictId = item.SocialConflictSensible.Id;
                    taskItem.ConflictCode = item.SocialConflictSensible.Code;
                    taskItem.ConflictName = item.SocialConflictSensible.CaseName;
                    taskItem.ConflictSite = ConflictSite.SocialConflictSensible;
                    taskItem.ConflictTerritorialUnits = string.Join(", ", item
                        .SocialConflictSensible
                        .Locations
                        .Where(p => p.TerritorialUnit != null)
                        .DistinctBy(p => p.TerritorialUnit.Id)
                        .Select(p => p.TerritorialUnit.Name));
                }

                taskItem.Id = item.Id;
                taskItem.Title = item.Title;
                taskItem.CreationTime = item.CreationTime;
                taskItem.Description = item.Description;
                taskItem.Status = item.Status;
                taskItem.StartTime = item.StartTime;
                taskItem.Deadline = item.Deadline;

                result.Add(taskItem);
            }

            return new PagedResultDto<SocialConflictTaskManagementGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<SocialConflictTaskManagementGetDto> UpdateTask(SocialConflictTaskManagementUpdateDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));

            var taskManagemet = await ValidateEntityTask(
                input: ObjectMapper.Map(input, await _taskManagementRepository.GetAsync(input.Id)),
                conflictId: -1,
                site: ConflictSite.All,
                persons: input.Persons ?? new List<SocialConflictTaskManagementPersonRelationDto>(),
                resources: input.Resources ?? new List<SocialConflictTaskManagementResourceGetDto>());

            await _taskManagementRepository.UpdateAsync(taskManagemet);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<SocialConflictTaskManagementGetDto>(_taskManagementRepository
                .GetAll()
                .Include(p => p.SocialConflictAlert)
                .Include(p => p.SocialConflictSensible)
                .Include(p => p.SocialConflict)
                .Include(p => p.Comments)
                .ThenInclude(p => p.User)
                .Include(p => p.Persons)
                .ThenInclude(p => p.Person)
                .Where(p => p.Id == input.Id)
                .First());
        }
        
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task CreateTaskExtend(SocialConflictTaskManagementExtendCreateDto input)
        {
            var taskManagemetExtend = await ValidateEntityTaskExtend(
                input: ObjectMapper.Map<SocialConflictTaskManagementExtend>(input), 
                taskManagementId: input.SocialConflictTaskManagement == null ? -1 : input.SocialConflictTaskManagement.Id);

            await _taskManagementExtendRepository.InsertAsync(taskManagemetExtend);

            var taskManagement = await _taskManagementRepository.GetAsync(taskManagemetExtend.SocialConflictTaskManagementId);
            taskManagement.Deadline = taskManagemetExtend.Deadline;
            taskManagement.SendedDeadline = false;

            await _taskManagementRepository.UpdateAsync(taskManagement);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task DeleteTaskExtend(EntityDto input)
        {
            VerifyCount(await _taskManagementExtendRepository.CountAsync(p => p.Id == input.Id));
            await _taskManagementExtendRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<SocialConflictTaskManagementExtendGetDto> GetTaskExtend(EntityDto input)
        {
            VerifyCount(await _taskManagementExtendRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<SocialConflictTaskManagementExtendGetDto>(await _taskManagementExtendRepository.GetAsync(input.Id));
        }
        
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task ChangeStateToPending(EntityDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskManagementRepository.GetAsync(input.Id);
            task.Status = TaskStatus.Pending;

            await _taskManagementRepository.UpdateAsync(task);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task ChangeStateToNonComplete(EntityDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskManagementRepository.GetAsync(input.Id);
            task.Status = TaskStatus.NonCompleted;

            await _taskManagementRepository.UpdateAsync(task);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task ChangeStateToComplete(EntityDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskManagementRepository.GetAsync(input.Id);
            task.Status = TaskStatus.Completed;

            await _taskManagementRepository.UpdateAsync(task);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<EntityDto> CreateResource(SocialConflictTaskManagementCreateResourceDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.TaskManagementId));

            if (ResourceManager.TokenIsValid(input.Resource.Token) == false)
                throw new UserFriendlyException("Aviso", "La validez de los archivos subidos a caducado, por favor intente nuevamente.");

            var resource = ObjectMapper.Map<SocialConflictTaskManagementResource>(ResourceManager.Create(input.Resource, ResourceConsts.SocialConflictTaskManagement));
            resource.SocialConflictTaskManagementId = input.TaskManagementId;

            var resourceId = await _taskManagementResourceRepository.InsertAndGetIdAsync(resource);

            return new EntityDto(resourceId);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task DeleteResource(EntityDto input)
        {
            VerifyCount(await _taskManagementResourceRepository.CountAsync(p => p.Id == input.Id));

            await _taskManagementResourceRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<SocialConflictTaskManagementEmailConfigurationDto> GetEmailConfiguration(EntityDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));

            var task = _taskManagementRepository
                .GetAll()
                .Where(p => p.Id == input.Id)
                .First();

            var persons = await FunctionManager.CallSocialConflictTaskManagementGetAllPersons(task.Id);
            var coordinators = persons.Where(p => p.Type == PersonType.Coordinator).ToList();
            var managers = persons.Where(p => p.Type == PersonType.Manager).ToList();

            var subject = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.SocialConflictTaskManagementAlertNotificationSubject, ContableConsts.DefaultTenantId);
            var template = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.SocialConflictTaskManagementAlertTemplate, ContableConsts.DefaultTenantId);

            template = _appEmailSender.CreateSocialConflictTaskTemplate(template, task, coordinators, managers);

            return new SocialConflictTaskManagementEmailConfigurationDto()
            {
                Id = task.Id,
                Subject = subject,
                Template = template
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task SendNotification(SocialConflictTaskManagementSendNotificationDto input)
        {
            VerifyCount(await _taskManagementRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskManagementRepository.GetAsync(input.Id);

            var emailValidator = new EmailAddressAttribute();

            foreach (var emailAddress in input.To)
                if (emailValidator.IsValid(emailAddress) == false)
                    throw new UserFriendlyException("Aviso", $"La dirección de correo electrónico en destinatarios \"Para\": {emailAddress} es inválida");
            foreach (var emailAddress in input.Copy)
                if (emailValidator.IsValid(emailAddress) == false)
                    throw new UserFriendlyException("Aviso", $"La dirección de correo electrónico en destinatarios \"Copia a\": {emailAddress} es inválida");

            await _socialConflictTaskManagementHistoryRepository.InsertAsync(new SocialConflictTaskManagementHistory()
            {
                SocialConflictTaskManagementId = task.Id,
                SocialConflictTaskManagement = task,
                Subject = input.Subject,
                Template = input.Template,
                To = string.Join(";", input.To),
                Copy = string.Join(";", input.Copy)
            });

            await _appEmailSender.SendEmail(
                to: input.To.ToArray(),
                cc: input.Copy.ToArray(),
                subject: input.Subject,
                body: input.Template,
                attachments: Array.Empty<EmailAttachment>());
        }

        #endregion

        #region Comment

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task<SocialConflictTaskManagementCommentGetDto> CreateComment(SocialConflictTaskManagementCommentCreateDto input)
        {
            var output = await _taskManagementCommentRepository.InsertAsync(await ValidateEntityComment(
                input: ObjectMapper.Map<SocialConflictTaskManagementComment>(input), 
                taskManagementId: input.SocialConflictTaskManagement == null ? -1 : input.SocialConflictTaskManagement.Id));

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<SocialConflictTaskManagementCommentGetDto>(output);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public async Task DeleteComment(EntityDto input)
        {
            VerifyCount(await _taskManagementCommentRepository.CountAsync(p => p.Id == input.Id));
            await _taskManagementCommentRepository.DeleteAsync(input.Id);
        }

        #endregion

        #region Export

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<FileDto> GetMatrixToExcel(SocialConflictTaskManagementGetAllInputDto input)
        {
            var query = _taskManagementRepository
               .GetAll()
               .Include(p => p.SocialConflict)
               .ThenInclude(p => p.Locations)
               .ThenInclude(p => p.TerritorialUnit)
               .Include(p => p.SocialConflictAlert)
               .ThenInclude(p => p.Locations)
               .ThenInclude(p => p.TerritorialUnit)
               .Include(p => p.SocialConflictSensible)
               .ThenInclude(p => p.Locations)
               .ThenInclude(p => p.TerritorialUnit)
               .Where(p => (p.SocialConflictId.HasValue && p.SocialConflict.IsDeleted == false) ||
                           (p.SocialConflictAlertId.HasValue && p.SocialConflictAlert.IsDeleted == false) ||
                           (p.SocialConflictSensibleId.HasValue && p.SocialConflictSensible.IsDeleted == false))
               .LikeAllBidirectional(input
                    .ConflictCode
                    .SplitByLike()
                    .Select(word => (Expression<Func<SocialConflictTaskManagement, bool>>)(expression =>
                        (expression.SocialConflict == null || EF.Functions.Like(expression.SocialConflict.Code, $"%{word}%")) ||
                        (expression.SocialConflictAlert == null || EF.Functions.Like(expression.SocialConflictAlert.Code, $"%{word}%")) ||
                        (expression.SocialConflictSensible == null || EF.Functions.Like(expression.SocialConflictSensible.Code, $"%{word}%"))
                        ))
                    .ToArray())
               .LikeAllBidirectional(input
                    .ConflictName
                    .SplitByLike()
                    .Select(word => (Expression<Func<SocialConflictTaskManagement, bool>>)(expression =>
                        (expression.SocialConflict == null || EF.Functions.Like(expression.SocialConflict.CaseName, $"%{word}%")) ||
                        (expression.SocialConflictAlert == null || EF.Functions.Like(expression.SocialConflictAlert.Description, $"%{word}%")) ||
                        (expression.SocialConflictSensible == null || EF.Functions.Like(expression.SocialConflictSensible.CaseName, $"%{word}%"))
                        ))
                    .ToArray())
               .LikeAllBidirectional(input.TaskTitle.SplitByLike(), nameof(SocialConflictTaskManagement.Title))
               .WhereIf(input.TaskStatus != TaskStatus.None, p => p.Status == input.TaskStatus)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflict, p => p.SocialConflictId == input.ConflictId)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflict, p => p.SocialConflictId == input.ConflictId)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflict, p => p.SocialConflictId == input.ConflictId)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflictAlert, p => p.SocialConflictAlertId == input.ConflictId)
               .WhereIf(input.ConflictSite == ConflictSite.SocialConflictSensible, p => p.SocialConflictSensibleId == input.ConflictId);

            var output = await query.OrderBy(input.Sorting).ToListAsync();

            var result = new List<SocialConflictTaskManagementGetAllDto>();

            foreach (var item in output)
            {
                var taskItem = new SocialConflictTaskManagementGetAllDto();

                if (item.SocialConflict != null)
                {
                    taskItem.ConflictId = item.SocialConflict.Id;
                    taskItem.ConflictCode = item.SocialConflict.Code;
                    taskItem.ConflictName = item.SocialConflict.CaseName;
                    taskItem.ConflictSite = ConflictSite.SocialConflict;
                    taskItem.ConflictTerritorialUnits = string.Join(", ", item
                        .SocialConflict
                        .Locations
                        .Where(p => p.TerritorialUnit != null)
                        .DistinctBy(p => p.TerritorialUnit.Id)
                        .Select(p => p.TerritorialUnit.Name));
                }

                if (item.SocialConflictAlert != null)
                {
                    taskItem.ConflictId = item.SocialConflictAlert.Id;
                    taskItem.ConflictCode = item.SocialConflictAlert.Code;
                    taskItem.ConflictName = item.SocialConflictAlert.Description;
                    taskItem.ConflictSite = ConflictSite.SocialConflictAlert;
                    taskItem.ConflictTerritorialUnits = string.Join(", ", item
                        .SocialConflictAlert
                        .Locations
                        .Where(p => p.TerritorialUnit != null)
                        .DistinctBy(p => p.TerritorialUnit.Id)
                        .Select(p => p.TerritorialUnit.Name));
                }

                if (item.SocialConflictSensible != null)
                {
                    taskItem.ConflictId = item.SocialConflictSensible.Id;
                    taskItem.ConflictCode = item.SocialConflictSensible.Code;
                    taskItem.ConflictName = item.SocialConflictSensible.CaseName;
                    taskItem.ConflictSite = ConflictSite.SocialConflictSensible;
                    taskItem.ConflictTerritorialUnits = string.Join(", ", item
                        .SocialConflictSensible
                        .Locations
                        .Where(p => p.TerritorialUnit != null)
                        .DistinctBy(p => p.TerritorialUnit.Id)
                        .Select(p => p.TerritorialUnit.Name));
                }

                taskItem.Id = item.Id;
                taskItem.Title = item.Title;
                taskItem.CreationTime = item.CreationTime;
                taskItem.Description = item.Description;
                taskItem.Status = item.Status;
                taskItem.StartTime = item.StartTime;
                taskItem.Deadline = item.Deadline;

                result.Add(taskItem);
            }

            return _taskExcelExporter.ExportMatrixToFile(result);
        }

        #endregion

        #region Validate      

        private async Task<SocialConflictTaskManagement> ValidateEntityTask(
            SocialConflictTaskManagement input, 
            int conflictId, 
            ConflictSite site, 
            List<SocialConflictTaskManagementPersonRelationDto> persons, 
            List<SocialConflictTaskManagementResourceGetDto> resources)
        {
            if(input.Id == 0)
            {
                input.Site = site;

                if (site == ConflictSite.All)
                    throw new UserFriendlyException("Aviso", "El tipo de conflicto es inválido");

                if (site == ConflictSite.SocialConflict)
                {
                    if (await _socialConflictRepository.CountAsync(p => p.Id == conflictId) == 0)
                        throw new UserFriendlyException(DefaultTitleMessage, "El caso de conflictividad seleccionado no existe o ya no se encuentra disponible. Verifique antes de continuar");

                    var socialConflict = await _socialConflictRepository.GetAsync(conflictId);

                    input.SocialConflictId = socialConflict.Id;
                    input.SocialConflict = socialConflict;
                }

                if (site == ConflictSite.SocialConflictAlert)
                {
                    if (await _socialConflictAlertRepository.CountAsync(p => p.Id == conflictId) == 0)
                        throw new UserFriendlyException(DefaultTitleMessage, "La alerta seleccionada no existe o ya no se encuentra disponible. Verifique la información antes de continuar");

                    var socialConflictAlert = await _socialConflictAlertRepository.GetAsync(conflictId);

                    input.SocialConflictAlertId = socialConflictAlert.Id;
                    input.SocialConflictAlert = socialConflictAlert;
                }

                if (site == ConflictSite.SocialConflictSensible)
                {
                    if (await _socialConflictSensibleRepository.CountAsync(p => p.Id == conflictId) == 0)
                        throw new UserFriendlyException(DefaultTitleMessage, "La situación sensible seleccionada no existe o ya no se encuentra disponible. Verifique antes de continuar");

                    var socialConflictSensible = await _socialConflictSensibleRepository.GetAsync(conflictId);

                    input.SocialConflictSensibleId = socialConflictSensible.Id;
                    input.SocialConflictSensible = socialConflictSensible;
                }
            }

            if (input.SocialConflictId.HasValue == false && input.SocialConflictAlertId.HasValue == false && input.SocialConflictSensibleId.HasValue == false)
                throw new UserFriendlyException("Aviso", "La tarea debe estar relacionada a un conflicto. Verifique la información antes de continuar");

            input.Title.IsValidOrException(DefaultTitleMessage, "El título de la tarea es obligatorio");
            input.Title.VerifyTableColumn(SocialConflictTaskManagementConsts.TitleMinLength,
                SocialConflictTaskManagementConsts.TitleMaxLength,
                DefaultTitleMessage, 
                $"El título de la tarea no debe exceder los {SocialConflictTaskManagementConsts.TitleMaxLength} caracteres");

            input.Description.VerifyTableColumn(SocialConflictTaskManagementConsts.DescriptionMinLength,
                SocialConflictTaskManagementConsts.DescriptionMaxLength, 
                DefaultTitleMessage, 
                $"La descripción de la tarea no debe exceder los {SocialConflictTaskManagementConsts.DescriptionMaxLength} caracteres");

            if (input.Deadline.HasValue && input.Deadline.Value < DateTime.Now)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha de vencimiento no puede ser menor a la fecha actual");

            input.Persons = new List<SocialConflictTaskManagementPerson>();

            foreach (var person in persons.DistinctBy(p => p.Person.Id))
            {

                if (person.Remove)
                {
                    if (person.Id > 0 && input.Id > 0 && await _taskManagementPersonRepository.CountAsync(p => p.Id == person.Id && p.SocialConflictTaskManagementId == input.Id) > 0)
                    {
                        await _taskManagementPersonRepository.DeleteAsync(person.Id);
                    }
                }
                else
                {
                    if(input.Id == 0 || await _taskManagementPersonRepository.CountAsync(p => p.Id == person.Id && p.SocialConflictTaskManagementId == input.Id) == 0)
                    {
                        var dbPerson = _personRepository
                           .GetAll()
                           .Where(p => p.Id == person.Person.Id)
                           .FirstOrDefault();

                        if (dbPerson != null)
                        {
                            input.Persons.Add(new SocialConflictTaskManagementPerson()
                            {
                                Person = dbPerson,
                                PersonId = dbPerson.Id
                            });
                        }
                    }
                }
            }

            foreach (var resource in resources)
            {

                if (resource.Remove)
                {
                    if (resource.Id > 0 && input.Id > 0 && await _taskManagementResourceRepository.CountAsync(p => p.Id == resource.Id && p.SocialConflictTaskManagementId == input.Id) > 0)
                    {
                        await _taskManagementResourceRepository.DeleteAsync(resource.Id);
                    }
                }
            }

            return input;
        }

        private async Task<SocialConflictTaskManagementExtend> ValidateEntityTaskExtend(SocialConflictTaskManagementExtend input, int taskManagementId)
        {
            if (await _taskManagementRepository.CountAsync(p => p.Id == taskManagementId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "La tarea no existe o ya no se encuentra disponible");

            var taskManagement = await _taskManagementRepository.GetAsync(taskManagementId);

            input.Description.IsValidOrException(DefaultTitleMessage, "El motivo de la extensión es obligatorio");
            input.Description.VerifyTableColumn(SocialConflictTaskManagementConsts.DescriptionMinLength,
                SocialConflictTaskManagementConsts.DescriptionMaxLength, 
                DefaultTitleMessage, 
                $"La descripción de la tarea no debe exceder los {SocialConflictTaskManagementConsts.DescriptionMaxLength} caracteres");

            if (input.Deadline < DateTime.Now)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha de vencimiento no puede ser menor a la fecha actual");

            if (taskManagement.Deadline.HasValue && input.Deadline <= taskManagement.Deadline)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha de ampliación no puede ser menor a la fecha de vencimiento actual");

            if (input.Id == 0)
            {
                input.SocialConflictTaskManagement = taskManagement;
                input.SocialConflictTaskManagementId = taskManagement.Id;
            }

            return input;
        }

        private async Task<SocialConflictTaskManagementComment> ValidateEntityComment(SocialConflictTaskManagementComment input, int taskManagementId)
        {
            if (await _taskManagementRepository.CountAsync(p => p.Id == taskManagementId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "La tarea no existe o ya no se encuentra disponible");

            var taskManagement = await _taskManagementRepository.GetAsync(taskManagementId);

            input.Description.IsValidOrException(DefaultTitleMessage, "La descripción del comentario es obligatorio");
            input.Description.VerifyTableColumn(SocialConflictTaskManagementCommentConsts.DescriptionMinLength,
                SocialConflictTaskManagementCommentConsts.DescriptionMaxLength, DefaultTitleMessage, 
                $"La descripción del comentario no debe exceder los {SocialConflictTaskManagementCommentConsts.DescriptionMaxLength} caracteres");
            input.User = await GetCurrentUserAsync();
            input.Type = CommentType.USER;
            input.SocialConflictTaskManagement = taskManagement;
            input.SocialConflictTaskManagementId = taskManagement.Id;

            return input;
        }

        #endregion
    }
}
