using System;
using System.Net;
using System.Threading.Tasks;
using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;
using Contable.Dto;
using Contable.Storage;
using System.Linq;
using Abp.UI;
using Abp.IO.Extensions;
using Contable.Application.Extensions;
using Contable.Web.Helpers;
using System.Drawing.Imaging;
using Abp.Extensions;
using Abp.Web.Models;
using System.IO;
using System.Drawing;
using Abp.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Contable.Configuration;
using System.Text.RegularExpressions;
using Contable.Authorization;
using Contable.Application.Uploaders.Dto;
using Contable.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Drawing2D;
using Contable.Web.Models.Resource;
using Abp.Runtime.Caching;
using Contable.Authorization.Users.Profile.Cache;

namespace Contable.Web.Controllers
{
    public class ResourceController : ContableControllerBase
    {
        private readonly ICacheManager _cacheManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imageRoute;
        private readonly string _separator;
        private const int MaxProfilePictureSize = 3_145_728; //5MB

        public ResourceController(ICacheManager cacheManager, IWebHostEnvironment webHostEnvironment, ITempFileCacheManager tempFileCacheManager)
        {
            _cacheManager = cacheManager;
            _tempFileCacheManager = tempFileCacheManager;
            _webHostEnvironment = webHostEnvironment;
            _configurationRoot = webHostEnvironment.GetAppConfiguration();
            _separator = Path.DirectorySeparatorChar.ToString();
            _imageRoute = $"{_webHostEnvironment.ContentRootPath}{_separator}Uploads{_separator}Content{_separator}Resources{_separator}";
        }

        [HttpGet]
        public ResourceCaptchaModelResult GetGenerateCaptcha()
        {
            //First declare a bitmap and declare graphic from this bitmap
            Bitmap bitmap = new Bitmap(400, 100, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            //And create a rectangle to delegete this image graphic 
            Rectangle rect = new Rectangle(0, 0, 400, 100);
            //And create a brush to make some drawings
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.DottedGrid, Color.Black, Color.White);
            g.FillRectangle(hatchBrush, rect);

            var captchaCode = HelperExtensions.GenerateCaptchaCode(6);
            //here we make the text configurations
            GraphicsPath graphicPath = new GraphicsPath();
            //add this string to image with the rectangle delegate
            graphicPath.AddString(captchaCode, FontFamily.GenericMonospace, (int)FontStyle.Bold, 90, rect, null);
            //And the brush that you will write the text
            hatchBrush = new HatchBrush(HatchStyle.Percent20, Color.Black, Color.Black);
            g.FillPath(hatchBrush, graphicPath);
            //We are adding the dots to the image
            var rnd = new Random();
            for (int i = 0; i < (int)(rect.Width * rect.Height / 50F); i++)
            {
                int x = rnd.Next(400);
                int y = rnd.Next(100);
                int w = rnd.Next(10);
                int h = rnd.Next(10);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }
            //Remove all of variables from the memory to save resource
            hatchBrush.Dispose();
            g.Dispose();
            //return the image to the related component

            var byteArray = ToByteArray(bitmap);
            var resource = "data:image/bmp;charset=utf-8;base64," + Convert.ToBase64String(byteArray);
            var securityCode = DateTime.Now.ToMD5("Login", "Captha");

            _cacheManager
                .GetLoginCaptchaCodeCache()
                .Set(securityCode, new LoginCacheItem()
                {
                    Code = captchaCode
                });

            return new ResourceCaptchaModelResult()
            {
                Resource = resource,
                SecurityCode = securityCode
            };
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_Compromise)]
        public IActionResult LoadCompromiseResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.Compromise, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_HelpMemory)]
        public IActionResult LoadHelpMemoryResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.HelpMemory, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Quiz_Platform)]
        public IActionResult LoadQuizCompleteAdministrative([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.QuizCompleteAdministrative, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Quiz_Platform)]
        public IActionResult LoadQuizCompletePublic([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.QuizCompletePublic, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_Record)]
        public IActionResult LoadRecordResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.Record, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictAlert)]
        public IActionResult LoadSocialConflictAlertResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.SocialConflictAlert, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public IActionResult LoadSocialConflictResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.SocialConflict, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflict)]
        public IActionResult LoadSocialConflictManagementResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.SocialConflictManagement, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictSensible)]
        public IActionResult LoadSocialConflictSensibleResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.SocialConflictSensible, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictSensible)]
        public IActionResult LoadSocialConflictSensibleManagementResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.SocialConflictSensibleManagement, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_Application_SocialConflictTaskManagement)]
        public IActionResult LoadSocialConflictTaskManagementResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.SocialConflictTaskManagement, resource);
        }

        [DisableAuditing]
        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
        public IActionResult LoadSectorMeetSessionResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.SectorMeetSession, resource);
        }

        [DisableAuditing]
        [AllowAnonymous]
        public IActionResult LoadProfilePictureResource([FromQuery] string resource)
        {
            return LoadResource(ResourceConsts.ProfilePicture, resource);
        }

        [HttpPost]
        public UploadResourcesDto UploadResources()
        {
            try
            {
                var multiplePictureOutput = new UploadResourcesDto();

                foreach (var file in Request.Form.Files)
                {
                    string md5 = DateTime.Now.ToMD5("Picture", "Multi");

                    if (file.Length > MaxProfilePictureSize)
                        throw new UserFriendlyException("Aviso", "Los ficheros adjuntos no deben exceder los 3MB");

                    byte[] allBytes;
                    using (Stream stream = file.OpenReadStream())
                        allBytes = stream.GetAllBytes();

                    if (FileTypeExtension.IfValidFileType(allBytes) == false)
                        throw new UserFriendlyException("Aviso", "Solo puede agregar archivos (jpg/png/doc/docx/pdf) con un tamaño máximo de 3MB.");

                    _tempFileCacheManager.SetFile(md5, allBytes);
                    multiplePictureOutput.FileTokens.Add(md5);
                }

                return multiplePictureOutput;
            }
            catch (Exception ex)
            {
                if (ex.GetType().IsAssignableFrom(typeof(UserFriendlyException)))
                    throw ex;
                else
                    throw new UserFriendlyException("Aviso", "No se pudo terminar la subida de archivos, por favor intente más tarde");
            }
        }

        private IActionResult LoadResource(string section, string resource)
        {
            if (string.IsNullOrWhiteSpace(resource))
                return NotFound();

            resource = Regex.Replace(resource.Trim(), @"[^A-Za-z0-9.]", "");

            var directory = $@"{_imageRoute}{section}{_separator}{resource}";

            if (!System.IO.File.Exists(directory))
                return NotFound();

            var extension = Path.GetExtension(directory);

            return extension switch
            {
                ".jpg" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".jpeg" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".jpe" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".png" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".xlsx" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".xls" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".pdf" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".csv" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".doc" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".docx" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".odt" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".mp3" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".mp4" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                _ => NotFound(),
            };
        }

        private static byte[] ToByteArray(Image img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }
    }
}