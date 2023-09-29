using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Contable.Application.Extensions;
using Contable.Application.Phases;
using Contable.Application.Phases.Dto;
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
    public class PhaseAppService : ContableAppServiceBase, IPhaseAppService
    {
        private readonly IRepository<Parameter> _parameterRepository;
        private readonly IRepository<ParameterCategory> _parameterCategoryRepository;

        public PhaseAppService(
            IRepository<Parameter> parameterRepository, 
            IRepository<ParameterCategory> parameterCategoryRepository)
        {
            _parameterRepository = parameterRepository;
            _parameterCategoryRepository = parameterCategoryRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase_Create)]
        public async Task Create(PhaseCreateDto input)
        {
            if (await _parameterCategoryRepository.CountAsync(p => p.Code == CompromiseConsts.ParameterCategoryPIPPhase) == 0)
                throw new UserFriendlyException("Aviso", "Error faltal, hay una inconsistensia en las categorías de las fases.");

            var category = _parameterCategoryRepository
                .GetAll()
                .Where(p => p.Code == CompromiseConsts.ParameterCategoryPIPPhase)
                .First();

            var phase = new Parameter();
            phase.Value = input.Name;
            phase.Order = input.Index;
            phase.ParameterCategory = category;
            phase.ParameterCategoryId = category.Id;
            phase.ParentId = 0;
            phase.Step = 0;

            await _parameterRepository.InsertAsync(ValidateEntity(phase));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _parameterRepository.CountAsync(p => p.Id == input.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPPhase));

            await _parameterRepository.DeleteAsync(p => p.Id == input.Id);
            await _parameterRepository.DeleteAsync(p => p.ParentId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase)]
        public async Task<PhaseGetDto> Get(EntityDto input)
        {
            VerifyCount(await _parameterRepository.CountAsync(p => p.Id == input.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPPhase));

            var phase = await _parameterRepository.GetAsync(input.Id);

            return new PhaseGetDto()
            {
                Id = phase.Id,
                Name = phase.Value,
                Index = phase.Order
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase)]
        public async Task<PagedResultDto<PhaseGetAllDto>> GetAll(PhaseGetAllInputDto input)
        {
            var query = _parameterRepository
                .GetAll()
                .Where(p => p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPPhase);

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input).ToList();
            var output = new List<PhaseGetAllDto>();

            foreach(var phase in result)
            {
                var milestones = _parameterRepository
                    .GetAll()
                    .Where(p => p.ParentId == phase.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPMilestone)
                    .OrderBy(p => p.Order)
                    .Select(p => new PhaseMilestoneGetAllDto()
                    {
                        Id = p.Id,
                        Name = p.Value,
                        Index = p.Order
                    }).ToList();

                output.Add(new PhaseGetAllDto()
                {
                    Id = phase.Id,
                    Name = phase.Value,
                    Index = phase.Order,
                    Milestones = milestones
                });
            }

            return new PagedResultDto<PhaseGetAllDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Phase_Edit)]
        public async Task Update(PhaseUpdateDto input)
        {
            VerifyCount(await _parameterRepository.CountAsync(p => p.Id == input.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPPhase));

            var phase = await _parameterRepository.GetAsync(input.Id);
            phase.Value = input.Name;
            phase.Order = input.Index;

            await _parameterRepository.UpdateAsync(ValidateEntity(phase));
        }

        private Parameter ValidateEntity(Parameter parameter)
        {
            parameter.Value.IsValidOrException("Aviso", "El nombre de la fase es obligatorio");
            parameter.Value.VerifyTableColumn(
                PhaseConsts.NameMinLength,
                PhaseConsts.NameMaxLength,
                "Aviso",
                $"El nombre de la fase no debe exceder los {PhaseConsts.NameMaxLength} caracteres");

            return parameter;
        }
    }
}
