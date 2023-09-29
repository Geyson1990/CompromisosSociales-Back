using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DialogSpaceDocumentTypes;
using Contable.Application.DialogSpaceDocumentTypes.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType)]
    public class DialogSpaceDocumentTypeAppService: ContableAppServiceBase, IDialogSpaceDocumentTypeAppService
    {
        private readonly IRepository<DialogSpaceDocumentType> _dialogSpaceDocumentTypeRepository;

        public DialogSpaceDocumentTypeAppService(IRepository<DialogSpaceDocumentType> dialogSpaceDocumentTypeRepository)
        {
            _dialogSpaceDocumentTypeRepository = dialogSpaceDocumentTypeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType_Create)]
        public async Task Create(DialogSpaceDocumentTypeCreateDto input)
        {
            await _dialogSpaceDocumentTypeRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DialogSpaceDocumentType>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _dialogSpaceDocumentTypeRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceDocumentTypeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType)]
        public async Task<DialogSpaceDocumentTypeGetDto> Get(EntityDto input)
        {
            VerifyCount(await _dialogSpaceDocumentTypeRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DialogSpaceDocumentTypeGetDto>(await _dialogSpaceDocumentTypeRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType)]
        public async Task<PagedResultDto<DialogSpaceDocumentTypeGetAllDto>> GetAll(DialogSpaceDocumentTypeGetAllInputDto input)
        {
            var query = _dialogSpaceDocumentTypeRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DialogSpaceDocumentType, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DialogSpaceDocumentTypeGetAllDto>(count, ObjectMapper.Map<List<DialogSpaceDocumentTypeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType_Edit)]
        public async Task Update(DialogSpaceDocumentTypeUpdateDto input)
        {
            VerifyCount(await _dialogSpaceDocumentTypeRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceDocumentTypeRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _dialogSpaceDocumentTypeRepository.GetAsync(input.Id))));
        }

        private DialogSpaceDocumentType ValidateEntity(DialogSpaceDocumentType input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de documento es obligatorio");
            input.Name.VerifyTableColumn(DialogSpaceDocumentTypeConsts.NameMinLength, 
                DialogSpaceDocumentTypeConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del tipo de documento no debe exceder los {DialogSpaceDocumentTypeConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
