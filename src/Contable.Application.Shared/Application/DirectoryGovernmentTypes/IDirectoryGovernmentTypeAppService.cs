using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectoryGovernmentTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectoryGovernmentTypes
{
    public interface IDirectoryGovernmentTypeAppService : IApplicationService
    {
        Task Create(DirectoryGovernmentTypeCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectoryGovernmentTypeGetDto> Get(EntityDto input);
        Task<PagedResultDto<DirectoryGovernmentTypeGetAllDto>> GetAll(DirectoryGovernmentTypeGetAllInputDto input);
        Task Update(DirectoryGovernmentTypeUpdateDto input);
    }
}
