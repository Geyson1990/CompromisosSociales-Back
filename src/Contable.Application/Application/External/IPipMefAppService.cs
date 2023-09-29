using Abp.Application.Services;
using Contable.Application.Compromises.Dto;
using Contable.Application.Orders.Dto;
using System.Threading.Tasks;

namespace Contable.Application.External
{
    public interface IPipMefAppService : IApplicationService
    {
        Task<PIPMEF> ValidatePIPMEFCompromise(CompromiseUpdatePIPMEFDto input);
        Task<PIPMEF> ValidatePIPMEFOrder(OrderPIPMEFUpdateDto input);
    }
}
