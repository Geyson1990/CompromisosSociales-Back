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
using Contable.Application.SocialConflictTaskManagementHistories;
using Contable.Application.SocialConflictTaskManagementHistories.Dto;
using Contable.Application.SocialConflictAlertHistories.Dto;
using Contable.Dto;
using NPOI.POIFS.Crypt.Dsig;
using Contable.Application.Exporting;
using Newtonsoft.Json;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement_History)]
    public class SocialConflictTaskManagementHistoryAppService : ContableAppServiceBase, ISocialConflictTaskManagementHistory
    {
        private readonly IRepository<SocialConflictTaskManagementHistory> _socialConflictTaskManagementHistoryRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly ISocialConflictTaskManagementHistoryExcelExporter _excelExporter;

        public SocialConflictTaskManagementHistoryAppService(
            IRepository<SocialConflictTaskManagementHistory> socialConflictTaskManagementHistoryRepository, 
            IRepository<User, long> userRepository,
            ISocialConflictTaskManagementHistoryExcelExporter excelExporter)
        {
            _socialConflictTaskManagementHistoryRepository = socialConflictTaskManagementHistoryRepository;
            _userRepository = userRepository;
            _excelExporter = excelExporter;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement_History)]
        public async Task<SocialConflictTaskManagementHistoryGetDto> Get(EntityDto input)
        {
            VerifyCount(await _socialConflictTaskManagementHistoryRepository.CountAsync(p => p.Id == input.Id));

            var dbHistory = (from history in _socialConflictTaskManagementHistoryRepository
                    .GetAll()
                    .Where(p => p.Id == input.Id)
                             join userCreation in _userRepository.GetAll() on history.CreatorUserId equals userCreation.Id
                             into userResult
                             from userData in userResult.DefaultIfEmpty()
                             select new SocialConflictTaskManagementHistoryGetDto()
                             {
                                 Id = history.Id,
                                 CreatorUser = userData == null ? null : new SocialConflictTaskManagementHistoryUserDto()
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
        public async Task<PagedResultDto<SocialConflictTaskManagementHistoryGetAllDto>> GetAll(SocialConflictTaskManagementHistoryGetAllInputDto input)
        {
            var query = (from history in _socialConflictTaskManagementHistoryRepository
                    .GetAll()
                    .LikeAllBidirectional(input.To.SplitByLike(), nameof(SocialConflictTaskManagementHistory.To))
                    .LikeAllBidirectional(input.Copy.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Copy))
                    .LikeAllBidirectional(input.Subject.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Subject))
                    .LikeAllBidirectional(input.Template.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Template))
                         join userCreation in _userRepository.GetAll() on history.CreatorUserId equals userCreation.Id
                         into userResult
                         from userData in userResult.DefaultIfEmpty()
                         select new SocialConflictTaskManagementHistoryGetAllDto()
                         {
                             Id = history.Id,
                             CreatorUser = userData == null ? null : new SocialConflictTaskManagementHistoryUserDto()
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

            return new PagedResultDto<SocialConflictTaskManagementHistoryGetAllDto>(count, result.ToList());
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement_History)]
        public async Task<FileDto> GetMatrizToExcel(SocialConflictTaskManagementHistoryGetAllInputDto input)
        {
            var query = (from history in _socialConflictTaskManagementHistoryRepository
                    .GetAll()
                    .LikeAllBidirectional(input.To.SplitByLike(), nameof(SocialConflictTaskManagementHistory.To))
                    .LikeAllBidirectional(input.Copy.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Copy))
                    .LikeAllBidirectional(input.Subject.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Subject))
                    .LikeAllBidirectional(input.Template.SplitByLike(), nameof(SocialConflictTaskManagementHistory.Template))
                                     join userCreation in _userRepository.GetAll() on history.CreatorUserId equals userCreation.Id
                                     into userResult
                                     from userData in userResult.DefaultIfEmpty()
                                     select new SocialConflictTaskManagementHistoryGetMatrixExcelDto()
                                     {
                                         Id = history.Id,
                                         CreatorUser = userData == null ? "" : userData.GetNameSurname(),
                                         Date = history.CreationTime.ToString("dd/MM/yyyy"),
                                         Time = history.CreationTime.ToString("HH:mm"),
                                         CreationTime = history.CreationTime,
                                         Subject = history.Subject,
                                         Template = history.Template,
                                         To = history.To,
                                         Copy = history.Copy
                                     });

            var count = await query.CountAsync();
            var data = await query.OrderBy(input.Sorting).ToListAsync();

            var request = await ReportManager.Create(new JasperReportRequest()
            {
                Name = ReportNames.SocialConflictTaskHistory,
                Type = ReportManager.GetType(ReportType.XLSX),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "Data",
                        Value = JsonConvert.SerializeObject(data)
                    }
                }
            });

            if (request.Success == false)
                throw new UserFriendlyException(request.Exception.Error.Title, request.Exception.Error.Message);

            return _excelExporter.ExportMatrixToFile(request.Report);
        }
    }
}
