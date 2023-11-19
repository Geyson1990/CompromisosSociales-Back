using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using Castle.Core.Logging;
using Contable.Application;
using Contable.Application.Reports.Dto;
using Contable.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Manager.Base
{
    public class ReportManagerBase : IDomainService, ITransientDependency, IReportManagerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IRepository<Report> _reportRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly string[] ReportTypes = { "PDF", "XLSX", "DOCX", "HTML" };

        public ReportManagerBase(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ILogger logger, IRepository<Report> reportRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _configurationRoot = _webHostEnvironment.GetAppConfiguration();
            _reportRepository = reportRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<JasperDocument> Create(JasperReportRequest input)
        {
            if (ReportServerEnabled() == false)
                throw new UserFriendlyException("Aviso", "La funcionalidad de reportes esta deshabilitada");
            if (string.IsNullOrWhiteSpace(input.Name))
                throw new UserFriendlyException("Aviso", "El nombre del reporte es obligatorio");
            if (string.IsNullOrWhiteSpace(input.Type))
                throw new UserFriendlyException("Aviso", "El formato del reporte es obligatorio");
            if (ReportTypes.Where(p => p == input.Type).Count() == 0)
                throw new UserFriendlyException("Aviso", "El formato del reporte solicitado es inválido");

            var report = _reportRepository
                .GetAll()
                .Where(p => p.Name.Equals(input.Name.Trim()))
                .FirstOrDefault();

            if (report == null)
                throw new UserFriendlyException("Aviso", "El reporte solicitado es inválido");
            if (report.Enabled == false)
                throw new UserFriendlyException("Aviso", "El reporte solicitado se encuentra deshabilitado");

            using var client = new HttpClient();

            try
            {
                var awtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

                client.BaseAddress = new Uri(_configurationRoot["ReportServer:Url"]);
                client.Timeout = TimeSpan.FromMinutes(120.00);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", awtToken);

                var json = JsonConvert.SerializeObject(input, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                var result = await client.PostAsync("/api/services/app/Report/Create", new StringContent(json, Encoding.UTF8, "application/json"));

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return new JasperDocument()
                    {
                        Report = await result.Content.ReadAsByteArrayAsync(),
                        Success = true
                    };
                }

                return new JasperDocument()
                {
                    Success = false,
                    Exception = JsonConvert.DeserializeObject<JasperException>(await result.Content.ReadAsStringAsync())
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Exception invoking report server", ex);
                throw new UserFriendlyException("Aviso", "Ha ocurrido un error al procesar la solicitud de generación del reporte");
            }
        }

        public bool ReportServerEnabled()
        {
            return bool.Parse(_configurationRoot["ReportServer:IsEnabled"]);
        }


        public string GetType(ReportType type)
        {
            switch (type)
            {
                case ReportType.PDF: return "PDF";
                case ReportType.HTML: return "HTML";
                case ReportType.XLSX: return "XLSX";
                case ReportType.DOCX: return "DOCX";
                default: return "PDF";
            }
        }

        public string CreateSocialConflictReportName(SocialConflict socialConflict, ReportType type)
        {
            return @$"CASO_SGSD_{(socialConflict.Generation ? $"{(socialConflict.Count < 10 ? "0" : "")}{socialConflict.Count}_{socialConflict.Year}" : $"{socialConflict.Id}")}.{GetType(type).ToLower()}";
        }

        public string CreateAlertReportName(SocialConflictAlert alert, ReportType type)
        {
            return @$"ALERTA_SGSD_{(alert.Generation ? $"{(alert.Count < 10 ? "0" : "")}{alert.Count}_{alert.Year}" : $"{alert.Id}")}.{GetType(type).ToLower()}";
        }

        public string CreateAlertResumeReportName(SocialConflictAlert alert, ReportType type)
        {
            return @$"RE_ALERTA_SGSD_{(alert.Generation ? $"{(alert.Count < 10 ? "0" : "")}{alert.Count}_{alert.Year}" : $"{alert.Id}")}.{GetType(type).ToLower()}";
        }


        public string CreateSensibleReportName(SocialConflictSensible sensible, ReportType type)
        {
            return @$"SSENSIBLE_SGSD_{(sensible.Generation ? $"{(sensible.Count < 10 ? "0" : "")}{sensible.Count}_{sensible.Year}" : $"{sensible.Id}")}.{GetType(type).ToLower()}";
        }

        public string CreateHelpMemoryReportName(SocialConflict socialConflict, ReportType type)
        {
            return @$"AM_XXXX_SGSD_XXXX.{GetType(type).ToLower()}";
        }

        public string CreateHelpMemoryReportName(SocialConflictSensible socialConflictSensible, ReportType type)
        {
            return @$"AM_XXXX_SGSD_XXXX.{GetType(type).ToLower()}";
        }

        public string CreateAlertResourceName(SocialConflictAlert alert, string extension, int index)
        {
            return @$"FUENTE_{(index < 10 ? "0" : "")}{index}_{(alert.Generation ? $"{(alert.Count < 10 ? "0" : "")}{alert.Count}_{alert.Year}" : $"{alert.Id}")}.{extension.ToLower()}";
        }

        public string CreateCrisisCommitteeReportName(CrisisCommittee crisisCommittee, ReportType type)
        {
            return @$"CC_{crisisCommittee.Count}_{crisisCommittee.Count}.{GetType(type).ToLower()}";
        }

        public string CreateInterventionPlanReportName(InterventionPlan interventionPlan, ReportType type)
        {
            return @$"IP_{interventionPlan.Count}_{interventionPlan.Count}.{GetType(type).ToLower()}";
        }

        public string CreateSectorMeetSessionReportName(SectorMeet sectorMeet, SectorMeetSession sectorMeetSession, ReportType type)
        {
            return @$"RGD_{sectorMeet.Count}_{sectorMeet.Count}_{sectorMeetSession.Id}.{GetType(type).ToLower()}";
        }

        public string CreateActasReportName( ReportType type)
        {
            return @$"ReporteDeActas.{GetType(type).ToLower()}";
        }
    }
}
