using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Orders.Dto;
using System.Threading.Tasks;

namespace Contable.Application.Records
{
    public interface IOrderAppService : IApplicationService
    {
        Task Create(OrderCreateDto input);
        Task Delete(EntityDto<long> input);
        Task<OrderGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<OrderGetAllDto>> GetAll(OrderGetAllInputDto input);
        Task Update(OrderUpdateDto input);
    }
}
