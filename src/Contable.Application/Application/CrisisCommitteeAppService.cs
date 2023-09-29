using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Contable.Application.CrisisCommittees;
using Contable.Application.CrisisCommittees.Dto;
using Contable.Application.Extensions;
using Contable.Authorization;
using Contable.Authorization.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_ConflictTools_CrisisCommittee)]
    public class CrisisCommitteeAppService : ContableAppServiceBase, ICrisisCommitteeAppService
    {
        private readonly IRepository<CrisisCommittee> _crisisCommitteeRepository;
        private readonly IRepository<CrisisCommitteeTeam> _crisisCommitteeTeamRepository;
        private readonly IRepository<CrisisCommitteeJob> _crisisCommitteeJobRepository;
        private readonly IRepository<CrisisCommitteePlan> _crisisCommitteePlanRepository;
        private readonly IRepository<CrisisCommitteeAction> _crisisCommitteeActionRepository;
        private readonly IRepository<CrisisCommitteeMessage> _crisisCommitteeMessageRepository;
        private readonly IRepository<CrisisCommitteeChannel> _crisisCommitteeChannelRepository;
        private readonly IRepository<CrisisCommitteeSector> _crisisCommitteeSectorRepository;
        private readonly IRepository<CrisisCommitteeTask> _crisisCommitteeTaskRepository;
        private readonly IRepository<CrisisCommitteeAgreement> _crisisCommitteeAgreementRepository;
        private readonly IRepository<AlertResponsible> _alertResponsibleRepository;
        private readonly IRepository<InterventionPlan> _interventionPlanRepository;
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Person> _personRepository;

        public CrisisCommitteeAppService(
            IRepository<CrisisCommittee> crisisCommitteeRepository,
            IRepository<CrisisCommitteeTeam> crisisCommitteeTeamRepository,
            IRepository<CrisisCommitteeJob> crisisCommitteeJobRepository,
            IRepository<CrisisCommitteePlan> crisisCommitteePlanRepository,
            IRepository<CrisisCommitteeAction> crisisCommitteeActionRepository,
            IRepository<CrisisCommitteeMessage> crisisCommitteeMessageRepository,
            IRepository<CrisisCommitteeChannel> crisisCommitteeChannelRepository,
            IRepository<CrisisCommitteeSector> crisisCommitteeSectorRepository,
            IRepository<CrisisCommitteeTask> crisisCommitteeTaskRepository,
            IRepository<CrisisCommitteeAgreement> crisisCommitteeAgreementRepository,
            IRepository<AlertResponsible> alertResponsibleRepository,
            IRepository<InterventionPlan> interventionPlanRepository,
            IRepository<DirectoryGovernment> directoryGovernmentRepository,
            IRepository<User, long> userRepository,
            IRepository<Person> personRepository)
        {
            _crisisCommitteeRepository = crisisCommitteeRepository;
            _crisisCommitteeTeamRepository = crisisCommitteeTeamRepository;
            _crisisCommitteeJobRepository = crisisCommitteeJobRepository;
            _crisisCommitteePlanRepository = crisisCommitteePlanRepository;
            _crisisCommitteeActionRepository = crisisCommitteeActionRepository;
            _crisisCommitteeMessageRepository = crisisCommitteeMessageRepository;
            _crisisCommitteeChannelRepository = crisisCommitteeChannelRepository;
            _crisisCommitteeSectorRepository = crisisCommitteeSectorRepository;
            _crisisCommitteeTaskRepository = crisisCommitteeTaskRepository;
            _crisisCommitteeAgreementRepository = crisisCommitteeAgreementRepository;
            _alertResponsibleRepository = alertResponsibleRepository;
            _interventionPlanRepository = interventionPlanRepository;
            _directoryGovernmentRepository = directoryGovernmentRepository;
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_CrisisCommittee_Create)]
        public async Task<EntityDto> Create(CrisisCommitteeCreateDto input)
        {
            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _crisisCommitteeRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var crisisCommitteeId = await _crisisCommitteeRepository.InsertAndGetIdAsync(await ValidateEntity(
                    input: ObjectMapper.Map<CrisisCommittee>(input),
                    interventionPlanId: input.InterventionPlan == null ? -1 : input.InterventionPlan.Id,
                    personId: input.Person == null ? -1 : input.Person.Id,
                    teams: input.Teams ?? new List<CrisisCommitteeTeamRelationDto>(),
                    plans: input.Plans ?? new List<CrisisCommitteePlanRelationDto>(),
                    actions: input.Actions ?? new List<CrisisCommitteeActionRelationDto>(),
                    messages: input.Messages ?? new List<CrisisCommitteeMessageRelationDto>(),
                    channels: input.Channels ?? new List<CrisisCommitteeChannelRelationDto>(),
                    sectors: input.Sectors ?? new List<CrisisCommitteeSectorRelationDto>(),
                    tasks: input.Tasks ?? new List<CrisisCommitteeTaskRelationDto>(),
                    agreements: input.Agreements ?? new List<CrisisCommitteeAgreementRelationDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateCrisisCommitteeCodeReplaceProcess(crisisCommitteeId, input.ReplaceYear, input.ReplaceCount);
            else
                await FunctionManager.CallCreateCrisisCommitteeCodeProcess(crisisCommitteeId);

            return new EntityDto(crisisCommitteeId);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_CrisisCommittee_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _crisisCommitteeRepository.CountAsync(p => p.Id == input.Id));

            await _crisisCommitteeRepository.DeleteAsync(p => p.Id == input.Id);
            await _crisisCommitteeTeamRepository.DeleteAsync(p => p.CrisisCommitteeId == input.Id);
            await _crisisCommitteePlanRepository.DeleteAsync(p => p.CrisisCommitteeId == input.Id);
            await _crisisCommitteeActionRepository.DeleteAsync(p => p.CrisisCommitteeId == input.Id);
            await _crisisCommitteeMessageRepository.DeleteAsync(p => p.CrisisCommitteeId == input.Id);
            await _crisisCommitteeChannelRepository.DeleteAsync(p => p.CrisisCommitteeId == input.Id);
            await _crisisCommitteeSectorRepository.DeleteAsync(p => p.CrisisCommitteeId == input.Id);
            await _crisisCommitteeTaskRepository.DeleteAsync(p => p.CrisisCommitteeId == input.Id);
            await _crisisCommitteeAgreementRepository.DeleteAsync(p => p.CrisisCommitteeId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_CrisisCommittee)]
        public async Task<CrisisCommitteeGetDataDto> Get(NullableIdDto input)
        {
            var output = new CrisisCommitteeGetDataDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _crisisCommitteeRepository.CountAsync(p => p.Id == input.Id));

                var dbCrisisCommittee = _crisisCommitteeRepository
                    .GetAll()
                    .Include(p => p.InterventionPlan)
                    .Include(p => p.Person)
                    .Where(p => p.Id == input.Id.Value)
                    .First();

                output.CrisisCommittee = ObjectMapper.Map<CrisisCommitteeGetDto>(dbCrisisCommittee);

                output.CrisisCommittee.Teams = ObjectMapper.Map<List<CrisisCommitteeTeamRelationDto>>(await _crisisCommitteeTeamRepository
                    .GetAll()
                    .Include(p => p.AlertResponsible)
                    .Include(p => p.CrisisCommitteeJob)
                    .Where(p => p.CrisisCommitteeId == dbCrisisCommittee.Id)
                    .OrderBy(p => p.Index)
                    .ToListAsync());

                output.CrisisCommittee.Plans = ObjectMapper.Map<List<CrisisCommitteePlanRelationDto>>(await _crisisCommitteePlanRepository
                    .GetAll()
                    .OrderBy(p => p.Description)
                    .Where(p => p.CrisisCommitteeId == dbCrisisCommittee.Id)
                    .OrderBy(p => p.Index)
                    .ToListAsync());

                output.CrisisCommittee.Actions = ObjectMapper.Map<List<CrisisCommitteeActionRelationDto>>(await _crisisCommitteeActionRepository
                    .GetAll()
                    .OrderBy(p => p.Description)
                    .Where(p => p.CrisisCommitteeId == dbCrisisCommittee.Id)
                    .OrderBy(p => p.Index)
                    .ToListAsync());

                output.CrisisCommittee.Messages = ObjectMapper.Map<List<CrisisCommitteeMessageRelationDto>>(await _crisisCommitteeMessageRepository
                    .GetAll()
                    .OrderBy(p => p.Description)
                    .Where(p => p.CrisisCommitteeId == dbCrisisCommittee.Id)
                    .OrderBy(p => p.Index)
                    .ToListAsync());

                output.CrisisCommittee.Channels = ObjectMapper.Map<List<CrisisCommitteeChannelRelationDto>>(await _crisisCommitteeChannelRepository
                    .GetAll()
                    .OrderBy(p => p.Description)
                    .Where(p => p.CrisisCommitteeId == dbCrisisCommittee.Id)
                    .OrderBy(p => p.Index)
                    .ToListAsync());

                output.CrisisCommittee.Sectors = ObjectMapper.Map<List<CrisisCommitteeSectorRelationDto>>(await _crisisCommitteeSectorRepository
                    .GetAll()
                    .Include(p => p.DirectoryGovernment)
                    .ThenInclude(p => p.DirectoryGovernmentSector)
                    .Where(p => p.CrisisCommitteeId == dbCrisisCommittee.Id)
                    .OrderBy(p => p.Index)
                    .ToListAsync());

                output.CrisisCommittee.Tasks = ObjectMapper.Map<List<CrisisCommitteeTaskRelationDto>>(await _crisisCommitteeTaskRepository
                    .GetAll()
                    .OrderBy(p => p.Description)
                    .Where(p => p.CrisisCommitteeId == dbCrisisCommittee.Id)
                    .OrderBy(p => p.Index)
                    .ToListAsync());

                output.CrisisCommittee.Agreements = ObjectMapper.Map<List<CrisisCommitteeAgreementRelationDto>>(await _crisisCommitteeAgreementRepository
                    .GetAll()
                    .OrderBy(p => p.Description)
                    .Where(p => p.CrisisCommitteeId == dbCrisisCommittee.Id)
                    .OrderBy(p => p.Index)
                    .ToListAsync());

                var creatorUser = dbCrisisCommittee.CreatorUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbCrisisCommittee.CreatorUserId.Value)
                    .FirstOrDefault() : null;

                var editUser = dbCrisisCommittee.LastModifierUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbCrisisCommittee.LastModifierUserId.Value)                    
                    .FirstOrDefault() : null;

                output.CrisisCommittee.CreatorUser = creatorUser == null ? null : ObjectMapper.Map<CrisisCommitteeUserDto>(creatorUser);
                output.CrisisCommittee.EditUser = editUser == null ? null : ObjectMapper.Map<CrisisCommitteeUserDto>(editUser);
            }

            output.AlertResponsibles = ObjectMapper.Map<List<CrisisCommitteeAlertResponsibleRelationDto>>(await _alertResponsibleRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToListAsync());

            output.Jobs = ObjectMapper.Map<List<CrisisCommitteeJobRelationDto>>(await _crisisCommitteeJobRepository
                .GetAll()
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToListAsync());

            output.Persons = ObjectMapper.Map<List<CrisisCommitteePersonRelationDto>>(_personRepository
                .GetAll()
                .Where(p => p.Enabled && (p.Type == PersonType.Coordinator || p.Type == PersonType.Manager))
                .OrderBy(p => p.Name)
                .ToList());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_CrisisCommittee)]
        public async Task<PagedResultDto<CrisisCommitteeGetAllDto>> GetAll(CrisisCommitteeGetAllInputDto input)
        {
            var query = _crisisCommitteeRepository
                .GetAll()
                .Include(p => p.InterventionPlan)
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CrisisCommitteeTime >= input.StartTime.Value && p.CrisisCommitteeTime <= input.EndTime.Value)
                .LikeAllBidirectional(input.Code.SplitByLike(), nameof(CrisisCommittee.Count))
                .LikeAllBidirectional(input.CaseName.SplitByLike(), nameof(CrisisCommittee.CaseName))
                .LikeAllBidirectional(input
                    .InterventionPlanCode
                    .SplitByLike()
                    .Select(word => (Expression<Func<CrisisCommittee, bool>>)(expression =>
                        (expression.InterventionPlan == null || EF.Functions.Like(expression.InterventionPlan.Code, $"%{word}%"))))
                    .ToArray())
                .LikeAllBidirectional(input
                    .InterventionPlanCaseName
                    .SplitByLike()
                    .Select(word => (Expression<Func<CrisisCommittee, bool>>)(expression =>
                        (expression.InterventionPlan == null || EF.Functions.Like(expression.InterventionPlan.CaseName, $"%{word}%"))))
                    .ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<CrisisCommitteeGetAllDto>(count, ObjectMapper.Map<List<CrisisCommitteeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_ConflictTools_CrisisCommittee_Edit)]
        public async Task Update(CrisisCommitteeUpdateDto input)
        {
            VerifyCount(await _crisisCommitteeRepository.CountAsync(p => p.Id == input.Id));

            if (input.ReplaceCode)
            {
                if (input.ReplaceYear <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Año) de reemplazo es obligatorio");
                if (input.ReplaceCount <= 0)
                    throw new UserFriendlyException("Aviso", "El Código (Número) de reemplazo es obligatorio");
                if (await _crisisCommitteeRepository.CountAsync(p => p.Year == input.ReplaceYear && p.Count == input.ReplaceCount) > 0)
                    throw new UserFriendlyException("Aviso", "El código de reemplazo ya esta en uso. Verifique la información antes de continuar");
            }

            var crisisCommitteeId = await _crisisCommitteeRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                    input: ObjectMapper.Map(input, await _crisisCommitteeRepository.GetAsync(input.Id)),
                    interventionPlanId: input.InterventionPlan == null ? -1 : input.InterventionPlan.Id,
                    personId: input.Person == null ? -1 : input.Person.Id,
                    teams: input.Teams ?? new List<CrisisCommitteeTeamRelationDto>(),
                    plans: input.Plans ?? new List<CrisisCommitteePlanRelationDto>(),
                    actions: input.Actions ?? new List<CrisisCommitteeActionRelationDto>(),
                    messages: input.Messages ?? new List<CrisisCommitteeMessageRelationDto>(),
                    channels: input.Channels ?? new List<CrisisCommitteeChannelRelationDto>(),
                    sectors: input.Sectors ?? new List<CrisisCommitteeSectorRelationDto>(),
                    tasks: input.Tasks ?? new List<CrisisCommitteeTaskRelationDto>(),
                    agreements: input.Agreements ?? new List<CrisisCommitteeAgreementRelationDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ReplaceCode)
                await FunctionManager.CallCreateCrisisCommitteeCodeReplaceProcess(crisisCommitteeId, input.ReplaceYear, input.ReplaceCount);
        }

        private async Task<CrisisCommittee> ValidateEntity(
            CrisisCommittee input,
            int interventionPlanId,
            int personId,
            List<CrisisCommitteeTeamRelationDto> teams,
            List<CrisisCommitteePlanRelationDto> plans,
            List<CrisisCommitteeActionRelationDto> actions,
            List<CrisisCommitteeMessageRelationDto> messages,
            List<CrisisCommitteeChannelRelationDto> channels,
            List<CrisisCommitteeSectorRelationDto> sectors,
            List<CrisisCommitteeTaskRelationDto> tasks,
            List<CrisisCommitteeAgreementRelationDto> agreements)
        {
            input.CaseName.IsValidOrException(DefaultTitleMessage, "La denominación del plan de intervención es obligatoria");
            input.CaseName.VerifyTableColumn(
                CrisisCommitteeConsts.CaseNameMinLength,
                CrisisCommitteeConsts.CaseNameMaxLength,
                DefaultTitleMessage,
                $"La denominación del comité de crisis no debe exceder los {CrisisCommitteeConsts.CaseNameMaxLength} caracteres");

            if (personId > 0)
            {
                if (await _personRepository.CountAsync(p => p.Id == personId) == 0)
                    throw new UserFriendlyException("Aviso", "La persona que elabora el comité de crisis ya no existe o fue eliminado. Verifique la información antes de continuar");

                var person = await _personRepository.GetAsync(personId);

                input.Person = person;
                input.PersonId = person.Id;
            }
            else
            {
                input.Person = null;
                input.PersonId = null;
            }

            if (await _interventionPlanRepository.CountAsync(p => p.Id == interventionPlanId) == 0)
                throw new UserFriendlyException("Aviso", "El plan de intervención ya no existe o fue eliminado. Verifique la información antes de continuar");

            var interventionPlan = await _interventionPlanRepository.GetAsync(interventionPlanId);

            input.InterventionPlan = interventionPlan;
            input.InterventionPlanId = interventionPlan.Id;

            input.Teams = new List<CrisisCommitteeTeam>();
            input.Plans = new List<CrisisCommitteePlan>();
            input.Actions = new List<CrisisCommitteeAction>();
            input.Messages = new List<CrisisCommitteeMessage>();
            input.Channels = new List<CrisisCommitteeChannel>();
            input.Sectors = new List<CrisisCommitteeSector>();
            input.Tasks = new List<CrisisCommitteeTask>();
            input.Agreements = new List<CrisisCommitteeAgreement>();

            var index = 0;

            foreach (var team in teams)
            {
                if (team.Remove)
                {
                    if (team.Id > 0 && input.Id > 0 && await _crisisCommitteeTeamRepository.CountAsync(p => p.Id == team.Id && p.CrisisCommitteeId == input.Id) > 0)
                    {
                        await _crisisCommitteeTeamRepository.DeleteAsync(team.Id);
                    }
                }
                else
                {
                    var dbAlertResponsible = _alertResponsibleRepository
                            .GetAll()
                            .Where(p => p.Id == team.AlertResponsible.Id)
                            .FirstOrDefault();

                    if (dbAlertResponsible == null)
                        throw new UserFriendlyException("Aviso", $"La Secretaría/Subsecretaría {team.AlertResponsible.Name} de los integrantes ya no existe o fue eliminado. Verifique la información antes de continuar");

                    var dbCrisisCommitteeJob = _crisisCommitteeJobRepository
                        .GetAll()
                        .Where(p => p.Id == team.CrisisCommitteeJob.Id)
                        .FirstOrDefault();

                    if (dbCrisisCommitteeJob == null)
                        throw new UserFriendlyException("Aviso", $"El cargo {team.CrisisCommitteeJob.Name} de los integrantes ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (dbCrisisCommitteeJob.ShowDescription)
                    {
                        team.Job.IsValidOrException(DefaultTitleMessage, $"La descripción del cargo {team.CrisisCommitteeJob.Name} de los integrantes es obligatoria");
                        team.Job.VerifyTableColumn(CrisisCommitteeTeamConsts.JobMinLength,
                            CrisisCommitteeTeamConsts.JobMaxLength,
                            DefaultTitleMessage,
                            $"La descripción del cargo {team.CrisisCommitteeJob.Name} de los integrantes no debe exceder los " +
                            $"{CrisisCommitteeTeamConsts.JobMaxLength} caracteres");
                    }

                    team.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de todos los integrantes es obligatorio");
                    team.Name.VerifyTableColumn(CrisisCommitteeTeamConsts.NameMinLength,
                        CrisisCommitteeTeamConsts.NameMaxLength,
                        DefaultTitleMessage,
                        $"El nombre {team.Name} del integrante no debe exceder los " +
                        $"{CrisisCommitteeTeamConsts.NameMaxLength} caracteres");

                    team.Surname.IsValidOrException(DefaultTitleMessage, $"El apellido paterno de todos los registros de integrantes son obligatorios");
                    team.Surname.VerifyTableColumn(CrisisCommitteeTeamConsts.SurnameMinLength,
                        CrisisCommitteeTeamConsts.SurnameMaxLength,
                        DefaultTitleMessage,
                        $"El apellido paterno del integrante {team.Name} no debe exceder los " +
                        $"{CrisisCommitteeTeamConsts.SurnameMaxLength} caracteres");

                    team.SecondSurname.VerifyTableColumn(CrisisCommitteeTeamConsts.SecondSurnameMinLength,
                        CrisisCommitteeTeamConsts.SecondSurnameMaxLength,
                        DefaultTitleMessage,
                        $"El apellido materno del integrante {team.Name} no debe exceder los " +
                        $"{CrisisCommitteeTeamConsts.SecondSurnameMaxLength} caracteres");

                    if (team.Id > 0)
                    {
                        if (await _crisisCommitteeTeamRepository.CountAsync(p => p.Id == team.Id && p.CrisisCommitteeId == input.Id) > 0)
                        {
                            var dbCrisisCommitteePlanTeam = await _crisisCommitteeTeamRepository.GetAsync(team.Id);

                            dbCrisisCommitteePlanTeam.Index = index;
                            dbCrisisCommitteePlanTeam.Document = (team.Document ?? "").Trim().ToUpper();
                            dbCrisisCommitteePlanTeam.Name = (team.Name ?? "").Trim().ToUpper();
                            dbCrisisCommitteePlanTeam.Surname = (team.Surname ?? "").Trim().ToUpper();
                            dbCrisisCommitteePlanTeam.SecondSurname = (team.SecondSurname ?? "").Trim().ToUpper();
                            dbCrisisCommitteePlanTeam.AlertResponsibleId = dbAlertResponsible.Id;
                            dbCrisisCommitteePlanTeam.AlertResponsible = dbAlertResponsible;
                            dbCrisisCommitteePlanTeam.CrisisCommitteeJobId = dbCrisisCommitteeJob.Id;
                            dbCrisisCommitteePlanTeam.CrisisCommitteeJob = dbCrisisCommitteeJob;
                            dbCrisisCommitteePlanTeam.Job = (team.Job ?? "").Trim().ToUpper();

                            await _crisisCommitteeTeamRepository.UpdateAsync(dbCrisisCommitteePlanTeam);
                        }
                    }
                    else
                    {
                        input.Teams.Add(new CrisisCommitteeTeam()
                        {
                            Index = index,
                            Document = (team.Document ?? "").Trim().ToUpper(),
                            Name = (team.Name ?? "").Trim().ToUpper(),
                            Surname = (team.Surname ?? "").Trim().ToUpper(),
                            SecondSurname = (team.SecondSurname ?? "").Trim().ToUpper(),
                            AlertResponsibleId = dbAlertResponsible.Id,
                            AlertResponsible = dbAlertResponsible,
                            CrisisCommitteeJobId = dbCrisisCommitteeJob.Id,
                            CrisisCommitteeJob = dbCrisisCommitteeJob,
                            Job = (team.Job ?? "").Trim().ToUpper()
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var plan in plans)
            {
                if (plan.Remove)
                {
                    if (plan.Id > 0 && input.Id > 0 && await _crisisCommitteePlanRepository.CountAsync(p => p.Id == plan.Id && p.CrisisCommitteeId == input.Id) > 0)
                    {
                        await _crisisCommitteePlanRepository.DeleteAsync(plan.Id);
                    }
                }
                else
                {
                    plan.Description.IsValidOrException("Aviso", "La descripción de los planes de contingencia son obligatiorias");
                    plan.Description.VerifyTableColumn(CrisisCommitteePlanConsts.DescriptionMinLength,
                        CrisisCommitteePlanConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del plan de contingencia {plan.Description} no debe exceder los " +
                        $"{CrisisCommitteePlanConsts.DescriptionMaxLength} caracteres");

                    if (plan.Id > 0)
                    {
                        if (await _crisisCommitteePlanRepository.CountAsync(p => p.Id == plan.Id && p.CrisisCommitteeId == input.Id) > 0)
                        {
                            var dbPlan = await _crisisCommitteePlanRepository.GetAsync(plan.Id);

                            dbPlan.Index = index;
                            dbPlan.Description = plan.Description;

                            await _crisisCommitteePlanRepository.UpdateAsync(dbPlan);
                        }
                    }
                    else
                    {
                        input.Plans.Add(new CrisisCommitteePlan()
                        {
                            Index = index,
                            Description = plan.Description
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var action in actions)
            {
                if (action.Remove)
                {
                    if (action.Id > 0 && input.Id > 0 && await _crisisCommitteeActionRepository.CountAsync(p => p.Id == action.Id && p.CrisisCommitteeId == input.Id) > 0)
                    {
                        await _crisisCommitteeActionRepository.DeleteAsync(action.Id);
                    }
                }
                else
                {
                    action.Description.IsValidOrException("Aviso", "La descripción de las acciones son obligatiorias");
                    action.Description.VerifyTableColumn(CrisisCommitteeActionConsts.DescriptionMinLength,
                        CrisisCommitteeActionConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la acción {action.Description} no debe exceder los " +
                        $"{CrisisCommitteeActionConsts.DescriptionMaxLength} caracteres");

                    if (action.Id > 0)
                    {
                        if (await _crisisCommitteeActionRepository.CountAsync(p => p.Id == action.Id && p.CrisisCommitteeId == input.Id) > 0)
                        {
                            var dbAction = await _crisisCommitteeActionRepository.GetAsync(action.Id);

                            dbAction.Index = index;
                            dbAction.Description = action.Description;

                            await _crisisCommitteeActionRepository.UpdateAsync(dbAction);
                        }
                    }
                    else
                    {
                        input.Actions.Add(new CrisisCommitteeAction()
                        {
                            Index = index,
                            Description = action.Description
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var message in messages)
            {
                if (message.Remove)
                {
                    if (message.Id > 0 && input.Id > 0 && await _crisisCommitteeMessageRepository.CountAsync(p => p.Id == message.Id && p.CrisisCommitteeId == input.Id) > 0)
                    {
                        await _crisisCommitteeMessageRepository.DeleteAsync(message.Id);
                    }
                }
                else
                {
                    message.Description.IsValidOrException("Aviso", "La descripción de los mensajes centrales son obligatiorias");
                    message.Description.VerifyTableColumn(CrisisCommitteeMessageConsts.DescriptionMinLength,
                        CrisisCommitteeMessageConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del mensaje central {message.Description} no debe exceder los " +
                        $"{CrisisCommitteeMessageConsts.DescriptionMaxLength} caracteres");

                    if (message.Id > 0)
                    {
                        if (await _crisisCommitteeMessageRepository.CountAsync(p => p.Id == message.Id && p.CrisisCommitteeId == input.Id) > 0)
                        {
                            var dbMessage = await _crisisCommitteeMessageRepository.GetAsync(message.Id);

                            dbMessage.Index = index;
                            dbMessage.Description = message.Description;

                            await _crisisCommitteeMessageRepository.UpdateAsync(dbMessage);
                        }
                    }
                    else
                    {
                        input.Messages.Add(new CrisisCommitteeMessage()
                        {
                            Index = index,
                            Description = message.Description
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var channel in channels)
            {
                if (channel.Remove)
                {
                    if (channel.Id > 0 && input.Id > 0 && await _crisisCommitteeChannelRepository.CountAsync(p => p.Id == channel.Id && p.CrisisCommitteeId == input.Id) > 0)
                    {
                        await _crisisCommitteeChannelRepository.DeleteAsync(channel.Id);
                    }
                }
                else
                {
                    channel.Description.IsValidOrException("Aviso", "La descripción de los canales de comunicación son obligatorias");
                    channel.Description.VerifyTableColumn(CrisisCommitteeChannelConsts.DescriptionMinLength,
                        CrisisCommitteeChannelConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción del canal de comunicación {channel.Description} no debe exceder los " +
                        $"{CrisisCommitteeChannelConsts.DescriptionMaxLength} caracteres");

                    if (channel.Id > 0)
                    {
                        if (await _crisisCommitteeChannelRepository.CountAsync(p => p.Id == channel.Id && p.CrisisCommitteeId == input.Id) > 0)
                        {
                            var dbChannel = await _crisisCommitteeChannelRepository.GetAsync(channel.Id);

                            dbChannel.Index = index;
                            dbChannel.Description = channel.Description;

                            await _crisisCommitteeChannelRepository.UpdateAsync(dbChannel);
                        }
                    }
                    else
                    {
                        input.Channels.Add(new CrisisCommitteeChannel()
                        {
                            Index = index,
                            Description = channel.Description
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var sector in sectors)
            {
                if (sector.Remove)
                {
                    if (sector.Id > 0 && input.Id > 0 && await _crisisCommitteeSectorRepository.CountAsync(p => p.Id == sector.Id && p.CrisisCommitteeId == input.Id) > 0)
                    {
                        await _crisisCommitteeSectorRepository.DeleteAsync(sector.Id);
                    }
                }
                else
                {
                    var dbDirectoryGovernment = _directoryGovernmentRepository
                        .GetAll()
                        .Where(p => p.Id == sector.DirectoryGovernment.Id)
                        .FirstOrDefault();

                    if (dbDirectoryGovernment == null)
                        throw new UserFriendlyException("Aviso", $"El sector {sector.DirectoryGovernment.Name} ya no existe o fue eliminado. Verifique la información antes de continuar");

                    if (sector.Id > 0)
                    {
                        if (await _crisisCommitteeSectorRepository.CountAsync(p => p.Id == sector.Id && p.CrisisCommitteeId == input.Id) > 0)
                        {
                            var dbSector = await _crisisCommitteeSectorRepository.GetAsync(sector.Id);

                            dbSector.Index = index;
                            dbSector.DirectoryGovernment = dbDirectoryGovernment;
                            dbSector.DirectoryGovernmentId = dbDirectoryGovernment.Id;

                            await _crisisCommitteeSectorRepository.UpdateAsync(dbSector);
                        }
                    }
                    else
                    {
                        input.Sectors.Add(new CrisisCommitteeSector()
                        {
                            Index = index,
                            DirectoryGovernment = dbDirectoryGovernment,
                            DirectoryGovernmentId = dbDirectoryGovernment.Id
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var agreement in agreements)
            {
                if (agreement.Remove)
                {
                    if (agreement.Id > 0 && input.Id > 0 && await _crisisCommitteeAgreementRepository.CountAsync(p => p.Id == agreement.Id && p.CrisisCommitteeId == input.Id) > 0)
                    {
                        await _crisisCommitteeAgreementRepository.DeleteAsync(agreement.Id);
                    }
                }
                else
                {
                    agreement.Description.IsValidOrException("Aviso", "La descripción de las actividades críticas son obligatiorias");
                    agreement.Description.VerifyTableColumn(CrisisCommitteeAgreementConsts.DescriptionMinLength,
                        CrisisCommitteeAgreementConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la actividad crítica {agreement.Description} no debe exceder los " +
                        $"{CrisisCommitteeAgreementConsts.DescriptionMaxLength} caracteres");

                    if (agreement.Id > 0)
                    {
                        if (await _crisisCommitteeAgreementRepository.CountAsync(p => p.Id == agreement.Id && p.CrisisCommitteeId == input.Id) > 0)
                        {
                            var dbAgreement = await _crisisCommitteeAgreementRepository.GetAsync(agreement.Id);

                            dbAgreement.Index = index;
                            dbAgreement.Description = agreement.Description;

                            await _crisisCommitteeAgreementRepository.UpdateAsync(dbAgreement);
                        }
                    }
                    else
                    {
                        input.Agreements.Add(new CrisisCommitteeAgreement()
                        {
                            Index = index,
                            Description = agreement.Description
                        });
                    }

                    index++;
                }
            }

            index = 0;

            foreach (var task in tasks)
            {
                if (task.Remove)
                {
                    if (task.Id > 0 && input.Id > 0 && await _crisisCommitteeTaskRepository.CountAsync(p => p.Id == task.Id && p.CrisisCommitteeId == input.Id) > 0)
                    {
                        await _crisisCommitteeTaskRepository.DeleteAsync(task.Id);
                    }
                }
                else
                {
                    task.Description.IsValidOrException("Aviso", "La descripción de las tareas son obligatiorias");
                    task.Description.VerifyTableColumn(CrisisCommitteeTaskConsts.DescriptionMinLength,
                        CrisisCommitteeTaskConsts.DescriptionMaxLength,
                        DefaultTitleMessage,
                        $"La descripción de la tarea {task.Description} no debe exceder los " +
                        $"{CrisisCommitteeTaskConsts.DescriptionMaxLength} caracteres");

                    if (task.Id > 0)
                    {
                        if (await _crisisCommitteeTaskRepository.CountAsync(p => p.Id == task.Id && p.CrisisCommitteeId == input.Id) > 0)
                        {
                            var dbTask = await _crisisCommitteeTaskRepository.GetAsync(task.Id);

                            dbTask.Index = index;
                            dbTask.Description = task.Description;

                            await _crisisCommitteeTaskRepository.UpdateAsync(dbTask);
                        }
                    }
                    else
                    {
                        input.Tasks.Add(new CrisisCommitteeTask()
                        {
                            Index = index,
                            Description = task.Description
                        });
                    }

                    index++;
                }
            }

            return input;
        }
    }
}
