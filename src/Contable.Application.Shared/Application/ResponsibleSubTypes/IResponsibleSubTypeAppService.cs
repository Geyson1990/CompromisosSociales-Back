using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.ResponsibleSubTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.ResponsibleSubTypes
{
    public interface IResponsibleSubTypeAppService : IApplicationService
    {
        Task Create(ResponsibleSubTypeCreateDto input);
        Task Delete(EntityDto input);
        Task<ResponsibleSubTypeGetDto> Get(EntityDto input);
        Task<PagedResultDto<ResponsibleSubTypeGetAllDto>> GetAll(ResponsibleSubTypeGetAllInputDto input);
        Task Update(ResponsibleSubTypeUpdateDto input);
    }
}
