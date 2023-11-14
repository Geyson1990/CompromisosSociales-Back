using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Contable.Application.Extensions;
using Contable.Application.Meets;
using Contable.Application.Meets.Dto;
using Contable.Application.MeetsResponsibles.Dto;
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
    public class MeetAppService : ContableAppServiceBase, IMeetAppService
    {
        private readonly IRepository<Meet> _meetRepository;
        private readonly IRepository<SectorMeetResource> _sectorMeetResourceRepository;

        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;

        public MeetAppService(
            IRepository<Meet> meetRepository,
            IRepository<SectorMeetResource> sectorMeetResourceRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<TerritorialUnit> territorialUnitRepository)
        {
            _meetRepository = meetRepository;
            _socialConflictRepository = socialConflictRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _sectorMeetResourceRepository = sectorMeetResourceRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet_Create)]
        public async Task<EntityDto> Create(MeetCreateDto input)
        {
            //if (input.ReplaceCode)
            //{
            //    if (input.ReplaceYear <= 0)
            //        throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
            //    if (input.ReplaceCount <= 0)
            //        throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                
            //    if (await _meetRepository.CountAsync(p => p.Code == $"{input.ReplaceYear} - {input.ReplaceCount}") > 0)
            //        throw new UserFriendlyException(DefaultTitleMessage, "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            //}

            var data = new Meet() { 
                Year= input.ReplaceYear,
                Objet= input.Objet,
                ModalityId= input.ModalityId,   
                MeetName= input.MeetName,
                LevelRiskId= input.LevelRiskId,
                PlaceId= input.PlaceId,
                RolId= input.RolId,
                SocialConflictId= input.SocialConflict.Id,
                ResponsibleId= input.ResponsibleId, 
                ResponsibleName= input.ResponsibleName, 
                TypeId= input.TypeId,
                FactorRisk= input.FactorRisk,
                CreationTime= DateTime.Now,
                //Participants= ObjectMapper.Map<Meet>(input)

            };

            var meetModel = ObjectMapper.Map<Meet>(input);

            var sectorMeetId = await _meetRepository.InsertAndGetIdAsync(await ValidateEntity(
                input: meetModel,
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id
             
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
            VerifyCount(await _meetRepository.CountAsync(p => p.Id == input.Id));

            await _meetRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
        public async Task<MeetGetDataDto> Get(NullableIdDto input)
        {
            var output = new MeetGetDataDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _meetRepository.CountAsync(p => p.Id == input.Id.Value));

                var dbSectorMeet = _meetRepository
                    .GetAll()                   
                    .Include(p => p.SocialConflict)
                    .Include(p => p.Participants)
                    .Where(p => p.Id == input.Id.Value)
                    .First();

                output.Meet = ObjectMapper.Map<MeetGetDto>(dbSectorMeet); 
                
                output.MeetsParticipants = ObjectMapper.Map<List<MeetParticipantsGetDto>>(dbSectorMeet.Participants);
            }

            

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
        public async Task<PagedResultDto<SectorMeetGetAllDto>> GetAll(SectorMeetGetAllInputDto input)
        {
            var query = _meetRepository
                .GetAll()
                .Include(p => p.SocialConflict)              
               
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.SectorMeetCode.SplitByLike(), nameof(Meet.Code))
                .LikeAllBidirectional(input.SectorMeetName.SplitByLike(), nameof(Meet.MeetName));

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
                
            }

            var sectorMeetId = await _meetRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                input: ObjectMapper.Map(input, await _meetRepository.GetAsync(input.Id)),
                socialConflictId: input.SocialConflict == null ? -1 : input.SocialConflict.Id              
                ));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateSectorMeetCodeReplaceProcess(sectorMeetId, input.ReplaceYear, input.ReplaceCount);

            return new EntityDto(sectorMeetId);
        }

        private async Task<Meet> ValidateEntity(Meet input, int socialConflictId)
        {
            input.MeetName.IsValidOrException("Aviso", "El nombre de la reunión es obligatorio");
            input.MeetName.VerifyTableColumn(SectorMeetConsts.MeetNameMinLength,
                SectorMeetConsts.MeetNameMaxLength,
                "Aviso",
                $"El nombre de la reunión no debe exceder los {SectorMeetConsts.MeetNameMaxLength} caracteres");

           

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
            

            return input;
        }
    }
}
