using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Contable.Application.TaskManagements;
using Contable.Application.TaskManagements.Dto;
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
using Contable.Configuration;
using Contable.Net.Emailing;
using System.ComponentModel.DataAnnotations;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
    public class TaskManagementAppService : ContableAppServiceBase, ITaskManagementAppService
    {
        private readonly IRepository<TaskManagement, long> _taskRepository;
        private readonly IRepository<TaskManagementPerson> _taskManagementPersonRepository;
        private readonly IRepository<TaskManagemetExtend, long> _taskExtendRepository;
        private readonly IRepository<TaskManagementHistory> _taskManagementHistoryRepository;
        private readonly IRepository<Comment, long> _commentRepository;
        private readonly IRepository<Compromise, long> _compromiseRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ITaskExcelExporter _taskExcelExporter;
        private readonly IAppEmailSender _appEmailSender;

        public TaskManagementAppService(IUnitOfWorkManager unitOfWorkManager, 
            IRepository<TaskManagement, long> taskRepository,
            IRepository<TaskManagementPerson> taskManagementPersonRepository,
            IRepository<TaskManagemetExtend, long> taskExtendRepository,
            IRepository<TaskManagementHistory> taskManagementHistoryRepository,
            IRepository<Comment, long> commentRepository,
            IRepository<Compromise, long> compromiseRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<Person> personRepository,
            ITaskExcelExporter taskExcelExporter,
            IAppEmailSender appEmailSender)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _taskRepository = taskRepository;
            _taskManagementPersonRepository = taskManagementPersonRepository;
            _taskExtendRepository = taskExtendRepository;
            _taskManagementHistoryRepository = taskManagementHistoryRepository;
            _compromiseRepository = compromiseRepository;
            _commentRepository = commentRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _personRepository = personRepository;
            _taskExcelExporter = taskExcelExporter;
            _appEmailSender = appEmailSender;
        }

        #region Task

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<PagedResultDto<TaskManagementCompromiseGetAllDto>> GetAllCompromises(TaskManagementCompromiseGetAllInputDto input)
        {
            var query = _compromiseRepository
                .GetAll()
                .Include(p => p.Status)
                .Include(p => p.Record)
                    .ThenInclude(p => p.SocialConflict)
                .Include(p => p.CompromiseLocations)
                    .ThenInclude(p => p.SocialConflictLocation)
                    .ThenInclude(p => p.TerritorialUnit)
                .Where(p => p.IsPriority)
                .WhereIf(input.Type.HasValue && input.Type > 0, p => p.Type == (input.Type.Value == 1 ? CompromiseType.PIP : CompromiseType.Activity))
                .WhereIf(input.CodeRecord.IsValid(), p => p.Record.Code.Contains(input.CodeRecord))
                .WhereIf(input.CodeSocialConflict.IsValid(), p => p.Record.SocialConflict.Code.Contains(input.CodeSocialConflict))
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0,
                    p => p.CompromiseLocations.Any(p => p.SocialConflictLocation.TerritorialUnit.Id == input.TerritorialUnitId))                    
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(Compromise.Filter));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var result = new List<TaskManagementCompromiseGetAllDto>();

            foreach (var compromise in output)
            {
                var compromiseItem = ObjectMapper.Map<TaskManagementCompromiseGetAllDto>(compromise);
                compromiseItem.TerritorialUnits = compromise.CompromiseLocations.Select(p => p.SocialConflictLocation.TerritorialUnit.Name).Distinct().JoinAsString(",");
                result.Add(compromiseItem);
            }

            return new PagedResultDto<TaskManagementCompromiseGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<TaskManagementCompromiseGetAllDto> GetCompromise(EntityDto<long> input)
        {
            VerifyCount(await _compromiseRepository.CountAsync(p => p.Id == input.Id && p.IsPriority));

            var compromise = _compromiseRepository
                .GetAll()
                .Include(p => p.Status)
                .Include(p => p.Record)
                    .ThenInclude(p => p.SocialConflict)
                .Include(p => p.CompromiseLocations)
                    .ThenInclude(p => p.SocialConflictLocation)
                    .ThenInclude(p => p.TerritorialUnit)
                .Where(p => p.Id == input.Id)
                .First();
         

            var compromiseItem = ObjectMapper.Map<TaskManagementCompromiseGetAllDto>(compromise);
            compromiseItem.TerritorialUnits = compromise.CompromiseLocations.Select(p => p.SocialConflictLocation.TerritorialUnit.Name).Distinct().JoinAsString(",");

            return compromiseItem;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<PagedResultDto<TaskManagementPersonGetAllDto>> GetAllPersons(TaskManagementPersonGetAllInputDto input)
        {
            var query =
                (from person in _personRepository.GetAll()
                 join task in _taskManagementPersonRepository.GetAll().Where(p => p.TaskManagementId == input.TaskManagementId) on person.Id equals task.PersonId into TaskData
                 from result in TaskData.DefaultIfEmpty()
                 select new TaskManagementPersonGetAllDto()
                 {
                     Id = person.Id,
                     Document = person.Document,
                     Name = person.Name,
                     EmailAddress = person.EmailAddress,
                     Type = person.Type,
                     Selected = result != null
                 }).LikeAnyBidirectional(input.Document.SplitByLike(), nameof(TaskManagementPersonGetAllDto.Document))
                 .LikeAnyBidirectional(input.Names.SplitByLike(), nameof(TaskManagementPersonGetAllDto.Name))
                 .LikeAnyBidirectional(input.EmailAddress.SplitByLike(), nameof(TaskManagementPersonGetAllDto.EmailAddress));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<TaskManagementPersonGetAllDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<TaskManagementPersonChangeOutputDto> PersonChanges(TaskManagementPersonChangeInputDto input)
        {
            VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskRepository.GetAsync(input.Id);

            foreach(var change in input.Changes.ToArray().DistinctBy(p => p.PersonId))
            {
                if(change.Checked)
                {
                    if(await _taskManagementPersonRepository.CountAsync(p => p.TaskManagementId == task.Id && p.PersonId == change.PersonId) == 0)
                    {
                        var dbPerson = _personRepository
                            .GetAll()
                            .Where(p => p.Id == change.PersonId)
                            .FirstOrDefault();

                        if (dbPerson != null)
                        {
                            await _taskManagementPersonRepository.InsertAsync(new TaskManagementPerson()
                            {
                                Person = dbPerson,
                                PersonId = dbPerson.Id,
                                TaskManagement = task,
                                TaskManagementId = task.Id
                            });
                        }
                    }
                } 
                else
                {
                    await _taskManagementPersonRepository.DeleteAsync(p => p.TaskManagementId == task.Id && p.PersonId == change.PersonId);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return new TaskManagementPersonChangeOutputDto()
            {
                Persons = ObjectMapper.Map<List<TaskManagementPersonRelationDto>>(_taskManagementPersonRepository
                    .GetAll()
                    .Include(p => p.Person)
                    .Where(p => p.TaskManagementId == task.Id))
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<TaskManagementGetDto> CreateTask(TaskManagementCreateDto input)
        {
            var taskManagement = await ValidateEntityTask(
                input: ObjectMapper.Map<TaskManagement>(input),
                compromiseId: input.Compromise.Id,
                persons: input.Persons == null ? new List<TaskManagementPersonRelationDto>() : input.Persons);

            VerifyStatusTask(taskManagement);

            var taskId = await _taskRepository.InsertAndGetIdAsync(taskManagement);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<TaskManagementGetDto>(_taskRepository
                .GetAll()
                .Include(p => p.Compromise)
                .Include(p => p.Comments)
                .ThenInclude(p => p.User)
                .Include(p => p.Persons)
                .ThenInclude(p => p.Person)
                .Where(p => p.Id == taskId)
                .First());
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task UpdateTask(TaskManagementUpdateDto input)
        {
            VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));
            TaskManagement taskManagement = await ValidateEntityTask(
                input: ObjectMapper.Map<TaskManagement>(input),
                compromiseId: input.Compromise.Id,
                persons: input.Persons == null ? new List<TaskManagementPersonRelationDto>() : input.Persons);

            VerifyStatusTask(taskManagement);
            await _taskRepository.UpdateAsync(taskManagement);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task DeleteTask(EntityDto<long> input)
        {
            VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));
            await _taskRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<TaskManagementGetDto> GetTask(NullableIdDto<long> input)
        {
            var output = new TaskManagementGetDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));

                var taskManagement = _taskRepository
                    .GetAll()
                    .Include(p => p.Compromise)
                    .Include(p => p.Comments)
                    .ThenInclude(p => p.User)
                    .Include(p => p.Persons)
                    .ThenInclude(p => p.Person)
                    .Where(p => p.Id == input.Id)
                    .First();

                output = ObjectMapper.Map<TaskManagementGetDto>(taskManagement);
            }
                        
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<PagedResultDto<TaskManagementGetAllDto>> GetTaskAll(TaskManagementGetAllInputDto input)
        {
            //TODO: reemplazar por un procedimiento almacenado
            var query = _taskRepository
               .GetAll()
               .Include(p => p.Compromise)
               .ThenInclude(p => p.Record)
               .ThenInclude(p => p.SocialConflict)  
               .Where(p => !p.Compromise.IsDeleted)
               .WhereIf(input.Compromise.HasValue, p => p.Compromise.Id == input.Compromise.Value)
               .WhereIf(input.Status != TaskStatus.None, p => p.Status == input.Status)
               .WhereIf(input.Title.IsValid(), p => p.Title.Contains(input.Title))
               .WhereIf(input.Description.IsValid(), p => p.Description.Contains(input.Description))
               .WhereIf(input.CompromiseName.IsValid(), p => p.Compromise.Name.Contains(input.CompromiseName));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var result = new List<TaskManagementGetAllDto>();

            foreach (var item in output)
            {
                var taskItem = ObjectMapper.Map<TaskManagementGetAllDto>(item);
                taskItem.CompromiseName = item.Compromise.Name;                
                taskItem.CompromiseCode = item.Compromise.Code;
                taskItem.CompromiseId = item.Compromise.Id;
                taskItem.RecordCode = item.Compromise.Record.Code;
                taskItem.CaseCode = item.Compromise.Record.SocialConflict.Code;
                taskItem.Advance = await _commentRepository.GetAll()
                    .Where(p => p.TaskManagement.Id == item.Id && !p.IsDeleted)                    
                    .OrderByDescending(p => p.CreationTime)
                    .Select(p => p.Description)
                    .FirstOrDefaultAsync();

                taskItem.Alert = GenerateAlert(taskItem.Deadline);

                result.Add(taskItem);
            }

            return new PagedResultDto<TaskManagementGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task CreateTaskExtend(TaskManagementExtendCreateDto input)
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                TaskManagemetExtend taskManagemetExtend = await ValidateEntityTaskExtend(ObjectMapper.Map<TaskManagemetExtend>(input), input.TaskManagement.Id);
                await _taskExtendRepository.InsertAsync(taskManagemetExtend);

                taskManagemetExtend.TaskManagement.Deadline = taskManagemetExtend.Deadline;
                taskManagemetExtend.TaskManagement.SendedDeadline = false;

                await _taskRepository.UpdateAsync(taskManagemetExtend.TaskManagement);

                unitOfWork.Complete();
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<TaskManagementExtendGetDto> GetTaskExtend(NullableIdDto input)
        {
            var output = new TaskManagementExtendGetDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));
                output = ObjectMapper.Map<TaskManagementExtendGetDto>(await _taskRepository.GetAsync(input.Id.Value));                
            }
            
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<PagedResultDto<TaskManagementExtendGetAllDto>> GetAllTaskExtend(TaskManagementExtendGetAllInputDto input)
        {
            var query = _taskExtendRepository
               .GetAll()                              
               .Where(p => !p.TaskManagement.IsDeleted)
               .WhereIf(input.TaskManagementId.HasValue, p => p.TaskManagement.Id == input.TaskManagementId);

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            
            return new PagedResultDto<TaskManagementExtendGetAllDto>(count, ObjectMapper.Map<List<TaskManagementExtendGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task DeleteTaskExtend(EntityDto<long> input)
        {
            VerifyCount(await _taskExtendRepository.CountAsync(p => p.Id == input.Id));
            await _taskExtendRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task ChangeStateToPending(EntityDto<long> input)
        {
            VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskRepository.GetAsync(input.Id);
            task.Status = TaskStatus.Pending;

            await _taskRepository.UpdateAsync(task);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task ChangeStateToNonComplete(EntityDto<long> input)
        {
            VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskRepository.GetAsync(input.Id);
            task.Status = TaskStatus.NonCompleted;

            await _taskRepository.UpdateAsync(task);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task ChangeStateToComplete(EntityDto<long> input)
        {
            VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskRepository.GetAsync(input.Id);
            task.Status = TaskStatus.Completed;

            await _taskRepository.UpdateAsync(task);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<TaskManagementEmailConfigurationDto> GetEmailConfiguration(EntityDto<long> input)
        {
            VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));

            var task = _taskRepository
                .GetAll()
                .Where(p => p.Id == input.Id)
                .First();

            var persons = await FunctionManager.CallTaskManagementGetAllPersons(task.Id);
            var coordinators = persons.Where(p => p.Type == PersonType.Coordinator).ToList();
            var managers = persons.Where(p => p.Type == PersonType.Manager).ToList();

            var subject = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.TaskManagementAlertNotificationSubject, ContableConsts.DefaultTenantId);
            var template = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.TaskManagementAlertTemplate, ContableConsts.DefaultTenantId);

            template = _appEmailSender.CreateTaskTemplate(template, task, coordinators, managers);

            return new TaskManagementEmailConfigurationDto()
            {
                Id = task.Id,
                Subject = subject,
                Template = template
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task SendNotification(TaskManagementSendNotificationDto input)
        {
            VerifyCount(await _taskRepository.CountAsync(p => p.Id == input.Id));

            var task = await _taskRepository.GetAsync(input.Id);

            var emailValidator = new EmailAddressAttribute();

            foreach (var emailAddress in input.To)
                if (emailValidator.IsValid(emailAddress) == false)
                    throw new UserFriendlyException("Aviso", $"La dirección de correo electrónico en destinatarios \"Para\": {emailAddress} es inválida");
            foreach (var emailAddress in input.Copy)
                if (emailValidator.IsValid(emailAddress) == false)
                    throw new UserFriendlyException("Aviso", $"La dirección de correo electrónico en destinatarios \"Copia a\": {emailAddress} es inválida");

            await _taskManagementHistoryRepository.InsertAsync(new TaskManagementHistory()
            {
                TaskManagementId = task.Id,
                TaskManagement = task,
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

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<TaskManagementCommentGetDto> CreateComment(TaskManagementCommentCreateDto input)
        {
            var comment = ObjectMapper.Map<Comment>(input);
            comment.Type = CommentType.USER;

            var output = await _commentRepository.InsertAsync(await ValidateEntityComment(comment, input.TaskManagement.Id));

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<TaskManagementCommentGetDto>(output);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task UpdateComment(TaskManagementCommentUpdateDto input)
        {
            VerifyCount(await _commentRepository.CountAsync(p => p.Id == input.Id));
            Comment comment = await ValidateEntityComment(ObjectMapper.Map<Comment>(input), input.TaskManagement.Id);
            await _commentRepository.UpdateAsync(comment);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task DeleteComment(EntityDto<long> input)
        {
            VerifyCount(await _commentRepository.CountAsync(p => p.Id == input.Id));
            await _commentRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<PagedResultDto<TaskManagementCommentGetAllDto>> GetAllComment(TaskManagementCommentGetAllInputDto input)
        {
            var query = _commentRepository
              .GetAll()
              .Where(p => !p.TaskManagement.IsDeleted)
              .WhereIf(input.TaskManagementId.HasValue, p => p.TaskManagement.Id == input.TaskManagementId);

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<TaskManagementCommentGetAllDto>(count, ObjectMapper.Map<List<TaskManagementCommentGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<TaskManagementCommentGetDto> GetComment(NullableIdDto input)
        {
            var output = new TaskManagementCommentGetDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _commentRepository.CountAsync(p => p.Id == input.Id));
                output = ObjectMapper.Map<TaskManagementCommentGetDto>(await _commentRepository.GetAsync(input.Id ?? 0));
            }

            return output;
        }

        #endregion

        #region Export

        [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement)]
        public async Task<FileDto> GetMatrixToExcel(TaskManagementGetAllExcellInputDto input)
        {
            var query = _taskRepository
                .GetAll()
                .Include(p => p.Compromise)
                .ThenInclude(p => p.PIPMEF)
                .Include(p => p.Comments)
                .Where(p => !p.Compromise.IsDeleted)
                .WhereIf(input.Compromise.HasValue, p => p.Compromise.Id == input.Compromise.Value)
                .WhereIf(input.Status != TaskStatus.None, p => p.Status == input.Status)
                .WhereIf(input.Description.IsValid(), p => p.Description.Contains(input.Description))
                .WhereIf(input.CompromiseName.IsValid(), p => p.Compromise.Name.Contains(input.CompromiseName));

            var output = await query.OrderBy(input.Sorting).ToListAsync();

            var result = new List<TaskManagementGetMatrixExcelDto>();

            foreach (var item in output)
            {
                var taskItem = ObjectMapper.Map<TaskManagementGetMatrixExcelDto>(item);
                taskItem.CompromiseName = item.Compromise.Name;
                taskItem.CompromiseCode = item.Compromise.Code;
                if (item.Compromise.IsPriority)
                    taskItem.PIPMEF = ObjectMapper.Map<PIPMEFDto>(item.Compromise.PIPMEF);
                else
                    taskItem.PIPMEF = null;

                taskItem.Advance = await _commentRepository.GetAll()
                    .Where(p => p.TaskManagement.Id == item.Id && !p.IsDeleted)
                    .OrderByDescending(p => p.CreationTime)
                    .Select(p => p.Description)
                    .FirstOrDefaultAsync();

                taskItem.Alert = GenerateAlert(taskItem.Deadline);

                result.Add(taskItem);
            }

            return _taskExcelExporter.ExportMatrixToFile(result);

        }

        #endregion

        #region Validate      

        private async Task<TaskManagement> ValidateEntityTask(TaskManagement input, long compromiseId, List<TaskManagementPersonRelationDto> persons)
        {
            if (await _compromiseRepository.CountAsync(p => p.Id == compromiseId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El compromiso no existe o ya no se encuentra disponible");

            input.Title.IsValidOrException(DefaultTitleMessage, "El título de la tarea es obligatorio");
            input.Title.VerifyTableColumn(TaskManagementConsts.TitleMinLength, TaskManagementConsts.TitleMaxLength, DefaultTitleMessage, $"El título de la tarea no debe exceder los {TaskManagementConsts.TitleMaxLength} caracteres");

            input.Description.VerifyTableColumn(TaskManagementConsts.DescriptionMinLength, TaskManagementConsts.DescriptionMaxLength, DefaultTitleMessage, $"La descripción de la tarea no debe exceder los {TaskManagementConsts.DescriptionMaxLength} caracteres");

            if (input.Deadline.HasValue && input.Deadline.Value < DateTime.Now)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha de vencimiento no puede ser menor a la fecha actual");

            input.Compromise = await _compromiseRepository.GetAsync(compromiseId);

            if(input.Id == 0 && !input.Compromise.IsPriority)
                throw new UserFriendlyException(DefaultTitleMessage, "No se puede crear tareas en compromisos No Priorizados");

            input.Persons = new List<TaskManagementPerson>();

            foreach (var person in persons.DistinctBy(p => p.Person.Id))
            {
                if (person.Remove)
                {
                    if (person.Id > 0 && input.Id > 0 && await _taskManagementPersonRepository.CountAsync(p => p.Id == person.Id && p.TaskManagementId == input.Id) > 0)
                    {
                        await _taskManagementPersonRepository.DeleteAsync(person.Id);
                    }
                }
                else
                {
                    if (input.Id == 0 || await _taskManagementPersonRepository.CountAsync(p => p.Id == person.Id && p.TaskManagementId == input.Id) == 0)
                    {
                        var dbPerson = _personRepository
                           .GetAll()
                           .Where(p => p.Id == person.Person.Id)
                           .FirstOrDefault();

                        if (dbPerson != null)
                        {
                            input.Persons.Add(new TaskManagementPerson()
                            {
                                Person = dbPerson,
                                PersonId = dbPerson.Id
                            });
                        }
                    }
                }
            }

            return input;
        }

        private async Task<TaskManagemetExtend> ValidateEntityTaskExtend(TaskManagemetExtend input, long taskId)
        {
            if (await _taskRepository.CountAsync(p => p.Id == taskId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "La tarea no existe o ya no se encuentra disponible");

            input.Description.IsValidOrException(DefaultTitleMessage, "El motivo de la extensión es obligatorio");
            input.Description.VerifyTableColumn(TaskManagementConsts.DescriptionMinLength, TaskManagementConsts.DescriptionMaxLength, DefaultTitleMessage, $"La descripción de la tarea no debe exceder los {TaskManagementConsts.DescriptionMaxLength} caracteres");

            if (input.Deadline.HasValue && input.Deadline.Value < DateTime.Now)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha de vencimiento no puede ser menor a la fecha actual");

            input.TaskManagement = await _taskRepository.GetAsync(taskId);

            if(input.Deadline.Value <= input.TaskManagement.Deadline)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha de ampliación no puede ser menor a la fecha de vencimiento actual");

            return input;
        }

        private async Task<Comment> ValidateEntityComment(Comment input, long taskId)
        {
            if (await _taskRepository.CountAsync(p => p.Id == taskId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "La tarea no existe o ya no se encuentra disponible");

            input.Description.IsValidOrException(DefaultTitleMessage, "La descripción de la tarea es obligatorio");
            input.Description.VerifyTableColumn(CommentsConst.DescriptionMinLength, CommentsConst.DescriptionMaxLength, DefaultTitleMessage, $"La descripción de la tarea no debe exceder los {CommentsConst.DescriptionMaxLength} caracteres");
            input.TaskManagement = await _taskRepository.GetAsync(taskId);
            input.User = await GetCurrentUserAsync();

            return input;
        }

        private string GenerateAlert(DateTime? Deadline)
        {
            var alert = "";
            if (Deadline != null)
            {
                var difDays = (Deadline - DateTime.Now).Value.Days;
                if (difDays > 15)
                    alert = "GREEN";
                else if (difDays > 7)
                    alert = "AMBER";
                else
                    alert = "RED";
            }
            return alert;
        }

        private void VerifyStatusTask(TaskManagement task)
        {
            if (task.Id == 0)
                task.Status = TaskStatus.Pending;
            if (task.Deadline.HasValue && task.Deadline < DateTime.Now && task.Status == TaskStatus.Pending)
                task.Status = TaskStatus.NonCompleted;
        }

        #endregion

    }
}
