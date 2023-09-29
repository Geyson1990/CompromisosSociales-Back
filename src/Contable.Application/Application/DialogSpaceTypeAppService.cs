using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DialogSpaceTypes;
using Contable.Application.DialogSpaceTypes.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceType)]
    public class DialogSpaceTypeAppService: ContableAppServiceBase, IDialogSpaceTypeAppService
    {
        private readonly IRepository<DialogSpaceType> _dialogSpaceTypeRepository;

        public DialogSpaceTypeAppService(IRepository<DialogSpaceType> dialogSpaceTypeRepository)
        {
            _dialogSpaceTypeRepository = dialogSpaceTypeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceType_Create)]
        public async Task Create(DialogSpaceTypeCreateDto input)
        {
            await _dialogSpaceTypeRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DialogSpaceType>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceType_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _dialogSpaceTypeRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceTypeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceType)]
        public async Task<DialogSpaceTypeGetDto> Get(EntityDto input)
        {
            VerifyCount(await _dialogSpaceTypeRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DialogSpaceTypeGetDto>(await _dialogSpaceTypeRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceType)]
        public async Task<PagedResultDto<DialogSpaceTypeGetAllDto>> GetAll(DialogSpaceTypeGetAllInputDto input)
        {
            var query = _dialogSpaceTypeRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DialogSpaceType, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DialogSpaceTypeGetAllDto>(count, ObjectMapper.Map<List<DialogSpaceTypeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceType_Edit)]
        public async Task Update(DialogSpaceTypeUpdateDto input)
        {
            VerifyCount(await _dialogSpaceTypeRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceTypeRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _dialogSpaceTypeRepository.GetAsync(input.Id))));
        }

        private DialogSpaceType ValidateEntity(DialogSpaceType input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del Tipo de Espacio de Diálogo es obligatorio");
            input.Name.VerifyTableColumn(DialogSpaceTypeConsts.NameMinLength, DialogSpaceTypeConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del Tipo de Espacio de Diálogo no debe exceder los {DialogSpaceTypeConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
