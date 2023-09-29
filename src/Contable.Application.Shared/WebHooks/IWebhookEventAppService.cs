using System.Threading.Tasks;
using Abp.Webhooks;

namespace Contable.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
