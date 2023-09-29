using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectoryConflictTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectoryConflictTypes
{
    public interface IDirectoryConflictTypeAppService : IApplicationService
    {
        Task Create(DirectoryConflictTypeCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectoryConflictTypeGetDto> Get(EntityDto input);
        Task<PagedResultDto<DirectoryConflictTypeGetAllDto>> GetAll(DirectoryConflictTypeGetAllInputDto input);
        Task Update(DirectoryConflictTypeUpdateDto input);
    }
}
