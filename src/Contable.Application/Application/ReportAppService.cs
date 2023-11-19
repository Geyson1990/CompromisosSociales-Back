using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Abp.Web.Models;
using Contable.Application.Reports;
using Contable.Application.Reports.Dto;
using Contable.Authorization;
using Contable.Manager;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contable.Net.Emailing;
using Castle.MicroKernel.Registration;
using System.Collections;
using Contable.Application.SocialConflicts.Dto;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Report)]
    [Route("api/services/app/Report", Name = "Report")]
    public class ReportAppService : AbpController
    {
        private readonly ReportManager _reportManager;
        private readonly ResourceManager _resourceManager;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SocialConflictAlert> _socialConflictAlertRepository;
        private readonly IRepository<SocialConflictSensible> _socialConflictSensibleRepository;
        private readonly IRepository<CrisisCommittee> _crisisComitteRepository;
        private readonly IRepository<InterventionPlan> _interventionPlanRepository;
        private readonly IRepository<SectorMeet> _sectorMeetRepository;
        private readonly IRepository<SectorMeetSession> _sectorMeetSessionRepository;
        private readonly IRepository<SectorMeetSessionResource> _sectorMeetSessionResourceRepository;

        public ReportAppService(
            ReportManager reportManager,
            ResourceManager resourceManager,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<SocialConflictAlert> socialConflictAlertRepository,
            IRepository<SocialConflictSensible> socialConflictSensibleRepository,
            IRepository<CrisisCommittee> crisisComitteRepository,
            IRepository<InterventionPlan> interventionPlanRepository,
            IRepository<SectorMeet> sectorMeetRepository,
            IRepository<SectorMeetSession> sectorMeetSessionRepository,
            IRepository<SectorMeetSessionResource> sectorMeetSessionResourceRepository)
        {
            _reportManager = reportManager;
            _resourceManager = resourceManager;
            _socialConflictRepository = socialConflictRepository;
            _socialConflictAlertRepository = socialConflictAlertRepository;
            _socialConflictSensibleRepository = socialConflictSensibleRepository;
            _crisisComitteRepository = crisisComitteRepository;
            _interventionPlanRepository = interventionPlanRepository;            
            _sectorMeetRepository = sectorMeetRepository;
            _sectorMeetSessionRepository = sectorMeetSessionRepository;
            _sectorMeetSessionResourceRepository = sectorMeetSessionResourceRepository;
        }

        [HttpPost]
        [Route("CreateSocialConflict")]
        [AbpAuthorize(AppPermissions.Pages_Report_SocialConflict)]
        public async Task<ActionResult> CreateSocialConflict([FromBody] ReportCreateDto input)
        {

            if (await _socialConflictRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "El conflicto social solicitado ya no existe. Por favor verifique la información antes de continuar")));

            var dbSocialConflict = await _socialConflictRepository.GetAsync(input.Id);

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.SocialConflict,
                ReportType = input.Type,
                FileName = _reportManager.CreateSocialConflictReportName(dbSocialConflict, input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "UserId",
                        Value = AbpSession.UserId.Value.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "SocialConflictId",
                        Value = $"{dbSocialConflict.Id}"
                    }
                }
            });
        }

        [HttpPost]
        [Route("CreateSocialConflictAlert")]
        [AbpAuthorize(AppPermissions.Pages_Report_SocialConflictAlert)]
        public async Task<ActionResult> CreateSocialConflictAlert([FromBody] ReportCreateDto input)
        {

            if (await _socialConflictAlertRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "La alerta solicitada ya no existe. Por favor verifique la información antes de continuar")));

            var alert = await _socialConflictAlertRepository.GetAsync(input.Id);

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.SocialConflictAlert,
                ReportType = input.Type,
                FileName = _reportManager.CreateAlertReportName(alert, input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "UserId",
                        Value = AbpSession.UserId.Value.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "AlertId",
                        Value = $"{alert.Id}"
                    }
                }
            });
        }

        [HttpPost]
        [Route("CreateSocialConflictSensible")]
        [AbpAuthorize(AppPermissions.Pages_Report_SocialConflictSensible)]
        public async Task<ActionResult> CreateSocialConflictSensible([FromBody] ReportCreateDto input)
        {

            if (await _socialConflictSensibleRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "La situación sensible solicitada ya no existe. Por favor verifique la información antes de continuar")));

            var dbSocialConflictSensible = await _socialConflictSensibleRepository.GetAsync(input.Id);

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.SocialConflictSensible,
                ReportType = input.Type,
                FileName = _reportManager.CreateSensibleReportName(dbSocialConflictSensible, input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "UserId",
                        Value = AbpSession.UserId.Value.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "SocialConflictSensibleId",
                        Value = $"{dbSocialConflictSensible.Id}"
                    }
                }
            });
        }

        [HttpPost]
        [Route("CreateSocialConflictAlertResume")]
        [AbpAuthorize(AppPermissions.Pages_Report_SocialConflictAlertResume)]
        public async Task<ActionResult> CreateSocialConflictAlertResume([FromBody] ReportCreateDto input)
        {

            if (await _socialConflictAlertRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "La alerta solicitada ya no existe. Por favor verifique la información antes de continuar")));

            var alert = await _socialConflictAlertRepository.GetAsync(input.Id);

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.SocialConflictAlertResume,
                ReportType = input.Type,
                FileName = _reportManager.CreateAlertResumeReportName(alert, input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "UserId",
                        Value = AbpSession.UserId.Value.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "SocialConflictAlertId",
                        Value = $"{alert.Id}"
                    }
                }
            });
        }

        [HttpPost]
        [Route("CreateSocialConflictHelpMemory")]
        [AbpAuthorize(AppPermissions.Pages_Report_HelpMemory_SocialConflict)]
        public async Task<ActionResult> CreateSocialConflictHelpMemory([FromBody] ReportCreateDto input)
        {
            if (await _socialConflictRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "El caso de conflictividad ya no existe. Por favor verifique la información antes de continuar")));

            var conflict = await _socialConflictRepository.GetAsync(input.Id);

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.SocialConflictHelpMemory,
                ReportType = input.Type,
                FileName = _reportManager.CreateHelpMemoryReportName(conflict, input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "UserId",
                        Value = AbpSession.UserId.Value.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "SocialConflictId",
                        Value = $"{conflict.Id}"
                    }
                }
            });
        }

        [HttpPost]
        [Route("CreateSocialConflictSensibleHelpMemory")]
        [AbpAuthorize(AppPermissions.Pages_Report_HelpMemory_SocialConflictSensible)]
        public async Task<ActionResult> CreateSocialConflictSensibleHelpMemory([FromBody] ReportCreateDto input)
        {
            if (await _socialConflictSensibleRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "El caso de conflictividad ya no existe. Por favor verifique la información antes de continuar")));

            var socialConflictSensible = await _socialConflictSensibleRepository.GetAsync(input.Id);

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.SocialConflictSensibleHelpMemory,
                ReportType = input.Type,
                FileName = _reportManager.CreateHelpMemoryReportName(socialConflictSensible, input.Type),
                Parameters = new List<JasperReportParameter>()
                    {
                        new JasperReportParameter()
                        {
                            Name = "UserId",
                            Value = AbpSession.UserId.Value.ToString()
                        },
                        new JasperReportParameter()
                        {
                            Name = "SocialConflictSensibleId",
                            Value = $"{socialConflictSensible.Id}"
                        }
                    }
            });
        }

        [HttpPost]
        [Route("CreateCrisisCommittee")]
        [AbpAuthorize(AppPermissions.Pages_Report_ConflictTools_CrisisCommittee)]
        public async Task<ActionResult> CreateCrisisCommittee([FromBody] ReportCreateDto input)
        {
            if (await _crisisComitteRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "El comité de crisis solicitado ya no existe o fue eliminado. Por favor verifique la información antes de continuar")));

            var crisisComitte = await _crisisComitteRepository.GetAsync(input.Id);

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.CrisisCommittee,
                ReportType = input.Type,
                FileName = _reportManager.CreateCrisisCommitteeReportName(crisisComitte, input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "UserId",
                        Value = AbpSession.UserId.Value.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "CrisisComitteeId",
                        Value = $"{crisisComitte.Id}"
                    }
                }
            });
        }

        [HttpPost]
        [Route("CreateInterventionPlan")]
        [AbpAuthorize(AppPermissions.Pages_Report_ConflictTools_InteventionPlan)]
        public async Task<ActionResult> CreateInterventionPlan([FromBody] ReportCreateDto input)
        {
            if (await _interventionPlanRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "El plan de intervención solicitado ya no existe o fue eliminado. Por favor verifique la información antes de continuar")));

            var interventionPlan = await _interventionPlanRepository.GetAsync(input.Id);

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.InterventionPlan,
                ReportType = input.Type,
                FileName = _reportManager.CreateInterventionPlanReportName(interventionPlan, input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "UserId",
                        Value = AbpSession.UserId.Value.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "InterventionPlanId",
                        Value = $"{interventionPlan.Id}"
                    }
                }
            });
        }

        [HttpPost]
        [Route("CreateSectorMeetSession")]
        [AbpAuthorize(AppPermissions.Pages_Report_ConflictTools_SectorMeetSession)]
        public async Task<ActionResult> CreateSectorMeetSession([FromBody] ReportCreateDto input)
        {
            if (await _sectorMeetSessionRepository.CountAsync(p => p.Id == input.Id) == 0)
                return Json(new AjaxResponse(new ErrorInfo("Aviso", "La sesión solicitada ya no existe o fue eliminada. Por favor verifique la información antes de continuar")));

            var sectorMeetSession = await _sectorMeetSessionRepository.GetAsync(input.Id);
            var sectorMeet = await _sectorMeetRepository.GetAsync(sectorMeetSession.SectorMeetId);

            var sectorMeetSessionResources = _sectorMeetSessionResourceRepository
                .GetAll()
                .Where(p => p.SectorMeetSessionId == sectorMeetSession.Id)
                .OrderBy(p => p.CreationTime)
                .ToList();

            var attachments = new List<byte[]>();

            foreach(var resource in sectorMeetSessionResources)
            {
                if(resource.FileName.EndsWith("png") || resource.FileName.EndsWith("jpg") || resource.FileName.EndsWith("jpeg"))
                {
                    var resourceContent = _resourceManager.Get(
                        commonFolder: resource.CommonFolder,
                        resourceFolder: resource.ResourceFolder,
                        sectionFolder: resource.SectionFolder,
                        fileName: resource.FileName);

                    if (resourceContent != null)
                        attachments.Add(resourceContent);
                }
            }

            var base64Resources = new List<JasperReportAttachment>();

            foreach(var attachment in attachments)
            {
                base64Resources.Add(new JasperReportAttachment()
                {
                    Resource = Convert.ToBase64String(attachment, 0, attachment.Length)
                });
            }

            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.SectorMeetSession,
                ReportType = input.Type,
                FileName = _reportManager.CreateSectorMeetSessionReportName(sectorMeet, sectorMeetSession, input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "UserId",
                        Value = AbpSession.UserId.Value.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "SectorMeetSessionId",
                        Value = $"{sectorMeetSession.Id}"
                    },
                    new JasperReportParameter()
                    {
                        Name = "Attachments",
                        Value = JsonConvert.SerializeObject(base64Resources)
                    }
                }
            });
        }

        private async Task<ActionResult> ResolveReport(ReportRequestDto input)
        {
            try
            {
                var request = await _reportManager.Create(new JasperReportRequest()
                {
                    Name = input.ReportName,
                    Type = _reportManager.GetType(input.ReportType),
                    Parameters = input.Parameters
                });

                if (request.Success == false)
                    throw new UserFriendlyException(request.Exception.Error.Title, request.Exception.Error.Message);

                var contentType =
                    input.ReportType == ReportType.HTML ? "text/html; charset=utf-8" :
                    input.ReportType == ReportType.PDF ? "application/pdf" :
                    input.ReportType == ReportType.XLSX ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" :
                    input.ReportType == ReportType.DOCX ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" :
                    "text/plain";

                Response.Headers.Add("Content-Disposition", string.Format("attachment;filename={0}", input.FileName));
                Response.ContentType = contentType;

                return File(request.Report, contentType);
            }
            catch (UserFriendlyException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new AjaxResponse(new ErrorInfo(ex.Message, ex.Details)));
            }
        }

        [HttpPost]
        [Route("ReportTeams")]
        [AbpAuthorize(AppPermissions.Pages_Report_SocialConflictAlertResume)]
        public async Task<ActionResult> ReportTeams([FromBody] SocialConflictReportGetAllInputDto input)
        {
            return await ResolveReport(new ReportRequestDto()
            {
                ReportName = ReportNames.ReporteDeParticipantes,
                ReportType = input.Type,
                FileName = _reportManager.CreateTeamsReportName(input.Type),
                Parameters = new List<JasperReportParameter>()
                {
                    new JasperReportParameter()
                    {
                        Name = "FECHA_INICIO",
                        Value = input.StartTime?.ToString("dd/MM/yyyy")
                    },
                    new JasperReportParameter()
                    {
                        Name = "FECHA_FIN",
                        Value = input.EndTime?.ToString("dd/MM/yyyy")
                    },
                    new JasperReportParameter()
                    {
                        Name = "SOCIAL_CONFLICT_ID",
                        Value = input.SocialConflictId?.ToString()
                    },
                    new JasperReportParameter()
                    {
                        Name = "TERRITORIAL_UNIT_ID",
                        Value = input.TerritorialUnitId?.ToString()
                    },

                }
            }); ;
        }
    }
}
