using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.ActorMovements;
using Contable.Application.ActorMovements.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorMovement)]
    public class ActorMovementAppService: ContableAppServiceBase, IActorMovementAppService
    {
        private readonly IRepository<ActorMovement> _actorMovementRepository;

        public ActorMovementAppService(IRepository<ActorMovement> actorMovementRepository)
        {
            _actorMovementRepository = actorMovementRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorMovement_Create)]
        public async Task Create(ActorMovementCreateDto input)
        {
            await _actorMovementRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<ActorMovement>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorMovement_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _actorMovementRepository.CountAsync(p => p.Id == input.Id));

            await _actorMovementRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorMovement)]
        public async Task<ActorMovementGetDto> Get(EntityDto input)
        {
            VerifyCount(await _actorMovementRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<ActorMovementGetDto>(await _actorMovementRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorMovement)]
        public async Task<PagedResultDto<ActorMovementGetAllDto>> GetAll(ActorMovementGetAllInputDto input)
        {
            var query = _actorMovementRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<ActorMovement, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ActorMovementGetAllDto>(count, ObjectMapper.Map<List<ActorMovementGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorMovement_Edit)]
        public async Task Update(ActorMovementUpdateDto input)
        {
            VerifyCount(await _actorMovementRepository.CountAsync(p => p.Id == input.Id));

            await _actorMovementRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _actorMovementRepository.GetAsync(input.Id))));
        }

        private ActorMovement ValidateEntity(ActorMovement input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de movilización es obligatorio");
            input.Name.VerifyTableColumn(ActorMovementConsts.NameMinLength, ActorMovementConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del tipo de movilización no debe exceder los {ActorMovementConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
