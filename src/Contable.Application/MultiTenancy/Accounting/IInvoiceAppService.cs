using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Contable.MultiTenancy.Accounting.Dto;

namespace Contable.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
