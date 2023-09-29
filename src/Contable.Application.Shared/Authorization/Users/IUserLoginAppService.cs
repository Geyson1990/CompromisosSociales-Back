using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Authorization.Users.Dto;

namespace Contable.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
