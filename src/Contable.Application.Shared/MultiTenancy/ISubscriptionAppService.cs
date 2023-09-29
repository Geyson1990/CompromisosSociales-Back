using System.Threading.Tasks;
using Abp.Application.Services;

namespace Contable.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
