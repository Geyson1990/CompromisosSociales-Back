using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.Sectors;
using Contable.Application.Sectors.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Sector)]
    public class SectorAppService : ContableAppServiceBase, ISectorAppService
    {
        private readonly IRepository<Sector> _sectorRepository;

        public SectorAppService(IRepository<Sector> sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Sector_Create)]
        public async Task Create(SectorCreateDto input)
        {
            await _sectorRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<Sector>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Sector_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _sectorRepository.CountAsync(p => p.Id == input.Id));

            await _sectorRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Sector)]
        public async Task<SectorGetDto> Get(EntityDto input)
        {
            VerifyCount(await _sectorRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<SectorGetDto>(await _sectorRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Sector)]
        public async Task<PagedResultDto<SectorGetAllDto>> GetAll(SectorGetAllInputDto input)
        {
            var query = _sectorRepository
                .GetAll()
                .LikeAllBidirectional(input.Filter.SplitByLike()
                    .Select(word => (Expression<Func<Sector, bool>>) (expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<SectorGetAllDto>(count, ObjectMapper.Map<List<SectorGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Sector_Edit)]
        public async Task Update(SectorUpdateDto input)
        {
            VerifyCount(await _sectorRepository.CountAsync(p => p.Id == input.Id));

            await _sectorRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _sectorRepository.GetAsync(input.Id))));
        }

        private Sector ValidateEntity(Sector input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del sector es obligatorio");
            input.Name.VerifyTableColumn(SectorConsts.NameMinLength, SectorConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del sector no debe exceder los {SectorConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
