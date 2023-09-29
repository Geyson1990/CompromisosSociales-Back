using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DirectoryGovernmentLevels;
using Contable.Application.DirectoryGovernmentLevels.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel)]
    public class DirectoryGovernmentLevelAppService: ContableAppServiceBase, IDirectoryGovernmentLevelAppService
    {
        private readonly IRepository<DirectoryGovernmentLevel> _directoryGovernmentLevelRepository;

        public DirectoryGovernmentLevelAppService(IRepository<DirectoryGovernmentLevel> directoryGovernmentLevelRepository)
        {
            _directoryGovernmentLevelRepository = directoryGovernmentLevelRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel_Create)]
        public async Task Create(DirectoryGovernmentLevelCreateDto input)
        {
            await _directoryGovernmentLevelRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DirectoryGovernmentLevel>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _directoryGovernmentLevelRepository.CountAsync(p => p.Id == input.Id));

            await _directoryGovernmentLevelRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel)]
        public async Task<DirectoryGovernmentLevelGetDto> Get(EntityDto input)
        {
            VerifyCount(await _directoryGovernmentLevelRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DirectoryGovernmentLevelGetDto>(await _directoryGovernmentLevelRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel)]
        public async Task<PagedResultDto<DirectoryGovernmentLevelGetAllDto>> GetAll(DirectoryGovernmentLevelGetAllInputDto input)
        {
            var query = _directoryGovernmentLevelRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DirectoryGovernmentLevel, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DirectoryGovernmentLevelGetAllDto>(count, ObjectMapper.Map<List<DirectoryGovernmentLevelGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel_Edit)]
        public async Task Update(DirectoryGovernmentLevelUpdateDto input)
        {
            VerifyCount(await _directoryGovernmentLevelRepository.CountAsync(p => p.Id == input.Id));

            await _directoryGovernmentLevelRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _directoryGovernmentLevelRepository.GetAsync(input.Id))));
        }

        private DirectoryGovernmentLevel ValidateEntity(DirectoryGovernmentLevel input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre es obligatorio");
            input.Name.VerifyTableColumn(DirectoryGovernmentLevelConsts.NameMinLength, 
                DirectoryGovernmentLevelConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre no debe exceder los {DirectoryGovernmentLevelConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
