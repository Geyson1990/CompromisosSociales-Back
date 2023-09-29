using System.Threading.Tasks;
using Abp.Application.Services;
using Contable.MultiTenancy.Payments.Dto;
using Contable.MultiTenancy.Payments.Stripe.Dto;

namespace Contable.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}