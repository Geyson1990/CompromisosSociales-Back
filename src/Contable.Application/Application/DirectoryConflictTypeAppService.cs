using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DirectoryConflictTypes;
using Contable.Application.DirectoryConflictTypes.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryConflictType)]
    public class DirectoryConflictTypeAppService: ContableAppServiceBase, IDirectoryConflictTypeAppService
    {
        private readonly IRepository<DirectoryConflictType> _directoryConflictTypeRepository;

        public DirectoryConflictTypeAppService(IRepository<DirectoryConflictType> directoryConflictTypeRepository)
        {
            _directoryConflictTypeRepository = directoryConflictTypeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryConflictType_Create)]
        public async Task Create(DirectoryConflictTypeCreateDto input)
        {
            await _directoryConflictTypeRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DirectoryConflictType>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryConflictType_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _directoryConflictTypeRepository.CountAsync(p => p.Id == input.Id));

            await _directoryConflictTypeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryConflictType)]
        public async Task<DirectoryConflictTypeGetDto> Get(EntityDto input)
        {
            VerifyCount(await _directoryConflictTypeRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DirectoryConflictTypeGetDto>(await _directoryConflictTypeRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryConflictType)]
        public async Task<PagedResultDto<DirectoryConflictTypeGetAllDto>> GetAll(DirectoryConflictTypeGetAllInputDto input)
        {
            var query = _directoryConflictTypeRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DirectoryConflictType, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DirectoryConflictTypeGetAllDto>(count, ObjectMapper.Map<List<DirectoryConflictTypeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryConflictType_Edit)]
        public async Task Update(DirectoryConflictTypeUpdateDto input)
        {
            VerifyCount(await _directoryConflictTypeRepository.CountAsync(p => p.Id == input.Id));

            await _directoryConflictTypeRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _directoryConflictTypeRepository.GetAsync(input.Id))));
        }

        private DirectoryConflictType ValidateEntity(DirectoryConflictType input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre es obligatorio");
            input.Name.VerifyTableColumn(DirectoryConflictTypeConsts.NameMinLength, 
                DirectoryConflictTypeConsts.NameMaxLength,
                DefaultTitleMessage, 
                $"El nombre no debe exceder los {DirectoryConflictTypeConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
