using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.ActorTypes;
using Contable.Application.ActorTypes.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorType)]
    public class ActorTypeAppService: ContableAppServiceBase, IActorTypeAppService
    {
        private readonly IRepository<ActorType> _actorTypeRepository;

        public ActorTypeAppService(IRepository<ActorType> actorTypeRepository)
        {
            _actorTypeRepository = actorTypeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorType_Create)]
        public async Task Create(ActorTypeCreateDto input)
        {
            await _actorTypeRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<ActorType>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorType_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _actorTypeRepository.CountAsync(p => p.Id == input.Id));

            await _actorTypeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorType)]
        public async Task<ActorTypeGetDto> Get(EntityDto input)
        {
            VerifyCount(await _actorTypeRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<ActorTypeGetDto>(await _actorTypeRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorType)]
        public async Task<PagedResultDto<ActorTypeGetAllDto>> GetAll(ActorTypeGetAllInputDto input)
        {
            var query = _actorTypeRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<ActorType, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ActorTypeGetAllDto>(count, ObjectMapper.Map<List<ActorTypeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ActorType_Edit)]
        public async Task Update(ActorTypeUpdateDto input)
        {
            VerifyCount(await _actorTypeRepository.CountAsync(p => p.Id == input.Id));

            await _actorTypeRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _actorTypeRepository.GetAsync(input.Id))));
        }

        private ActorType ValidateEntity(ActorType input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de actor es obligatorio");
            input.Name.VerifyTableColumn(ActorTypeConsts.NameMinLength, ActorTypeConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del tipo de actor no debe exceder los {ActorTypeConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
