using Abp.EntityFrameworkCore;
using Contable.Application.Reporting.Dto;
using Contable.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Contable.Application.Extensions;
using System;
using Contable.Application.Portal.Dto;
using Contable.Application.Portal;
using Abp.UI;
using Contable.Application.Uploaders.Dto;

namespace Contable.Application.Reporting
{
    public class PortalAppService : ContableAppServiceBase, IPortalAppService
    {
        private readonly IDbContextProvider<ContableDbContext> _dbContextProvider;
        private ContableDbContext _context => _dbContextProvider.GetDbContext();

        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<PIPMEF, long> _pipMefRepository;
        private readonly IRepository<CompromiseLocation> _compromiseLocationRepository;
        private readonly IRepository<Compromise, long> _compromiseRepository;
        private readonly IRepository<Risk> _riskRepository;
        private readonly IRepository<Typology> _typologyRepository;
        private readonly IRepository<QuizForm> _quizFormRepository;
        private readonly IRepository<QuizFormOption> _quizFormOptionRepository;
        private readonly IRepository<QuizComplete> _quizCompleteRepository;
        private readonly IRepository<QuizState> _quizStateRepository;

        public PortalAppService(
            IDbContextProvider<ContableDbContext> dbContextProvider, 
            IRepository<Department> departmentRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<PIPMEF, long> pipMefRepository,
            IRepository<CompromiseLocation> compromiseLocationRepository,
            IRepository<Compromise, long> compromiseRepository,
            IRepository<Risk> riskRepository,
            IRepository<Typology> typologyRepository,
            IRepository<QuizForm> quizFormRepository,
            IRepository<QuizFormOption> quizFormOptionRepository,
            IRepository<QuizComplete> quizCompleteRepository,
            IRepository<QuizState> quizStateRepository)
        {
            _dbContextProvider = dbContextProvider;
            _departmentRepository = departmentRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _socialConflictRepository = socialConflictRepository;
            _pipMefRepository = pipMefRepository;
            _compromiseLocationRepository = compromiseLocationRepository;
            _compromiseRepository = compromiseRepository;
            _riskRepository = riskRepository;
            _typologyRepository = typologyRepository;
            _quizFormRepository = quizFormRepository;
            _quizFormOptionRepository = quizFormOptionRepository;
            _quizCompleteRepository = quizCompleteRepository;
            _quizStateRepository = quizStateRepository;
        }

        public async Task<PagedResultDto<PortalQuizFormDto>> GetQuestions()
        {
            var forms = await _quizFormRepository
                .GetAll()
                .Include(P => P.Options)
                .Where(p => p.Type == QuizFormType.PUBLIC && p.Enabled)
                .OrderBy(p => p.Index)
                .ToListAsync();

            var output = new List<PortalQuizFormDto>();

            foreach (var form in forms)
            {
                if (form.Options.Count > 0)
                {
                    var item = ObjectMapper.Map<PortalQuizFormDto>(form);
                    item.Options = ObjectMapper.Map<List<PortalQuizFormOptionDto>>(form.Options.OrderBy(p => p.Index).ToList());

                    output.Add(item);
                }
            }

            return new PagedResultDto<PortalQuizFormDto>(output.Count, output);
        }

