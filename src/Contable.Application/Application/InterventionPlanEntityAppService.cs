using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.InterventionPlanEntities;
using Contable.Application.InterventionPlanEntities.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanEntity)]
    public class InterventionPlanEntityAppService : ContableAppServiceBase, IInterventionPlanEntityAppService
    {
        private readonly IRepository<InterventionPlanEntity> _interventionPlanEntityRepository;

        public InterventionPlanEntityAppService(IRepository<InterventionPlanEntity> interventionPlanEntityRepository)
        {
            _interventionPlanEntityRepository = interventionPlanEntityRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanEntity_Create)]
        public async Task Create(InterventionPlanEntityCreateDto input)
        {
            await _interventionPlanEntityRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<InterventionPlanEntity>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanEntity_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _interventionPlanEntityRepository.CountAsync(p => p.Id == input.Id));

            await _interventionPlanEntityRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanEntity)]
        public async Task<InterventionPlanEntityGetDto> Get(EntityDto input)
        {
            VerifyCount(await _interventionPlanEntityRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<InterventionPlanEntityGetDto>(await _interventionPlanEntityRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanEntity)]
        public async Task<PagedResultDto<InterventionPlanEntityGetAllDto>> GetAll(InterventionPlanEntityGetAllInputDto input)
        {
            var query = _interventionPlanEntityRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<InterventionPlanEntity, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<InterventionPlanEntityGetAllDto>(count, ObjectMapper.Map<List<InterventionPlanEntityGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanEntity_Edit)]
        public async Task Update(InterventionPlanEntityUpdateDto input)
        {
            VerifyCount(await _interventionPlanEntityRepository.CountAsync(p => p.Id == input.Id));

            await _interventionPlanEntityRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _interventionPlanEntityRepository.GetAsync(input.Id))));
        }

        private InterventionPlanEntity ValidateEntity(InterventionPlanEntity input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de entidad es obligatorio");
            input.Name.VerifyTableColumn(
                InterventionPlanEntityConsts.NameMinLength, 
                InterventionPlanEntityConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del tipo de entidad no debe exceder los {InterventionPlanEntityConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
