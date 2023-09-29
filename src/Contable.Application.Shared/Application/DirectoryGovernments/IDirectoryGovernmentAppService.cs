using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectoryGovernments.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectoryGovernments
{
    public interface IDirectoryGovernmentAppService : IApplicationService
    {
        Task Create(DirectoryGovernmentCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectoryGovernmentGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<DirectoryGovernmentGetAllDto>> GetAll(DirectoryGovernmentGetAllInputDto input);
        Task Update(DirectoryGovernmentUpdateDto input);
    }
}
