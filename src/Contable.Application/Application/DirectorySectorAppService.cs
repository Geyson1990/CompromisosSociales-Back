using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DirectorySectors;
using Contable.Application.DirectorySectors.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectorySector)]
    public class DirectorySectorAppService : ContableAppServiceBase, IDirectorySectorAppService
    {
        private readonly IRepository<DirectorySector> _directorySectorRepository;

        public DirectorySectorAppService(IRepository<DirectorySector> directorySectorRepository)
        {
            _directorySectorRepository = directorySectorRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectorySector_Create)]
        public async Task Create(DirectorySectorCreateDto input)
        {
            await _directorySectorRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DirectorySector>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectorySector_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _directorySectorRepository.CountAsync(p => p.Id == input.Id));

            await _directorySectorRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectorySector)]
        public async Task<DirectorySectorGetDto> Get(EntityDto input)
        {
            VerifyCount(await _directorySectorRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DirectorySectorGetDto>(await _directorySectorRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectorySector)]
        public async Task<PagedResultDto<DirectorySectorGetAllDto>> GetAll(DirectorySectorGetAllInputDto input)
        {
            var query = _directorySectorRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DirectorySector, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DirectorySectorGetAllDto>(count, ObjectMapper.Map<List<DirectorySectorGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DirectorySector_Edit)]
        public async Task Update(DirectorySectorUpdateDto input)
        {
            VerifyCount(await _directorySectorRepository.CountAsync(p => p.Id == input.Id));

            await _directorySectorRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _directorySectorRepository.GetAsync(input.Id))));
        }

        private DirectorySector ValidateEntity(DirectorySector input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del sector o rubro es obligatorio");
            input.Name.VerifyTableColumn(DirectorySectorConsts.NameMinLength, 
                DirectorySectorConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del sector o rubro no debe exceder los {DirectorySectorConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
