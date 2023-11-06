using Abp.Application.Services.Dto;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Extensions;
using Abp.Localization;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Abp.UI;
using Contable.Application;
using Contable.Application.Extensions;
using Contable.Authorization.Users;
using Contable.Configuration;
using Contable.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Contable.Application.Uploaders.Dto;

namespace Contable.Manager.Base
{
    public class ResourceManagerBase : IDomainService, ITransientDependency
    {
        private readonly ISettingManager _settingManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IAbpSession _abpSession;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<User, long> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string DefaultTitleMessage => L(ContableConsts.DefaultTitleText);

        public ResourceManagerBase(
          ISettingManager settingManager,
          ILocalizationManager localizationManager,
          IAbpSession abpSession,
          ITempFileCacheManager tempFileCacheManager,
          IWebHostEnvironment webHostEnvironment,
          IObjectMapper objectMapper,
          IRepository<User, long> userRepository,
          IHttpContextAccessor httpContextAccessor)
        {
            _settingManager = settingManager;
            _localizationManager = localizationManager;
            _abpSession = abpSession;
            _tempFileCacheManager = tempFileCacheManager;
            _webHostEnvironment = webHostEnvironment;
            _objectMapper = objectMapper;
            _configurationRoot = webHostEnvironment.GetAppConfiguration();
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public void DeletePicture(string currentDir = null)
        {
            if (currentDir == null || currentDir.IsNullOrWhiteSpace())
                return;

            if (currentDir.IsValid() && File.Exists(currentDir))
                File.Delete(currentDir);
        }

        public bool TokenIsValid(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            return _tempFileCacheManager.GetFile(token) != null;
        }

        public byte[] Get(string commonFolder, string resourceFolder, string sectionFolder, string fileName)
        {
            var separator = Path.DirectorySeparatorChar;
            var server = $@"{_webHostEnvironment.ContentRootPath}{separator}Uploads{separator}{commonFolder}{separator}{resourceFolder}{separator}{sectionFolder}{separator}";
            var resourseRoute = $@"{server}{fileName}";

            try
            {
                return File.ReadAllBytes(resourseRoute);
            }
            catch
            {

            }

            return null;
        }

        public UploadResourceOutputDto Create(UploadResourceInputDto resource, string section, bool replaceName = false)
        {
            if (resource.Token.IsNullOrWhiteSpace())
                throw new UserFriendlyException(DefaultTitleMessage, "Por favor intente nuevamente la solicitud.");

            var resourceBytes = _tempFileCacheManager.GetFile(resource.Token);

            var output = new UploadResourceOutputDto()
            {
                CommonFolder = "Content",
                ResourceFolder = "Resources",
                SectionFolder = section,
                FileName = replaceName ? @$"{resource.FileName}.{resource.Extension.ToString().ToLower()}" : @$"{resource.Token}.{resource.Extension.ToString().ToLower()}",
                Name = resource.Name,
                Size = resource.Size,
                Extension = resource.Extension,
                ClassName = resource.ClassName,
                Description= resource.Description
            };

            output.Resource = @$"/Resource/{GetPath(section)}?resource=";

            var separator = Path.DirectorySeparatorChar;
            var server = $@"{_webHostEnvironment.ContentRootPath}{separator}Uploads{separator}{output.CommonFolder}{separator}{output.ResourceFolder}{separator}{output.SectionFolder}{separator}";
            var resourseRoute = $@"{server}{output.FileName}";

            if (!Directory.Exists(server))
                Directory.CreateDirectory(server);

            File.WriteAllBytes(resourseRoute, resourceBytes);

            return output;
        }

        public string GetPath(string section)
        {
            if(ResourceConsts.Compromise == section)
                return ResourceConsts.Compromise_Method;
            if (ResourceConsts.HelpMemory == section)
                return ResourceConsts.HelpMemory_Method;
            if (ResourceConsts.QuizCompleteAdministrative == section)
                return ResourceConsts.QuizCompleteAdministrative_Method;
            if (ResourceConsts.QuizCompletePublic == section)
                return ResourceConsts.QuizCompletePublic_Method;
            if (ResourceConsts.Record == section)
                return ResourceConsts.Record_Method;
            if (ResourceConsts.SocialConflictAlert == section)
                return ResourceConsts.SocialConflictAlert_Method;
            if (ResourceConsts.SocialConflict == section)
                return ResourceConsts.SocialConflict_Method;
            if (ResourceConsts.SocialConflictManagement == section)
                return ResourceConsts.SocialConflictManagement_Method;
            if (ResourceConsts.SocialConflictSensible == section)
                return ResourceConsts.SocialConflictSensible_Method;
            if (ResourceConsts.SocialConflictSensibleManagement == section)
                return ResourceConsts.SocialConflictSensibleManagement_Method;
            if (ResourceConsts.SocialConflictTaskManagement == section)
                return ResourceConsts.SocialConflictTaskManagement_Method;
            if (ResourceConsts.SectorMeetSession == section)
                return ResourceConsts.SectorMeetSession_Method;
            if (ResourceConsts.ProfilePicture == section)
                return ResourceConsts.ProfilePicture_Method;
            if (ResourceConsts.DialogSpaceDocument == section)
                return ResourceConsts.DialogSpaceDocument_Method;
            if (ResourceConsts.SectorMeet == section)
                return ResourceConsts.SectorMeet_Method;

            throw new UserFriendlyException("Aviso", "Ruta para recursos no soportada");
        }

        private string L(string name)
        {
            return _localizationManager.GetString(Localize(name));
        }

        private string L(string name, params string[] args)
        {
            return string.Format(_localizationManager.GetString(Localize(name)), args);
        }

        private LocalizableString Localize(string name)
        {
            return new LocalizableString(name, ContableConsts.LocalizationSourceName);
        }
    }
}
