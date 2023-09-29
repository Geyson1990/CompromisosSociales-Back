using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.Facts;
using Contable.Application.Facts.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Fact)]
    public class FactAppService: ContableAppServiceBase, IFactAppService
    {
        private readonly IRepository<Fact> _factRepository;
        public FactAppService(IRepository<Fact> factRepository)
        {
            _factRepository = factRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Fact_Create)]
        public async Task Create(FactCreateDto input)
        {
            await _factRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<Fact>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Fact_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _factRepository.CountAsync(p => p.Id == input.Id));

            await _factRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Fact)]
        public async Task<FactGetDto> Get(EntityDto input)
        {
            VerifyCount(await _factRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<FactGetDto>(await _factRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Fact)]
        public async Task<PagedResultDto<FactGetAllDto>> GetAll(FactGetAllInputDto input)
        {
            var query = _factRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<Fact, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<FactGetAllDto>(count, ObjectMapper.Map<List<FactGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Fact_Edit)]
        public async Task Update(FactUpdateDto input)
        {
            VerifyCount(await _factRepository.CountAsync(p => p.Id == input.Id));

            await _factRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _factRepository.GetAsync(input.Id))));
        }

        private Fact ValidateEntity(Fact input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del hecho es obligatorio");
            input.Name.VerifyTableColumn(FactConsts.NameMinLength, FactConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del hecho no debe exceder los {FactConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
