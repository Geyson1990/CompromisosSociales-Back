using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetZeroCore.Net;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contable.Authorization.Users.Profile;
using Contable.Authorization.Users.Profile.Dto;
using Contable.Dto;
using Contable.Storage;
using Contable.Web.Helpers;
using Contable.Application.Uploaders.Dto;
using Contable.Application.Extensions;
using Abp.Runtime.Session;
using Abp.Authorization;
using Contable.Extensions;

namespace Contable.Web.Controllers
{
    [AbpAuthorize]
    public abstract class ProfileControllerBase : ContableControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IProfileAppService _profileAppService;
        
        private const int MaxProfilePictureSize = 5_242_880; //5MB
        private const int MaxSoundVideoSize = 524_288_000; //500MB

        protected ProfileControllerBase(ITempFileCacheManager tempFileCacheManager, IProfileAppService profileAppService)
        {
            _tempFileCacheManager = tempFileCacheManager;
            _profileAppService = profileAppService;
        }

        [HttpPost]
        public UploadResourceDto UploadResource()
        {
            try
            {
                var file = Request.Form.Files.First(); 
                string md5 = DateTime.Now.ToMD5("SinglePicture", "Multi");

                if (file.Length > MaxProfilePictureSize)
                    throw new UserFriendlyException("Aviso", "Los ficheros adjuntos no deben exceder los 5MB");

                byte[] allBytes;
                using (Stream stream = file.OpenReadStream())
                    allBytes = stream.GetAllBytes();

                if (FileTypeExtension.IfValidFileType(allBytes) == false)
                    throw new UserFriendlyException("Aviso", "Ud. puede seleccionar un archivo WORD (doc, docx), PDF (.pdf), Excel (.xslx, .xlsl) e imagenes (JPG, JPEG, PNG).");

                _tempFileCacheManager.SetFile(md5, allBytes);

                return new UploadResourceDto
                {
                    FileToken = md5
                };
            }
            catch (UserFriendlyException ex)
            {
                if (ex.GetType().IsAssignableFrom(typeof(UserFriendlyException)))
                    throw ex;
                else
                    throw new UserFriendlyException("Aviso", "No se pudo terminar la subida de archivos, por favor intente más tarde");
            }
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
                        throw new UserFriendlyException("Aviso", "Los ficheros adjuntos no deben exceder los 5MB");

                    byte[] allBytes;
                    using (Stream stream = file.OpenReadStream())
                        allBytes = stream.GetAllBytes();

                    if (FileTypeExtension.IfValidFileType(allBytes) == false)
                        throw new UserFriendlyException("Aviso", "Solo puede agregar archivos WORD (doc, docx), PDF (.pdf), Excel (.xslx, .xlsl) y imágenes (JPEG, PNG, JPG, JPNG) con un tamaño máximo de 5MB.");

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

        [HttpPost]
        public UploadResourcesDto UploadStreamResources()
        {
            try
            {
                var multiplePictureOutput = new UploadResourcesDto();

                foreach (var file in Request.Form.Files)
                {
                    string md5 = DateTime.Now.ToMD5("Stream", "Multi");

                    if (file.Length > MaxSoundVideoSize)
                        throw new UserFriendlyException("Aviso", "Los ficheros adjuntos no deben exceder los 500MB");

                    byte[] allBytes;
                    using (Stream stream = file.OpenReadStream())
                        allBytes = stream.GetAllBytes();

                    if (FileTypeExtension.IfValidStreamFileType(allBytes) == false)
                        throw new UserFriendlyException("Aviso", "Solo puede agregar archivos MP3 (.mp3), MP4 (.mp4) con un tamaño máximo de 500MB.");

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
    }
}