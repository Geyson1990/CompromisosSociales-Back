using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using Contable.Application;
using Contable.Application.Utilities.Dto;
using Contable.Authorization.Users;
using Contable.Configuration;
using Contable.Editions;
using Contable.Net.Emailing;
using Contable.Repositories;

namespace Contable.Worker
{
    public class SocialConflictTaskManagementCheckWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private const int CheckPeriodAsMilliseconds = 10000;//1 * 60 * 60 * 1000; //1 hour

        private readonly IRepository<SocialConflictTaskManagement> _taskManagementRepository;
        private readonly IRepository<SocialConflictTaskManagementHistory> _socialConflictTaskManagementHistoryRepository;
        private readonly IProcedureRepository _procedureRepository;
        private readonly IAppEmailSender _appEmailSender;
        private readonly EmailAddressAttribute _emailValidator;

        public SocialConflictTaskManagementCheckWorker(
            AbpTimer timer, 
            IRepository<SocialConflictTaskManagement> taskManagementRepository,
            IRepository<SocialConflictTaskManagementHistory> socialConflictTaskManagementHistoryRepository,
            IProcedureRepository procedureRepository,
            IAppEmailSender appEmailSender) : base(timer)
        {
            _taskManagementRepository = taskManagementRepository;
            _socialConflictTaskManagementHistoryRepository = socialConflictTaskManagementHistoryRepository;
            _procedureRepository = procedureRepository;
            _appEmailSender = appEmailSender;
            _emailValidator = new EmailAddressAttribute();

            Timer.Period = CheckPeriodAsMilliseconds;
            Timer.RunOnStart = true;

            LocalizationSourceName = ContableConsts.LocalizationSourceName;
        }

        protected override void DoWork()
        {
            AsyncHelper.RunSync(() => Run());
        }

        public async Task Run()
        {
            var utcNow = DateTime.Now;
            var deadLine = DateTime.Now.AddDays(3);
            var creationTime = DateTime.Now.AddMinutes(-15);

            if (utcNow.Hour > 8 && utcNow.Hour < 20)
            {

                var tasks = _taskManagementRepository.GetAllList(
                    task => (task.SendedCreation == false ||
                            (task.SendedDeadline == false && task.Deadline.HasValue && task.Deadline < deadLine)) &&
                            task.CreationTime < creationTime
                );

                var socialConflictTaskManagementAlertSubject = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.SocialConflictTaskManagementAlertSubject, ContableConsts.DefaultTenantId);
                var socialConflictTaskManagementAlertExpirationSubject = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.SocialConflictTaskManagementAlertExpirationSubject, ContableConsts.DefaultTenantId);
                var socialConflictTaskManagementAlertTemplate = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.SocialConflictTaskManagementAlertTemplate, ContableConsts.DefaultTenantId);

                foreach (var task in tasks)
                {
                    try
                    {
                        var persons = await _procedureRepository.CallSocialConflictTaskManagementGetAllPersons(task.Id);

                        var coordinators = persons.Where(p => p.Type == PersonType.Coordinator).ToList();
                        var managers = persons.Where(p => p.Type == PersonType.Manager).ToList();

                        var toAddress = coordinators
                            .Where(p => _emailValidator.IsValid(p.EmailAddress))
                            .Select(p => p.EmailAddress)
                            .Distinct();

                        var copyAddress = managers
                            .Where(p => _emailValidator.IsValid(p.EmailAddress))
                            .Select(p => p.EmailAddress)
                            .Where(p => toAddress.Any(d => d == p) == false)
                            .Distinct();

                        var toEmailAddresses = toAddress.Count() == 0 ? copyAddress.ToArray() : toAddress.ToArray();
                        var copyEmailAddresses = toAddress.Count() == 0 ? Array.Empty<string>() : copyAddress.ToArray();

                        try
                        {
                            if (task.SendedCreation == false)
                            {
                                if (toAddress.Count() > 0 || copyAddress.Count() > 0)
                                {
                                    var template = _appEmailSender.CreateSocialConflictTaskTemplate(socialConflictTaskManagementAlertTemplate, task, coordinators, managers);

                                    await _appEmailSender.SendEmail(
                                        to: toEmailAddresses,
                                        cc: copyEmailAddresses,
                                        subject: socialConflictTaskManagementAlertSubject,
                                        body: template,
                                        attachments: null);

                                    _socialConflictTaskManagementHistoryRepository.Insert(new SocialConflictTaskManagementHistory()
                                    {
                                        SocialConflictTaskManagementId = task.Id,
                                        SocialConflictTaskManagement = task,
                                        Subject = socialConflictTaskManagementAlertSubject,
                                        Template = template,
                                        To = string.Join(";", toEmailAddresses),
                                        Copy = string.Join(";", copyEmailAddresses)
                                    });
                                }

                                task.SendedCreation = true;
                                _taskManagementRepository.Update(task);
                            }
                        }
                        catch
                        {

                        }

                        try
                        {
                            if (task.SendedDeadline == false && task.Deadline.HasValue && task.Deadline < deadLine)
                            {
                                if (toAddress.Count() > 0 || copyAddress.Count() > 0)
                                {
                                    var template = _appEmailSender.CreateSocialConflictTaskTemplate(socialConflictTaskManagementAlertTemplate, task, coordinators, managers);

                                    await _appEmailSender.SendEmail(
                                        to: toEmailAddresses,
                                        cc: copyEmailAddresses,
                                        subject: socialConflictTaskManagementAlertExpirationSubject,
                                        body: template,
                                        attachments: null);

                                    _socialConflictTaskManagementHistoryRepository.Insert(new SocialConflictTaskManagementHistory()
                                    {
                                        SocialConflictTaskManagementId = task.Id,
                                        SocialConflictTaskManagement = task,
                                        Subject = socialConflictTaskManagementAlertExpirationSubject,
                                        Template = template,
                                        To = string.Join(";", toEmailAddresses),
                                        Copy = string.Join(";", copyEmailAddresses)
                                    });
                                }

                                task.SendedDeadline = true;
                                _taskManagementRepository.Update(task);
                            }
                        }
                        catch
                        {

                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
