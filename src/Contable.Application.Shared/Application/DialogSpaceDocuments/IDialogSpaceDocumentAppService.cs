using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DialogSpaceDocuments.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DialogSpaceDocuments
{
    public interface IDialogSpaceDocumentAppService : IApplicationService
    {
        Task<EntityDto> Create(DialogSpaceDocumentCreateDto input);
        Task Delete(EntityDto input);
        Task<DialogSpaceDocumentGetDataDto> Get(DialogSpaceDocumentGetInputDto input);
        Task<PagedResultDto<DialogSpaceDocumentGetAllDto>> GetAll(DialogSpaceDocumentGetAllInputDto input);
        Task Update(DialogSpaceDocumentUpdateDto input);
    }
}
