using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DinamicVariables.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DinamicVariables
{
    public interface IDinamicVariableAppService : IApplicationService
    {
        Task<EntityDto> Create(DinamicVariableEntityCreateDto input);
        Task Delete(EntityDto input);
        Task<DinamicVariableGetDto> Get(EntityDto input);
        Task<PagedResultDto<DinamicVariableGetAllDto>> GetAll(DinamicVariableGetAllInputDto input);
        Task<PagedResultDto<DinamicVariableDetailDto>> GetAllDetails(DinamicVariableGetDetailInputDto input);
        Task<EntityDto> Update(DinamicVariableEntityUpdateDto input);
    }
}
