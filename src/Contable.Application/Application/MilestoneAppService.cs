using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Contable.Application.Extensions;
using Contable.Application.Milestones;
using Contable.Application.Milestones.Dto;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase)]
    public class MilestoneAppService : ContableAppServiceBase, IMilestoneAppService
    {
        private readonly IRepository<Parameter> _parameterRepository;
        private readonly IRepository<ParameterCategory> _parameterCategoryRepository;

        public MilestoneAppService(
            IRepository<Parameter> parameterRepository,
            IRepository<ParameterCategory> parameterCategoryRepository)
        {
            _parameterRepository = parameterRepository;
            _parameterCategoryRepository = parameterCategoryRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase_Create)]
        public async Task Create(MilestoneCreateDto input)
        {
            if (await _parameterCategoryRepository.CountAsync(p => p.Code == CompromiseConsts.ParameterCategoryPIPMilestone) == 0)
                throw new UserFriendlyException("Aviso", "Error faltal, hay una inconsistensia en las categorías de las etapas.");
            if (await _parameterRepository.CountAsync(p => p.Id == input.Phase.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPPhase) == 0)
                throw new UserFriendlyException("Aviso", "La fase selecciona ya no existe o no esta disponible. Verifique la información antes de continuar.");

            var category = _parameterCategoryRepository
                .GetAll()
                .Where(p => p.Code == CompromiseConsts.ParameterCategoryPIPMilestone)
                .First();

            var phase = _parameterRepository
                .GetAll()
                .Where(p => p.Id == input.Phase.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPPhase)
                .First();

            var milestone = new Parameter()
            {
                Value = input.Name,
                Order = input.Index,
                ParameterCategory = category,
                ParameterCategoryId = category.Id,
                ParentId = phase.Id,
                Step = 0
            };

            await _parameterRepository.InsertAsync(ValidateEntity(milestone));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _parameterRepository.CountAsync(p => p.Id == input.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPMilestone));

            await _parameterRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase)]
        public async Task<MilestoneGetDto> Get(EntityDto input)
        {
            VerifyCount(await _parameterRepository.CountAsync(p => p.Id == input.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPMilestone));

            var milestone = _parameterRepository
                .GetAll()
                .Include(p => p.ParameterCategory)
                .Where(p => p.Id == input.Id)
                .First();

            var phase = _parameterRepository
               .GetAll()
               .Where(p => p.Id == milestone.ParentId && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPPhase)
               .FirstOrDefault();

            if (phase == null)
                throw new UserFriendlyException("Aviso", "La fase a la que pertenece la etapa ya no existe o fue eliminada. Verifique la información antes de continuar.");

            return new MilestoneGetDto()
            {
                Id = milestone.Id,
                Name = milestone.Value,
                Index = milestone.Order,
                Phase = new MilestonePhaseGetDto()
                {
                    Id = phase.Id,
                    Name = phase.Value
                }
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase_Edit)]
        public async Task Update(MilestoneUpdateDto input)
        {
            VerifyCount(await _parameterRepository.CountAsync(p => p.Id == input.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPMilestone));

            var phase = await _parameterRepository.GetAsync(input.Id);
            phase.Value = input.Name;
            phase.Order = input.Index;

            await _parameterRepository.UpdateAsync(ValidateEntity(phase));
        }

        private Parameter ValidateEntity(Parameter parameter)
        {
            parameter.Value.IsValidOrException("Aviso", "El nombre de la etapa es obligatorio");
            parameter.Value.VerifyTableColumn(
                MilestoneConsts.NameMinLength,
                MilestoneConsts.NameMaxLength,
                "Aviso",
                $"El nombre de la etapa no debe exceder los {MilestoneConsts.NameMaxLength} caracteres");

            return parameter;
        }
    }
}
