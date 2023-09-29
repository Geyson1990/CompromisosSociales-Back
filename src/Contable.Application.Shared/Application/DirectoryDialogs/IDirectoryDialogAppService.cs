using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DirectoryDialogs.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DirectoryDialogs
{
    public interface IDirectoryDialogAppService : IApplicationService
    {
        Task Create(DirectoryDialogCreateDto input);
        Task Delete(EntityDto input);
        Task<DirectoryDialogGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<DirectoryDialogGetAllDto>> GetAll(DirectoryDialogGetAllInputDto input);
        Task Update(DirectoryDialogUpdateDto input);
    }
}