        public async Task CreateQuiz(PortalQuizCreateDto input)
        {
            input.Name.IsValidOrException("Aviso", "Por favor ingrese su nombre en los datos personales");
            input.Name.VerifyTableColumn(QuizCompleteConsts.NameMinLength, 
                QuizCompleteConsts.NameMaxLength,
                "Aviso",
                $"El nombre no debe exceder los {QuizCompleteConsts.NameMaxLength} caracteres");
            input.Name = input.Name.Trim().ToUpper();

            input.Surname.IsValidOrException("Aviso", "Por favor ingrese su apellido en los datos personales");
            input.Surname.VerifyTableColumn(QuizCompleteConsts.SurnameMinLength,
                QuizCompleteConsts.SurnameMaxLength,
                "Aviso",
                $"El apellido paterno no debe exceder los {QuizCompleteConsts.SurnameMaxLength} caracteres");
            input.Surname = input.Surname.Trim().ToUpper();

            input.SecondSurname.VerifyTableColumn(QuizCompleteConsts.SecondSurnameMinLength,
                QuizCompleteConsts.SecondSurnameMaxLength,
                "Aviso",
                $"El apellido materno no debe exceder los {QuizCompleteConsts.SecondSurnameMaxLength} caracteres");
            input.SecondSurname = (input.SecondSurname ?? "").Trim().ToUpper();

            input.EmailAddress.IsValidOrException("Aviso", "Por favor ingrese su correo electrónico en los datos personales");
            input.EmailAddress.VerifyTableColumn(QuizCompleteConsts.EmailAddressMinLength,
                QuizCompleteConsts.EmailAddressMaxLength,
                "Aviso",
                $"El correo electrónico no debe exceder los {QuizCompleteConsts.EmailAddressMaxLength} caracteres");
            input.EmailAddress = input.EmailAddress.Trim().ToUpper();

            if (input.Forms == null || input.Forms.Count == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "No se puede registrar una encuesta vacía");

            if (input.UploadFiles == null)
                input.UploadFiles = new List<UploadResourceInputDto>();

            var defaultQuizState = _quizStateRepository
                .GetAll()
                .Where(p => p.Default)
                .FirstOrDefault();

            var quizComplete = new QuizComplete()
            {
                Name = input.Name,
                Surname = input.Surname,
                SecondSurname = input.SecondSurname,
                EmailAddress = input.EmailAddress,
                QuizState = defaultQuizState,
                QuizStateId = defaultQuizState?.Id,
                Type = QuizCompleteType.PUBLIC,
                Forms = new List<QuizCompleteForm>(),
                Resources = new List<QuizCompleteResource>()
            };

            foreach (var form in input.Forms)
            {
                var dbForm = await _quizFormRepository
                    .GetAsync(form.Id);

                var dbOptions = _quizFormOptionRepository
                    .GetAll()
                    .Where(p => p.QuizFormId == dbForm.Id)
                    .ToList();

                if (form.Options == null || form.Options.Count == 0 || dbOptions.Count(p => p.Id == form.SelectedOptionId) == 0)
                    throw new UserFriendlyException(DefaultTitleMessage, "Por favor complete todas las preguntas antes de enviar la encuesta");

                if (dbOptions.Count == 1 && dbOptions[0].Extra && dbForm.Required && string.IsNullOrWhiteSpace(form.Options[0].Response))
                    throw new UserFriendlyException(DefaultTitleMessage, "Por favor complete todas las preguntas antes de enviar la encuesta");

                quizComplete.Forms.Add(new QuizCompleteForm()
                {
                    Name = dbForm.Name,
                    Index = dbForm.Index,
                    Required = dbForm.Required,
                    SelectedOptionId = form.SelectedOptionId,
                    FormReferenceId = form.Id,
                    Options = dbOptions.Select(p =>
                    {
                        var option = form
                            .Options
                            .Where(d => d.Id == p.Id)
                            .FirstOrDefault();

                        return new QuizCompleteOption()
                        {
                            Name = p.Name,
                            Index = p.Index,
                            Extra = p.Extra,
                            QuizOptionReferenceId = option == null ? 0 : option.Id,
                            Description = option == null ? null : option.Response
                        };
                    })
                    .ToList()
                });
            }

            foreach (var resource in input.UploadFiles)
            {
                var createdResource = ObjectMapper.Map<QuizCompleteResource>(ResourceManager.Create(resource, ResourceConsts.QuizCompletePublic));
                createdResource.Name = createdResource.FileName;

                quizComplete.Resources.Add(createdResource);
            }

            await _quizCompleteRepository.InsertAsync(quizComplete);
        }

