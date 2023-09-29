using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Coordinators.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Coordinators
{
    public interface ICoordinatorAppService : IApplicationService
    {
        Task AddTerritorialUnit(CoordinatorAddTerritorialUnitDto input);
        Task Create(CoordinatorCreateDto input);
        Task Delete(EntityDto input);
        Task DeleteTerritorialUnit(EntityDto input);
        Task<CoordinatorGetDto> Get(EntityDto input);
        Task<PagedResultDto<CoordinatorGetAllDto>> GetAll(CoordinatorGetAllInputDto input);
        Task<PagedResultDto<CoordinatorTerritorialUnitGetAllDto>> GetAllTerritorialUnits(CoordinatorTerritorialUnitGetAllInputDto input);
        Task Update(CoordinatorUpdateDto input);
    }
}
