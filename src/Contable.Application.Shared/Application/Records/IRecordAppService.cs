using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Records.Dto;
using System.Threading.Tasks;

namespace Contable.Application.Records
{
    public interface IRecordAppService : IApplicationService
    {
        Task<EntityDto<long>> Create(RecordCreateDto input);
        Task Delete(EntityDto<long> input);
        Task<RecordGetDataDto> Get(NullableIdDto<long> input);
        Task<PagedResultDto<RecordGetAllDto>> GetAll(RecordGetAllInputDto input);
        Task Update(RecordUpdateDto input);
    }
}
