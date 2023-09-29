using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.InterventionPlanActivities;
using Contable.Application.InterventionPlanActivities.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanActivity)]
    public class InterventionPlanActivityAppService : ContableAppServiceBase, IInterventionPlanActivityAppService
    {
        private readonly IRepository<InterventionPlanActivity> _interventionPlanActivityRepository;

        public InterventionPlanActivityAppService(IRepository<InterventionPlanActivity> interventionPlanActivityRepository)
        {
            _interventionPlanActivityRepository = interventionPlanActivityRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanActivity_Create)]
        public async Task Create(InterventionPlanActivityCreateDto input)
        {
            await _interventionPlanActivityRepository.InsertAsync(ValidateActivity(ObjectMapper.Map<InterventionPlanActivity>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanActivity_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _interventionPlanActivityRepository.CountAsync(p => p.Id == input.Id));

            await _interventionPlanActivityRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanActivity)]
        public async Task<InterventionPlanActivityGetDto> Get(EntityDto input)
        {
            VerifyCount(await _interventionPlanActivityRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<InterventionPlanActivityGetDto>(await _interventionPlanActivityRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanActivity)]
        public async Task<PagedResultDto<InterventionPlanActivityGetAllDto>> GetAll(InterventionPlanActivityGetAllInputDto input)
        {
            var query = _interventionPlanActivityRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<InterventionPlanActivity, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<InterventionPlanActivityGetAllDto>(count, ObjectMapper.Map<List<InterventionPlanActivityGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanActivity_Edit)]
        public async Task Update(InterventionPlanActivityUpdateDto input)
        {
            VerifyCount(await _interventionPlanActivityRepository.CountAsync(p => p.Id == input.Id));

            await _interventionPlanActivityRepository.UpdateAsync(ValidateActivity(ObjectMapper.Map(input, await _interventionPlanActivityRepository.GetAsync(input.Id))));
        }

        private InterventionPlanActivity ValidateActivity(InterventionPlanActivity input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de actividad es obligatorio");
            input.Name.VerifyTableColumn(
                InterventionPlanActivityConsts.NameMinLength, 
                InterventionPlanActivityConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del tipo de actividad no debe exceder los {InterventionPlanActivityConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
