using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Contable.Application.ResponsibleActors;
using Contable.Application.ResponsibleActors.Dto;
using Contable.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contable.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor)]
    public class ResponsibleActorAppService : ContableAppServiceBase, ICompromiseStateAppService
    {
        private readonly IRepository<ResponsibleActor> _responsibleActorRepository;
        private readonly IRepository<ResponsibleSubActor> _responsibleSubActorRepository;
        private readonly IRepository<ResponsibleType> _responsibleTypeRepository;
        private readonly IRepository<ResponsibleSubType> _responsibleSubTypeRepository;

        public ResponsibleActorAppService(
            IRepository<ResponsibleActor> responsibleActorRepository, 
            IRepository<ResponsibleSubActor> responsibleSubActorRepository,
            IRepository<ResponsibleType> responsibleTypeRepository,
            IRepository<ResponsibleSubType> responsibleSubTypeRepository)
        {
            _responsibleActorRepository = responsibleActorRepository;
            _responsibleSubActorRepository = responsibleSubActorRepository;
            _responsibleTypeRepository = responsibleTypeRepository;
            _responsibleSubTypeRepository = responsibleSubTypeRepository;
        }

        #region ResponsibleActor

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor_Create)]
        public async Task CreateResponsibleActor(ResponsibleActorCreateDto input)
        {
            await _responsibleActorRepository.InsertAsync(await ValidateEntity(
                input: ObjectMapper.Map<ResponsibleActor>(input),
                responsibleTypeId: input.ResponsibleType == null ? -1 : input.ResponsibleType.Id,
                responsibleSubTypeId: input.ResponsibleSubType == null ? -1 : input.ResponsibleSubType.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor_Delete)]
        public async Task DeleteResponsibleAction(EntityDto input)
        {
            VerifyCount(await _responsibleActorRepository.CountAsync(p => p.Id == input.Id));

            await _responsibleActorRepository.DeleteAsync(input.Id);
            await _responsibleSubActorRepository.DeleteAsync(p => p.ResponsibleActor.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor)]
        public async Task<PagedResultDto<ResponsibleActorGetAllDto>> GetAllResponsibleActors(ResponsibleActorGetAllInputDto input)
        {
            var query = _responsibleActorRepository
                .GetAll()
                .Include(p => p.ResponsibleSubActors)
                .Include(p => p.ResponsibleType)
                .Include(p => p.ResponsibleSubType)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(ResponsibleActor.Name));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ResponsibleActorGetAllDto>(count, ObjectMapper.Map<List<ResponsibleActorGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor)]
        public async Task<ResponsibleActorGetDataDto> GetResponsibleActor(NullableIdDto input)
        {
            var output = new ResponsibleActorGetDataDto();

            if(input.Id.HasValue)
            {
                VerifyCount(await _responsibleActorRepository.CountAsync(p => p.Id == input.Id));

                output.ResponsibleActor = ObjectMapper.Map<ResponsibleActorGetDto>(_responsibleActorRepository
                    .GetAll()
                    .Include(p => p.ResponsibleType)
                    .Include(p => p.ResponsibleSubType)
                    .Where(p => p.Id == input.Id.Value)
                    .First());
            }

            output.Types = ObjectMapper.Map<List<ResponsibleActorTypeDto>>(_responsibleTypeRepository
                .GetAll()
                .Include(p => p.SubTypes)
                .OrderBy(p => p.Name)
                .ToList());

            foreach(var type in output.Types)
                type.SubTypes = type.SubTypes.OrderBy(p => p.Name).ToList();

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor_Edit)]
        public async Task UpdateResponsibleActor(ResponsibleActorUpdateDto input)
        {
            VerifyCount(await _responsibleActorRepository.CountAsync(p => p.Id == input.Id));
            await _responsibleActorRepository.UpdateAsync(await ValidateEntity(
                input: ObjectMapper.Map(input, await _responsibleActorRepository.GetAsync(input.Id)),
                responsibleTypeId: input.ResponsibleType == null ? -1 : input.ResponsibleType.Id,
                responsibleSubTypeId: input.ResponsibleSubType == null ? -1 : input.ResponsibleSubType.Id));
        }

        private async Task<ResponsibleActor> ValidateEntity(ResponsibleActor input, int responsibleTypeId, int responsibleSubTypeId)
        {
            if (await _responsibleActorRepository.CountAsync(p => p.Name == input.Name && p.Id != input.Id) > 0)
                throw new UserFriendlyException(DefaultTitleMessage, $"Ya existe un actor responsable con el nombre {input.Name}");

            input.Name.IsValidOrException(DefaultTitleMessage, "El nombre del actor responsable es obligatorio");
            input.Name.VerifyTableColumn(ResponsibleActorConsts.NameMinLength, ResponsibleActorConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del actor responsable no debe exceder los {ResponsibleActorConsts.NameMaxLength} caracteres");

            if (await _responsibleTypeRepository.CountAsync(p => p.Id == responsibleTypeId) == 0)
                throw new UserFriendlyException("Aviso", "El tipo de actor seleccionado ya no existe o fue eliminado. Por favor revise la información antes de continuar");

            var type = await _responsibleTypeRepository.GetAsync(responsibleTypeId);

            input.ResponsibleType = type;
            input.ResponsibleTypeId = type.Id;

            if (await _responsibleSubTypeRepository.CountAsync(p => p.Id == responsibleSubTypeId) == 0)
                throw new UserFriendlyException("Aviso", "El subtipo de actor seleccionado ya no existe o fue eliminado. Por favor revise la información antes de continuar");

            var subtype = await _responsibleSubTypeRepository.GetAsync(responsibleSubTypeId);

            input.ResponsibleSubType = subtype;
            input.ResponsibleSubTypeId = subtype.Id;

            return input;
        }

        #endregion

        #region ResponsibleSubActor

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor_SubActor_Create)]
        public async Task CreateResponsibleSubActor(ResponsibleSubActorCreateDto input)
        {
            if (await _responsibleActorRepository.CountAsync(p => p.Id == input.ResponsibleActorId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El actor reposable es necesario para registrar o actualizar un responsable específico");

            await _responsibleSubActorRepository.InsertAsync(await ValidateEntity(ObjectMapper.Map<ResponsibleSubActor>(input), await _responsibleActorRepository.GetAsync(input.ResponsibleActorId)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor_SubActor_Delete)]
        public async Task DeleteResponsibleSubActor(EntityDto input)
        {
            VerifyCount(await _responsibleSubActorRepository.CountAsync(p => p.Id == input.Id));
            await _responsibleSubActorRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor_SubActor)]
        public async Task<ResponsibleSubActorGetDto> GetResponsibleSubActor(EntityDto input)
        {
            VerifyCount(await _responsibleSubActorRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<ResponsibleSubActorGetDto>(await _responsibleSubActorRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Responsible_Actor_SubActor_Edit)]
        public async Task UpdateResponsibleSubActor(ResponsibleSubActorUpdateDto input)
        {
            VerifyCount(await _responsibleSubActorRepository.CountAsync(p => p.Id == input.Id));
            await _responsibleSubActorRepository.UpdateAsync(await ValidateEntity(ObjectMapper.Map<ResponsibleSubActor>(input), await _responsibleActorRepository.GetAsync(input.ResponsibleActorId)));
        }

        private async Task<ResponsibleSubActor> ValidateEntity(ResponsibleSubActor responsibleSubActor, ResponsibleActor responsibleActor = null)
        {
            if (await _responsibleSubActorRepository.CountAsync(p => p.Name == responsibleSubActor.Name && p.Id != responsibleSubActor.Id && p.ResponsibleActor.Id != responsibleActor.Id) > 0)
                throw new UserFriendlyException(DefaultTitleMessage, $"Ya existe un responsable específico con el nombre {responsibleSubActor.Name} en el actor responsable {responsibleActor.Name}");

            responsibleSubActor.Name.IsValidOrException(DefaultTitleMessage, "El nombre del responsable específico es obligatorio");
            responsibleSubActor.Name.VerifyTableColumn(ResponsibleActorConsts.NameMinLength, ResponsibleActorConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del responsable específico no debe exceder los {ResponsibleActorConsts.NameMaxLength} caracteres");

            if (responsibleActor != null)
                responsibleSubActor.ResponsibleActor = responsibleActor;

            return responsibleSubActor;
        }

        #endregion
    }
}
