using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.TerritorialUnits.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.TerritorialUnits
{
    public interface ITerritorialUnitAppService : IApplicationService
    {
        Task AddDepartment(TerritorialUnitAddDepartmentDto input);
        Task Create(TerritorialUnitCreateDto input);
        Task Delete(EntityDto input);
        Task DeleteDepartmentUnit(EntityDto input);
        Task<TerritorialUnitGetDto> Get(EntityDto input);
        Task<PagedResultDto<TerritorialUnitGetAllDto>> GetAll(TerritorialUnitGetAllInputDto input);
        Task<PagedResultDto<TerritorialUnitCoordinatorGetAllDto>> GetAllPersons(TerritorialUnitCoordinatorGetAllInputDto input);
        Task<PagedResultDto<TerritorialUnitDepartmentDto>> GetAllDepartments(TerritorialUnitGetAllDepartmentInputDto input);
        Task Update(TerritorialUnitUpdateDto input);
    }
}
