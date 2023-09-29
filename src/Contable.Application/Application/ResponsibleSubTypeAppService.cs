using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.ResponsibleSubTypes;
using Contable.Application.ResponsibleSubTypes.Dto;
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
using Abp.UI;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType)]
    public class ResponsibleSubTypeAppService: ContableAppServiceBase, IResponsibleSubTypeAppService
    {
        private readonly IRepository<ResponsibleType> _responsibleTypeRepository;
        private readonly IRepository<ResponsibleSubType> _responsibleSubTypeRepository;

        public ResponsibleSubTypeAppService(
            IRepository<ResponsibleType> responsibleTypeRepository,
            IRepository<ResponsibleSubType> responsibleSubTypeRepository)
        {
            _responsibleTypeRepository = responsibleTypeRepository;
            _responsibleSubTypeRepository = responsibleSubTypeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType_Create)]
        public async Task Create(ResponsibleSubTypeCreateDto input)
        {
            await _responsibleSubTypeRepository.InsertAsync(
                await ValidateEntity(
                    input: ObjectMapper.Map<ResponsibleSubType>(input),
                    responsibleTypeId: input.ResponsibleType == null ? -1 : input.ResponsibleType.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _responsibleSubTypeRepository.CountAsync(p => p.Id == input.Id));

            await _responsibleSubTypeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType)]
        public async Task<ResponsibleSubTypeGetDto> Get(EntityDto input)
        {
            VerifyCount(await _responsibleSubTypeRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<ResponsibleSubTypeGetDto>(_responsibleSubTypeRepository
                .GetAll()
                .Include(p => p.ResponsibleType)
                .Where(p => p.Id == input.Id)
                .First());
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType)]
        public async Task<PagedResultDto<ResponsibleSubTypeGetAllDto>> GetAll(ResponsibleSubTypeGetAllInputDto input)
        {
            var query = _responsibleSubTypeRepository
                .GetAll()
                .Include(p => p.ResponsibleType)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<ResponsibleSubType, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ResponsibleSubTypeGetAllDto>(count, ObjectMapper.Map<List<ResponsibleSubTypeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType_Edit)]
        public async Task Update(ResponsibleSubTypeUpdateDto input)
        {
            VerifyCount(await _responsibleSubTypeRepository.CountAsync(p => p.Id == input.Id));

            await _responsibleSubTypeRepository.UpdateAsync(
                await ValidateEntity(
                    input: ObjectMapper.Map(input, await _responsibleSubTypeRepository.GetAsync(input.Id)),
                    responsibleTypeId: input.ResponsibleType == null ? -1 : input.ResponsibleType.Id));
        }

        private async Task<ResponsibleSubType> ValidateEntity(ResponsibleSubType input, int responsibleTypeId)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del subtipo de actor es obligatorio");
            input.Name.VerifyTableColumn(
                ResponsibleSubTypeConsts.NameMinLength, 
                ResponsibleSubTypeConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del subtipo de actor no debe exceder los {ResponsibleSubTypeConsts.NameMaxLength} caracteres");

            if (await _responsibleTypeRepository.CountAsync(p => p.Id == responsibleTypeId) == 0)
                throw new UserFriendlyException("Aviso", "El tipo de actor seleccionado ya no existe o fue eliminado. Por favor verifique la información antes de continuar");

            var responsibleType = _responsibleTypeRepository
                .GetAll()
                .Where(p => p.Id == responsibleTypeId)
                .First();

            input.ResponsibleType = responsibleType;
            input.ResponsibleTypeId = responsibleType.Id;

            return input;
        }
    }
}
