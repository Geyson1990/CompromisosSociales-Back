using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Contable.Application.Extensions;
using Contable.Application.SectorMeets;
using Contable.Application.SectorMeets.Dto;
using Contable.Application.SectorMeetSessions.Dto;
using Contable.Application.Uploaders.Dto;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
    public class SectorMeetAppService : ContableAppServiceBase, ISectorMeetAppService
    {
        private readonly IRepository<SectorMeet> _sectorMeetRepository;
        private readonly IRepository<SectorMeetResource> _sectorMeetResourceRepository;

        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;

        public SectorMeetAppService(
            IRepository<SectorMeet> sectorMeetRepository,
            IRepository<SectorMeetResource> sectorMeetResourceRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<TerritorialUnit> territorialUnitRepository)
        {
            _sectorMeetRepository = sectorMeetRepository;
            _socialConflictRepository = socialConflictRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _sectorMeetResourceRepository = sectorMeetResourceRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet_Create)]
        public async Task<EntityDto> Create(SectorMeetCreateDto input)
        {
            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _sectorMeetRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
                if (await _sectorMeetRepository.CountAsync(p => p.Code == $"{input.ReplaceYear} - {input.ReplaceCount}") > 0)
                    throw new UserFriendlyException(DefaultTitleMessage, "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var sectorMeetId = await _sectorMeetRepository.InsertAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map<SectorMeet>(input),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                territorialUnitId: input.TerritorialUnit == null ? -1 : input.TerritorialUnit.Id,
                uploadFiles: input.UploadFiles ?? new List<SectorMeetSessionAttachmentDto>()
            ));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateSectorMeetCodeReplaceProcess(sectorMeetId, input.ReplaceYear, input.ReplaceCount);
            else
                await FunctionManager.CallCreateSectorMeetCodeProcess(sectorMeetId);

            return new EntityDto(sectorMeetId);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _sectorMeetRepository.CountAsync(p => p.Id == input.Id));

            await _sectorMeetRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
        public async Task<SectorMeetGetDataDto> Get(NullableIdDto input)
        {
            var output = new SectorMeetGetDataDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _sectorMeetRepository.CountAsync(p => p.Id == input.Id.Value));

                var dbSectorMeet = _sectorMeetRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.SocialConflict)
                    .Where(p => p.Id == input.Id.Value)
                    .First();

                output.SectorMeet = ObjectMapper.Map<SectorMeetGetDto>(dbSectorMeet);

                var datos = _sectorMeetResourceRepository.GetAll()
                  .Where(p => p.SectorMeetId == dbSectorMeet.Id)
                  .ToList();

                //var mapperDatos = ObjectMapper.Map<List<SectorMeetResourceRelationDto>>(datos);
                var mapperDatos = new List<SectorMeetResourceRelationDto>();
                foreach (var item in datos)
                {
                    mapperDatos.Add(new SectorMeetResourceRelationDto
                    {
                        ClassName= item.ClassName,
                        CreationTime= item.CreationTime,
                        Description= item.Description,
                        Name= item.Name,
                        Extension= item.Extension,
                        FileName= item.FileName,
                        Id= item.Id,
                        Size= item.Size,
                        SectionFolder= item.SectionFolder,                       

                    });
                }

                output.SectorMeet.Resources = mapperDatos;
            }

            output.TerritorialUnits = ObjectMapper.Map<List<SectorMeetTerritorialUnitRelationDto>>(_territorialUnitRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .ToList());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
        public async Task<PagedResultDto<SectorMeetGetAllDto>> GetAll(SectorMeetGetAllInputDto input)
        {
            var query = _sectorMeetRepository
                .GetAll()
                .Include(p => p.SocialConflict)
                .Include(p => p.TerritorialUnit)
                .WhereIf(input.DepartmentId.HasValue, p => p.Sessions.Any(p => p.DepartmentId == input.DepartmentId.Value))
                .WhereIf(input.ProvinceId.HasValue, p => p.Sessions.Any(p => p.ProvinceId == input.ProvinceId.Value))
                .WhereIf(input.DistrictId.HasValue, p => p.Sessions.Any(p => p.DistrictId == input.DistrictId.Value))
                .WhereIf(input.PersonId.HasValue, p => p.Sessions.Any(p => p.PersonId == input.PersonId.Value))
                .WhereIf(input.SectorMeetSessionType.HasValue && input.SectorMeetSessionType.Value != SectorMeetSessionType.NONE, p => p.Sessions.Any(d => d.Type == input.SectorMeetSessionType.Value))
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.SectorMeetCode.SplitByLike(), nameof(SectorMeet.Code))
                .LikeAllBidirectional(input.SectorMeetName.SplitByLike(), nameof(SectorMeet.MeetName));

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<SectorMeetGetAllDto>(count, ObjectMapper.Map<List<SectorMeetGetAllDto>>(result));
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet_Edit)]
        public async Task<EntityDto> Update(SectorMeetUpdateDto input)
        {
            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _sectorMeetRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var sectorMeetId = await _sectorMeetRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map(input, await _sectorMeetRepository.GetAsync(input.Id)),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id,
                territorialUnitId: input.TerritorialUnit == null ? -1 : input.TerritorialUnit.Id,
                 uploadFiles: input.UploadFiles ?? new List<SectorMeetSessionAttachmentDto>()
                ));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateSectorMeetCodeReplaceProcess(sectorMeetId, input.ReplaceYear, input.ReplaceCount);

            return new EntityDto(sectorMeetId);
        }

        private async Task<SectorMeet> ValidateEntity(SectorMeet input, int socialConflictId, int territorialUnitId, List<SectorMeetSessionAttachmentDto> uploadFiles)
        {
            input.MeetName.IsValidOrException("Aviso", "El nombre de la reunión es obligatorio");
            input.MeetName.VerifyTableColumn(SectorMeetConsts.MeetNameMinLength,
                SectorMeetConsts.MeetNameMaxLength,
                "Aviso",
                $"El nombre de la reunión no debe exceder los {SectorMeetConsts.MeetNameMaxLength} caracteres");

            if (await _territorialUnitRepository.CountAsync(p => p.Id == territorialUnitId) == 0)
                throw new UserFriendlyException("Aviso", "La unidad territorial seleccionada es inválida o ya no existe. Por favor verifique la información antes de continuar.");

            if (socialConflictId > 0)
            {
                var dbSocialConflict = _socialConflictRepository
                    .GetAll()
                    .Where(p => p.Id == socialConflictId)
                    .FirstOrDefault();

                if (dbSocialConflict == null)
                    throw new UserFriendlyException("Aviso", "El caso seleccionado es inválido o ya no existe. Por favor verifique la información antes de continuar.");

                input.SocialConflict = dbSocialConflict;
                input.SocialConflictId = dbSocialConflict.Id;
            }
            else
            {
                input.SocialConflict = null;
                input.SocialConflictId = null;
            }

            var territorialUnit = await _territorialUnitRepository.GetAsync(territorialUnitId);

            input.TerritorialUnit = territorialUnit;
            input.TerritorialUnitId = territorialUnit.Id;
            input.Resources = new List<SectorMeetResource>();

            foreach (var uploadFile in uploadFiles)
            {
                var recurso = ResourceManager.Create(
                    resource: ObjectMapper.Map<UploadResourceInputDto>(uploadFile),
                    section: ResourceConsts.SectorMeet
                );
                //var dbResource = ObjectMapper.Map<SectorMeetResource>(recurso);
                var dbResource = new SectorMeetResource
                {
                    Name = recurso.Name,
                    LastModificationTime = DateTime.Now,
                    ResourceFolder = recurso.ResourceFolder,
                    Resource = recurso.Resource,
                    FileName = recurso.FileName,
                    ClassName = recurso.ClassName,
                    SectionFolder = recurso.SectionFolder,
                    CommonFolder = recurso.CommonFolder,
                    Description = recurso.Description,
                    Extension = recurso.Extension,
                    Size = recurso.Size,
                };

                input.Resources.Add(dbResource);
            }

            return input;
        }
    }
}
