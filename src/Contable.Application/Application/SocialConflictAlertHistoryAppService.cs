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
using Contable.Net.Emailing;
using System.ComponentModel.DataAnnotations;
using Contable.Application.Reports;
using Microsoft.AspNetCore.Mvc;
using Contable.Application.Reports.Dto;
using Contable.Configuration;
using Contable.Application.SocialConflictAlertHistories;
using Contable.Application.SocialConflictAlertHistories.Dto;
using Contable.Application.Exporting;
using Contable.Dto;
using Contable.Manager;
using Newtonsoft.Json;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_History)]
    public class SocialConflictAlertHistoryAppService : ContableAppServiceBase, ISocialConflictAlertHistoryAppService
    {
        private readonly IRepository<SocialConflictAlertHistory> _socialConflictAlertHistoryRepository;
        private readonly ISocialConflictAlertHistoryExcelExporter _socialConflictAlertHistoryExcelExporter;
        private readonly IRepository<User, long> _userRepository;

        public SocialConflictAlertHistoryAppService(
            IRepository<SocialConflictAlertHistory> socialConflictAlertHistoryRepository, 
            ISocialConflictAlertHistoryExcelExporter socialConflictAlertHistoryExcelExporter,
            IRepository<User, long> userRepository)
        {
            _socialConflictAlertHistoryRepository = socialConflictAlertHistoryRepository;
            _socialConflictAlertHistoryExcelExporter = socialConflictAlertHistoryExcelExporter;
            _userRepository = userRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_History)]
        public async Task<SocialConflictAlertHistoryGetDto> Get(EntityDto input)
        {
            VerifyCount(await _socialConflictAlertHistoryRepository.CountAsync(p => p.Id == input.Id));

            var dbHistory = (from history in _socialConflictAlertHistoryRepository
                    .GetAll()
                    .Where(p => p.Id == input.Id)
             join userCreation in _userRepository.GetAll() on history.CreatorUserId equals userCreation.Id
             into userResult
             from userData in userResult.DefaultIfEmpty()
             select new SocialConflictAlertHistoryGetDto()
             {
                 Id = history.Id,
                 CreatorUser = userData == null ? null : new SocialConflictAlertHistoryUserDto()
                 {
                     Name = userData.Name,
                     Surname = userData.Surname,
                     EmailAddress = userData.EmailAddress
                 },
                 CreationTime = history.CreationTime,
                 Code = history.Code,
                 Subject = history.Subject,
                 Template = history.Template,
                 To = history.To,
                 Copy = history.Copy,
                 Files = history.Files
             }).First();

            return dbHistory;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_History)]
        public async Task<PagedResultDto<SocialConflictAlertHistoryGetAllDto>> GetAll(SocialConflictAlertHistoryGetAllInputDto input)
        {
            var query = (from history in _socialConflictAlertHistoryRepository
                    .GetAll()
                    .LikeAllBidirectional(input.Code.SplitByLike(), nameof(SocialConflictAlertHistory.Code))
                    .LikeAllBidirectional(input.To.SplitByLike(), nameof(SocialConflictAlertHistory.To))
                    .LikeAllBidirectional(input.Copy.SplitByLike(), nameof(SocialConflictAlertHistory.Copy))
                    .LikeAllBidirectional(input.Subject.SplitByLike(), nameof(SocialConflictAlertHistory.Subject))
                    .LikeAllBidirectional(input.Template.SplitByLike(), nameof(SocialConflictAlertHistory.Template))
                 join userCreation in _userRepository.GetAll() on history.CreatorUserId equals userCreation.Id
                 into userResult
                 from userData in userResult.DefaultIfEmpty()
                 select new SocialConflictAlertHistoryGetAllDto()
                 {
                     Id = history.Id,
                     CreatorUser = userData == null ? null : new SocialConflictAlertHistoryUserDto()
                     {
                         Name = userData.Name,
                         Surname = userData.Surname,
                         EmailAddress = userData.EmailAddress
                     },
                     CreationTime = history.CreationTime,
                     Code = history.Code,
                     Subject = history.Subject,
                     Template = history.Template,
                     To = history.To,
                     Copy = history.Copy,
                     Files = history.Files
                 });

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<SocialConflictAlertHistoryGetAllDto>(count, result.ToList());
        }

        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert_History)]
        public async Task<FileDto> GetMatrizToExcel(SocialConflictAlertHistoryGetAllInputDto input)
        {
            var query = (from history in _socialConflictAlertHistoryRepository
                    .GetAll()
                    .LikeAllBidirectional(input.Code.SplitByLike(), nameof(SocialConflictAlertHistory.Code))
                    .LikeAllBidirectional(input.To.SplitByLike(), nameof(SocialConflictAlertHistory.To))
                    .LikeAllBidirectional(input.Copy.SplitByLike(), nameof(SocialConflictAlertHistory.Copy))
                    .LikeAllBidirectional(input.Subject.SplitByLike(), nameof(SocialConflictAlertHistory.Subject))
                    .LikeAllBidirectional(input.Template.SplitByLike(), nameof(SocialConflictAlertHistory.Template))
                         join userCreation in _userRepository.GetAll() on history.CreatorUserId equals userCreation.Id
                         into userResult
                         from userData in userResult.DefaultIfEmpty()
                         select new SocialConflictAlertHistoryMatrizExportDto()
                         {
                             CreatorUser = userData == null ? "" : userData.GetNameSurname(),
                             CreationTime = history.CreationTime,
                             Date = history.CreationTime.ToString("dd/MM/yyyy"),
                             Time = history.CreationTime.ToString("HH:mm"),
                             Code = history.Code,
                             Subject = history.Subject,
                             Template = history.Template,
                             To = history.To,
                             Copy = history.Copy,
                             Files = history.Files
                         });

            var data = await query.OrderBy(input.Sorting).ToListAsync();

            var request = await ReportManager.Create(new JasperReportRequest()
            {
                Name = ReportNames.SocialConflictAlertHistory,
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

            return _socialConflictAlertHistoryExcelExporter.ExportMatrizToFile(request.Report);
        }
    }
}
