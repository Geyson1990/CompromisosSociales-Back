using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DialogSpaceTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DialogSpaceTypes
{
    public interface IDialogSpaceTypeAppService : IApplicationService
    {
        Task Create(DialogSpaceTypeCreateDto input);
        Task Delete(EntityDto input);
        Task<DialogSpaceTypeGetDto> Get(EntityDto input);
        Task<PagedResultDto<DialogSpaceTypeGetAllDto>> GetAll(DialogSpaceTypeGetAllInputDto input);
        Task Update(DialogSpaceTypeUpdateDto input);
    }
}
