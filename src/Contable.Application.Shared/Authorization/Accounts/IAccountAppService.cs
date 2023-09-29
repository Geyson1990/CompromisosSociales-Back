using System.Threading.Tasks;
using Abp.Application.Services;
using Contable.Authorization.Accounts.Dto;

namespace Contable.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task SendPasswordResetCode(SendPasswordResetCodeInput input);
        Task<ResetPasswordOutput> ResetPassword(ResetPasswordInput input);
    }
}
