using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.StaticVariables.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.StaticVariables
{
    public interface IStaticVariableAppService : IApplicationService
    {
        Task<EntityDto> Create(StaticVariableCreateDto input);
        Task Delete(EntityDto input);
        Task<StaticVariableGetDto> Get(EntityDto input);
        Task<PagedResultDto<StaticVariableGetAllDto>> GetAll(StaticVariableGetAllInputDto input);
        Task<PagedResultDto<StaticVariableCuantitativeGetAllDto>> GetAllDinamicVariables(StaticVariableCuantitativeGetAllInputDto input);
        Task<EntityDto> Update(StaticVariableUpdateDto input);
    }
}
