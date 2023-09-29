using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using Contable.Authorization.Users.Profile;
using Contable.Storage;

namespace Contable.Web.Controllers
{
    [Authorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(
            ITempFileCacheManager tempFileCacheManager,
            IProfileAppService profileAppService) :
            base(tempFileCacheManager, profileAppService)
        {
        }
    }
}