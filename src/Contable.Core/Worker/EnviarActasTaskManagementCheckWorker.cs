using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using Abp.UI;
using Contable.Application;
using Contable.Application.Reports.Dto;
using Contable.Application.Utilities.Dto;
using Contable.Authorization.Users;
using Contable.Configuration;
using Contable.Editions;
using Contable.Manager.Base;
using Contable.Net.Emailing;
using Contable.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contable.Worker
{
    public class EnviarActasTaskManagementCheckWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private const int CheckPeriodAsMilliseconds = 10000;//1 * 60 * 60 * 1000; //1 hour

        private readonly IRepository<SocialConflictTaskManagement> _taskManagementRepository;
        private readonly IRepository<SocialConflictTaskManagementHistory> _socialConflictTaskManagementHistoryRepository;
        private readonly IProcedureRepository _procedureRepository;
        private readonly IAppEmailSender _appEmailSender;
        private readonly EmailAddressAttribute _emailValidator;

        private readonly IReportManagerBase _reportManagerBase;
        private readonly IRepository<Person> _personRepository;

        public EnviarActasTaskManagementCheckWorker(
            AbpTimer timer,
            IRepository<SocialConflictTaskManagement> taskManagementRepository,
            IRepository<SocialConflictTaskManagementHistory> socialConflictTaskManagementHistoryRepository,
            IProcedureRepository procedureRepository,
            IReportManagerBase reportManagerBase,
            IAppEmailSender appEmailSender,
            IRepository<Person> personRepository) : base(timer)
        {
            _taskManagementRepository = taskManagementRepository;
            _socialConflictTaskManagementHistoryRepository = socialConflictTaskManagementHistoryRepository;
            _procedureRepository = procedureRepository;
            _appEmailSender = appEmailSender;
            _emailValidator = new EmailAddressAttribute();

            Timer.Period = CheckPeriodAsMilliseconds;
            Timer.RunOnStart = true;

            LocalizationSourceName = ContableConsts.LocalizationSourceName;

            _reportManagerBase = reportManagerBase;
            _personRepository = personRepository;
        }

        protected override void DoWork()
        {
            AsyncHelper.RunSync(() => Run());
        }

        protected bool ExecuteActaTask()
        {
            bool ejecutar = false;

            string dateFormat = "HH:mm:ss";
            TimeSpan dateStart = TimeSpan.Parse(DateTime.Now.ToString(dateFormat));
            TimeSpan rangeStart = TimeSpan.Parse("21:12:00");
            TimeSpan rangeEnd = TimeSpan.Parse("21:12:00");

            if (dateStart.IsBetween(rangeStart, rangeEnd))
            {
                ejecutar = true;
            }
            return ejecutar;
        }

        public async Task Run()
        {
            var utcNow = DateTime.Now;
            var deadLine = DateTime.Now.AddDays(3);
            var creationTime = DateTime.Now.AddMinutes(-15);

            if (ExecuteActaTask())
            {

                try
                    {
                        var persons = _personRepository.GetAll().Include(p=>p.Type).Where(p=>p.AlertSend);

                        var personal = persons.Where(p => p.AlertSend).ToList();

                        if (!personal.Any()) throw new Exception("No hay registros del personal");

                        var toAddress = personal
                            .Where(p => _emailValidator.IsValid(p.EmailAddress))
                            .Select(p => p.EmailAddress)
                            .Distinct();



                        var toEmailAddresses = toAddress.ToArray();


                        try
                        {
                                if (toAddress.Count() > 0)
                                {
                                    var template = ContableConsts.SubjectAlertConflict;

                                    var attachments = new List<EmailAttachment>();

                                    var request = await _reportManagerBase.Create(new JasperReportRequest()
                                    {
                                        Name = ReportNames.ReporteActasAlert,
                                        Type = _reportManagerBase.GetType(ReportType.PDF),
                                        Parameters = new List<JasperReportParameter>()
                                        {
                                            new JasperReportParameter()
                                            {

                                            }
                                        }
                                    });

                                    if (request.Success == false)
                                        throw new UserFriendlyException(request.Exception.Error.Title, request.Exception.Error.Message);

                                    attachments.Add(new EmailAttachment()
                                    {
                                        Name = _reportManagerBase.CreateActasReportName(ReportType.PDF),
                                        Content = request.Report
                                    });

                                    await _appEmailSender.SendEmail(
                                        to: toEmailAddresses,
                                        cc: toEmailAddresses,
                                        subject: "Reporte de Actas",
                                        body: template,
                                        attachments: attachments.ToArray());

                                }

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    catch
                    {
                        throw;
                    }
            }
        }
    }
}
