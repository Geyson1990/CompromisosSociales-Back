using Abp.Runtime.Caching;

namespace Contable.Authorization.Users.Profile.Cache
{
    public static class LoginCaptchaCodeCacheExtensions
    {
        public static ITypedCache<string, LoginCacheItem> GetLoginCaptchaCodeCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, LoginCacheItem>(LoginCacheItem.CacheName);
        }
    }
}