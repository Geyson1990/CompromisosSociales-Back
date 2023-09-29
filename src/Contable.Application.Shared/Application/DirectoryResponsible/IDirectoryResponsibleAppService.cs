using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectoryResponsibles.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectoryResponsibles
{
    public interface IDirectoryResponsibleAppService : IApplicationService
    {
        Task Create(DirectoryResponsibleCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectoryResponsibleGetDto> Get(EntityDto input);
        Task<PagedResultDto<DirectoryResponsibleGetAllDto>> GetAll(DirectoryResponsibleGetAllInputDto input);
        Task Update(DirectoryResponsibleUpdateDto input);
    }
}
