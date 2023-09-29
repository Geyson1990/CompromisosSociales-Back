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
using Contable.EntityFrameworkCore;
using Abp.BackgroundJobs;
using Abp.EntityFrameworkCore;
using Abp.Runtime.Session;
using Contable.Application.ProjectStages;
using Contable.Application.ProjectStages.Dto;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage)]
    public class ProjectStageAppService : ContableAppServiceBase, IProjectStageAppService
    {
        private readonly IRepository<ProjectStage> _projectStageRepository;
        private readonly IRepository<ProjectStageDetail> _projectStageDetailRepository;
        private readonly IRepository<StaticVariable> _staticVariableRepository;
        private readonly IRepository<ProjectRisk> _projectRiskRepository;
        private readonly IRepository<ProjectRiskDetail> _projectRiskDetailRepository;

        public ProjectStageAppService(
            IRepository<ProjectStage> projectStageRepository,
            IRepository<ProjectStageDetail> projectStageDetailRepository,
            IRepository<StaticVariable> staticVariableRepository,
            IRepository<ProjectRisk> projectRiskRepository,
            IRepository<ProjectRiskDetail> projectRiskDetailRepository)
        {
            _projectStageRepository = projectStageRepository;
            _projectStageDetailRepository = projectStageDetailRepository;
            _staticVariableRepository = staticVariableRepository;
            _projectRiskRepository = projectRiskRepository;
            _projectRiskDetailRepository = projectRiskDetailRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage_Create)]
        public async Task Create(ProjectStageCreateDto input)
        {
            var projectStage = ValidateEntity(ObjectMapper.Map<ProjectStage>(input));

            if (input.Details == null)
                input.Details = new List<ProjectStageDetailCreateDto>();

            foreach (var detail in input.Details)
            {
                if (await _staticVariableRepository.CountAsync(p => p.Id == detail.StaticVariable.Id && p.Family == StaticVariableFamily.ProjectRisk) == 0)
                    throw new UserFriendlyException("Aviso", $"Estimado usuario no podemos procesar su transacción debido a que la variable {detail.StaticVariable.Name} es inválida o ya no existe");
            }

            var staticVariablesIds = input.Details.Select(p => p.StaticVariable.Id).Distinct();
            var projectStageId = await _projectStageRepository.InsertAndGetIdAsync(projectStage);

            foreach(var detail in input.Details)
            {
                await _projectStageDetailRepository.InsertAsync(new ProjectStageDetail()
                {
                    ProjectStageId = projectStageId,
                    StaticVariableId = detail.StaticVariable.Id
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage_Delete)]
        public async Task Delete(EntityDto input)
        {
            if (await _projectStageRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            await _projectStageRepository.DeleteAsync(input.Id);
            await _projectStageDetailRepository.DeleteAsync(p => p.ProjectStageId == input.Id);
            await _projectRiskDetailRepository.DeleteAsync(p => p.ProjectStageDetail.ProjectStageId == input.Id);

            var projectRisks = _projectRiskRepository
                .GetAll()
                .Where(p => p.StageId == input.Id)
                .ToList();

            foreach(var projectRisk in projectRisks)
            {
                projectRisk.StageId = null;
                await _projectRiskRepository.UpdateAsync(projectRisk);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage_Disable)]
        public async Task Disable(EntityDto input)
        {
            if (await _projectStageRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var projectStage = await _projectStageRepository.GetAsync(input.Id);

            if (projectStage.Enabled == false)
                throw new UserFriendlyException("Aviso", "La etapa de proyecto ya se encuentra deshabilitada, por favor refresque su búsqueda");

            projectStage.Enabled = false;

            await _projectStageRepository.UpdateAsync(projectStage);

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage_Enable)]
        public async Task Enable(EntityDto input)
        {
            if (await _projectStageRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var projectStage = await _projectStageRepository.GetAsync(input.Id);

            if (projectStage.Enabled)
                throw new UserFriendlyException("Aviso", "La etapa de proyecto ya se encuentra habilitada, por favor refresque su búsqueda");

            projectStage.Enabled = true;

            await _projectStageRepository.UpdateAsync(projectStage);

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage)]
        public async Task<ProjectStageGetDto> Get(EntityDto input)
        {
            if (await _projectStageRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var projectStage = _projectStageRepository
                .GetAll()
                .Include(p => p.Details)
                .ThenInclude(p => p.StaticVariable)
                .ThenInclude(p => p.Options)
                .ThenInclude(p => p.DinamicVariable)
                .Where(p => p.Id == input.Id)
                .First();

            return ObjectMapper.Map<ProjectStageGetDto>(projectStage);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage)]
        public async Task<PagedResultDto<ProjectStageGetAllDto>> GetAll(ProjectStageGetAllInputDto input)
        {
            var query = _projectStageRepository
                .GetAll()
                .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<ProjectStage, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ProjectStageGetAllDto>(count, ObjectMapper.Map<List<ProjectStageGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage_Create, AppPermissions.Pages_Management_ProjectStage_Edit, RequireAllPermissions = false)]
        public async Task<PagedResultDto<ProjectStageStaticVariableGetAllDto>> GetAllStaticVariables(ProjectStageStaticVariableGetAllInputDto input)
        {
            var query = _staticVariableRepository
                .GetAll()
                .Include(p => p.Options)
                .ThenInclude(p => p.DinamicVariable)
                .Where(p => p.Family == StaticVariableFamily.ProjectRisk && p.Enabled)
                .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<StaticVariable, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ProjectStageStaticVariableGetAllDto>(count, ObjectMapper.Map<List<ProjectStageStaticVariableGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProjectStage_Edit)]
        public async Task Update(ProjectStageUpdateDto input)
        {
            if (await _projectStageRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var projectStage = ValidateEntity(ObjectMapper.Map(input, await _projectStageRepository.GetAsync(input.Id)));

            if (input.Details == null)
                input.Details = new List<ProjectStageDetailCreateDto>();
            if (input.DeletedDetails == null)
                input.DeletedDetails = new List<EntityDto>();

            foreach (var detail in input.Details)
            {
                if (await _staticVariableRepository.CountAsync(p => p.Id == detail.StaticVariable.Id && p.Family == StaticVariableFamily.ProjectRisk) == 0)
                    throw new UserFriendlyException("Aviso", $"Estimado usuario no podemos procesar su transacción debido a que la variable {detail.StaticVariable.Name} es inválida o ya no existe");
            }

            var staticVariablesIds = input.Details.Select(p => p.StaticVariable.Id).Distinct();
            await _projectStageRepository.UpdateAsync(projectStage);

            foreach (var detail in input.Details)
            {
                await _projectStageDetailRepository.InsertAsync(new ProjectStageDetail()
                {
                    ProjectStageId = projectStage.Id,
                    StaticVariableId = detail.StaticVariable.Id
                });
            }

            foreach(var deleteDetail in input.DeletedDetails)
            {
                if(await _projectStageDetailRepository.CountAsync(p => p.ProjectStageId == projectStage.Id && p.Id == deleteDetail.Id) > 0)
                {
                    await _projectStageDetailRepository.DeleteAsync(deleteDetail.Id);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        private ProjectStage ValidateEntity(ProjectStage input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, "El nombre de la etapa del proyecto es obligatorio");
            input.Name.VerifyTableColumn(ProjectStageConsts.NameMinLength, ProjectStageConsts.NameMaxLength, DefaultTitleMessage, $"El nombre de la etapa del proyecto no debe exceder los {ProjectStageConsts.NameMaxLength} caracteres");

            return input;
        }

    }
}
