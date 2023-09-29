using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectorySectors.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectorySectors
{
    public interface IDirectorySectorAppService : IApplicationService
    {
        Task Create(DirectorySectorCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectorySectorGetDto> Get(EntityDto input);
        Task<PagedResultDto<DirectorySectorGetAllDto>> GetAll(DirectorySectorGetAllInputDto input);
        Task Update(DirectorySectorUpdateDto input);
    }
}
