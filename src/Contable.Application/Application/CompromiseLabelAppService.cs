using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.CompromiseLabels;
using Contable.Application.CompromiseLabels.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseLabel)]
    public class CompromiseLabelAppService: ContableAppServiceBase, ICompromiseLabelAppService
    {
        private readonly IRepository<CompromiseLabel> _compromiseLabelRepository;

        public CompromiseLabelAppService(IRepository<CompromiseLabel> compromiseLabelRepository)
        {
            _compromiseLabelRepository = compromiseLabelRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseLabel_Create)]
        public async Task Create(CompromiseLabelCreateDto input)
        {
            await _compromiseLabelRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<CompromiseLabel>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseLabel_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _compromiseLabelRepository.CountAsync(p => p.Id == input.Id));

            await _compromiseLabelRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseLabel)]
        public async Task<CompromiseLabelGetDto> Get(EntityDto input)
        {
            VerifyCount(await _compromiseLabelRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<CompromiseLabelGetDto>(await _compromiseLabelRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseLabel)]
        public async Task<PagedResultDto<CompromiseLabelGetAllDto>> GetAll(CompromiseLabelGetAllInputDto input)
        {
            var query = _compromiseLabelRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<CompromiseLabel, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<CompromiseLabelGetAllDto>(count, ObjectMapper.Map<List<CompromiseLabelGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CompromiseLabel_Edit)]
        public async Task Update(CompromiseLabelUpdateDto input)
        {
            VerifyCount(await _compromiseLabelRepository.CountAsync(p => p.Id == input.Id));

            await _compromiseLabelRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _compromiseLabelRepository.GetAsync(input.Id))));
        }

        private CompromiseLabel ValidateEntity(CompromiseLabel input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la etiqueta de compromisos es obligatoria");
            input.Name.VerifyTableColumn(
                CompromiseLabelConsts.NameMinLength,
                CompromiseLabelConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre de la etiqueta de compromisos no debe exceder los {CompromiseLabelConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
