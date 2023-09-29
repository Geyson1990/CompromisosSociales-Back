using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.HelpMemories;
using Contable.Application.HelpMemories.Dto;
using Contable.Application.Extensions;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contable.Application.Uploaders.Dto;
using Abp.UI;
using Contable.Authorization.Users;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_HelpMemory)]
    public class HelpMemoryAppService : ContableAppServiceBase, IHelpMemoryAppService
    {
        private readonly IRepository<HelpMemory> _helpMemoryRepository;
        private readonly IRepository<HelpMemoryResource> _helpMemoryResourceRepository;
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SocialConflictSensible> _socialConflictSensibleRepository;
        private readonly IRepository<User, long> _userRepository;

        public HelpMemoryAppService(
            IRepository<HelpMemory> helpMemoryRepository, 
            IRepository<HelpMemoryResource> helpMemoryResourceRepository, 
            IRepository<DirectoryGovernment> directoryGovernmentRepository,
            IRepository<SocialConflict> socialConflictRepository, 
            IRepository<SocialConflictSensible> socialConflictSensibleRepository,
            IRepository<User, long> userRepository)
        {
            _helpMemoryRepository = helpMemoryRepository;
            _helpMemoryResourceRepository = helpMemoryResourceRepository;
            _directoryGovernmentRepository = directoryGovernmentRepository;
            _socialConflictRepository = socialConflictRepository;
            _socialConflictSensibleRepository = socialConflictSensibleRepository;
            _userRepository = userRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_HelpMemory_Create)]
        public async Task<EntityDto> Create(HelpMemoryCreateDto input)
        {
            var helpMemoryId = await _helpMemoryRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map<HelpMemory>(input),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                socialConflictSensibleId: input.SocialConflictSensible == null ? -1 : input.SocialConflictSensible.Id,
                directoryGovernmentId: input.DirectoryGovernment == null ? -1 : input.DirectoryGovernment.Id,
                resources: input.Resources,
                uploads: input.UploadFiles));

            await CurrentUnitOfWork.SaveChangesAsync();

            await FunctionManager.CallCreateHelpMemoryCodeProcess(helpMemoryId);

            return new EntityDto(helpMemoryId);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_HelpMemory_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _helpMemoryRepository.CountAsync(p => p.Id == input.Id));

            await _helpMemoryRepository.DeleteAsync(p => p.Id == input.Id);
            await _helpMemoryResourceRepository.DeleteAsync(p => p.HelpMemoryId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_HelpMemory)]
        public async Task<HelpMemoryGetDto> Get(HelpMemoryGetDto input)
        {
            VerifyCount(await _helpMemoryRepository.CountAsync(p => p.Id == input.Id));

            var helpMemory = _helpMemoryRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.SocialConflictSensible)
                .Include(p => p.DirectoryGovernment)
                .Include(p => p.Resources)
                .Where(p => p.Id == input.Id)
                .FirstOrDefault();

            var output = ObjectMapper.Map<HelpMemoryGetDto>(helpMemory);

            foreach(var resource in output.Resources)
            {
                resource.Name = helpMemory.Code;
            }

            output.CreatorUser = helpMemory.CreatorUserId.HasValue == false ? null : ObjectMapper.Map<HelpMemoryUserDto>(_userRepository
            .GetAll()
            .Where(p => p.Id == helpMemory.CreatorUserId.Value)
            .FirstOrDefault());

            output.EditionUser = helpMemory.LastModifierUserId.HasValue == false ? null : ObjectMapper.Map<HelpMemoryUserDto>(_userRepository
                .GetAll()
                .Where(p => p.Id == helpMemory.LastModifierUserId.Value)
                .FirstOrDefault());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_HelpMemory)]
        public async Task<PagedResultDto<HelpMemoryGetAllDto>> GetAll(HelpMemoryGetAllInputDto input)
        {
            var query = _helpMemoryRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.SocialConflictSensible)
                .Include(p => p.DirectoryGovernment)
                .Include(p => p.Resources)
                .WhereIf(input.SocialConflictId.HasValue, p => p.SocialConflictId == input.SocialConflictId.Value)
                .WhereIf(input.SocialConflictSensibleId.HasValue, p => p.SocialConflictSensibleId == input.SocialConflictSensibleId.Value)
                .WhereIf(input.DirectoryGovernmentId.HasValue, p => p.DirectoryGovernmentId == input.DirectoryGovernmentId.Value)
                .LikeAllBidirectional(input
                    .SocialConflictCode
                    .SplitByLike()
                    .Select(word => (Expression<Func<HelpMemory, bool>>)(expression => expression.SocialConflict != null && EF.Functions.Like(expression.SocialConflict.Code, $"%{word}%")))
                    .ToArray())
                .LikeAllBidirectional(input
                    .SocialConflictSensibleCode
                    .SplitByLike()
                    .Select(word => (Expression<Func<HelpMemory, bool>>)(expression => expression.SocialConflictSensible != null && EF.Functions.Like(expression.SocialConflictSensible.Code, $"%{word}%")))
                    .ToArray())
                .LikeAllBidirectional(input.HelpMemoryRequest.SplitByLike(), nameof(HelpMemory.Request));

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<HelpMemoryGetAllDto>(count, ObjectMapper.Map<List<HelpMemoryGetAllDto>>(result));
        }

        [AbpAuthorize(AppPermissions.Pages_Application_HelpMemory_Edit)]
        public async Task<EntityDto> Update(HelpMemoryUpdateDto input)
        {
            VerifyCount(await _helpMemoryRepository.CountAsync(p => p.Id == input.Id));

            var helpMemoryId = await _helpMemoryRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map(input, await _helpMemoryRepository.GetAsync(input.Id)),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                socialConflictSensibleId: input.SocialConflictSensible == null ? -1 : input.SocialConflictSensible.Id,
                directoryGovernmentId: input.DirectoryGovernment == null ? -1 : input.DirectoryGovernment.Id,
                resources: input.Resources,
                uploads: input.UploadFiles));

            await CurrentUnitOfWork.SaveChangesAsync();

            return new EntityDto(helpMemoryId);
        }

        private async Task<HelpMemory> ValidateEntity(
            HelpMemory input,
            int socialConflictId,
            int socialConflictSensibleId,
            int directoryGovernmentId,
            List<HelpMemoryResourceRelationDto> resources,
            List<UploadResourceInputDto> uploads)
        {
            input.Request.IsValidOrException(DefaultTitleMessage, "La denominación del caso (mesa de diálogo) es obligatoria");
            input.Request.VerifyTableColumn(HelpMemoryConsts.RequestMinLength,
                HelpMemoryConsts.RequestMaxLength, 
                DefaultTitleMessage, 
                $"El texto de la persona solicitante no debe exceder los {HelpMemoryConsts.RequestMaxLength} caracteres");

            if (input.RequestTime > DateTime.Now)
                throw new UserFriendlyException("Aviso", "La fecha de solicitud no debe ser mayor a la fecha actual");

            if (input.Id <= 0 && uploads.Count == 0)
                throw new UserFriendlyException("Aviso", "Debe subir el documento de ayuda y memoria para guardar el registro.");

            if (uploads.Count > 1)
                throw new UserFriendlyException("Aviso", "Solo puedes subir un archivo de ayuda y memoria, remueve alguno para continuar");

            input.Resources = new List<HelpMemoryResource>();

            if (socialConflictId > 0)
            {
                if (await _socialConflictRepository.CountAsync(p => p.Id == socialConflictId) == 0)
                    throw new UserFriendlyException("Aviso", "El conflicto social seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

                var socialConflict = await _socialConflictRepository.GetAsync(socialConflictId);

                input.SocialConflict = socialConflict;
                input.SocialConflictId = socialConflict.Id;
                input.Site = ConflictSite.SocialConflict;
            }
            else
            {
                input.SocialConflict = null;
                input.SocialConflictId = null;
                input.Site = ConflictSite.All;
            }

            if (socialConflictSensibleId > 0)
            {
                if (await _socialConflictRepository.CountAsync(p => p.Id == socialConflictSensibleId) == 0)
                    throw new UserFriendlyException("Aviso", "El coordinador de la UT ya no existe o fue eliminado. Verifique la información antes de continuar");

                var socialConflictSensible = await _socialConflictSensibleRepository.GetAsync(socialConflictSensibleId);

                input.SocialConflictSensible = socialConflictSensible;
                input.SocialConflictSensibleId = socialConflictSensible.Id;
                input.Site = ConflictSite.SocialConflictSensible;
            }
            else
            {
                if(socialConflictId <= 0 && socialConflictSensibleId <= 0)
                {
                    input.SocialConflictSensible = null;
                    input.SocialConflictSensibleId = null;
                    input.Site = ConflictSite.All;
                }
            }

            if (await _directoryGovernmentRepository.CountAsync(p => p.Id == directoryGovernmentId) == 0)
                throw new UserFriendlyException("Aviso", "El gestor del cargo ya no existe o fue eliminado. Verifique la información antes de continuar");

            var directoryGovernment = await _directoryGovernmentRepository.GetAsync(directoryGovernmentId);

            input.DirectoryGovernment = directoryGovernment;
            input.DirectoryGovernmentId = directoryGovernment.Id;

            if (input.Site == ConflictSite.All)
                throw new UserFriendlyException("Aviso", "Debe seleccionar el conflicto antes de continuar");

            if(input.Id > 0)
            {
                var dbResources = _helpMemoryResourceRepository
                    .GetAll()
                    .Where(p => p.HelpMemoryId == input.Id)
                    .ToList();

                foreach (var dbResource in dbResources)
                    await _helpMemoryResourceRepository.DeleteAsync(dbResource.Id);
            }

            foreach (var resource in resources)
            {
                if (resource.Remove)
                {
                    if (resource.Id > 0 && input.Id > 0 && await _helpMemoryResourceRepository.CountAsync(p => p.Id == resource.Id && p.HelpMemoryId == input.Id) > 0)
                    {
                        await _helpMemoryResourceRepository.DeleteAsync(resource.Id);
                    }
                }
            }

            foreach (var upload in uploads)
            {
                if (ResourceManager.TokenIsValid(upload.Token) == false)
                    throw new UserFriendlyException("Aviso", "La validez de los archivos subidos a caducado, por favor intente nuevamente.");
            }

            foreach (var upload in uploads)
            {
                input.Resources.Add(ObjectMapper.Map<HelpMemoryResource>(ResourceManager.Create(upload, ResourceConsts.HelpMemory)));
            }

            return input;
        }
    }
}
