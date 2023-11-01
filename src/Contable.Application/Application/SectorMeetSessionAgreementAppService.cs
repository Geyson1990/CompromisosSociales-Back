using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Contable.Application.SectorMeetSessions.Dto;
using Contable.Application.SectorMeetSessionsAgreement;
using Contable.Application.SectorMeetSessionsAgreement.Dto;
using Contable.Authorization;
using Contable.Authorization.Users.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_ConflictTools_SectorMeet)]
    public class SectorMeetSessionAgreementAppService : ContableAppServiceBase, ISectorMeetSessionAgreementAppService
    {
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SectorMeetSession> _sectorMeetSessionRepository;
        private readonly IRepository<SectorMeet> _sectorMeetRepository;
        private readonly IRepository<SectorMeetSessionAction> _sectorMeetSessionActionRepository;
        private readonly IRepository<SectorMeetSessionAgreement> _sectorMeetSessionAgreementRepository;
        private readonly IRepository<SectorMeetSessionAgreementCompromise, long> _sectorMeetSessionAgreementCompromiseRepository;
        private readonly IRepository<SectorMeetSessionCriticalAspect> _sectorMeetSessionCriticalAspectRepository;
        private readonly IRepository<SectorMeetSessionSchedule> _sectorMeetSessionScheduleRepository;
        private readonly IRepository<SectorMeetSessionSummary> _sectorMeetSessionSummaryRepository;
        private readonly IRepository<SectorMeetSessionResource> _sectorMeetSessionResourceRepository;
        private readonly IRepository<SectorMeetSessionLeader> _sectorMeetSessionLeaderRepository;
        private readonly IRepository<SectorMeetSessionTeam> _sectorMeetSessionTeamRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<DirectoryIndustry> _directoryIndustryRepository;
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;

        public SectorMeetSessionAgreementAppService(
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<SectorMeetSession> sectorMeetSessionRepository,
            IRepository<SectorMeet> sectorMeetRepository,
            IRepository<SectorMeetSessionAction> sectorMeetSessionActionRepository,
            IRepository<SectorMeetSessionAgreement> sectorMeetSessionAgreementRepository,
            IRepository<SectorMeetSessionCriticalAspect> sectorMeetSessionCriticalAspectRepository,
            IRepository<SectorMeetSessionSchedule> sectorMeetSessionScheduleRepository,
            IRepository<SectorMeetSessionAgreementCompromise, long> sectorMeetSessionAgreementCompromiseRepository,
            IRepository<SectorMeetSessionSummary> sectorMeetSessionSummaryRepository,
            IRepository<SectorMeetSessionResource> sectorMeetSessionResourceRepository,
            IRepository<SectorMeetSessionLeader> sectorMeetSessionLeaderRepository,
            IRepository<SectorMeetSessionTeam> sectorMeetSessionTeamRepository,
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<District> districtRepository,
            IRepository<Person> personRepository,
            IRepository<DirectoryIndustry> directoryIndustryRepository,
            IRepository<DirectoryGovernment> directoryGovernmentRepository)
        {
            _socialConflictRepository = socialConflictRepository;
            _sectorMeetSessionRepository = sectorMeetSessionRepository;
            _sectorMeetRepository = sectorMeetRepository;
            _sectorMeetSessionActionRepository = sectorMeetSessionActionRepository;
            _sectorMeetSessionAgreementRepository = sectorMeetSessionAgreementRepository;
            _sectorMeetSessionCriticalAspectRepository = sectorMeetSessionCriticalAspectRepository;
            _sectorMeetSessionAgreementCompromiseRepository = sectorMeetSessionAgreementCompromiseRepository;
            _sectorMeetSessionScheduleRepository = sectorMeetSessionScheduleRepository;
            _sectorMeetSessionSummaryRepository = sectorMeetSessionSummaryRepository;
            _sectorMeetSessionResourceRepository = sectorMeetSessionResourceRepository;
            _sectorMeetSessionLeaderRepository = sectorMeetSessionLeaderRepository;
            _sectorMeetSessionTeamRepository = sectorMeetSessionTeamRepository;
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _personRepository = personRepository;
            _directoryIndustryRepository = directoryIndustryRepository;
            _directoryGovernmentRepository = directoryGovernmentRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Report_ConflictTools_SectorMeetSession)]
        public async Task<EntityDto<long>> Create(SectorMeetSessionAgreementCompromiseDto input)
        {
            //var entity = ObjectMapper.Map<SectorMeetSessionAgreementCompromise>(input);
            var entity = new SectorMeetSessionAgreementCompromise
            {
                CompromiseId = input.CompromiseId,
                StatusId = input.StatusId,
                SectorMeetSessionAgreementId = input.SectorMeetSessionAgreementId,
                CreationTime = DateTime.Now,
                IsDeleted = false
            };

            var SaveId = await _sectorMeetSessionAgreementCompromiseRepository.InsertAndGetIdAsync(entity);

            await CurrentUnitOfWork.SaveChangesAsync();

            return new EntityDto<long>(SaveId);
        }

        public async Task Delete(EntityDto input)
        {
            await _sectorMeetSessionAgreementCompromiseRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Report_ConflictTools_SectorMeetSession)]
        public async Task<List<SectorMeetSessionsAgreement.Dto.SectorMeetSessionAgreementCompromise>> Get(SocialConflictGetInputDto input)
        {
            var output = new List<SectorMeetSessionsAgreement.Dto.SectorMeetSessionAgreementCompromise>();

            if (input.SocialConflictId.HasValue)
            {
                VerifyCount(await _sectorMeetRepository.CountAsync(p => p.SocialConflictId == input.SocialConflictId.Value));

                var dbSectorMeetSessionAgreements = (from a1 in _socialConflictRepository.GetAll()
                                                     join a in _sectorMeetRepository.GetAll() on a1.Id equals a.SocialConflictId
                                                     join b in _sectorMeetSessionRepository.GetAll()
                                                     .Include(p => p.Person)
                                                     .Include(p => p.Resources)
                                                     on a.Id equals b.SectorMeetId
                                                     join c in _sectorMeetSessionAgreementRepository.GetAll() on b.Id equals c.SectorMeetSessionId
                                                     join d in _sectorMeetSessionAgreementCompromiseRepository.GetAll() on c.Id equals d.SectorMeetSessionAgreementId
                                                     into asignados
                                                     from x in asignados.DefaultIfEmpty()

                                                     where a.SocialConflictId == input.SocialConflictId
                                                     select new SectorMeetSessionsAgreement.Dto.SectorMeetSessionAgreementCompromise()
                                                     {
                                                         SocialConflictId = a.Id,
                                                         Code = a1.Code,
                                                         CaseName = a1.CaseName,
                                                         MeetName = a.MeetName,
                                                         PersonId = b.PersonId,
                                                         CreationTime = b.CreationTime,
                                                         SectorMeetSessionId = c.SectorMeetSessionId,
                                                         Id = (x == null ? (long)0 : x.Id),
                                                         Index = c.Index,
                                                         Description = c.Description,
                                                         SectorMeetSessionAgreementId = x == null ? 0 : x.SectorMeetSessionAgreementId,
                                                         CompromiseId = (x == null ? 0 : x.CompromiseId),
                                                         Person = b.Person != null ? ObjectMapper.Map<UserPersonDto>(b.Person) : null,
                                                         ResourceRelationDto = b.Resources != null ? ObjectMapper.Map<List<SectorMeetSessionResourceRelationDto>>(b.Resources) : null

                                                     }

                  );

                output = dbSectorMeetSessionAgreements.ToList();


            }


            return output;
        }


    }
}
