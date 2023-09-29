using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectoryIndustries.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectoryIndustries
{
    public interface IDirectoryIndustryAppService : IApplicationService
    {
        Task Create(DirectoryIndustryCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectoryIndustryGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<DirectoryIndustryGetAllDto>> GetAll(DirectoryIndustryGetAllInputDto input);
        Task Update(DirectoryIndustryUpdateDto input);
    }
}
