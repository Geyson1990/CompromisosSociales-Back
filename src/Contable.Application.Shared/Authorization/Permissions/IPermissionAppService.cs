using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Authorization.Permissions.Dto;

namespace Contable.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
