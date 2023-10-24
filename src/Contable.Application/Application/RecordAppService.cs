using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Contable.Application.Records;
using Contable.Application.Records.Dto;
using Contable.Application.Uploaders.Dto;
using Contable.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contable.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.Collections.Extensions;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Dynamic.Core;
using System.Linq;
using Contable.Authorization.Users;
using Contable.Dto;
using Contable.Application.Exporting;
using Contable.Application.Compromises.Dto;
using System.IO.Compression;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Contable.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Abp.IO.Extensions;
using Castle.MicroKernel.Registration;
using Contable.FileManager;
using AutoMapper.Configuration.Conventions;
using Abp.AspNetZeroCore.Net;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_Record)]
    public class RecordAppService : ContableAppServiceBase, IRecordAppService
    {
        private readonly IRepository<Record, long> _recordRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<RecordResource, long> _recordResourceRepository;
        private readonly IRepository<RecordResourceType> _recordResourceTypeRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<SocialConflictLocation> _socialConflictLocationRepository;
        private readonly IRepository<Compromise, long> _compromiseRepository;
        private readonly IRecordExcelExporter _recordExcelExporter;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _configurationRoot;

        public RecordAppService(
            IRepository<Record, long> recordRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<RecordResource, long> recordResourceRepository,
            IRepository<RecordResourceType> recordResourceTypeRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<User, long> userRepository,
            IRepository<SocialConflictLocation> socialConflictLocationRepository,
            IRecordExcelExporter recordExcelExporter,
            IRepository<Compromise, long> compromiseRepository,
            IWebHostEnvironment hostingEnvironment)
        {
            _recordRepository = recordRepository;
            _socialConflictRepository = socialConflictRepository;
            _recordResourceRepository = recordResourceRepository;
            _recordResourceTypeRepository = recordResourceTypeRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _userRepository = userRepository;
            _socialConflictLocationRepository = socialConflictLocationRepository;
            _recordExcelExporter = recordExcelExporter;
            _compromiseRepository = compromiseRepository;

            _hostingEnvironment = hostingEnvironment;
            _configurationRoot = hostingEnvironment.GetAppConfiguration();

            _separator = Path.DirectorySeparatorChar.ToString();
            _imageRoute = $"{_hostingEnvironment.ContentRootPath}{_separator}Uploads{_separator}Content{_separator}Resources{_separator}";
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Record_Create)]
        public async Task<EntityDto<long>> Create(RecordCreateDto input)
        {
            var recordId = await _recordRepository.InsertAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map<Record>(input),
                socialConflictId: input.SocialConflict.Id, null,
                uploadFiles: input.UploadFiles ?? new List<UploadResourceInputDto>()
            ));

            await CurrentUnitOfWork.SaveChangesAsync();

            await FunctionManager.CallCreateRecordCodeProcess(input.SocialConflict.Id, recordId);

            return new EntityDto<long>(recordId);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Record_Edit)]
        public async Task Update(RecordUpdateDto input)
        {
            VerifyCount(await _recordRepository.CountAsync(p => p.Id == input.Id));

            await _recordRepository.UpdateAsync(await ValidateEntity(
                input: ObjectMapper.Map(input, await _recordRepository.GetAsync(input.Id)),
                socialConflictId: input.SocialConflict.Id, input.Resources,
                uploadFiles: input.UploadFiles ?? new List<UploadResourceInputDto>()
            ));
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Record_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            VerifyCount(await _recordRepository.CountAsync(p => p.Id == input.Id));

            await _recordRepository.DeleteAsync(input.Id);
            await _recordResourceRepository.DeleteAsync(p => p.Record.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Record)]
        public async Task<RecordGetDataDto> Get(NullableIdDto<long> input)
        {
            var output = new RecordGetDataDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _recordRepository.CountAsync(p => p.Id == input.Id));

                var record = _recordRepository
                    .GetAll()
                    .Include(p => p.SocialConflict)
                    .Include(p => p.Resources)
                    .ThenInclude(p => p.RecordResourceType)
                    .Where(p => p.Id == input.Id)
                    .First();

                output.Record = ObjectMapper.Map<RecordGetDto>(record);
                var resources = new List<RecordResourceDto>();

                foreach (var resource in record.Resources)
                {
                    var resourceItem = ObjectMapper.Map<RecordResourceDto>(resource);
                    var userResourceExists = resource.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == resource.CreatorUserId) > 0;

                    if (userResourceExists)
                    {
                        var user = await _userRepository.GetAsync(resource.CreatorUserId.Value);
                        resourceItem.CreatorUserName = (user.Name ?? "").Trim() + " " + (user.Surname ?? "").Trim();
                    }

                    resources.Add(resourceItem);
                }

                var userCreateExits = record.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == record.CreatorUserId) > 0;
                var userEditExits = record.LastModifierUserId.HasValue && await _userRepository.CountAsync(p => p.Id == record.LastModifierUserId) > 0;

                output.Record.CreatorUser = userCreateExits ? ObjectMapper.Map<RecordUserDto>(await _userRepository.GetAsync(record.CreatorUserId.Value)) : null;
                output.Record.EditUser = userEditExits ? ObjectMapper.Map<RecordUserDto>(await _userRepository.GetAsync(record.LastModifierUserId.Value)) : null;
                output.Record.Resources = resources;
                output.Record.WomanCompromise = _compromiseRepository
                    .GetAll()
                    .Where(p => p.Record.Id == output.Record.Id && p.WomanCompromise)
                    .Any();
            }

            output.ResourceTypes = ObjectMapper.Map<List<RecordResourceTypeDto>>(_recordResourceTypeRepository
                .GetAll()
                .Where(p => p.Enabled)
                .ToList());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Record)]
        public async Task<PagedResultDto<RecordGetAllDto>> GetAll(RecordGetAllInputDto input)
        {
            var query = _recordRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                    .ThenInclude(p => p.Locations)
                        .ThenInclude(p => p.TerritorialUnit)
                .Where(p => !p.SocialConflict.IsDeleted)
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.SocialConflictCode.IsValid(), p => p.SocialConflict.Code.Contains(input.SocialConflictCode))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.SocialConflict.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.RecordTime >= input.StartTime.Value && p.RecordTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflict.Filter));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var result = new List<RecordGetAllDto>();

            foreach (var record in output)
            {
                var recordItem = ObjectMapper.Map<RecordGetAllDto>(record);

                var userCreateExits = record.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == record.CreatorUserId) > 0;
                var userEditExits = record.LastModifierUserId.HasValue && await _userRepository.CountAsync(p => p.Id == record.LastModifierUserId) > 0;

                recordItem.CreatorUser = userCreateExits ? ObjectMapper.Map<RecordUserDto>(await _userRepository.GetAsync(record.CreatorUserId.Value)) : new RecordUserDto() { Name = "N/A", Surname = "" };
                recordItem.EditUser = userEditExits ? ObjectMapper.Map<RecordUserDto>(await _userRepository.GetAsync(record.LastModifierUserId.Value)) : new RecordUserDto() { Name = "N/A", Surname = "" };

                recordItem.TerritorialUnits = record.SocialConflict.Locations.Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(",");

                result.Add(recordItem);
            }

            return new PagedResultDto<RecordGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Record)]
        public async Task<FileDto> GetMatrixToExcel(RecordGetMatrixExcelInputDto input)
        {
            var query = _recordRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                    .ThenInclude(p => p.Locations)
                        .ThenInclude(p => p.TerritorialUnit)
                .Include(p => p.Resources)
                    .ThenInclude(p => p.RecordResourceType)
                .Where(p => !p.SocialConflict.IsDeleted)
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.SocialConflictCode.IsValid(), p => p.SocialConflict.Code.Contains(input.SocialConflictCode))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.SocialConflict.Locations.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.StartTime.HasValue && input.EndTime.HasValue, p => p.RecordTime.Value >= input.StartTime.Value && p.RecordTime.Value <= input.EndTime.Value)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(SocialConflict.Filter));

            var output = await query.OrderBy(input.Sorting).ToListAsync();

            var result = new List<RecordGetMatrixExcelDto>();

            foreach (var record in output)
            {
                var recordItem = ObjectMapper.Map<RecordGetMatrixExcelDto>(record);

                var locations = await _socialConflictLocationRepository.GetAll()
                        .Include(p => p.TerritorialUnit)
                        .Include(p => p.Department)
                        .Include(p => p.Province)
                        .Include(p => p.District)
                        .Where(p => p.SocialConflict.Id == record.SocialConflict.Id).ToListAsync();

                if (locations.Count > 0)
                {
                    recordItem.TerritorialUnits = locations.Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(", ");
                    recordItem.Departments = locations.Select(p => p.Department.Name).Distinct().JoinAsString(", ");
                    recordItem.Provinces = locations.Select(p => p.Province.Name).Distinct().JoinAsString(", ");
                    recordItem.Districts = locations.Select(p => p.District.Name).Distinct().JoinAsString(", ");
                }

                recordItem.ResourcesNames = record.Resources.Select(p => p.Name + "." + p.Extension).JoinAsString(", ");
                recordItem.ResourcesTypes = record.Resources.Select(p => p.RecordResourceType == null ? "Otros" : p.RecordResourceType.Name).JoinAsString(", ");

                recordItem.WomanCompromise = _compromiseRepository
                     .GetAll()
                     .Where(p => p.Record.Id == recordItem.Id && p.WomanCompromise)
                     .Any();

                var userCreateExits = record.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == record.CreatorUserId) > 0;
                var userEditExits = record.LastModifierUserId.HasValue && await _userRepository.CountAsync(p => p.Id == record.LastModifierUserId) > 0;

                recordItem.CreatorUser = userCreateExits ? ObjectMapper.Map<RecordUserDto>(await _userRepository.GetAsync(record.CreatorUserId.Value)) : null;
                recordItem.EditUser = userEditExits ? ObjectMapper.Map<RecordUserDto>(await _userRepository.GetAsync(record.LastModifierUserId.Value)) : null;

                result.Add(recordItem);
            }

            return _recordExcelExporter.ExportMatrixToFile(result);
        }

        private async Task<Record> ValidateEntity(Record input, int socialConflictId, List<RecordResourceDto> resources, List<UploadResourceInputDto> uploadFiles)
        {
            if (await _socialConflictRepository.CountAsync(p => p.Id == socialConflictId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El conflicto social no existe o ya no se encuentra disponible");

            input.Title.IsValidOrException(DefaultTitleMessage, "El título del acta es obligatorio");
            input.Title.VerifyTableColumn(RecordConsts.TitleMinLength, RecordConsts.TitleMaxLength, DefaultTitleMessage, $"El título del acta no debe exceder los {RecordConsts.TitleMaxLength} caracteres");

            if (!input.RecordTime.HasValue)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha del acta es obligatorio");

            if (input.RecordTime.Value > DateTime.Now)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha del acta no puede ser mayor a la fecha actual");

            input.SocialConflict = await _socialConflictRepository
                            .GetAll()
                            .Where(p => p.Id == socialConflictId)
                            .FirstAsync();

            foreach (var resource in resources ?? new List<RecordResourceDto>())
            {
                if (resource.Remove && await _recordResourceRepository.CountAsync(p => p.Id == resource.Id && p.Record.Id == input.Id) > 0)
                    await _recordResourceRepository.DeleteAsync(resource.Id);
            }

            input.Resources = new List<RecordResource>();

            foreach (var resource in uploadFiles)
            {
                if (resource.RecordResourceType == null || resource.RecordResourceType.Id <= 0)
                    throw new UserFriendlyException("Aviso", "El tipo de documento de sustento es obligatorio en todos los documentos");

                var resourceType = _recordResourceTypeRepository
                    .GetAll()
                    .Where(p => p.Id == resource.RecordResourceType.Id)
                    .FirstOrDefault();

                if (resourceType == null)
                    throw new UserFriendlyException("Aviso", "El tipo de documento de sustento utilizado es inválido o ya fue eliminado, por favor verifique la información antes de continuar");

                var newResource = ObjectMapper.Map<RecordResource>(ResourceManager.Create(resource, ResourceConsts.Record));

                newResource.RecordResourceType = resourceType;
                newResource.RecordResourceType.Id = resourceType.Id;

                input.Resources.Add(newResource);
            }

            input.Filter = string.Concat(input.Code ?? "", " ", input.Title ?? "");

            return input;
        }

        public async Task<FileDto> GetActasZip(RecordGetMatrixExcelInputDto input, bool replaceName)
        {
            var nameFolder = Guid.NewGuid();

            var records = _recordRepository
                   .GetAll()
                   //.Include(p => p.SocialConflict)
                   .Include(p => p.Resources)
                   .ThenInclude(p => p.RecordResourceType)
                   //.Where(p => p.Id == input.Id)
                   .ToList();



            var output = new UploadResourceOutputDto()
            {
                CommonFolder = "Content",
                ResourceFolder = "Resources",
                SectionFolder = "Temp"
            };

            output.Resource = @$"/Resource/Actas/Temp/Zip?resource=";

            var separator = Path.DirectorySeparatorChar;
            var server = $@"{_hostingEnvironment.ContentRootPath}{separator}Uploads{separator}{output.CommonFolder}{separator}{output.ResourceFolder}{separator}{output.SectionFolder}{separator}";

            server = Path.Combine(server, nameFolder.ToString());

            if (!Directory.Exists(server))
                Directory.CreateDirectory(server);


            foreach (var collection in records)
            {
                foreach (var item in collection.Resources)
                {
                    var archivo = LoadResource(ResourceConsts.Record, item.FileName);
                  
                    if (archivo != null)
                    {
                        var resourseRoute = Path.Combine(server, item.FileName);
                        try
                        {
                            var _file = archivo.FileStream.GetAllBytes();
                            File.WriteAllBytes(resourseRoute, _file);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());

                        }

                    }
                }

            }
            Compress(server);


            return new FileDto($"{server}.zip", MimeTypeNames.ApplicationXGzip);
        }

       
        private static void Compress(string pathFolder)
        {
            ZipFile.CreateFromDirectory(pathFolder, $"{pathFolder}.zip", CompressionLevel.Fastest, true);

        }


        private readonly string _imageRoute;
        private readonly string _separator;

        private FileStreamResult LoadResource(string section, string resource)
        {
            if (string.IsNullOrWhiteSpace(resource))
                return null;

            resource = Regex.Replace(resource.Trim(), @"[^A-Za-z0-9.]", "");

            var directory = $@"{_imageRoute}{section}{_separator}{resource}";

            if (!System.IO.File.Exists(directory))
                return null;

            var extension = Path.GetExtension(directory);

            var archivo = extension switch
            {
                ".jpg" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".jpeg" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".jpe" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".png" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".xlsx" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".xls" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".pdf" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                //".pdf" => new FileStream(directory, FileMode.Open),
                ".csv" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".doc" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".docx" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".odt" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".mp3" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),
                ".mp4" => new FileStreamResult(new FileStream(directory, FileMode.Open), "application/octet-stream"),

                _ => null,
            };
            return archivo;

        }
    }
}
