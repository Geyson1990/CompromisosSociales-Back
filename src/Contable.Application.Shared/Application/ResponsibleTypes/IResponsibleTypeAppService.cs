using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.ResponsibleTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.ResponsibleTypes
{
    public interface IResponsibleTypeAppService : IApplicationService
    {
        Task Create(ResponsibleTypeCreateDto input);
        Task Delete(EntityDto input);
        Task<ResponsibleTypeGetDto> Get(EntityDto input);
        Task<PagedResultDto<ResponsibleTypeGetAllDto>> GetAll(ResponsibleTypeGetAllInputDto input);
        Task Update(ResponsibleTypeUpdateDto input);
    }
}
