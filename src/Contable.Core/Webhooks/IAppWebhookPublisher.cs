using System.Threading.Tasks;
using Contable.Authorization.Users;

namespace Contable.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
