using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Contable.Authorization.Users;
using Contable.Manager.Base;
using Contable.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Manager
{
    public class ResourceManager : ResourceManagerBase
    {
        public ResourceManager(
             ISettingManager settingManager,
             ILocalizationManager localizationManager,
             IAbpSession abpSession,
             ITempFileCacheManager tempFileCacheManager,
             IWebHostEnvironment webHostEnvironment,
             IObjectMapper objectMapper,
             IRepository<User, long> userRepository,
             IHttpContextAccessor httpContextAccessor) : base(
                 settingManager,
                 localizationManager,
                 abpSession,
                 tempFileCacheManager,
                 webHostEnvironment,
                 objectMapper,
                 userRepository,
                 httpContextAccessor)
        { }
    }
}
