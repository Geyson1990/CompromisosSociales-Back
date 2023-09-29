using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectoryGovernmentLevels.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectoryGovernmentLevels
{
    public interface IDirectoryGovernmentLevelAppService : IApplicationService
    {
        Task Create(DirectoryGovernmentLevelCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectoryGovernmentLevelGetDto> Get(EntityDto input);
        Task<PagedResultDto<DirectoryGovernmentLevelGetAllDto>> GetAll(DirectoryGovernmentLevelGetAllInputDto input);
        Task Update(DirectoryGovernmentLevelUpdateDto input);
    }
}
