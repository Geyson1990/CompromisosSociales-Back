using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Facts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Facts
{
    public interface IFactAppService : IApplicationService
    {
        Task Create(FactCreateDto input);
        Task Delete(EntityDto input);
        Task<FactGetDto> Get(EntityDto input);
        Task<PagedResultDto<FactGetAllDto>> GetAll(FactGetAllInputDto input);
        Task Update(FactUpdateDto input);
    }
}
