using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.RecordResourceTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.RecordResourceTypes
{
    public interface IRecordResourceTypeAppService : IApplicationService
    {
        Task Create(RecordResourceTypeCreateDto input);
        Task Delete(EntityDto input);
        Task<RecordResourceTypeGetDto> Get(EntityDto input);
        Task<PagedResultDto<RecordResourceTypeGetAllDto>> GetAll(RecordResourceTypeGetAllInputDto input);
        Task Update(RecordResourceTypeUpdateDto input);
    }
}
