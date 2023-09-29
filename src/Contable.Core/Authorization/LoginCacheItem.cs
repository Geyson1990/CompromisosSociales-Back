using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Authorization
{
    public class LoginCacheItem
    {
        public const string CacheName = "AppLoginCaptchaCache";
        public static readonly TimeSpan DefaultSlidingExpireTime = TimeSpan.FromMinutes(1);

        public string Code { get; set; }
    }
}
