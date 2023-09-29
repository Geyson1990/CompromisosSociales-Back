using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DirectoryResponsibles;
using Contable.Application.DirectoryResponsibles.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryResponsible)]
    public class DirectoryResponsibleAppService : ContableAppServiceBase, IDirectoryResponsibleAppService
    {
        private readonly IRepository<DirectoryResponsible> _directoryResponsibleRepository;

        public DirectoryResponsibleAppService(IRepository<DirectoryResponsible> directoryResponsibleRepository)
        {
            _directoryResponsibleRepository = directoryResponsibleRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryResponsible_Create)]
        public async Task Create(DirectoryResponsibleCreateDto input)
        {
            await _directoryResponsibleRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DirectoryResponsible>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryResponsible_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _directoryResponsibleRepository.CountAsync(p => p.Id == input.Id));

            await _directoryResponsibleRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryResponsible)]
        public async Task<DirectoryResponsibleGetDto> Get(EntityDto input)
        {
            VerifyCount(await _directoryResponsibleRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DirectoryResponsibleGetDto>(await _directoryResponsibleRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryResponsible)]
        public async Task<PagedResultDto<DirectoryResponsibleGetAllDto>> GetAll(DirectoryResponsibleGetAllInputDto input)
        {
            var query = _directoryResponsibleRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DirectoryResponsible, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DirectoryResponsibleGetAllDto>(count, ObjectMapper.Map<List<DirectoryResponsibleGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectoryResponsible_Edit)]
        public async Task Update(DirectoryResponsibleUpdateDto input)
        {
            VerifyCount(await _directoryResponsibleRepository.CountAsync(p => p.Id == input.Id));

            await _directoryResponsibleRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _directoryResponsibleRepository.GetAsync(input.Id))));
        }

        private DirectoryResponsible ValidateEntity(DirectoryResponsible input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del responsable es obligatorio");
            input.Name.VerifyTableColumn(DirectoryResponsibleConsts.NameMinLength, 
                DirectoryResponsibleConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del responsable no debe exceder los {DirectoryResponsibleConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
