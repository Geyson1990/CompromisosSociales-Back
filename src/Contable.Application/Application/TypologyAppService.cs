using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.Typologies;
using Contable.Application.Typologies.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology)]
    public class TypologyAppService : ContableAppServiceBase, ITypologyAppService
    {
        private readonly IRepository<Typology> _typologyRepository;
        private readonly IRepository<SubTypology> _subTypologyRepository;

        public TypologyAppService(IRepository<Typology> typologyRepository, IRepository<SubTypology> subTypologyRepository)
        {
            _typologyRepository = typologyRepository;
            _subTypologyRepository = subTypologyRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology_Create)]
        public async Task Create(TypologyCreateDto input)
        {
            await _typologyRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<Typology>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _typologyRepository.CountAsync(p => p.Id == input.Id));

            await _typologyRepository.DeleteAsync(input.Id);
            await _subTypologyRepository.DeleteAsync(p => p.TypologyId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology)]
        public async Task<TypologyGetDto> Get(EntityDto input)
        {
            VerifyCount(await _typologyRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<TypologyGetDto>(await _typologyRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology)]
        public async Task<PagedResultDto<TypologyGetAllDto>> GetAll(TypologyGetAllInputDto input)
        {
            var query = _typologyRepository
                .GetAll()
                .Include(p => p.SubTypologies)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<Typology, bool>>)
                        (expression => expression.SubTypologies.Any(d => EF.Functions.Like(d.Name, $"%{word}%")) ||
                                       EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<TypologyGetAllDto>(count, ObjectMapper.Map<List<TypologyGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology_Edit)]
        public async Task Update(TypologyUpdateDto input)
        {
            VerifyCount(await _typologyRepository.CountAsync(p => p.Id == input.Id));

            await _typologyRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _typologyRepository.GetAsync(input.Id))));
        }

        private Typology ValidateEntity(Typology input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la tipología general es obligatorio");
            input.Name.VerifyTableColumn(TypologyConsts.NameMinLength, TypologyConsts.NameMaxLength, DefaultTitleMessage, $"El nombre de la tipología general no debe exceder los {TypologyConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
