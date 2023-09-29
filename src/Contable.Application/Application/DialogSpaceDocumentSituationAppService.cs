using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DialogSpaceDocumentSituations;
using Contable.Application.DialogSpaceDocumentSituations.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation)]
    public class DialogSpaceDocumentSituationAppService: ContableAppServiceBase, IDialogSpaceDocumentSituationAppService
    {
        private readonly IRepository<DialogSpaceDocumentSituation> _dialogSpaceDocumentSituationRepository;

        public DialogSpaceDocumentSituationAppService(IRepository<DialogSpaceDocumentSituation> dialogSpaceDocumentSituationRepository)
        {
            _dialogSpaceDocumentSituationRepository = dialogSpaceDocumentSituationRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation_Create)]
        public async Task Create(DialogSpaceDocumentSituationCreateDto input)
        {
            await _dialogSpaceDocumentSituationRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DialogSpaceDocumentSituation>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _dialogSpaceDocumentSituationRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceDocumentSituationRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation)]
        public async Task<DialogSpaceDocumentSituationGetDto> Get(EntityDto input)
        {
            VerifyCount(await _dialogSpaceDocumentSituationRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DialogSpaceDocumentSituationGetDto>(await _dialogSpaceDocumentSituationRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation)]
        public async Task<PagedResultDto<DialogSpaceDocumentSituationGetAllDto>> GetAll(DialogSpaceDocumentSituationGetAllInputDto input)
        {
            var query = _dialogSpaceDocumentSituationRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DialogSpaceDocumentSituation, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DialogSpaceDocumentSituationGetAllDto>(count, ObjectMapper.Map<List<DialogSpaceDocumentSituationGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation_Edit)]
        public async Task Update(DialogSpaceDocumentSituationUpdateDto input)
        {
            VerifyCount(await _dialogSpaceDocumentSituationRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceDocumentSituationRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _dialogSpaceDocumentSituationRepository.GetAsync(input.Id))));
        }

        private DialogSpaceDocumentSituation ValidateEntity(DialogSpaceDocumentSituation input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la Situación de Documentos de Espacio de Diálogo es obligatorio");
            input.Name.VerifyTableColumn(DialogSpaceDocumentSituationConsts.NameMinLength, 
                DialogSpaceDocumentSituationConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre de la Situación de Documentos de Espacio de Diálogo no debe exceder los {DialogSpaceDocumentSituationConsts.NameMaxLength} caracteres");            
            
            return input;
        }
    }
}