        public async Task<PortalPipMefDto> GetPipMef(PortalGetAllInputDto input)
        {
            ChangeDefaultValue(input);

            var query = _pipMefRepository
                .GetAll()
                .Include(p => p.PIPPhase)
                .Where(p => p.PIPPhaseId.HasValue)
                .ToList()
                .GroupBy(p => p.PIPPhaseId);

            var output = new List<PortalPipMefDataDto>();

            var entityCount = await _dbContextProvider
                .GetDbContext()
                .PortalPipMef
                .FromSqlRaw("EXECUTE report_pip_filter @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId)
                .ToListAsync();

            foreach (var groupPipMef in query)
            {

                if (entityCount.Where(p => p.Id == groupPipMef.Key).Count() > 0)
                {
                    output.Add(new PortalPipMefDataDto()
                    {
                        Name = groupPipMef.First().PIPPhase.Value,
                        Order = groupPipMef.First().PIPPhase.Order,
                        Count = entityCount.Where(p => p.Id == groupPipMef.Key).Count(),
                        Total = entityCount.Where(p => p.Id == groupPipMef.Key).Sum(p => p.Total),
                        Step = groupPipMef.First().PIPPhase.Step
                    });
                }

            }

            if (entityCount.Where(p => p.Id == 0).Count() > 0)
            {
                output.Add(new PortalPipMefDataDto()
                {
                    Name = "No especifica",
                    Order = 9999,
                    Count = entityCount.Where(p => p.Id == 0).Count(),
                    Total = entityCount.Where(p => p.Id == 0).Sum(p => p.Total),
                    Step = ParameterStep.None
                });
            }

            var totals = _compromiseRepository
                .GetAll()
                .Include(p => p.PIPMEF)
                .Include(p => p.Status)
                .Include(p => p.CompromiseLocations)
                .ThenInclude(p => p.SocialConflictLocation)
                .ThenInclude(p => p.TerritorialUnit)
                .Include(p => p.CompromiseLocations)
                .ThenInclude(p => p.SocialConflictLocation)
                .ThenInclude(p => p.Department)
                .Include(p => p.CompromiseLocations)
                .ThenInclude(p => p.SocialConflictLocation)
                .ThenInclude(p => p.Province)
                .Include(p => p.CompromiseLocations)
                .ThenInclude(p => p.SocialConflictLocation)
                .ThenInclude(p => p.SocialConflict)
                .Where(p => p.Status.Type == ParameterType.Status_Open && p.Type == CompromiseType.PIP)
                .WhereIf(input.TerritorialUnitId > 0, p => p.CompromiseLocations.Any(p => p.SocialConflictLocation.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.DepartmentId > 0, p => p.CompromiseLocations.Any(p => p.SocialConflictLocation.Department.Id == input.DepartmentId))
                .WhereIf(input.ProvinceId > 0, p => p.CompromiseLocations.Any(p => p.SocialConflictLocation.Province.Id == input.ProvinceId))
                .WhereIf(input.SocialConflictId > 0, p => p.CompromiseLocations.Any(p => p.SocialConflictLocation.SocialConflict.Id == input.SocialConflictId))
                .ToList();

            var total = Convert.ToDecimal(Convert.ToInt64(totals.Where(p => p.PIPMEF != null).Sum(p => p.PIPMEF.UpdatedCost)));

            return new PortalPipMefDto() 
            {
                Total = total,
                NumberText = total.ToCardinal(""),
                ProyectQuantity = totals.Where(p => p.PIPMEF != null).Count(),
                Phases = output.OrderBy(p => p.Order).ToList()
            };
        }

        public async Task<PortalGetAllDto> GetAll(PortalGetAllInputDto input)
        {
            ChangeDefaultValue(input);

            return new PortalGetAllDto
            {
                Summary = TransformSummary(
                    await _context
                        .ReportSummary
                        .FromSqlRaw("EXECUTE report_summary @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId)
                        .ToListAsync()),
                Status = await _context
                    .ReportStatus
                    .FromSqlRaw("EXECUTE report_status @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId)
                    .ToListAsync(),
                OpenStatus = await _context
                    .ReportStatus
                    .FromSqlRaw("EXECUTE report_status_open @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId)
                    .ToListAsync(),
                Responsibles = TransformResponsible(
                    await _context
                        .ReportResponsible
                        .FromSqlRaw("EXECUTE report_responsible @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId)
                        .ToListAsync())
            };
        }

        public async Task<PortalSocialConflictDataDto> GetAllSocialConflicts(PortalGetAllSocialConflictInputDto input)
        {
            ChangeDefaultValue(input);

            input.SocialConflictRiskId = input.SocialConflictRiskId < 0 ? 0 : input.SocialConflictRiskId;
            input.GeographicType = input.GeographicType < 0 ? 0 : input.GeographicType;

            return new PortalSocialConflictDataDto
            {
                Risks = await _context
                    .ReportSocialConflictRisks
                    .FromSqlRaw("EXECUTE report_public_social_conflict @p0, @p1, @p2, @p3, @p4, @p5, @p6", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId, input.SocialConflictRiskId, input.GeographicType, "R")
                    .ToListAsync(),
                GeographycTypes = await _context
                    .ReportSocialConflictGeographycTypes
                    .FromSqlRaw("EXECUTE report_public_social_conflict @p0, @p1, @p2, @p3, @p4, @p5, @p6", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId, input.SocialConflictRiskId, input.GeographicType, "G")
                    .ToListAsync(),
                Locations = await _context
                    .ReportSocialConflictLocations
                    .FromSqlRaw("EXECUTE report_public_social_conflict @p0, @p1, @p2, @p3, @p4, @p5, @p6", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId, input.SocialConflictRiskId, input.GeographicType, "L")
                    .ToListAsync()
            };
        }

        public async Task<PortalSocialConflictAlertDataDto> GetAllSocialConflictAlerts(PortalGetAllInputDto input)
        {
            ChangeDefaultValue(input);

            return new PortalSocialConflictAlertDataDto
            {
                Risks = await _context.ReportSocialConflictAlertRisks
                    .FromSqlRaw("EXECUTE report_public_social_conflict_alert @p0, @p1, @p2, @p3, @p4", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId, "R")
                    .ToListAsync(),
                Sectors = await _context.ReportSocialConflictAlertSectors
                    .FromSqlRaw("EXECUTE report_public_social_conflict_alert @p0, @p1, @p2, @p3, @p4", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId, "A")
                    .ToListAsync(),
                States = await _context.ReportSocialConflictAlertStates
                    .FromSqlRaw("EXECUTE report_public_social_conflict_alert @p0, @p1, @p2, @p3, @p4", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId, "S")
                    .ToListAsync(),
                TerritorialUnits = await _context.ReportSocialConflictAlertTerritorialUnits
                    .FromSqlRaw("EXECUTE report_public_social_conflict_alert @p0, @p1, @p2, @p3, @p4", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId, "L")
                    .ToListAsync(),
                Typologies = await _context.ReportSocialConflictAlertTypologies
                    .FromSqlRaw("EXECUTE report_public_social_conflict_alert @p0, @p1, @p2, @p3, @p4", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId, "T")
                    .ToListAsync()
            };
        }

        public async Task<PortalSocialConflictSensibleDataDto> GetAllSocialConflictSensibles(PortalGetAllSocialConflictSensibleInputDto input)
        {
            ChangeDefaultValue(input);

            input.SocialConflictRiskId = input.SocialConflictRiskId < 0 ? 0 : input.SocialConflictRiskId;
            input.GeographicType = input.GeographicType < 0 ? 0 : input.GeographicType;

            return new PortalSocialConflictSensibleDataDto
            {
                Risks = await _context
                    .ReportSocialConflictSensibleRisks
                    .FromSqlRaw("EXECUTE report_public_social_conflict_sensible @p0, @p1, @p2, @p3, @p4, @p5", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictRiskId, input.GeographicType, "R")
                    .ToListAsync(),
                GeographycTypes = await _context
                    .ReportSocialConflictSensibleGeographycTypes
                    .FromSqlRaw("EXECUTE report_public_social_conflict_sensible @p0, @p1, @p2, @p3, @p4, @p5", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictRiskId, input.GeographicType, "G")
                    .ToListAsync(),
                Locations = await _context
                    .ReportSocialConflictSensibleLocations
                    .FromSqlRaw("EXECUTE report_public_social_conflict_sensible @p0, @p1, @p2, @p3, @p4, @p5", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictRiskId, input.GeographicType, "L")
                    .ToListAsync()
            };
        }

        public async Task<PortaReportDataDto> GetReportFilters()
        {
            var output = new PortaReportDataDto();

            var departments = await _departmentRepository
                            .GetAll()
                            .OrderBy(p => p.Name)
                            .Include(p => p.Provinces)
                            .ToListAsync();

            output.Departments = new List<PortalDepartmentDto>();

            var territorialUnitAvaliables = await _context.EntityDtos.FromSqlRaw("EXECUTE report_zones @p0", "TU").ToListAsync();
            var departmentAvaliables = await _context.EntityDtos.FromSqlRaw("EXECUTE report_zones @p0", "DE").ToListAsync();

            foreach (var department in departments)
            {
                if(departmentAvaliables.Any(d => d.Id == department.Id))
                {
                    var insertDepartment = new PortalDepartmentDto
                    {
                        Id = department.Id,
                        Name = department.Name,
                        TerritorialUnitIds = await _territorialUnitRepository.GetAll().Where(p => p.TerritorialUnitDepartments.Any(d => d.Department.Id == department.Id)).Select(p => new EntityDto(p.Id)).ToListAsync(),
                        Provinces = ObjectMapper.Map<List<PortalProvinceDto>>(department.Provinces)
                    };

                    insertDepartment.Provinces = insertDepartment.Provinces.Where(p => p != null).OrderBy(p => p.Name).ToList();

                    output.Departments.Add(insertDepartment);
                }
                
            }

            output.TerritorialUnits = ObjectMapper.Map<List<PortalTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().ToListAsync());
            output.TerritorialUnits = output.TerritorialUnits.Where(p => p != null).Where(p => territorialUnitAvaliables.Any(d => d.Id == p.Id)).OrderBy(p => p.Name).ToList();

           var socialConflcits = await _socialConflictRepository
                .GetAll()
                .Include(p => p.Locations)
                    .ThenInclude(p => p.TerritorialUnit)
                .Include(p => p.Locations)
                    .ThenInclude(p => p.Department)
                .Include(p => p.Locations)
                    .ThenInclude(p => p.Province)
                .Include(p => p.Locations)
                    .ThenInclude(p => p.District)
                .OrderBy(p => p.CaseName)
                .ToListAsync();

            output.SocialConflicts = socialConflcits.Where(p => p != null).Select(p =>
            {
                return new PortalSocialConflictDto()
                {
                    Id = p.Id,
                    CaseName = p.CaseName,
                    Code = p.Code,
                    CreationTime = p.CreationTime,
                    Description = p.Description,
                    Dialog = p.Dialog,
                    LastModificationTime = p.LastModificationTime,
                    Locations = ObjectMapper.Map<List<PortalSocialConflictLocationDto>>(p.Locations)
                };
            }).ToList();

            output.Risks = ObjectMapper.Map<List<PortalRiskDto>>(await _riskRepository.GetAll().Where(p => p.Enabled).OrderBy(p => p.Name).ToListAsync());

            output.Geographics = new List<PortalGeographicDto>()
            {
                new PortalGeographicDto()
                {
                    Id = (int)GeographycType.None,
                    Name = "Todos"
                },
                new PortalGeographicDto()
                {
                    Id = (int)GeographycType.Region,
                    Name = "Regional"
                },
                new PortalGeographicDto()
                {
                    Id = (int)GeographycType.Location,
                    Name = "Multiregional"
                },
                new PortalGeographicDto()
                {
                    Id = (int)GeographycType.National,
                    Name = "Nacional"
                }
            };

            output.Typologies = ObjectMapper.Map<List<PortalTypologyDto>>(await _typologyRepository.GetAll().Where(p => p.Enabled).OrderBy(p => p.Name).ToListAsync()); 

            return output;
        }

        #region Private Methods

        private void ChangeDefaultValue(PortalGetAllInputDto input)
        {
            input.TerritorialUnitId = input.TerritorialUnitId < 0 ? 0 : input.TerritorialUnitId;
            input.DepartmentId = input.DepartmentId < 0 ? 0 : input.DepartmentId;
            input.ProvinceId = input.ProvinceId < 0 ? 0 : input.ProvinceId;
            input.DistrictId = input.DistrictId < 0 ? 0 : input.DistrictId;
        }

        private List<ReportSummaryJoinDto> TransformSummary(List<ReportSummaryDto> summaryList)
        {
            var result = new Dictionary<string, ReportSummaryJoinDto>();
            foreach (var item in summaryList)
            {
                if (!result.ContainsKey(item.Name))
                    result[item.Name] = new ReportSummaryJoinDto() { Name = item.Name };

                var elementHash = result[item.Name];
                if (item.Type == CompromiseType.PIP)
                    elementHash.PipTotal = item.Count;
                else if (item.Type == CompromiseType.Activity)
                    elementHash.ActivityTotal = item.Count;
            }
            return result.Values.ToList();
        }

        private List<ReportResponsibleJoinDto> TransformResponsible(List<ReportResponsibleDto> responsibleList)
        {
            var result = new Dictionary<string, ReportResponsibleJoinDto>();
            foreach (var item in responsibleList)
            {
                if (!result.ContainsKey(item.Name))
                    result[item.Name] = new ReportResponsibleJoinDto() { Name = item.Name };

                var elementHash = result[item.Name];
                if (item.Type == CompromiseType.PIP)
                    elementHash.PipTotal = item.Count;
                else if (item.Type == CompromiseType.Activity)
                    elementHash.ActivityTotal = item.Count;
            }
            return result.Values.ToList();
        }

        #endregion
    }
}
