using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DirectoryGovernmentSectors;
using Contable.Application.DirectoryGovernmentSectors.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector)]
    public class DirectoryGovernmentSectorAppService : ContableAppServiceBase, IDirectoryGovernmentSectorAppService
    {
        private readonly IRepository<DirectoryGovernmentSector> _directoryGovernmentSectorRepository;

        public DirectoryGovernmentSectorAppService(IRepository<DirectoryGovernmentSector> directoryGovernmentSectorRepository)
        {
            _directoryGovernmentSectorRepository = directoryGovernmentSectorRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector_Create)]
        public async Task Create(DirectoryGovernmentSectorCreateDto input)
        {
            await _directoryGovernmentSectorRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DirectoryGovernmentSector>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _directoryGovernmentSectorRepository.CountAsync(p => p.Id == input.Id));

            await _directoryGovernmentSectorRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector)]
        public async Task<DirectoryGovernmentSectorGetDto> Get(EntityDto input)
        {
            VerifyCount(await _directoryGovernmentSectorRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DirectoryGovernmentSectorGetDto>(await _directoryGovernmentSectorRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector)]
        public async Task<PagedResultDto<DirectoryGovernmentSectorGetAllDto>> GetAll(DirectoryGovernmentSectorGetAllInputDto input)
        {
            var query = _directoryGovernmentSectorRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DirectoryGovernmentSector, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DirectoryGovernmentSectorGetAllDto>(count, ObjectMapper.Map<List<DirectoryGovernmentSectorGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector_Edit)]
        public async Task Update(DirectoryGovernmentSectorUpdateDto input)
        {
            VerifyCount(await _directoryGovernmentSectorRepository.CountAsync(p => p.Id == input.Id));

            await _directoryGovernmentSectorRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _directoryGovernmentSectorRepository.GetAsync(input.Id))));
        }

        private DirectoryGovernmentSector ValidateEntity(DirectoryGovernmentSector input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del sector es obligatorio");
            input.Name.VerifyTableColumn(DirectoryGovernmentSectorConsts.NameMinLength, 
                DirectoryGovernmentSectorConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del sector no debe exceder los {DirectoryGovernmentSectorConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
