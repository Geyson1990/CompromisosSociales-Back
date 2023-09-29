using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Sectors.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Sectors
{
    public interface ISectorAppService : IApplicationService
    {
        Task Create(SectorCreateDto input);
        Task Delete(EntityDto input);
        Task<SectorGetDto> Get(EntityDto input);
        Task<PagedResultDto<SectorGetAllDto>> GetAll(SectorGetAllInputDto input);
        Task Update(SectorUpdateDto input);
    }
}
