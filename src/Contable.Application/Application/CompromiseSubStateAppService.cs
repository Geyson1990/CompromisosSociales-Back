using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Contable.Application.CompromiseSubStates;
using Contable.Application.CompromiseSubStates.Dto;
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
    public class CompromiseSubStateAppService : ContableAppServiceBase, ICompromiseSubStateAppService
    {
        private readonly IRepository<CompromiseState> _compromiseStateRepository;
        private readonly IRepository<CompromiseSubState> _compromiseSubStateRepository;

        public CompromiseSubStateAppService(
            IRepository<CompromiseState> compromiseStateRepository, 
            IRepository<CompromiseSubState> compromiseSubStateRepository)
        {
            _compromiseStateRepository = compromiseStateRepository;
            _compromiseSubStateRepository = compromiseSubStateRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState_Create)]
        public async Task Create(CompromiseSubStateCreateDto input)
        {
            await _compromiseSubStateRepository.InsertAsync(await ValidateEntity(
                input: ObjectMapper.Map<CompromiseSubState>(input),
                compromiseStateId: input.CompromiseState == null ? -1 : input.CompromiseState.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _compromiseSubStateRepository.CountAsync(p => p.Id == input.Id));

            await _compromiseSubStateRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState)]
        public async Task<CompromiseSubStateGetDto> Get(EntityDto input)
        {
            VerifyCount(await _compromiseSubStateRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<CompromiseSubStateGetDto>(await _compromiseSubStateRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState)]
        public async Task<PagedResultDto<CompromiseSubStateGetAllDto>> GetAll(CompromiseSubStateGetAllInputDto input)
        {
            var query = _compromiseSubStateRepository
                .GetAll()
                .Include(p => p.CompromiseState)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<CompromiseSubState, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<CompromiseSubStateGetAllDto>(count, ObjectMapper.Map<List<CompromiseSubStateGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseState_Edit)]
        public async Task Update(CompromiseSubStateUpdateDto input)
        {
            VerifyCount(await _compromiseSubStateRepository.CountAsync(p => p.Id == input.Id));

            var updateEntity = ObjectMapper.Map(input, await _compromiseSubStateRepository.GetAsync(input.Id));

            await _compromiseSubStateRepository.UpdateAsync(await ValidateEntity(
                input: updateEntity,
                compromiseStateId: updateEntity.CompromiseStateId));
        }

        private async Task<CompromiseSubState> ValidateEntity(CompromiseSubState input, int compromiseStateId)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del estado es obligatorio");
            input.Name.VerifyTableColumn(CompromiseSubStateConsts.NameMinLength,
                CompromiseSubStateConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del estado no debe exceder los {CompromiseSubStateConsts.NameMaxLength} caracteres");

            if (await _compromiseStateRepository.CountAsync(p => p.Id == compromiseStateId) == 0)
                throw new UserFriendlyException("Aviso", "El estado seleccionado es inválido o ya no existe. Verifique la información antes de continuar");

            var state = await _compromiseStateRepository.GetAsync(compromiseStateId);

            input.CompromiseState = state;
            input.CompromiseStateId = state.Id;

            return input;
        }
    }
}
