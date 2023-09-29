using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Authorization.Users.Dto;
using Contable.Dto;

namespace Contable.Authorization.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input);

        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);

        Task CreateOrUpdateUser(CreateOrUpdateUserInput input);

        Task AssignSocialConflicts(UserSocialConflictAssingmentListDto input);

        Task DeleteUser(EntityDto<long> input);

        Task UnlockUser(EntityDto<long> input);
    }
}