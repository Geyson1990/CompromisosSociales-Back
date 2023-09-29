using System.Threading.Tasks;
using Abp.Application.Services;
using Contable.MultiTenancy.Payments.PayPal.Dto;

namespace Contable.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
