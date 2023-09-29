using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.InterventionPlanOptions;
using Contable.Application.InterventionPlanOptions.Dto;
using Contable.Application.Extensions;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanOption)]
    public class InterventionPlanOptionAppService : ContableAppServiceBase, IInterventionPlanOptionAppService
    {
        private readonly IRepository<InterventionPlanOption> _interventionPlanOptionRepository;

        public InterventionPlanOptionAppService(IRepository<InterventionPlanOption> interventionPlanOptionRepository)
        {
            _interventionPlanOptionRepository = interventionPlanOptionRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanOption_Create)]
        public async Task Create(InterventionPlanOptionCreateDto input)
        {
            await _interventionPlanOptionRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<InterventionPlanOption>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanOption_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _interventionPlanOptionRepository.CountAsync(p => p.Id == input.Id));

            await _interventionPlanOptionRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanOption)]
        public async Task<InterventionPlanOptionGetDto> Get(EntityDto input)
        {
            VerifyCount(await _interventionPlanOptionRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<InterventionPlanOptionGetDto>(await _interventionPlanOptionRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanOption)]
        public async Task<PagedResultDto<InterventionPlanOptionGetAllDto>> GetAll(InterventionPlanOptionGetAllInputDto input)
        {
            var query = _interventionPlanOptionRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<InterventionPlanOption, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<InterventionPlanOptionGetAllDto>(count, ObjectMapper.Map<List<InterventionPlanOptionGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanOption_Edit)]
        public async Task Update(InterventionPlanOptionUpdateDto input)
        {
            VerifyCount(await _interventionPlanOptionRepository.CountAsync(p => p.Id == input.Id));

            await _interventionPlanOptionRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _interventionPlanOptionRepository.GetAsync(input.Id))));
        }

        private InterventionPlanOption ValidateEntity(InterventionPlanOption input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la opción de intervención es obligatorio");
            input.Name.VerifyTableColumn(InterventionPlanOptionConsts.NameMinLength, InterventionPlanOptionConsts.NameMaxLength, DefaultTitleMessage, $"El nombre de la opción de intervención no debe exceder los {InterventionPlanOptionConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
