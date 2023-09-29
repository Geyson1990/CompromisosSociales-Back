using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.InterventionPlanRoles;
using Contable.Application.InterventionPlanRoles.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanRole)]
    public class InterventionPlanRoleAppService : ContableAppServiceBase, IInterventionPlanRoleAppService
    {
        private readonly IRepository<InterventionPlanRole> _interventionPlanRoleRepository;

        public InterventionPlanRoleAppService(IRepository<InterventionPlanRole> interventionPlanRoleRepository)
        {
            _interventionPlanRoleRepository = interventionPlanRoleRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanRole_Create)]
        public async Task Create(InterventionPlanRoleCreateDto input)
        {
            await _interventionPlanRoleRepository.InsertAsync(ValidateRole(ObjectMapper.Map<InterventionPlanRole>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanRole_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _interventionPlanRoleRepository.CountAsync(p => p.Id == input.Id));

            await _interventionPlanRoleRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanRole)]
        public async Task<InterventionPlanRoleGetDto> Get(EntityDto input)
        {
            VerifyCount(await _interventionPlanRoleRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<InterventionPlanRoleGetDto>(await _interventionPlanRoleRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanRole)]
        public async Task<PagedResultDto<InterventionPlanRoleGetAllDto>> GetAll(InterventionPlanRoleGetAllInputDto input)
        {
            var query = _interventionPlanRoleRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<InterventionPlanRole, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<InterventionPlanRoleGetAllDto>(count, ObjectMapper.Map<List<InterventionPlanRoleGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_InterventionPlanRole_Edit)]
        public async Task Update(InterventionPlanRoleUpdateDto input)
        {
            VerifyCount(await _interventionPlanRoleRepository.CountAsync(p => p.Id == input.Id));

            await _interventionPlanRoleRepository.UpdateAsync(ValidateRole(ObjectMapper.Map(input, await _interventionPlanRoleRepository.GetAsync(input.Id))));
        }

        private InterventionPlanRole ValidateRole(InterventionPlanRole input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El rol es obligatorio");
            input.Name.VerifyTableColumn(
                InterventionPlanRoleConsts.NameMinLength, 
                InterventionPlanRoleConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del rol no debe exceder los {InterventionPlanRoleConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
