using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.CompromiseStates;
using Contable.Application.CompromiseStates.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState)]
    public class CompromiseStateAppService: ContableAppServiceBase, ICompromiseStateAppService
    {
        private readonly IRepository<CompromiseState> _compromiseStateRepository;
        private readonly IRepository<CompromiseSubState> _compromiseSubStateRepository;

        public CompromiseStateAppService(IRepository<CompromiseState> compromiseStateRepository, IRepository<CompromiseSubState> compromiseSubStateRepository)
        {
            _compromiseStateRepository = compromiseStateRepository;
            _compromiseSubStateRepository = compromiseSubStateRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState_Create)]
        public async Task Create(CompromiseStateCreateDto input)
        {
            await _compromiseStateRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<CompromiseState>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _compromiseStateRepository.CountAsync(p => p.Id == input.Id));

            await _compromiseStateRepository.DeleteAsync(p => p.Id == input.Id);
            await _compromiseSubStateRepository.DeleteAsync(p => p.CompromiseStateId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState)]
        public async Task<CompromiseStateGetDto> Get(EntityDto input)
        {
            VerifyCount(await _compromiseStateRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<CompromiseStateGetDto>(await _compromiseStateRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState)]
        public async Task<PagedResultDto<CompromiseStateGetAllDto>> GetAll(CompromiseStateGetAllInputDto input)
        {
            var query = _compromiseStateRepository
                .GetAll()
                .Include(p => p.SubStates)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<CompromiseState, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<CompromiseStateGetAllDto>(count, ObjectMapper.Map<List<CompromiseStateGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState_Edit)]
        public async Task Update(CompromiseStateUpdateDto input)
        {
            VerifyCount(await _compromiseStateRepository.CountAsync(p => p.Id == input.Id));

            await _compromiseStateRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _compromiseStateRepository.GetAsync(input.Id))));
        }

        private CompromiseState ValidateEntity(CompromiseState input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del estado es obligatorio");
            input.Name.VerifyTableColumn(CompromiseStateConsts.NameMinLength, 
                CompromiseStateConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del estado no debe exceder los {CompromiseStateConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
