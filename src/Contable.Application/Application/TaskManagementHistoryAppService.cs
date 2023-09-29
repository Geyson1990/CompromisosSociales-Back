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
using Contable.Application.TaskManagementHistories;
using Contable.Application.TaskManagementHistories.Dto;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_TaskManagement_History)]
    public class TaskManagementHistoryAppService : ContableAppServiceBase, ITaskManagementHistoryAppService
    {
        private readonly IRepository<TaskManagementHistory> _taskManagementHistoryRepository;
        private readonly IRepository<User, long> _userRepository;

        public TaskManagementHistoryAppService(
            IRepository<TaskManagementHistory> taskManagementHistoryRepository, 
            IRepository<User, long> userRepository)
        {
            _taskManagementHistoryRepository = taskManagementHistoryRepository;
            _userRepository = userRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement_History)]
        public async Task<TaskManagementHistoryGetDto> Get(EntityDto input)
        {
            VerifyCount(await _taskManagementHistoryRepository.CountAsync(p => p.Id == input.Id));

            var dbHistory = (from history in _taskManagementHistoryRepository
                    .GetAll()
                    .Where(p => p.Id == input.Id)
                             join userCreation in _userRepository.GetAll() on history.CreatorUserId equals userCreation.Id
                             into userResult
                             from userData in userResult.DefaultIfEmpty()
                             select new TaskManagementHistoryGetDto()
                             {
                                 Id = history.Id,
                                 CreatorUser = userData == null ? null : new TaskManagementHistoryUserDto()
                                 {
                                     Name = userData.Name,
                                     Surname = userData.Surname,
                                     EmailAddress = userData.EmailAddress
                                 },
                                 CreationTime = history.CreationTime,
                                 Subject = history.Subject,
                                 Template = history.Template,
                                 To = history.To,
                                 Copy = history.Copy
                             }).First();

            return dbHistory;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement_History)]
        public async Task<PagedResultDto<TaskManagementHistoryGetAllDto>> GetAll(TaskManagementHistoryGetAllInputDto input)
        {
            var query = (from history in _taskManagementHistoryRepository
                    .GetAll()
                    .LikeAllBidirectional(input.To.SplitByLike(), nameof(SocialConflictTaskManagementHistory.To))
                    .LikeAllBidirectional(input.Copy.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Copy))
                    .LikeAllBidirectional(input.Subject.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Subject))
                    .LikeAllBidirectional(input.Template.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Template))
                         join userCreation in _userRepository.GetAll() on history.CreatorUserId equals userCreation.Id
                         into userResult
                         from userData in userResult.DefaultIfEmpty()
                         select new TaskManagementHistoryGetAllDto()
                         {
                             Id = history.Id,
                             CreatorUser = userData == null ? null : new TaskManagementHistoryUserDto()
                             {
                                 Name = userData.Name,
                                 Surname = userData.Surname,
                                 EmailAddress = userData.EmailAddress
                             },
                             CreationTime = history.CreationTime,
                             Subject = history.Subject,
                             Template = history.Template,
                             To = history.To,
                             Copy = history.Copy
                         });

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<TaskManagementHistoryGetAllDto>(count, result.ToList());
        }
    }
}
