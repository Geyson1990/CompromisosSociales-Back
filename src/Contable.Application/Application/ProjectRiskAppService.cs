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
using Abp.EntityFrameworkCore;
using Contable.EntityFrameworkCore;
using Abp.Runtime.Session;
using Contable.Application.ProjectRisks;
using Contable.Application.ProjectRisks.Dto;
using Contable.Authorization.Users;
using Abp.Domain.Uow;
using NUglify.Helpers;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk)]
    public class ProjectRiskAppService : ContableAppServiceBase, IProjectRiskAppService
    {
        private readonly IRepository<ProjectRisk> _projectRiskRepository;
        private readonly IRepository<ProjectRiskDetail> _projectRiskDetailRepository;
        private readonly IRepository<ProjectRiskHistory> _projectRiskHistoryRepository;
        private readonly IRepository<ProjectRiskHistoryDetail> _projectRiskHistoryDetailRepository;
        private readonly IRepository<ProjectStage> _projectStageRepository;
        private readonly IRepository<ProjectStageDetail> _projectStageDetailRepository;
        private readonly IRepository<StaticVariable> _staticVariableRepository;
        private readonly IRepository<StaticVariableOption> _staticVariableOptionRepository;
        private readonly IRepository<DinamicVariable> _dinamicVariableRepository;
        private readonly IRepository<DinamicVariableDetail> _dinamicVariableDetailRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<User, long> _userRepository;

        public ProjectRiskAppService(
            IRepository<ProjectRisk> projectRiskRepository,
            IRepository<ProjectRiskDetail> projectRiskDetailRepository,
            IRepository<ProjectRiskHistory> projectRiskHistoryRepository,
            IRepository<ProjectRiskHistoryDetail> projectRiskHistoryDetailRepository,
            IRepository<ProjectStage> projectStageRepository,
            IRepository<ProjectStageDetail> projectStageDetailRepository,
            IRepository<StaticVariable> staticVariableRepository,
            IRepository<StaticVariableOption> staticVariableOptionRepository,
            IRepository<DinamicVariable> dinamicVariableRepository,
            IRepository<DinamicVariableDetail> dinamicVariableDetailRepository,
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<User, long> userRepository)
        {
            _projectRiskRepository = projectRiskRepository;
            _projectRiskDetailRepository = projectRiskDetailRepository;
            _projectRiskHistoryRepository = projectRiskHistoryRepository;
            _projectRiskHistoryDetailRepository = projectRiskHistoryDetailRepository;
            _projectStageRepository = projectStageRepository;
            _projectStageDetailRepository = projectStageDetailRepository;
            _staticVariableRepository = staticVariableRepository;
            _staticVariableOptionRepository = staticVariableOptionRepository;
            _dinamicVariableRepository = dinamicVariableRepository;
            _dinamicVariableDetailRepository = dinamicVariableDetailRepository;
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _userRepository = userRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk_Delete)]
        public async Task Delete(EntityDto input)
        {
            if (await _projectRiskRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            await _projectRiskRepository.DeleteAsync(input.Id);
            await _projectRiskDetailRepository.DeleteAsync(p => p.ProjectRiskId == input.Id);
            await _projectRiskHistoryRepository.DeleteAsync(p => p.ProjectRiskId == input.Id);
            await _projectRiskHistoryDetailRepository.DeleteAsync(p => p.ProjectRiskHistory.ProjectRiskId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk_History_Delete)]
        public async Task DeleteHistory(EntityDto input)
        {
            if (await _projectRiskHistoryRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            await _projectRiskHistoryRepository.DeleteAsync(p => p.Id == input.Id);
            await _projectRiskHistoryDetailRepository.DeleteAsync(p => p.ProjectRiskHistoryId == input.Id);
        }
        
        [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk)]
        public async Task<ProjectRiskGetDataDto> Get(NullableIdDto input)
        {
            var output = new ProjectRiskGetDataDto();

            if (input.Id.HasValue)
            {
                if (await _projectRiskRepository.CountAsync(p => p.Id == input.Id) == 0)
                    throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

                var projectRisk = _projectRiskRepository
                    .GetAll()
                    .Include(p => p.Details)
                    .Where(p => p.Id == input.Id.Value)
                    .First();

                output.ProjectRisk = ObjectMapper.Map<ProjectRiskGetDto>(projectRisk);

                if (projectRisk.CreatorUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == projectRisk.CreatorUserId.Value)
                        .FirstOrDefault();

                    if(user != null)
                    {
                        output.ProjectRisk.CreationUser = new ProjectRiskUserDto()
                        {
                            Name = user == null ? "N/A" : user.Name,
                            Surname = user == null ? "" : user.Surname,
                            EmailAddress = user == null ? "" : user.EmailAddress
                        };
                    }
                }

                if (projectRisk.LastModifierUserId.HasValue)
                {
                    var user = _userRepository
                         .GetAll()
                         .Where(p => p.Id == projectRisk.LastModifierUserId.Value)
                         .FirstOrDefault();

                    if (user != null)
                    {
                        output.ProjectRisk.EditionUser = new ProjectRiskUserDto()
                        {
                            Name = user == null ? "N/A" : user.Name,
                            Surname = user == null ? "" : user.Surname,
                            EmailAddress = user == null ? "" : user.EmailAddress
                        };
                    }
                }
            }

            output.Stages = ObjectMapper.Map<List<ProjectRiskStageDto>>(_projectStageRepository
                .GetAll()
                .Include(p => p.Details)
                .ThenInclude(p => p.StaticVariable)
                .ThenInclude(p => p.Options)
                .ThenInclude(p => p.Details)
                .Include(p => p.Details)
                .ThenInclude(p => p.StaticVariable)
                .ThenInclude(p => p.Options)
                .ThenInclude(p => p.DinamicVariable)
                .WhereIf(output.ProjectRisk != null, p => p.Enabled || p.Id == output.ProjectRisk.StageId)
                .WhereIf(output.ProjectRisk == null, p => p.Enabled)
                .ToList());

            foreach(var stage in output.Stages)
            {
                stage.Details = stage
                    .Details
                    .Where(p => p.StaticVariable.Enabled)
                    .ToList();

                foreach (var detail in stage.Details)
                {
                    detail.StaticVariable.Options = detail
                        .StaticVariable
                        .Options
                        .Where(p => p.Enabled)
                        .ToList();

                    foreach(var option in detail.StaticVariable.Options)
                    {
                        option.Details = option.Details.OrderBy(p => p.Value).ToList();
                    }
                }
            }

            output.Departments = ObjectMapper.Map<List<ProjectRiskDepartmentDto>>(_departmentRepository
                .GetAll()
                .Include(p => p.Provinces)
                .Include(p => p.TerritorialUnitDepartments)
                .ThenInclude(p => p.TerritorialUnit)
                .ToList());

            output.DinamicValues = new List<ProjectRiskDinamicVariableDetailDto>();

            if(input.Id.HasValue)
            {
                var index = output.Departments.FindIndex(p => p.Provinces.Any(d => d.Id == output.ProjectRisk.ProvinceId));

                if(index != -1)
                {
                    output.ProjectRisk.DepartmentId = output.Departments[index].Id;

                    output.DinamicValues = ObjectMapper.Map<List<ProjectRiskDinamicVariableDetailDto>>(await _dinamicVariableDetailRepository
                    .GetAll()
                    .Where(p => p.ProvinceId == output.ProjectRisk.ProvinceId)
                    .ToListAsync());
                }
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk_History)]
        public async Task<ProjectRiskHistoryGetDto> GetHistory(EntityDto input)
        {
            if (await _projectRiskHistoryRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var history = await _projectRiskHistoryRepository.GetAsync(input.Id);
            var output = ObjectMapper.Map<ProjectRiskHistoryGetDto>(history);

            if (history.CreatorUserId.HasValue)
            {
                var user = _userRepository
                    .GetAll()
                    .Where(p => p.Id == history.CreatorUserId.Value)
                    .FirstOrDefault();

                output.CreationUser = new ProjectRiskUserDto()
                {
                    Name = user == null ? "N/A" : user.Name,
                    Surname = user == null ? "" : user.Surname,
                    EmailAddress = user == null ? "" : user.EmailAddress
                };
            }

            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var details = _projectRiskHistoryDetailRepository
                    .GetAll()
                    .Include(p => p.ProjectStageDetail)
                    .ThenInclude(p => p.StaticVariable)
                    .Include(p => p.StaticVariableOption)
                    .ThenInclude(p => p.Details)
                    .Where(p => p.ProjectRiskHistoryId == history.Id)
                    .ToList();

                output.Stage = ObjectMapper.Map<ProjectRiskHistoryStageGetDto>(await _projectStageRepository.GetAsync(history.StageId));

                output.Stage.Details = details
                    .Where(p => p.ProjectStageDetail.ProjectStageId == history.StageId)
                    .Select(p => p.ProjectStageDetail)
                    .DistinctBy(p => p.Id)
                    .Select(p => ObjectMapper.Map<ProjectRiskHistoryStageDetailGetDto>(p))
                    .ToList();

                foreach (var stageDetail in output.Stage.Details)
                {
                    stageDetail.StaticVariable.Options = ObjectMapper.Map<List<ProjectRiskHistoryStaticVariableOptionGetDto>>(details
                    .Where(p => p.StaticVariableId == stageDetail.StaticVariable.Id)
                    .OrderBy(p => p.StaticVariableOption.Index)
                    .Select(p => new ProjectRiskHistoryStaticVariableOptionGetDto()
                    {
                        Id = p.StaticVariableOptionId,
                        Name = p.StaticVariableOption.Name,
                        Index = p.StaticVariableOption.Index,
                        Value = p.Value,
                        Site = p.StaticVariableOption.Site,
                        Details = ObjectMapper.Map<List<ProjectRiskHistoryStaticVariableOptionDetailGetDto>>(p.StaticVariableOption
                            .Details
                            .OrderBy(p => p.Index)
                            .DistinctBy(p => p.Value)
                            .ToList())
                    }).DistinctBy(p => p.Id)
                    .ToList());
                }
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk_Create, AppPermissions.Pages_Management_ProjectRisk_Edit, RequireAllPermissions = false)]
        public async Task<EntityDto> CreateOrUpdate(ProjectRiskCreateOrUpdateDto input)
        {
            if (await _provinceRepository.CountAsync(p => p.Id == input.ProvinceId) == 0)
                throw new UserFriendlyException("Aviso", "La provincia solicitada no existe o ya fue eliminada");
            if (input.StageId <= 0)
                throw new UserFriendlyException("Aviso", "La fase del proyecto es obligatoria");
            if (await _projectStageRepository.CountAsync(p => p.Id == input.StageId) == 0)
                throw new UserFriendlyException("Aviso", "La fase del proyecto seleccionada ya no existe o fue eliminada");
            if (input.Id.HasValue && await _projectRiskRepository.CountAsync(p => p.Id == input.Id.Value) == 0)
                throw new UserFriendlyException("Aviso", "Lo sentimos el proyecto al que hace referencia ya no existe o fue eliminado");
            if(input.EvaluatedTime == null)
                throw new UserFriendlyException("Aviso", "La fecha de análisis es obligatoria");
            if (input.FixProbabilityRate <= 0)
                throw new UserFriendlyException("Aviso", "El factor de correción de la probabilidad no puede ser cero");
            if (input.FixImpactRate <= 0)
                throw new UserFriendlyException("Aviso", "El factor de correción del impacto no puede ser cero");

            var province = await _provinceRepository.GetAsync(input.ProvinceId);
            ProjectRisk projectRisk = null;
            
            if(input.Id.HasValue)
                projectRisk = ValidateEntity(ObjectMapper.Map(input, await _projectRiskRepository.GetAsync(input.Id.Value)));
            else
                projectRisk = ValidateEntity(ObjectMapper.Map<ProjectRisk>(input));

            var stage = _projectStageRepository
                .GetAll()
                .Include(p => p.Details)
                .ThenInclude(p => p.StaticVariable)
                .ThenInclude(p => p.Options)
                .ThenInclude(p => p.Details)
                .Include(p => p.Details)
                .ThenInclude(p => p.StaticVariable)
                .ThenInclude(p => p.Options)
                .ThenInclude(p => p.DinamicVariable)
                .Where(p => p.Id == input.StageId)
                .First();

            projectRisk.ProvinceId = province.Id;
            projectRisk.StageId = stage.Id;

            var impact = new List<ProjectRiskFormDto>();
            var probability = new List<ProjectRiskFormDto>();

            projectRisk.Id = await _projectRiskRepository.InsertOrUpdateAndGetIdAsync(projectRisk);

            //Is any option dont select
            foreach (var projectStageDetail in stage.Details)
            {
                foreach (var option in projectStageDetail.StaticVariable.Options)
                {
                    if (input.Details.Any(p => p.ProjectStageDetailId == projectStageDetail.Id && p.StaticVariableOptionId == option.Id) == false)
                    {
                        var projectRiskDetail = _projectRiskDetailRepository
                            .GetAll()
                            .Where(p => p.ProjectRiskId == projectRisk.Id && p.ProjectStageDetailId == projectStageDetail.Id && p.StaticVariableOptionId == option.Id)
                            .FirstOrDefault();

                        if (projectRiskDetail == null)
                            projectRiskDetail = new ProjectRiskDetail() 
                            { 
                                ProjectRiskId = projectRisk.Id, 
                                ProjectStageDetailId = projectStageDetail.Id, 
                                StaticVariableOptionId = option.Id
                            };

                        projectRiskDetail.Enabled = stage.Enabled && option.Enabled && projectStageDetail.StaticVariable.Enabled;

                        if (option.Type == StaticVariableType.Cuantitative)
                        {
                            var dinamicVariableDetail = _dinamicVariableDetailRepository
                                .GetAll()
                                .Where(p => p.DinamicVariableId == option.DinamicVariableId.Value && p.ProvinceId == province.Id)
                                .FirstOrDefault();

                            if (dinamicVariableDetail != null)
                                projectRiskDetail.Value = dinamicVariableDetail.Value;
                            else
                                projectRiskDetail.Value = 0;
                        }

                        if (option.Type == StaticVariableType.Cualitative)
                        {
                            projectRiskDetail.Value = 0;
                        }

                        if (projectRisk.StageId.HasValue && projectStageDetail.ProjectStageId == projectRisk.StageId.Value && stage.Enabled && option.Enabled && projectStageDetail.StaticVariable.Enabled)
                        {
                            if(option.Site == StaticVariableSite.Probability)
                            {
                                probability.Add(new ProjectRiskFormDto()
                                {
                                    StageDetailId = projectStageDetail.Id,
                                    StaticVariableId = option.StaticVariableId,
                                    StaticVariableOptionId = option.Id,
                                    Weight = option.Value,
                                    Value = projectRiskDetail.Value
                                });
                            }
                            if (option.Site == StaticVariableSite.Impact)
                            {
                                impact.Add(new ProjectRiskFormDto()
                                {
                                    StageDetailId = projectStageDetail.Id,
                                    StaticVariableId = option.StaticVariableId,
                                    StaticVariableOptionId = option.Id,
                                    Weight = option.Value,
                                    Value = projectRiskDetail.Value
                                });
                            }
                        }

                        await _projectRiskDetailRepository.InsertOrUpdateAsync(projectRiskDetail);
                    }
                }
            }

            //Options selected or changed
            foreach (var detail in input.Details)
            {
                //Verification for variable is correct for current project risk
                if (detail.Id.HasValue && detail.Id.Value > 0 && await _projectRiskDetailRepository.CountAsync(p => p.Id == detail.Id && p.ProjectRiskId == projectRisk.Id) == 0)
                    throw new UserFriendlyException("Aviso", "Hubo un error al procesar la solicitud debido a que una de las variables ya no existe o fue eliminada por favor refresque la página");

                if (await _staticVariableOptionRepository.CountAsync(p => p.Id == detail.StaticVariableOptionId && p.StaticVariable.Family == StaticVariableFamily.ProjectRisk) == 1 &&
                    await _projectStageDetailRepository.CountAsync(p => p.Id == detail.ProjectStageDetailId) == 1)
                {
                    var option = _staticVariableOptionRepository
                        .GetAll()
                        .Include(p => p.StaticVariable)
                        .Include(p => p.DinamicVariable)
                        .Where(p => p.Id == detail.StaticVariableOptionId)
                        .First();

                    var projectStageDetail = _projectStageDetailRepository
                        .GetAll()
                        .Include(p => p.ProjectStage)
                        .Where(p => p.Id == detail.ProjectStageDetailId)
                        .First();

                    ProjectRiskDetail projectRiskDetail = null;

                    if (detail.Id.HasValue)
                    {
                        projectRiskDetail = await _projectRiskDetailRepository.GetAsync(detail.Id.Value);
                    }
                    else
                    {
                        projectRiskDetail = _projectRiskDetailRepository
                        .GetAll()
                        .Where(p => p.ProjectRiskId == projectRisk.Id && p.ProjectStageDetailId == detail.Id && p.StaticVariableOptionId == option.Id)
                        .FirstOrDefault();
                    }

                    if (projectRiskDetail == null)
                        projectRiskDetail = new ProjectRiskDetail() { ProjectRiskId = projectRisk.Id, ProjectStageDetailId = projectStageDetail.Id, StaticVariableOptionId = option.Id };

                    projectRiskDetail.Enabled = projectStageDetail.ProjectStage.Enabled && option.Enabled && option.StaticVariable.Enabled;

                    if (option.Type == StaticVariableType.Cuantitative)
                    {
                        var dinamicVariableDetail = _dinamicVariableDetailRepository
                            .GetAll()
                            .Where(p => p.DinamicVariableId == option.DinamicVariableId.Value && p.ProvinceId == province.Id)
                            .FirstOrDefault();

                        if (dinamicVariableDetail != null)
                            projectRiskDetail.Value = dinamicVariableDetail.Value;
                        else
                            projectRiskDetail.Value = 0;
                    }

                    if (option.Type == StaticVariableType.Cualitative)
                    {
                        projectRiskDetail.Value = detail.Value;
                    }

                    if (projectRisk.StageId.HasValue && projectStageDetail.ProjectStageId == projectRisk.StageId.Value && projectStageDetail.ProjectStage.Enabled && option.Enabled && option.StaticVariable.Enabled)
                    {
                        if (option.Site == StaticVariableSite.Probability)
                        {
                            probability.Add(new ProjectRiskFormDto()
                            {
                                StageDetailId = projectStageDetail.Id,
                                StaticVariableId = option.StaticVariableId,
                                StaticVariableOptionId = option.Id,
                                Weight = option.Value,
                                Value = projectRiskDetail.Value
                            });
                        }
                        if (option.Site == StaticVariableSite.Impact)
                        {
                            impact.Add(new ProjectRiskFormDto()
                            {
                                StageDetailId = projectStageDetail.Id,
                                StaticVariableId = option.StaticVariableId,
                                StaticVariableOptionId = option.Id,
                                Weight = option.Value,
                                Value = projectRiskDetail.Value
                            });
                        }
                    }

                    await _projectRiskDetailRepository.InsertOrUpdateAsync(projectRiskDetail);
                }
            }

            var totalWeightProbability = probability.Sum(p => p.Weight);
            var totalPercentageProbability = probability.Sum(p => p.Weight * p.Value);
            var totalCalculatedProbability = totalWeightProbability == 0 ? 0 : ((totalPercentageProbability) / totalWeightProbability);

            var totalWeightImpact = impact.Sum(p => p.Weight);
            var totalPercentageImpact = impact.Sum(p => p.Weight * p.Value);
            var totalCalculatedImpact = totalWeightImpact == 0 ? 0 : ((totalPercentageImpact) / totalWeightImpact);

            projectRisk.FixProbabilityRate = input.FixProbabilityRate;
            projectRisk.FixImpactRate = input.FixImpactRate;
            projectRisk.ProbabilityWeight = totalWeightProbability;
            projectRisk.ImpactWeight = totalWeightImpact;
            projectRisk.Probability = totalCalculatedProbability * projectRisk.FixProbabilityRate;
            projectRisk.Impact = totalCalculatedImpact * projectRisk.FixImpactRate;
            projectRisk.Value = Decimal.Round(projectRisk.Probability * projectRisk.Impact, 2);

            await _projectRiskRepository.UpdateAsync(projectRisk);

            var historyId = await _projectRiskHistoryRepository.InsertAndGetIdAsync(new ProjectRiskHistory()
            {
                ProjectRiskId = projectRisk.Id,
                StageId = projectRisk.StageId.Value,
                EvaluatedTime = projectRisk.EvaluatedTime,
                Total = projectRisk.Value,
                FixProbabilityRate = projectRisk.FixProbabilityRate,
                Probability = projectRisk.Probability,
                ProbabilityWeight = projectRisk.ProbabilityWeight,
                Impact = projectRisk.Impact,
                FixImpactRate = projectRisk.FixImpactRate,
                ImpactWeight = projectRisk.ImpactWeight,
                Value = projectRisk.Value
            });

            foreach(var given in probability)
            {
                await _projectRiskHistoryDetailRepository.InsertAsync(new ProjectRiskHistoryDetail()
                {
                    ProjectRiskHistoryId = historyId,
                    ProjectStageDetailId = given.StageDetailId,
                    StaticVariableId = given.StaticVariableId,
                    StaticVariableOptionId = given.StaticVariableOptionId,
                    Weight = given.Weight,
                    Value = given.Value
                });
            }

            foreach (var given in impact)
            {
                await _projectRiskHistoryDetailRepository.InsertAsync(new ProjectRiskHistoryDetail()
                {
                    ProjectRiskHistoryId = historyId,
                    ProjectStageDetailId = given.StageDetailId,
                    StaticVariableId = given.StaticVariableId,
                    StaticVariableOptionId = given.StaticVariableOptionId,
                    Weight = given.Weight,
                    Value = given.Value
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return new EntityDto(projectRisk.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk)]
        public async Task<PagedResultDto<ProjectRiskGetAllDto>> GetAll(ProjectRiskGetAllInputDto input)
        {
            var query = _projectRiskRepository
                .GetAll()
                .Include(p => p.Province)
                .ThenInclude(p => p.Department)
                .ThenInclude(p => p.TerritorialUnitDepartments)
                .ThenInclude(p => p.TerritorialUnit)
                .Include(p => p.Stage)
                .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<ProjectRisk, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ProjectRiskGetAllDto>(count, ObjectMapper.Map<List<ProjectRiskGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk)]
        public async Task<ListResultDto<ProjectRiskDinamicVariableDetailDto>> GetAllDinamicValues(EntityDto input)
        {
            if (await _provinceRepository.CountAsync(p => p.Id == input.Id) == 0)
                return new ListResultDto<ProjectRiskDinamicVariableDetailDto>(new List<ProjectRiskDinamicVariableDetailDto>());

            return new ListResultDto<ProjectRiskDinamicVariableDetailDto>(ObjectMapper.Map<List<ProjectRiskDinamicVariableDetailDto>>(await _dinamicVariableDetailRepository
                .GetAll()
                .Where(p => p.ProvinceId == input.Id)
                .ToListAsync()));
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectRisk_History)]
        public async Task<PagedResultDto<ProjectRiskHistoryGetAllDto>> GetAllHistories(ProjectRiskHistoryGetAllInputDto input)
        {
            if (input.ProjectRiskId.HasValue == false || input.ProjectRiskId.Value <= 0)
                return new PagedResultDto<ProjectRiskHistoryGetAllDto>(0, new List<ProjectRiskHistoryGetAllDto>());

            var count = 0;
            var output = new List<ProjectRiskHistoryGetAllDto>();

            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = _projectRiskHistoryRepository
                .GetAll()
                .Include(p => p.Stage)
                .Where(p => p.IsDeleted == false && p.ProjectRiskId == input.ProjectRiskId)
                .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<ProjectRiskHistory, bool>>)(expression => EF.Functions.Like(expression.Stage.Name, $"%{word}%"))).ToArray());

                count = await query.CountAsync();
                output = ObjectMapper.Map<List<ProjectRiskHistoryGetAllDto>>(query.OrderBy(input.Sorting).PageBy(input));
            }

            return new PagedResultDto<ProjectRiskHistoryGetAllDto>(count, output);
        }

        private ProjectRisk ValidateEntity(ProjectRisk input)
        {
            input.Code.IsValidOrException(DefaultTitleMessage, "El código del proyecto es obligatorio");
            input.Code.VerifyTableColumn(ProjectRiskConsts.CodeMinLength, ProjectRiskConsts.CodeMaxLength, DefaultTitleMessage, $"El código del proyecto no debe exceder los {ProjectRiskConsts.CodeMaxLength} caracteres");

            input.Name.IsValidOrException(DefaultTitleMessage, "El nombre del proyecto es obligatorio");
            input.Name.VerifyTableColumn(ProjectRiskConsts.NameMinLength, ProjectRiskConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del proyecto no debe exceder los {ProjectRiskConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
