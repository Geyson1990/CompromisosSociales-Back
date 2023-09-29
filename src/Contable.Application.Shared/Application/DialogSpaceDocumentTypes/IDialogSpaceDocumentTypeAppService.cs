using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DialogSpaceDocumentTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DialogSpaceDocumentTypes
{
    public interface IDialogSpaceDocumentTypeAppService : IApplicationService
    {
        Task Create(DialogSpaceDocumentTypeCreateDto input);
        Task Delete(EntityDto input);
        Task<DialogSpaceDocumentTypeGetDto> Get(EntityDto input);
        Task<PagedResultDto<DialogSpaceDocumentTypeGetAllDto>> GetAll(DialogSpaceDocumentTypeGetAllInputDto input);
        Task Update(DialogSpaceDocumentTypeUpdateDto input);
    }
}
