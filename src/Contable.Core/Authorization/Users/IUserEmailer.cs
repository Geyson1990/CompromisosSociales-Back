using System.Threading.Tasks;
using Contable.Chat;

namespace Contable.Authorization.Users
{
    public interface IUserEmailer
    {
        Task SendEmailActivationAsync(User user, string password);
        Task SendPasswordResetAsync(User user);
    }
}
