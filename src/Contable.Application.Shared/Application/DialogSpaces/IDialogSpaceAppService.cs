using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DialogSpaces.Dto;
using Contable.Application.InterventionPlans.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DialogSpaces
{
    public interface IDialogSpaceAppService : IApplicationService
    {
        Task<EntityDto> Create(DialogSpaceCreateDto input);
        Task Delete(EntityDto input);
        Task<DialogSpaceGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<DialogSpaceGetAllDto>> GetAll(DialogSpaceGetAllInputDto input);
        Task<PagedResultDto<DialogSpaceLocationReferenceDto>> GetAllLocations(InterventionPlanLocationGetAllInputDto input);
        Task<EntityDto> Update(DialogSpaceUpdateDto input);
    }
}
