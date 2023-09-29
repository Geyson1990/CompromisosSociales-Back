using Abp.Linq.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Domain.Repositories;
using System.Threading.Tasks;
using Contable.Application.DialogSpaceDocuments;
using Contable.Application.DialogSpaceDocuments.Dto;
using System.Collections.Generic;
using Abp.UI;
using Abp.Application.Services.Dto;
using Contable.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Contable.Authorization.Users;
using Abp.Collections.Extensions;
using System;
using System.Linq.Expressions;
using NUglify.Helpers;
using Contable.Migrations;
using Contable.Application.Uploaders.Dto;
using Contable.Application.DialogSpaces.Dto;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace)]
    public class DialogSpaceDocumentAppService : ContableAppServiceBase, IDialogSpaceDocumentAppService
    {
        private readonly IRepository<DialogSpace> _dialogSpaceRepository;
        private readonly IRepository<DialogSpaceDocument> _dialogSpaceDocumentRepository;
        private readonly IRepository<DialogSpaceDocumentType> _dialogSpaceDocumentTypeRepository;
        private readonly IRepository<DialogSpaceDocumentResource> _dialogSpaceDocumentResourceRepository;
        private readonly IRepository<DialogSpaceDocumentSituation> _dialogSpaceDocumentSituationRepository;
        private readonly IRepository<User, long> _userRepository;

        public DialogSpaceDocumentAppService(
            IRepository<DialogSpace> dialogSpaceRepository, 
            IRepository<DialogSpaceDocument> dialogSpaceDocumentRepository,
            IRepository<DialogSpaceDocumentType> dialogSpaceDocumentTypeRepository,
            IRepository<DialogSpaceDocumentResource> dialogSpaceDocumentResourceRepository,
            IRepository<DialogSpaceDocumentSituation> dialogSpaceDocumentSituationRepository,
            IRepository<User, long> userRepository)
        {
            _dialogSpaceRepository = dialogSpaceRepository;
            _dialogSpaceDocumentRepository = dialogSpaceDocumentRepository;
            _dialogSpaceDocumentTypeRepository = dialogSpaceDocumentTypeRepository;
            _dialogSpaceDocumentResourceRepository = dialogSpaceDocumentResourceRepository;
            _dialogSpaceDocumentSituationRepository = dialogSpaceDocumentSituationRepository;
            _userRepository = userRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace_Create)]
        public async Task<EntityDto> Create(DialogSpaceDocumentCreateDto input)
        {
            var dialogSpaceDocumentId = await _dialogSpaceDocumentRepository.InsertAndGetIdAsync(ValidateEntity(
                input: ObjectMapper.Map<DialogSpaceDocument>(input),
                dialogSpaceId: input.DialogSpace == null ? -1 : input.DialogSpace.Id,
                dialogSpaceDocumentTypeId: input.DialogSpaceDocumentType == null ? -1 : input.DialogSpaceDocumentType.Id,
                dialogSpaceDocumentSituationId: input.DialogSpaceDocumentSituation == null ? -1 : input.DialogSpaceDocumentSituation.Id,
                uploadFiles: input.UploadFiles ?? new List<UploadResourceInputDto>()
            ));

            await FunctionManager.CallCreateDialogSpaceStateProcess(input.DialogSpace.Id);

            return new EntityDto(dialogSpaceDocumentId);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _dialogSpaceDocumentRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceDocumentRepository.DeleteAsync(p => p.Id == input.Id);

            await CurrentUnitOfWork.SaveChangesAsync(); 
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace)]
        public async Task<DialogSpaceDocumentGetDataDto> Get(DialogSpaceDocumentGetInputDto input)
        {
            if (await _dialogSpaceRepository.CountAsync(p => p.Id == input.DialogSpaceId) == 0)
                throw new UserFriendlyException("Aviso", "El Espacio de Diálogo seleccionado es inválido. Por favor verifique la información.");

            var output = new DialogSpaceDocumentGetDataDto();

            if(input.DialogSpaceDocumentId.HasValue)
            {
                VerifyCount(await _dialogSpaceDocumentRepository.CountAsync(p => p.Id == input.DialogSpaceDocumentId.Value));

                var dbDialogSpaceDocument = _dialogSpaceDocumentRepository
                    .GetAll()
                    .Include(p => p.DialogSpace)
                        .ThenInclude(p => p.SocialConflict)
                    .Include(p => p.DialogSpaceDocumentSituation)
                    .Include(p => p.DialogSpaceDocumentType)         
                    .Where(p => p.Id == input.DialogSpaceDocumentId.Value)
                    .First();

                output.DialogSpaceDocument = ObjectMapper.Map<DialogSpaceDocumentGetDto>(dbDialogSpaceDocument);

                var resources = _dialogSpaceDocumentResourceRepository
                    .GetAll()
                    .Where(p => p.DialogSpaceDocumentId == dbDialogSpaceDocument.Id)
                    .ToList();

                output.DialogSpaceDocument.Resources = new List<DialogSpaceDocumentResourceDto>();

                foreach (var resource in resources)
                {
                    var resourceItem = ObjectMapper.Map<DialogSpaceDocumentResourceDto>(resource);
                    var userResourceExists = resource.CreatorUserId.HasValue && await _userRepository.CountAsync(p => p.Id == resource.CreatorUserId) > 0;

                    if (userResourceExists)
                    {
                        var user = await _userRepository.GetAsync(resource.CreatorUserId.Value);
                        resourceItem.CreatorUserName = (user.Name ?? "").Trim() + " " + (user.Surname ?? "").Trim();
                    }

                    output.DialogSpaceDocument.Resources.Add(resourceItem);
                }

                var creatorUser = dbDialogSpaceDocument.CreatorUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbDialogSpaceDocument.CreatorUserId.Value)
                    .FirstOrDefault() : null;

                var editUser = dbDialogSpaceDocument.LastModifierUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbDialogSpaceDocument.LastModifierUserId.Value)
                    .FirstOrDefault() : null;

                output.DialogSpaceDocument.CreatorUser = creatorUser == null ? null : ObjectMapper.Map<DialogSpaceDocumentUserDto>(creatorUser);
                output.DialogSpaceDocument.EditUser = editUser == null ? null : ObjectMapper.Map<DialogSpaceDocumentUserDto>(editUser);
            } 
            else
            {
                output.DialogSpaceDocument = new DialogSpaceDocumentGetDto();

                var hasAnotherDocumentRegistered = await _dialogSpaceDocumentRepository.CountAsync(p => p.DialogSpaceId == input.DialogSpaceId);

                output.DialogSpaceDocument.Type = hasAnotherDocumentRegistered == 0 ? DocumentType.CREATE : DocumentType.UPDATE;
            }

            output.DialogSpace = ObjectMapper.Map<DialogSpaceDocumentDialogSpaceRelationDto>(_dialogSpaceRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Where(p => p.Id == input.DialogSpaceId)
                .First());

            output.DocumentTypes = ObjectMapper.Map<List<DialogSpaceDocumentTypeRelationDto>>(await _dialogSpaceDocumentTypeRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .Where(p => p.Enabled)
                .ToListAsync());

            output.Situations = ObjectMapper.Map<List<DialogSpaceDocumentSituationRelationDto>>(await _dialogSpaceDocumentSituationRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .Where(p => p.Enabled)
                .ToListAsync());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace)]
        public async Task<PagedResultDto<DialogSpaceDocumentGetAllDto>> GetAll(DialogSpaceDocumentGetAllInputDto input)
        {
            if (input.DialogSpaceId.HasValue == false)
                return new PagedResultDto<DialogSpaceDocumentGetAllDto>();

            var query = _dialogSpaceDocumentRepository
                .GetAll()
                .Include(p => p.DialogSpace)
                .Include(p => p.DialogSpaceDocumentType)
                .Include(p => p.DialogSpaceDocumentSituation)
                .WhereIf(input.DialogSpaceId.HasValue, p => p.DialogSpaceId == input.DialogSpaceId.Value);

            var count = await query.CountAsync();
            var result = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
                
            var output = new List<DialogSpaceDocumentGetAllDto>();

            foreach(var item in result)
            {
                output.Add(new DialogSpaceDocumentGetAllDto()
                {
                    Id = item.Id,
                    Document = item.Document,
                    DocumentType = item.DialogSpaceDocumentType?.Name,
                    DocumentTime = item.DocumentTime,
                    Situation = item.DialogSpaceDocumentSituation?.Name,
                    Type = item.Type,
                    Days = item.Days,
                    HasInstallation = item.HasInstallation,
                    InstallationMaxTime = item.InstallationMaxTime,
                    InstallationTime = item.InstallationTime,
                    Exposition = item.Exposition,
                    Range = item.Range,
                    RangeSide = item.RangeSide,
                    Observation = item.Observation,
                    VigencyDays = item.VigencyDays,
                    VigencyRangeSide = item.VigencyRangeSide,
                    VigencyTime = item.VigencyTime
                });
            }

            return new PagedResultDto<DialogSpaceDocumentGetAllDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_DialogSpace_Edit)]
        public async Task Update(DialogSpaceDocumentUpdateDto input)
        {
            VerifyCount(await _dialogSpaceDocumentRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceDocumentRepository.UpdateAsync(ValidateEntity(
                input: ObjectMapper.Map(input, await _dialogSpaceDocumentRepository.GetAsync(input.Id)),
                dialogSpaceId: input.DialogSpace == null ? -1 : input.DialogSpace.Id,
                dialogSpaceDocumentTypeId: input.DialogSpaceDocumentType == null ? -1 : input.DialogSpaceDocumentType.Id,
                dialogSpaceDocumentSituationId: input.DialogSpaceDocumentSituation == null ? -1 : input.DialogSpaceDocumentSituation.Id,
                uploadFiles: input.UploadFiles ?? new List<UploadResourceInputDto>()
            ));

            await FunctionManager.CallCreateDialogSpaceStateProcess(input.DialogSpace.Id);
        }

        private DialogSpaceDocument ValidateEntity(DialogSpaceDocument input, int dialogSpaceId, int dialogSpaceDocumentTypeId, int dialogSpaceDocumentSituationId, List<UploadResourceInputDto> uploadFiles)
        {
            if(input.Type == DocumentType.CREATE && input.Id <= 0)
            {
                var hasAnotherDocumentRegistered = _dialogSpaceDocumentRepository.Count(p => p.Id != input.Id && p.DialogSpaceId == dialogSpaceId);

                if (hasAnotherDocumentRegistered > 0)
                    throw new UserFriendlyException("Aviso", "Solo puede registrar un documento de tipo \"Creación\" para un Espacio de Diálogo");
            }

            input.DocumentTime = new DateTime(input.DocumentTime.Year, input.DocumentTime.Month, input.DocumentTime.Day, 0, 0, 0, DateTimeKind.Unspecified);

            if(input.InstallationTime.HasValue)
                input.InstallationTime = new DateTime(input.InstallationTime.Value.Year, input.InstallationTime.Value.Month, input.InstallationTime.Value.Day, 0, 0, 0, DateTimeKind.Unspecified);
            if (input.InstallationMaxTime.HasValue)
                input.InstallationMaxTime = new DateTime(input.InstallationMaxTime.Value.Year, input.InstallationMaxTime.Value.Month, input.InstallationMaxTime.Value.Day, 0, 0, 0, DateTimeKind.Unspecified);
            if (input.VigencyTime.HasValue)
                input.VigencyTime = new DateTime(input.VigencyTime.Value.Year, input.VigencyTime.Value.Month, input.VigencyTime.Value.Day, 0, 0, 0, DateTimeKind.Unspecified);

            input.Document.IsValidOrException("Aviso", "El número de documento es obligatorio");
            input.Document.VerifyTableColumn(DialogSpaceDocumentConsts.DocumentMinLength,
                DialogSpaceDocumentConsts.DocumentMaxLength,
                "Aviso",
                $"El número de documento no debe exceder los {DialogSpaceDocumentConsts.DocumentMaxLength} caracteres");

            var dbDialogSpace = _dialogSpaceRepository
                .GetAll()
                .Where(p => p.Id == dialogSpaceId)
                .FirstOrDefault();

            if (dbDialogSpace == null)
                throw new UserFriendlyException("Aviso", "El espacio de diálogo seleccionado ya no existe o fue eliminado. Por favor verifique la información antes de continuar");

            input.DialogSpace = dbDialogSpace;
            input.DialogSpaceId = dbDialogSpace.Id;

            var dbDialogSpaceDocumentType = _dialogSpaceDocumentTypeRepository
                .GetAll()
                .Where(p => p.Id == dialogSpaceDocumentTypeId)
                .FirstOrDefault();

            if (dbDialogSpaceDocumentType == null)
                throw new UserFriendlyException("Aviso", "El tipo de documento seleccionado ya no existe o fue eliminado. Por favor verifique la información antes de continuar");

            input.DialogSpaceDocumentType = dbDialogSpaceDocumentType;
            input.DialogSpaceDocumentTypeId = dbDialogSpaceDocumentType.Id;

            if(input.HasInstallation == false)
            {
                input.InstallationTime = null;
                input.Range = DocumentRange.NONE;
            }

            if (input.Type == DocumentType.NONE)
                throw new UserFriendlyException("Aviso", "Debe seleccionar el tipo de. Por favor verifique la información antes de continuar");
            if (input.HasInstallation && input.Range == DocumentRange.NONE)
                throw new UserFriendlyException("Aviso", "Debe seleccionar el plazo de instalación se contabiliza. Por favor verifique la información antes de continuar");

            if(input.Type == DocumentType.CREATE && input.HasInstallation)
            {
                if (input.RangeSide == DocumentRangeSide.NONE)
                    throw new UserFriendlyException("Aviso", "Debe seleccionar el plazo máximo para la instalación. Por favor verifique la información antes de continuar");
                if (input.Days.HasValue == false)
                    throw new UserFriendlyException("Aviso", "Debe ingresar el plazo máximo para la instalación en (días) en los datos de la instalación del espacio de diálogo. Por favor verifique la información antes de continuar");
                if (input.InstallationMaxTime.HasValue == false)
                    throw new UserFriendlyException("Aviso", "Debe ingresar el fecha máxima en los datos de la instalación del espacio de diálogo. Por favor verifique la información antes de continuar");
            }
            else
            {
                input.RangeSide = null;
                input.Days = null;
                input.InstallationMaxTime = null;
            }

            var dialogSpaceDocumentSituation = _dialogSpaceDocumentSituationRepository
                .GetAll()
                .Where(p => p.Id == dialogSpaceDocumentSituationId)
                .FirstOrDefault();

            if (dialogSpaceDocumentSituation == null)
                throw new UserFriendlyException("Aviso", "La situación actual seleccionada ya no existe o fue eliminado. Por favor verifique la información antes de continuar");

            input.DialogSpaceDocumentSituation = dialogSpaceDocumentSituation;
            input.DialogSpaceDocumentSituationId = dialogSpaceDocumentSituation.Id;

            input.Resources = new List<DialogSpaceDocumentResource>();

            foreach (var resource in uploadFiles)
                input.Resources.Add(ObjectMapper.Map<DialogSpaceDocumentResource>(ResourceManager.Create(resource, ResourceConsts.DialogSpaceDocument)));

            return input;
        }
    }
}
