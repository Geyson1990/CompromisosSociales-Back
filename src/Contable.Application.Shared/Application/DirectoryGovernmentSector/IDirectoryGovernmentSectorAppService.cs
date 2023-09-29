using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectoryGovernmentSectors.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectoryGovernmentSectors
{
    public interface IDirectoryGovernmentSectorAppService : IApplicationService
    {
        Task Create(DirectoryGovernmentSectorCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectoryGovernmentSectorGetDto> Get(EntityDto input);
        Task<PagedResultDto<DirectoryGovernmentSectorGetAllDto>> GetAll(DirectoryGovernmentSectorGetAllInputDto input);
        Task Update(DirectoryGovernmentSectorUpdateDto input);
    }
}
