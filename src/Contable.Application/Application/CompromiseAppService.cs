using Abp.Domain.Repositories;
using Abp.UI;
using Contable.Application.Compromises;
using Contable.Application.Compromises.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contable.Application.Extensions;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Linq.Extensions;
using Contable.Dto;
using Contable.Application.ResponsibleActors.Dto;
using Abp.Collections.Extensions;
using Contable.Application.Parameters.Dto;
using Abp.Authorization;
using Contable.Authorization;
using Contable.Application.Exporting;
using Contable.Application.External;
using System;
using Contable.Authorization.Users;
using Microsoft.AspNetCore.Mvc;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_Compromise)]
    public class CompromiseAppService : ContableAppServiceBase, ICompromiseAppService
    {
        private readonly IRepository<Compromise, long> _compromiseRepository;
        private readonly IRepository<Record, long> _recordRepository;
        private readonly IRepository<Parameter> _parameterRepository;
        private readonly IRepository<SocialConflictLocation> _socialConflictLocationRepository;
        private readonly IRepository<CompromiseLocation> _compromiseLocationRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<ResponsibleSubActor> _responsibleSubActorRepository;
        private readonly IRepository<ResponsibleActor> _responsibleActorRepository;
        private readonly IRepository<Situation, long> _situationRepository;
        private readonly IRepository<SituationResource, long> _situationResourceRepository;
        private readonly ICompromiseExcelExporter _compromiseExcelExporter;
        private readonly IRepository<TaskManagement, long> _taskRepository;
        private readonly IRepository<Comment, long> _commentRepository;
        private readonly IPipMefAppService _pipMefAppService;
        private readonly IRepository<CompromiseInvolved> _compromiseInvolvedRepository;
        private readonly IRepository<PIPMEF, long> _pipmefRepository;
        private readonly IRepository<CompromiseTimeLine> _compromiseTimeLineRepository;
        private readonly IRepository<ResponsibleType> _responsibleTypeRepository;
        private readonly IRepository<ResponsibleSubType> _responsibleSubTypeRepository;
        private readonly IRepository<CompromiseLabel> _compromiseLabelRepository;
        private readonly IRepository<CompromiseResponsible> _compromiseResponsibleRepository;
        private readonly IRepository<CompromiseState> _compromiseStateRepository;
        private readonly IRepository<CompromiseSubState> _compromiseSubStateRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;

        public CompromiseAppService(
            IRepository<Compromise, long> compromiseRepository,
            IRepository<Record, long> recordRepository,
            IRepository<Parameter> parameterRepository,
            IRepository<SocialConflictLocation> socialConflictLocationRepository,
            IRepository<CompromiseLocation> compromiseLocationRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<ResponsibleSubActor> responsibleSubActorRepository,
            IRepository<ResponsibleActor> responsibleActorRepository,
            IRepository<Situation, long> situationRepository,
            IRepository<SituationResource, long> situationResourceRepository,
            IRepository<TaskManagement, long> taskRepository,
            IRepository<Comment, long> commentRepository,
            IRepository<CompromiseInvolved> compromiseInvolvedRepository,
            ICompromiseExcelExporter compromiseExcelExporter,
            IPipMefAppService pipMefAppService,
            IRepository<PIPMEF, long> pipmefRepository,
            IRepository<CompromiseTimeLine> compromiseTimeLineRepository,
            IRepository<ResponsibleType> responsibleTypeRepository,
            IRepository<ResponsibleSubType> responsibleSubTypeRepository,
            IRepository<CompromiseLabel> compromiseLabelRepository,
            IRepository<CompromiseResponsible> compromiseResponsibleRepository,
            IRepository<CompromiseState> compromiseStateRepository,
            IRepository<CompromiseSubState> compromiseSubStateRepository,
            IRepository<User, long> userRepository,
            IRepository<SocialConflict> socialConflictRepository)
        {
            _compromiseRepository = compromiseRepository;
            _recordRepository = recordRepository;
            _parameterRepository = parameterRepository;
            _socialConflictLocationRepository = socialConflictLocationRepository;
            _compromiseLocationRepository = compromiseLocationRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _responsibleSubActorRepository = responsibleSubActorRepository;
            _responsibleActorRepository = responsibleActorRepository;
            _situationRepository = situationRepository;
            _situationResourceRepository = situationResourceRepository;
            _taskRepository = taskRepository;
            _compromiseRepository = compromiseRepository;
            _commentRepository = commentRepository;
            _compromiseExcelExporter = compromiseExcelExporter;
            _pipMefAppService = pipMefAppService;
            _compromiseInvolvedRepository = compromiseInvolvedRepository;
            _pipmefRepository = pipmefRepository;
            _compromiseTimeLineRepository = compromiseTimeLineRepository;
            _responsibleTypeRepository = responsibleTypeRepository;
            _responsibleSubTypeRepository = responsibleSubTypeRepository;
            _compromiseLabelRepository = compromiseLabelRepository;
            _compromiseResponsibleRepository = compromiseResponsibleRepository;
            _compromiseStateRepository = compromiseStateRepository;
            _compromiseSubStateRepository = compromiseSubStateRepository;
            _userRepository = userRepository;
            _socialConflictRepository = socialConflictRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Compromise_Create)]
        public async Task<EntityDto<long>> Create(CompromiseCreateDto input)
        {
            var compromiseId = await _compromiseRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                compromise: ObjectMapper.Map<Compromise>(input),
                record: input.Record ?? new EntityDto<long>(),
                state: input.Status ?? new EntityDto(),
                label: input.CompromiseLabel ?? new EntityDto(),
                compromiseState: input.CompromiseState ?? new EntityDto(),
                compromiseSubState: input.CompromiseSubState ?? new EntityDto(),
                compromiseLocations: input.CompromiseLocations ?? new List<CompromiseLocationDto>(),
                pipmef: input.PIPMEF ?? new CompromiseUpdatePIPMEFDto(),
                responsibleActor: input.ResponsibleActor ?? new EntityDto(),
                responsibleSubActor: input.ResponsibleSubActor ?? new EntityDto(),
                involved: input.Involved ?? new List<CompromiseInvolvedDto>(),
                situations: new List<CompromiseSituationDto>(),
                uploads: input.Uploads ?? new List<CompromiseUploadResourceDto>(),
                timelines: input.Timelines ?? new List<CompromiseTimeLineDto>(),
                responsibles: input.Responsibles ?? new List<CompromiseResponsibleDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();

            //await FunctionManager.CallCreateCompromiseCodeProcess(compromiseId);

            return new EntityDto<long>(compromiseId);
        }

        private string GenerateCode(string codeCompromise, string codeSocialConflict, bool isCreate)
        {
            
            string[] codes = codeCompromise.Split('-');
            // Obtener la última parte
            string lastCode = codes[^1].Trim();
            // Extraer el número del código actual
            // Ignorar el primer carácter "C"
            string numberString = lastCode.Substring(1); 
            int counter = int.Parse(numberString);
            // Incrementar el número
            if (isCreate) counter++;
            return codeSocialConflict+" - C" + counter.ToString("000");
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Compromise_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            VerifyCount(await _compromiseRepository.CountAsync(p => p.Id == input.Id));

            await _compromiseRepository.DeleteAsync(input.Id);
            await _compromiseLocationRepository.DeleteAsync(p => p.Compromise.Id == input.Id);
            await _compromiseTimeLineRepository.DeleteAsync(p => p.CompromiseId == input.Id);
            await _situationRepository.DeleteAsync(p => p.Compromise.Id == input.Id);
            await _situationResourceRepository.DeleteAsync(p => p.Situation.Compromise.Id == input.Id);
            await _commentRepository.DeleteAsync(p => p.TaskManagement.Compromise.Id == input.Id);
            await _taskRepository.DeleteAsync(p => p.Compromise.Id == input.Id);
            await _compromiseResponsibleRepository.DeleteAsync(p => p.CompromiseId == input.Id);
        }

        [HttpPost]
        [AbpAuthorize(AppPermissions.Pages_Application_Compromise_Delete)]
        public async Task DeleteList(EntityDto<long>[] inputs)
        {
            foreach (var input in inputs)
            {
                //var tasks = new List<Task>();

                VerifyCount(await _compromiseRepository.CountAsync(p => p.Id == input.Id));

                await _compromiseRepository.DeleteAsync(input.Id);
                await _compromiseLocationRepository.DeleteAsync(p => p.Compromise.Id == input.Id);
                await _compromiseTimeLineRepository.DeleteAsync(p => p.CompromiseId == input.Id);
                await _situationRepository.DeleteAsync(p => p.Compromise.Id == input.Id);
                await _situationResourceRepository.DeleteAsync(p => p.Situation.Compromise.Id == input.Id);
                await _commentRepository.DeleteAsync(p => p.TaskManagement.Compromise.Id == input.Id);
                await _taskRepository.DeleteAsync(p => p.Compromise.Id == input.Id);
                await _compromiseResponsibleRepository.DeleteAsync(p => p.CompromiseId == input.Id);

                //Task.WaitAll(tasks.ToArray());
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Compromise)]
        public async Task<CompromiseGetDataDto> Get(NullableIdDto input)
        {
            var output = new CompromiseGetDataDto
            {
                Compromise = new CompromiseGetDto()
            };

            if (input.Id.HasValue)
            {
                VerifyCount(await _compromiseRepository.CountAsync(p => p.Id == input.Id));

                var compromise = _compromiseRepository
                    .GetAll()
                    .Include(p => p.CompromiseState)
                    .Include(p => p.CompromiseSubState)
                    .Include(p => p.CompromiseLabel)
                    .Include(p => p.ResponsibleActor)
                        .ThenInclude(p => p.ResponsibleType)
                    .Include(p => p.ResponsibleActor)
                        .ThenInclude(p => p.ResponsibleSubType)
                    .Include(p => p.ResponsibleSubActor)
                    .Include(p => p.Record)
                        .ThenInclude(p => p.SocialConflict)
                    .Include(p => p.Situations)
                        .ThenInclude(p => p.Resource)
                    .Include(p => p.Status)
                    .Include(p => p.PIPMEF)
                        .ThenInclude(p => p.PIPMilestone)
                    .Include(p => p.PIPMEF)
                        .ThenInclude(p => p.PIPPhase)
                    .Include(p => p.Timelines)
                        .ThenInclude(p => p.Phase)
                    .Include(p => p.Timelines)
                        .ThenInclude(p => p.Milestone)
                    .Where(p => p.Id == input.Id)
                    .First();

                output.Compromise = ObjectMapper.Map<CompromiseGetDto>(compromise);

                output.Compromise.CompromiseLocations = ObjectMapper.Map<List<CompromiseLocationDto>>(_compromiseLocationRepository
                    .GetAll()
                    .Include(p => p.SocialConflictLocation)
                        .ThenInclude(p => p.TerritorialUnit)
                    .Include(p => p.SocialConflictLocation)
                        .ThenInclude(p => p.Department)
                    .Include(p => p.SocialConflictLocation)
                        .ThenInclude(p => p.Province)
                    .Include(p => p.SocialConflictLocation)
                        .ThenInclude(p => p.District)
                    .Where(p => p.Compromise.Id == compromise.Id)
                    .ToList());

                output.Compromise.Responsibles = ObjectMapper.Map<List<CompromiseResponsibleDto>>(await _compromiseResponsibleRepository
                    .GetAll()
                    .Include(p => p.ResponsibleActor)
                    .ThenInclude(p => p.ResponsibleType)
                    .Include(p => p.ResponsibleActor)
                    .ThenInclude(p => p.ResponsibleSubType)
                    .Include(p => p.ResponsibleSubActor)
                    .Where(p => p.CompromiseId == compromise.Id)
                    .ToListAsync());

                output.Compromise.Involved = ObjectMapper.Map<List<CompromiseInvolvedDto>>(await _compromiseInvolvedRepository
                    .GetAll()
                    .Include(p => p.ResponsibleActor)
                    .ThenInclude(p => p.ResponsibleType)
                    .Include(p => p.ResponsibleActor)
                    .ThenInclude(p => p.ResponsibleSubType)
                    .Include(p => p.ResponsibleSubActor)
                    .Where(p => p.CompromiseId == compromise.Id)
                    .ToListAsync());

                var socialConflictLocations = await _socialConflictLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Where(e => e.SocialConflict.Id == compromise.Record.SocialConflict.Id)
                    .ToListAsync();

                foreach (var location in socialConflictLocations)
                {
                    var index = output.Compromise.CompromiseLocations.FindIndex(p => p.SocialConflictLocation.Id == location.Id);

                    if (index != -1)
                    {
                        output.Compromise.CompromiseLocations[index].Check = true;
                    }
                    else
                    {
                        output.Compromise.CompromiseLocations.Add(new CompromiseLocationDto()
                        {
                            Id = 0,
                            Check = false,
                            SocialConflictLocation = ObjectMapper.Map<CompromiseSocialConflictLocationDto>(location)
                        });
                    }
                }

                if (output.Compromise.IsPriority)
                {
                    var comments = await _commentRepository.GetAll()
                        .Include(p => p.TaskManagement)
                        .Where(p => p.TaskManagement.Compromise.Id == compromise.Id)
                        .ToListAsync();

                    foreach (var item in comments)
                    {
                        output.Compromise.Situations.Add(new CompromiseSituationDto()
                        {
                            Id = item.Id,
                            Description = item.TaskManagement.Title + ": " + item.Description,
                            CreationTime = item.CreationTime
                        });
                    }
                }

                output.Compromise.Situations = output.Compromise.Situations.OrderByDescending(p => p.CreationTime).ToList();

                var creatorUser = compromise.CreatorUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == compromise.CreatorUserId.Value)
                    .FirstOrDefault() : null;

                var editUser = compromise.LastModifierUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == compromise.LastModifierUserId.Value)
                    .FirstOrDefault() : null;

                output.Compromise.CreatorUser = creatorUser == null ? null : ObjectMapper.Map<CompromiseUserDto>(creatorUser);
                output.Compromise.EditUser = editUser == null ? null : ObjectMapper.Map<CompromiseUserDto>(editUser);
            }

            //Get PIP Phase Values
            output.PIPPhases = ObjectMapper.Map<List<ParameterDto>>(await FunctionManager.GetParameterValues(CompromiseConsts.ParameterCategoryPIPPhase));
            //Get PIP Stage Values           
            output.PIPMilestones = ObjectMapper.Map<List<ParameterDto>>(await FunctionManager.GetParameterValues(CompromiseConsts.ParameterCategoryPIPMilestone));
            //Get Status Value
            output.Statuses = ObjectMapper.Map<List<ParameterDto>>(await FunctionManager.GetParameterValues(CompromiseConsts.ParameterCategoryStatus)).OrderBy(p => p.Value).ToList();
            //Get ResponsibleActors
            output.ResponsibleActors = ObjectMapper.Map<List<ResponsibleActorGetAllDto>>(await _responsibleActorRepository
                .GetAll()
                .Include(p => p.ResponsibleType)
                .Include(p => p.ResponsibleSubType)
                .Include(p => p.ResponsibleSubActors)
                .ToListAsync());

            foreach (var responsibleActor in output.ResponsibleActors)
                responsibleActor.ResponsibleSubActors = responsibleActor.ResponsibleSubActors.OrderBy(p => p.Name).ToList();

            output.ResponsibleTypes = ObjectMapper.Map<List<CompromiseResponsibleTypeDto>>(await _responsibleTypeRepository
                .GetAll()
                .Where(p => p.Enabled)
                .Include(p => p.SubTypes)
                .OrderBy(p => p.Name)
                .ToListAsync());

            foreach (var responsibleType in output.ResponsibleTypes)
            {
                responsibleType.SubTypes = responsibleType.SubTypes.Where(p => p.Enabled).OrderBy(p => p.Name).ToList();
            }

            output.Labels = ObjectMapper.Map<List<CompromiseLabelLocationDto>>(await _compromiseLabelRepository
                .GetAll()
                .Where(p => p.Enabled)
                .ToListAsync());



            var states = await _compromiseStateRepository
                .GetAll()
                .Include(p => p.SubStates)
                .Where(p => p.Enabled)
                .OrderBy(p => p.Name)
                .ToListAsync();

            output.States = new List<CompromiseStateDto>();

            foreach (var state in states)
            {
                var newState = ObjectMapper.Map<CompromiseStateDto>(state);
                newState.SubStates = ObjectMapper.Map<List<CompromiseSubStateDto>>(state.SubStates.Where(p => p.Enabled).OrderBy(p => p.Name).ToList());

                output.States.Add(newState);
            }

            //output.TerritorialUnits = ObjectMapper.Map<List<CompromiseTerritorialUnitDto>>(_territorialUnitRepository
            //        .GetAll()
            //        .ToList());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Compromise)]
        public async Task<PagedResultDto<CompromiseGetAllDto>> GetAll(CompromiseGetAllInputDto input)
        {
            var query = _compromiseRepository
                .GetAll()
                .Include(p => p.Status)
                .Include(p => p.Record)
                    .ThenInclude(p => p.SocialConflict)
                .Include(p => p.CompromiseLocations)
                .ThenInclude(p => p.SocialConflictLocation)
                .ThenInclude(p => p.TerritorialUnit)
                .Include(p => p.CompromiseLabel)
                .Where(p => p.Record.IsDeleted == false)
                .WhereIf(input.Type.HasValue && input.Type > 0, p => p.Type == (input.Type.Value == 1 ? CompromiseType.PIP : CompromiseType.Activity))
                .WhereIf(input.CodeSocialConflict.IsValid(), p => p.Record.SocialConflict.Code.Contains(input.CodeSocialConflict))
                .WhereIf(input.CodeRecord.IsValid(), p => p.Record.Code.Contains(input.CodeRecord))
                .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0,
                    p => p.CompromiseLocations.Any(p => p.SocialConflictLocation.TerritorialUnit.Id == input.TerritorialUnitId))
                .WhereIf(input.FilterByDate, p => p.CreationTime >= input.StartTime && p.CreationTime <= input.EndTime)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(Compromise.Filter));

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var result = new List<CompromiseGetAllDto>();

            foreach (var compromise in output)
            {
                var compromiseItem = ObjectMapper.Map<CompromiseGetAllDto>(compromise);
                compromiseItem.TerritorialUnits = compromise.CompromiseLocations.Select(p => p.SocialConflictLocation.TerritorialUnit.Name).Distinct().JoinAsString(",");
                result.Add(compromiseItem);
            }

            return new PagedResultDto<CompromiseGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Compromise_Edit)]
        public async Task Update(CompromiseUpdateDto input)
        {
            VerifyCount(await _compromiseRepository.CountAsync(p => p.Id == input.Id));

            var dbCompromise = await _compromiseRepository.GetAsync(input.Id);
            dbCompromise.DueDate = input.DueDate;
            dbCompromise.DeadLine = input.Deadline;

            var compromiseId = await _compromiseRepository.InsertOrUpdateAndGetIdAsync(await ValidateEntity(
                compromise: ObjectMapper.Map(input, dbCompromise),
                record: input.Record ?? new EntityDto<long>(),
                state: input.Status ?? new EntityDto(),
                label: input.CompromiseLabel ?? new EntityDto(),
                compromiseState: input.CompromiseState ?? new EntityDto(),
                compromiseSubState: input.CompromiseSubState ?? new EntityDto(),
                pipmef: input.PIPMEF ?? new CompromiseUpdatePIPMEFDto(),
                compromiseLocations: input.CompromiseLocations ?? new List<CompromiseLocationDto>(),
                responsibleActor: input.ResponsibleActor ?? new EntityDto(),
                responsibleSubActor: input.ResponsibleSubActor ?? new EntityDto(),
                involved: input.Involved ?? new List<CompromiseInvolvedDto>(),
                situations: input.Situations ?? new List<CompromiseSituationDto>(),
                uploads: input.Uploads ?? new List<CompromiseUploadResourceDto>(),
                timelines: input.Timelines ?? new List<CompromiseTimeLineDto>(),
                responsibles: input.Responsibles ?? new List<CompromiseResponsibleDto>()));

            await CurrentUnitOfWork.SaveChangesAsync();


            await FunctionManager.CallUpdateCompromiseCodeProcess(compromiseId);


            var compromise = _compromiseRepository
                .GetAll()
                .Include(p => p.Record)
                .ThenInclude(p => p.SocialConflict)
                .Where(p => p.Id == compromiseId)
                .First();

            if (compromise != null && compromise.Record != null && compromise.Record.SocialConflict != null)
                await _compromiseLocationRepository.DeleteAsync(p => p.Compromise.Id == compromiseId && p.SocialConflictLocation.SocialConflict.Id != compromise.Record.SocialConflict.Id);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        private async Task<Compromise> ValidateEntity(Compromise compromise,
            EntityDto<long> record,
            EntityDto state,
            EntityDto label,
            EntityDto compromiseState,
            EntityDto compromiseSubState,
            CompromiseUpdatePIPMEFDto pipmef,
            List<CompromiseLocationDto> compromiseLocations,
            EntityDto responsibleActor,
            EntityDto responsibleSubActor,
            List<CompromiseInvolvedDto> involved,
            List<CompromiseSituationDto> situations,
            List<CompromiseUploadResourceDto> uploads,
            List<CompromiseTimeLineDto> timelines,
            List<CompromiseResponsibleDto> responsibles)
        {
            if (record.Id == 0 || await _recordRepository.CountAsync(p => p.Id == record.Id) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El acta no existe o ya no se encuentra disponible");

            if (state.Id > 0 && await _parameterRepository.CountAsync(p => p.Id == state.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryStatus) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El estado del compromiso seleccionado no es válido o ya no existe");

            compromise.Code.VerifyTableColumn(CompromiseConsts.CodeMinLength, CompromiseConsts.CodeMaxLength, DefaultTitleMessage, $"El código del compromiso no debe exceder los {CompromiseConsts.CodeMaxLength} caracteres");

            compromise.Name.IsValidOrException(DefaultTitleMessage, "El nombre del compromiso es obligatorio");
            compromise.Name.VerifyTableColumn(CompromiseConsts.NameMinLength, CompromiseConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del compromiso no debe exceder los {CompromiseConsts.NameMaxLength} caracteres");

            compromise.Name.VerifyTableColumn(CompromiseConsts.DescriptionMinLength, CompromiseConsts.DescriptionMaxLength, DefaultTitleMessage, $"La descripción del compromiso no debe exceder los {CompromiseConsts.DescriptionMaxLength} caracteres");

            //validate task count for priority
            if (record.Id > 0 && !compromise.IsPriority && await _taskRepository.CountAsync(p => p.Compromise.Id == compromise.Id) > 0)
                throw new UserFriendlyException(DefaultTitleMessage, "No se puede establecer el compromiso como No Priorizado si tiene tareas asociadas.");

            var dbRecord = await _recordRepository.GetAsync(record.Id);
            
            compromise.Record = dbRecord;
            compromise.RecordId = dbRecord.Id;

            #region Generate Code
            string returnCode;
            var objSocialConflict = await _socialConflictRepository.GetAsync(dbRecord.SocialConflictId);

            if (string.IsNullOrEmpty(compromise.Code))
            {
                returnCode = GenerateCode("C001", objSocialConflict.Code, false);
                var compromisesList = await _compromiseRepository.GetAllListAsync(x => x.Code.Contains(returnCode));
                if (compromisesList.Any())
                    returnCode = GenerateCode(compromisesList.OrderByDescending(x => x.Code).FirstOrDefault().Code, objSocialConflict.Code, true);
            }
            else
            {
                var compromisesList = await _compromiseRepository.GetAllListAsync(x => x.Code.Contains(objSocialConflict.Code + " - " + compromise.Code) && x.Id != compromise.Id);
                if (compromisesList.Any())
                {
                    int i = 0;
                    do
                    {                        
                        returnCode = GenerateCode(compromisesList.OrderByDescending(x => x.Code).FirstOrDefault().Code, objSocialConflict.Code, true);
                        var verificar = await _compromiseRepository.GetAllListAsync(x => x.Code.Contains(objSocialConflict.Code + " - " + compromise.Code) && x.Id != compromise.Id);
                        if (verificar.Any()) { i = 0; compromise.Code = GenerateCode(returnCode, objSocialConflict.Code, true); }
                        else { returnCode = compromise.Code; i = 1; } 

                    } while (i == 0);
                }
                else 
                {
                    returnCode = GenerateCode(compromise.Code, objSocialConflict.Code, false);
                }                
            }
            compromise.Code = returnCode;
            #endregion

            if (state.Id > 0) compromise.Status = await _parameterRepository.GetAsync(state.Id);
            if (responsibleActor.Id > 0)
                compromise.ResponsibleActor = await _responsibleActorRepository.GetAsync(responsibleActor.Id);
            if (responsibleSubActor != null && responsibleSubActor.Id > 0)
                compromise.ResponsibleSubActor = await _responsibleSubActorRepository.GetAsync(responsibleSubActor.Id);

            if (responsibleSubActor.Id <= 0)
            {
                compromise.ResponsibleSubActor = null;
                compromise.ResponsibleSubActorId = null;
            }

            if (responsibleActor.Id <= 0)
            {
                compromise.ResponsibleActor = null;
                compromise.ResponsibleActorId = null;
                compromise.ResponsibleSubActor = null;
                compromise.ResponsibleSubActorId = null;
            }

            if (compromiseState.Id > 0)
            {
                var dbCompromiseState = _compromiseStateRepository
                    .GetAll()
                    .Where(p => p.Id == compromiseState.Id)
                    .FirstOrDefault();

                if (dbCompromiseState == null)
                    throw new UserFriendlyException("Aviso", "El estado del criterio de seguimiento no existe o es inválida. Verifique la información antes de continuar");

                compromise.CompromiseState = dbCompromiseState;
                compromise.CompromiseStateId = dbCompromiseState.Id;

                if (compromiseSubState.Id > 0)
                {
                    var dbCompromiseSubState = _compromiseSubStateRepository
                        .GetAll()
                        .Where(p => p.Id == compromiseSubState.Id)
                        .FirstOrDefault();

                    if (dbCompromiseSubState == null)
                        throw new UserFriendlyException("Aviso", "El estado actual del criterio de seguimiento no existe o es inválida. Verifique la información antes de continuar");

                    compromise.CompromiseSubState = dbCompromiseSubState;
                    compromise.CompromiseSubStateId = dbCompromiseSubState.Id;
                }
                else
                {
                    compromise.CompromiseSubState = null;
                    compromise.CompromiseSubStateId = null;
                }
            }
            else
            {
                compromise.CompromiseState = null;
                compromise.CompromiseStateId = null;
                compromise.CompromiseSubState = null;
                compromise.CompromiseSubStateId = null;
            }

            if (label.Id > 0)
            {
                var dbLabel = _compromiseLabelRepository
                    .GetAll()
                    .Where(p => p.Id == label.Id)
                    .FirstOrDefault();

                if (dbLabel == null)
                    throw new UserFriendlyException("Aviso", "La etiqueta seleccionada no existe o es inválida. Verifique la información antes de continuar");

                compromise.CompromiseLabel = dbLabel;
                compromise.CompromiseLabelId = dbLabel.Id;
            }
            else
            {
                compromise.CompromiseLabel = null;
                compromise.CompromiseLabelId = null;
            }

            compromise.CompromiseInvolveds = new List<CompromiseInvolved>();
            compromise.CompromiseLocations = new List<CompromiseLocation>();
            compromise.Situations = new List<Situation>();
            compromise.Timelines = new List<CompromiseTimeLine>();
            compromise.CompromiseResponsibles = new List<CompromiseResponsible>();

            if (compromise.PIPMEFId.HasValue && compromise.PIPMEFId.Value == 0)
                compromise.PIPMEFId = null;

            await _compromiseRepository.InsertOrUpdateAsync(compromise);
            await CurrentUnitOfWork.SaveChangesAsync();

            //validate involved list
            foreach (var responsible in involved)
            {
                if (responsible.Remove)
                {
                    if (responsible.Id > 0 && compromise.Id > 0)
                        await _compromiseInvolvedRepository.DeleteAsync(p => p.Id == responsible.Id && p.CompromiseId == compromise.Id);
                }
                else
                {
                    if (responsible.Id <= 0)
                    {
                        if (responsible.ResponsibleSubActor == null || responsible.ResponsibleSubActor.Id <= 0)
                        {
                            if (await _compromiseInvolvedRepository.CountAsync(p => p.ResponsibleActorId == responsible.Id && p.CompromiseId == compromise.Id) == 0)
                            {
                                var dbResponsibleActor = _responsibleActorRepository
                                    .GetAll()
                                    .Where(p => p.Id == responsible.ResponsibleActor.Id)
                                    .FirstOrDefault();

                                if (dbResponsibleActor != null && compromise.CompromiseInvolveds.Count(p => p.ResponsibleActor.Id == responsible.Id) == 0)
                                {
                                    compromise.CompromiseInvolveds.Add(new CompromiseInvolved()
                                    {
                                        Compromise = compromise,
                                        ResponsibleActor = dbResponsibleActor,
                                        ResponsibleActorId = dbResponsibleActor.Id
                                    });
                                }
                            }
                        }
                        else
                        {
                            if (await _compromiseInvolvedRepository.CountAsync(p => p.ResponsibleActorId == responsible.Id && p.ResponsibleSubActorId == responsible.ResponsibleSubActor.Id && p.CompromiseId == compromise.Id) == 0)
                            {

                                var dbResponsibleActor = _responsibleActorRepository
                                    .GetAll()
                                    .Where(p => p.Id == responsible.ResponsibleActor.Id)
                                    .FirstOrDefault();

                                var dbResponsibleSubActor = _responsibleSubActorRepository
                                    .GetAll()
                                    .Where(p => p.Id == responsible.ResponsibleSubActor.Id)
                                    .FirstOrDefault();

                                if (dbResponsibleActor != null && dbResponsibleSubActor != null && compromise
                                    .CompromiseInvolveds
                                    .Count(p => p.ResponsibleActor.Id == responsible.Id &&
                                                p.ResponsibleSubActor != null &&
                                                p.ResponsibleSubActor.Id == responsible.ResponsibleSubActor.Id) == 0)
                                {
                                    compromise.CompromiseInvolveds.Add(new CompromiseInvolved()
                                    {
                                        Compromise = compromise,
                                        ResponsibleActor = dbResponsibleActor,
                                        ResponsibleActorId = dbResponsibleActor.Id,
                                        ResponsibleSubActor = dbResponsibleSubActor,
                                        ResponsibleSubActorId = dbResponsibleSubActor.Id
                                    });
                                }
                            }
                        }
                    }
                }
            }

            //validate responsibles
            foreach (var responsible in responsibles)
            {
                if (responsible.Remove)
                {
                    if (responsible.Id > 0 && compromise.Id > 0)
                        await _compromiseResponsibleRepository.DeleteAsync(p => p.Id == responsible.Id && p.CompromiseId == compromise.Id);
                }
                else
                {
                    if (responsible.Id <= 0)
                    {
                        if (responsible.ResponsibleSubActor == null || responsible.ResponsibleSubActor.Id <= 0)
                        {
                            if (await _compromiseResponsibleRepository.CountAsync(p => p.ResponsibleActorId == responsible.Id && p.CompromiseId == compromise.Id) == 0)
                            {
                                var dbResponsibleActor = _responsibleActorRepository
                                    .GetAll()
                                    .Where(p => p.Id == responsible.ResponsibleActor.Id)
                                    .FirstOrDefault();

                                if (dbResponsibleActor != null && compromise.CompromiseResponsibles.Count(p => p.ResponsibleActor.Id == responsible.Id) == 0)
                                {
                                    compromise.CompromiseResponsibles.Add(new CompromiseResponsible()
                                    {
                                        Compromise = compromise,
                                        ResponsibleActor = dbResponsibleActor,
                                        ResponsibleActorId = dbResponsibleActor.Id
                                    });
                                }
                            }
                        }
                        else
                        {
                            if (await _compromiseResponsibleRepository.CountAsync(p => p.ResponsibleActorId == responsible.Id && p.ResponsibleSubActorId == responsible.ResponsibleSubActor.Id && p.CompromiseId == compromise.Id) == 0)
                            {

                                var dbResponsibleActor = _responsibleActorRepository
                                    .GetAll()
                                    .Where(p => p.Id == responsible.ResponsibleActor.Id)
                                    .FirstOrDefault();

                                var dbResponsibleSubActor = _responsibleSubActorRepository
                                    .GetAll()
                                    .Where(p => p.Id == responsible.ResponsibleSubActor.Id)
                                    .FirstOrDefault();

                                if (dbResponsibleActor != null && dbResponsibleSubActor != null && compromise
                                    .CompromiseResponsibles
                                    .Count(p => p.ResponsibleActor.Id == responsible.Id &&
                                                p.ResponsibleSubActor != null &&
                                                p.ResponsibleSubActor.Id == responsible.ResponsibleSubActor.Id) == 0)
                                {
                                    compromise.CompromiseResponsibles.Add(new CompromiseResponsible()
                                    {
                                        Compromise = compromise,
                                        ResponsibleActor = dbResponsibleActor,
                                        ResponsibleActorId = dbResponsibleActor.Id,
                                        ResponsibleSubActor = dbResponsibleSubActor,
                                        ResponsibleSubActorId = dbResponsibleSubActor.Id
                                    });
                                }
                            }
                        }
                    }
                }
            }

            //validate locations
            foreach (var location in compromiseLocations)
            {
                var exists = (await _compromiseLocationRepository.CountAsync(p => p.Compromise.Id == compromise.Id && p.SocialConflictLocation.Id == location.SocialConflictLocation.Id) > 0);

                if (location.Check)
                {
                    if (await _socialConflictLocationRepository.CountAsync(p => p.Id == location.SocialConflictLocation.Id) > 0 && !exists)
                    {
                        compromise.CompromiseLocations.Add(new CompromiseLocation()
                        {
                            SocialConflictLocation = await _socialConflictLocationRepository.GetAsync(location.SocialConflictLocation.Id)
                        });
                    }
                }

                if (!location.Check && exists)
                {
                    await _compromiseLocationRepository.DeleteAsync(p => p.Compromise.Id == compromise.Id && p.SocialConflictLocation.Id == location.SocialConflictLocation.Id);
                }
            }

            //validate resource
            foreach (var uploadResource in uploads)
            {
                await _situationRepository.InsertAsync(new Situation()
                {
                    Description = uploadResource.Description,
                    Resource = uploadResource.UploadFile == null ? null : ObjectMapper.Map<SituationResource>(ResourceManager.Create(uploadResource.UploadFile, ResourceConsts.Compromise)),
                    Compromise = compromise
                });
            }

            foreach (var timeline in timelines)
            {

                if (timeline.Remove)
                {
                    if (timeline.Id > 0 && compromise.Id > 0 && await _compromiseTimeLineRepository.CountAsync(p => p.Id == timeline.Id && p.CompromiseId == compromise.Id) > 0)
                    {
                        await _compromiseTimeLineRepository.DeleteAsync(timeline.Id);
                    }
                }
                else
                {

                    timeline.Observation.VerifyTableColumn(
                        CompromiseTimeLineConsts.ObservationMinLength,
                        CompromiseTimeLineConsts.ObservationMaxLength,
                        DefaultTitleMessage,
                        $"Las observaciones del cronograma de cumplimiento no deben exceder los {CompromiseTimeLineConsts.ObservationMaxLength} caracteres");

                    var dbPhase = _parameterRepository
                       .GetAll()
                       .Where(p => p.Id == timeline.Phase.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPPhase)
                       .FirstOrDefault();

                    var dbMilestone = _parameterRepository
                       .GetAll()
                       .Where(p => p.Id == timeline.Milestone.Id && p.ParentId == timeline.Phase.Id && p.ParameterCategory.Code == CompromiseConsts.ParameterCategoryPIPMilestone)
                       .FirstOrDefault();

                    if (dbPhase != null && dbMilestone != null)
                    {
                        if (timeline.Id > 0)
                        {
                            if (await _compromiseTimeLineRepository.CountAsync(p => p.Id == timeline.Id && p.CompromiseId == compromise.Id) > 0)
                            {
                                var dbTimeline = await _compromiseTimeLineRepository.GetAsync(timeline.Id);

                                dbTimeline.PhaseId = dbPhase.Id;
                                dbTimeline.Phase = dbPhase;
                                dbTimeline.MilestoneId = dbMilestone.Id;
                                dbTimeline.Milestone = dbMilestone;
                                dbTimeline.Observation = timeline.Observation;
                                dbTimeline.ProyectedTime = timeline.ProyectedTime;
                                dbTimeline.CompletedTime = timeline.CompletedTime;

                                await _compromiseTimeLineRepository.UpdateAsync(dbTimeline);
                            }
                        }
                        else
                        {
                            compromise.Timelines.Add(new CompromiseTimeLine()
                            {
                                PhaseId = dbPhase.Id,
                                Phase = dbPhase,
                                MilestoneId = dbMilestone.Id,
                                Milestone = dbMilestone,
                                Observation = timeline.Observation,
                                ProyectedTime = timeline.ProyectedTime,
                                CompletedTime = timeline.CompletedTime
                            });
                        }
                    }
                }
            }

            //validate PIP  
            if (pipmef.UnifiedCode.IsValid() || pipmef.SNIPCode.IsValid())
            {
                compromise.PIPMEF = await _pipMefAppService.ValidatePIPMEFCompromise(pipmef);

                if (compromise.PIPMEF.ProjectName != null && compromise.PIPMEF.ProjectName.IsValid())
                    compromise.Name = compromise.PIPMEF.ProjectName;
            }
            else if (pipmef.PIPPhase != null || pipmef.PIPMilestone != null)
            {
                compromise.PIPMEF = new PIPMEF();

                if (compromise.PIPMEFId.HasValue && await _pipmefRepository.CountAsync(p => p.Id == compromise.PIPMEFId.Value) > 0)
                    compromise.PIPMEF = await _pipmefRepository.GetAsync(compromise.PIPMEFId.Value);

                if (pipmef.PIPPhase != null && pipmef.PIPPhase.Id != -1)
                {
                    compromise.PIPMEF.PIPPhase = await _parameterRepository.GetAsync(pipmef.PIPPhase.Id);
                }
                else if (pipmef.PIPPhase == null || pipmef.PIPPhase.Id != -1)
                {
                    compromise.PIPMEF.PIPPhase = null;
                }

                if (pipmef.PIPMilestone != null && pipmef.PIPMilestone.Id != -1)
                {
                    compromise.PIPMEF.PIPMilestone = await _parameterRepository.GetAsync(pipmef.PIPMilestone.Id);
                }
                else if (pipmef.PIPMilestone == null || pipmef.PIPMilestone.Id != -1)
                {
                    compromise.PIPMEF.PIPMilestone = null;
                }
            }

            foreach (var situation in situations)
            {
                if (situation.Remove)
                {
                    if (situation.Id > 0 && compromise.Id > 0 && await _situationRepository.CountAsync(p => p.Id == situation.Id && p.Compromise.Id == compromise.Id) > 0)
                    {
                        await _situationRepository.DeleteAsync(situation.Id);
                    }
                }
            }

            compromise.Filter = string.Concat(
            compromise.Code ?? "", " ",
            compromise.Name ?? "", " ",
            compromise.Record != null ? compromise.Record.Code ?? "" : "", " ",
            compromise.Record != null ? compromise.Record.Title ?? "" : "", " ",
            compromise.Record != null ? (compromise.Record.SocialConflict != null ? compromise.Record.SocialConflict.Code ?? "" : "") : "", " ",
            compromise.Record != null ? (compromise.Record.SocialConflict != null ? compromise.Record.SocialConflict.CaseName ?? "" : "") : "");

            return compromise;
        }


        private async Task<List<CompromiseGetMatrixExcelDto>> ReportAllExcel(CompromiseGetToExcelInput input)
        {
            var query = _compromiseRepository
                    .GetAll()
                    .Include(p => p.CompromiseLabel)
                    .Include(p => p.Status)
                    .Include(p => p.Record)
                        .ThenInclude(p => p.SocialConflict)
                    .Include(p => p.ResponsibleActor)
                        .ThenInclude(p => p.ResponsibleType)
                    .Include(p => p.ResponsibleActor)
                        .ThenInclude(p => p.ResponsibleSubType)
                    .Include(p => p.ResponsibleSubActor)
                    .Include(p => p.CompromiseResponsibles)
                        .ThenInclude(p => p.ResponsibleActor)
                            .ThenInclude(p => p.ResponsibleType)
                    .Include(p => p.CompromiseResponsibles)
                        .ThenInclude(p => p.ResponsibleActor)
                            .ThenInclude(p => p.ResponsibleSubType)
                    .Include(p => p.CompromiseResponsibles)
                        .ThenInclude(p => p.ResponsibleSubActor)
                    .Include(p => p.PIPMEF)
                        .ThenInclude(p => p.PIPPhase)
                    .Include(p => p.PIPMEF)
                        .ThenInclude(p => p.PIPMilestone)
                    .Include(p => p.CompromiseInvolveds)
                        .ThenInclude(p => p.ResponsibleActor)
                            .ThenInclude(p => p.ResponsibleType)
                    .Include(p => p.CompromiseInvolveds)
                        .ThenInclude(p => p.ResponsibleActor)
                            .ThenInclude(p => p.ResponsibleSubType)
                    .Include(p => p.CompromiseInvolveds)
                        .ThenInclude(p => p.ResponsibleSubActor)
                    .Include(p => p.Timelines)
                        .ThenInclude(p => p.Phase)
                    .Include(p => p.Timelines)
                        .ThenInclude(p => p.Milestone)
                    .Where(p => p.Record.IsDeleted == false)
                    .WhereIf(input.Type.HasValue && input.Type > 0, p => p.Type == (input.Type.Value == 1 ? CompromiseType.PIP : CompromiseType.Activity))
                    .WhereIf(input.CodeSocialConflict.IsValid(), p => p.Record.SocialConflict.Code.Contains(input.CodeSocialConflict))
                    .WhereIf(input.CodeRecord.IsValid(), p => p.Record.Code.Contains(input.CodeRecord))
                    .WhereIf(input.Code.IsValid(), p => p.Code.Contains(input.Code))
                    .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0,
                        p => p.CompromiseLocations.Any(p => p.SocialConflictLocation.TerritorialUnit.Id == input.TerritorialUnitId))
                    .WhereIf(input.FilterByDate, p => p.CreationTime >= input.StartTime && p.CreationTime <= input.EndTime)
                    .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(Compromise.Filter));

            var output = await query.OrderBy(input.Sorting).ToListAsync();

            var result = new List<CompromiseGetMatrixExcelDto>();

            foreach (var compromise in output)
            {
                var compromiseItem = ObjectMapper.Map<CompromiseGetMatrixExcelDto>(compromise);

                compromiseItem.Responsibles = compromise
                    .CompromiseResponsibles
                    .Where(p => p.ResponsibleActor != null)
                    .Select(e => e.ResponsibleActor.Name)
                    .Distinct()
                    .JoinAsString(", ");

                compromiseItem.SubResponsibles = compromise
                    .CompromiseResponsibles
                    .Where(p => p.ResponsibleSubActor != null)
                    .Select(e => e.ResponsibleSubActor.Name)
                    .Distinct()
                    .JoinAsString(", ");

                compromiseItem.ResponsibleTypes = compromise
                    .CompromiseResponsibles
                    .Where(p => p.ResponsibleActor != null && p.ResponsibleActor.ResponsibleType != null)
                    .Select(e => e.ResponsibleActor.ResponsibleType.Name)
                    .Distinct()
                    .JoinAsString(", ");

                compromiseItem.ResponsibleSubTypes = compromise
                    .CompromiseResponsibles
                    .Where(p => p.ResponsibleActor != null && p.ResponsibleActor.ResponsibleSubType != null)
                    .Select(e => e.ResponsibleActor.ResponsibleSubType.Name)
                    .Distinct()
                    .JoinAsString(", ");

                compromiseItem.InvolvedResponsibles = compromise
                    .CompromiseInvolveds
                    .Where(p => p.ResponsibleActor != null)
                    .Select(e => e.ResponsibleActor.Name)
                    .Distinct()
                    .JoinAsString(", ");

                compromiseItem.InvolvedSubResponsibles = compromise
                    .CompromiseInvolveds
                    .Where(p => p.ResponsibleSubActor != null)
                    .Select(e => e.ResponsibleSubActor.Name)
                    .Distinct()
                    .JoinAsString(", ");

                compromiseItem.InvolvedTypes = compromise
                    .CompromiseInvolveds
                    .Where(p => p.ResponsibleActor != null && p.ResponsibleActor.ResponsibleType != null)
                    .Select(e => e.ResponsibleActor.ResponsibleType.Name)
                    .Distinct()
                    .JoinAsString(", ");

                compromiseItem.InvolvedSubTypes = compromise
                    .CompromiseInvolveds
                    .Where(p => p.ResponsibleActor != null && p.ResponsibleActor.ResponsibleSubType != null)
                    .Select(e => e.ResponsibleActor.ResponsibleSubType.Name)
                    .Distinct()
                    .JoinAsString(", ");

                ///TODO:JJARAMILLOR -> listar el array de linea de tiempo como string y con saltos de línea
                compromiseItem.Timelines = compromise
                    .Timelines
                    .OrderBy(p => p.Phase.Order)
                    .ThenBy(p => p.Milestone.Order)
                    .Select(p =>
                    {
                        var top = $"{(p.Phase == null ? "" : p.Phase.Value)}{(p.Milestone == null ? "" : ", " + p.Milestone.Value)}";
                        top = $"{top}{(p.ProyectedTime.HasValue ? ", " + p.ProyectedTime.Value.ToString("dd/MM/yyyy") : "")}";
                        top = $"{top}{(p.CompletedTime.HasValue ? ", " + p.CompletedTime.Value.ToString("dd/MM/yyyy") : "")}";
                        top = $"{top}{(string.IsNullOrWhiteSpace(p.Observation) ? "" : "," + p.Observation)}";

                        return top;
                    }).JoinAsString(";\n");

                var locations = await _socialConflictLocationRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Include(p => p.Department)
                    .Include(p => p.Province)
                    .Include(p => p.District)
                    .Where(p => p.CompromiseLocations.Any(c => c.Compromise.Id == compromise.Id)).ToListAsync();

                if (locations.Count > 0)
                {
                    compromiseItem.TerritorialUnits = locations.Select(p => p.TerritorialUnit.Name).Distinct().JoinAsString(", ");
                    compromiseItem.Departments = locations.Select(p => p.Department.Name).Distinct().JoinAsString(", ");
                    compromiseItem.Provinces = locations.Select(p => p.Province.Name).Distinct().JoinAsString(", ");
                    compromiseItem.Districts = locations.Select(p => p.District.Name).Distinct().JoinAsString(", ");
                }

                var lastSituation = await _situationRepository
                    .GetAll()
                    .Where(p => p.Compromise.Id == compromise.Id)
                    .OrderByDescending(p => p.CreationTime)
                    .FirstOrDefaultAsync();

                if (lastSituation != null)
                    compromiseItem.Advance = lastSituation.Description;

                if (compromise.IsPriority)
                {
                    var lastComment = await _commentRepository.GetAll()
                        .Where(p => p.TaskManagement.Compromise.Id == compromise.Id && !p.TaskManagement.IsDeleted)
                        .OrderByDescending(p => p.CreationTime)
                        .FirstOrDefaultAsync();

                    if (lastComment != null)
                    {
                        if (lastSituation == null)
                            compromiseItem.Advance = lastComment.Description;
                        else if (lastComment.CreationTime > lastSituation.CreationTime)
                            compromiseItem.Advance = lastComment.Description;
                    }
                }


                var creatorUser = compromise.CreatorUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == compromise.CreatorUserId.Value)
                    .FirstOrDefault() : null;

                var editUser = compromise.LastModifierUserId.HasValue ? _userRepository
                    .GetAll()
                    .Where(p => p.Id == compromise.LastModifierUserId.Value)
                    .FirstOrDefault() : null;

                compromiseItem.CreatorUser = creatorUser == null ? null : ObjectMapper.Map<CompromiseUserDto>(creatorUser);
                compromiseItem.EditUser = editUser == null ? null : ObjectMapper.Map<CompromiseUserDto>(editUser);

                result.Add(compromiseItem);
            }
            return result;
        }

        public async Task<FileDto> GetMatrixToExcel(CompromiseGetToExcelInput input)
        {
            return _compromiseExcelExporter.ExportMatrixToFile(await ReportAllExcel(input));
        }

        public async Task<FileDto> GetAllToExcel(CompromiseGetToExcelInput input)
        {
            return _compromiseExcelExporter.ExportAllToFile(await ReportAllExcel(input));
        }

       
    }
}
