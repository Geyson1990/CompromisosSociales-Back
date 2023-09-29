using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DialogSpaceDocumentSituations.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DialogSpaceDocumentSituations
{
    public interface IDialogSpaceDocumentSituationAppService : IApplicationService
    {
        Task Create(DialogSpaceDocumentSituationCreateDto input);
        Task Delete(EntityDto input);
        Task<DialogSpaceDocumentSituationGetDto> Get(EntityDto input);
        Task<PagedResultDto<DialogSpaceDocumentSituationGetAllDto>> GetAll(DialogSpaceDocumentSituationGetAllInputDto input);
        Task Update(DialogSpaceDocumentSituationUpdateDto input);
    }
}
