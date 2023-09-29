using Abp.Domain.Repositories;
using Abp.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contable.Application.Extensions;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Collections.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using System;
using System.Linq.Expressions;
using Contable.Application.StaticVariables.Dto;
using Contable.Application.StaticVariables;
using Abp.BackgroundJobs;
using Contable.Gdpr;
using Abp.Runtime.Session;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable)]
    public class StaticVariableAppService : ContableAppServiceBase, IStaticVariableAppService
    {
        private readonly IRepository<StaticVariable> _staticVariableRepository;
        private readonly IRepository<StaticVariableOption> _staticVariableOptionRepository;
        private readonly IRepository<StaticVariableOptionDetail> _staticVariableOptionDetailRepository;
        private readonly IRepository<DinamicVariable> _dinamicVariableRepository;
        private readonly IRepository<ProspectiveRisk> _prospectiveRiskRepository;
        private readonly IRepository<ProspectiveRiskDetail> _prospectiveRiskDetailRepository;

        public StaticVariableAppService(
            IRepository<StaticVariable> staticVariableRepository,
            IRepository<StaticVariableOption> staticVariableOptionRepository,
            IRepository<StaticVariableOptionDetail> staticVariableOptionDetailRepository,
            IRepository<DinamicVariable> dinamicVariableRepository,
            IRepository<ProspectiveRisk> prospectiveRiskRepository,
            IRepository<ProspectiveRiskDetail> prospectiveRiskDetailRepository)
        {
            _staticVariableRepository = staticVariableRepository;
            _staticVariableOptionRepository = staticVariableOptionRepository;
            _staticVariableOptionDetailRepository = staticVariableOptionDetailRepository;
            _dinamicVariableRepository = dinamicVariableRepository;
            _prospectiveRiskRepository = prospectiveRiskRepository;
            _prospectiveRiskDetailRepository = prospectiveRiskDetailRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable_Create)]
        public async Task<EntityDto> Create(StaticVariableCreateDto input)
        {
            var staticVariable = ValidateEntity(ObjectMapper.Map<StaticVariable>(input));

            var index = 0;
            var weight = 0m;

            foreach(var option in input.Options)
            {
                option.Index = index;

                if (option.Type == StaticVariableType.Cuantitative && (option.DinamicVariable == null || await _dinamicVariableRepository.CountAsync(p => p.Id == option.DinamicVariable.Id) == 0))
                    throw new UserFriendlyException("Aviso", $"La variable dinámica asociada a la opción Nº {option.Index + 1} es obligatoria");
                
                if (staticVariable.Family == StaticVariableFamily.ProspectiveRisk)
                    option.Site = StaticVariableSite.None;

                if (staticVariable.Family == StaticVariableFamily.ProjectRisk && option.Site == StaticVariableSite.None)
                    throw new UserFriendlyException("Aviso", $"La variable asociada a la opción Nº {option.Index + 1} debe especificar si es de Impacto o Probabilidad antes de continuar");

                var dbOption = ValidateEntity(ObjectMapper.Map<StaticVariableOption>(option));
                dbOption.Details = new List<StaticVariableOptionDetail>();

                var internalIndex = 0;
                weight += dbOption.Value;

                foreach (var detail in option.Details)
                {
                    detail.Index = internalIndex;

                    ValidateEntity(ObjectMapper.Map<StaticVariableOptionDetail>(detail), dbOption);

                    internalIndex++;
                }

                index++;
            }

            staticVariable.Weight = weight;
            var dbStaticVariableId = await _staticVariableRepository.InsertAndGetIdAsync(staticVariable);

            foreach (var option in input.Options)
            {
                var dbOption = ValidateEntity(ObjectMapper.Map<StaticVariableOption>(option));

                dbOption.StaticVariableId = dbStaticVariableId;

                if (option.Type == StaticVariableType.Cuantitative)
                {
                    var dinamicVariable = await _dinamicVariableRepository.GetAsync(option.DinamicVariable.Id);

                    dbOption.DinamicVariableId = dinamicVariable.Id;
                }

                var dbOptionId = await _staticVariableOptionRepository.InsertAndGetIdAsync(dbOption);

                foreach(var detail in option.Details)
                {
                    var dbOptionDetail = ValidateEntity(ObjectMapper.Map<StaticVariableOptionDetail>(detail), dbOption);
                    dbOptionDetail.StaticVariableOptionId = dbOptionId;
                    dbOptionDetail.StaticVariableId = dbStaticVariableId;

                    await _staticVariableOptionDetailRepository.InsertAsync(dbOptionDetail);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());

            return new EntityDto(dbStaticVariableId);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable_Delete)]
        public async Task Delete(EntityDto input)
        {
            if (await _staticVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            await _staticVariableRepository.DeleteAsync(input.Id);
            await _staticVariableOptionRepository.DeleteAsync(p => p.StaticVariableId == input.Id);
            await _staticVariableOptionDetailRepository.DeleteAsync(p => p.StaticVariableId == input.Id);
            await _prospectiveRiskDetailRepository.DeleteAsync(p => p.StaticVariableOption.StaticVariableId == input.Id);

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable_Disable)]
        public async Task Disable(EntityDto input)
        {
            if (await _staticVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var staticVariable = await _staticVariableRepository.GetAsync(input.Id);

            if (staticVariable.Enabled == false)
                throw new UserFriendlyException("Aviso", "La variable ya se encuentra deshabilitada, por favor refresque su búsqueda");

            staticVariable.Enabled = false;

            await _staticVariableRepository.UpdateAsync(staticVariable);

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable_Enable)]
        public async Task Enable(EntityDto input)
        {
            if (await _staticVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var staticVariable = await _staticVariableRepository.GetAsync(input.Id);

            if (staticVariable.Enabled == true)
                throw new UserFriendlyException("Aviso", "La variable ya se encuentra deshabilitada, por favor refresque su búsqueda");

            staticVariable.Enabled = true;

            await _staticVariableRepository.UpdateAsync(staticVariable);

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable)]
        public async Task<StaticVariableGetDto> Get(EntityDto input)
        {
            if (await _staticVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var dinamicVariable = await _staticVariableRepository
                .GetAll()
                .Include(p => p.Options)
                .ThenInclude(p => p.Details)
                .Include(p => p.Options)
                .ThenInclude(p => p.DinamicVariable)
                .Where(p => p.Id == input.Id)
                .FirstAsync();

            var output = ObjectMapper.Map<StaticVariableGetDto>(dinamicVariable);

            output.Options = output
                .Options
                .OrderBy(p => p.Index)
                .ToList();

            foreach(var option in output.Options)
            {
                option.Details = option
                    .Details
                    .OrderBy(p => p.Index)
                    .ToList();
            }
            
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable)]
        public async Task<PagedResultDto<StaticVariableGetAllDto>> GetAll(StaticVariableGetAllInputDto input)
        {
            var query = _staticVariableRepository
                .GetAll()
                .Where(p => p.Family == input.Family)
                .LikeAnyBidirectional(input.Filter.SplitByLike(), nameof(StaticVariable.Name));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<StaticVariableGetAllDto>(count, ObjectMapper.Map<List<StaticVariableGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable_Create, AppPermissions.Pages_Management_StaticVariable_Edit, RequireAllPermissions = false)]
        public async Task<PagedResultDto<StaticVariableCuantitativeGetAllDto>> GetAllDinamicVariables(StaticVariableCuantitativeGetAllInputDto input)
        {
            var query = _dinamicVariableRepository
                .GetAll()
                .Where(p => p.Type == input.Type && p.Enabled)
                .LikeAnyBidirectional(input.Filter.SplitByLike(), nameof(DinamicVariable.Name));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<StaticVariableCuantitativeGetAllDto>(count, ObjectMapper.Map<List<StaticVariableCuantitativeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Management_StaticVariable_Edit)]
        public async Task<EntityDto> Update(StaticVariableUpdateDto input)
        {
            if (await _staticVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var staticVariable = ValidateEntity(ObjectMapper.Map(input, await _staticVariableRepository.GetAsync(input.Id)));

            var index = 0;
            var weight = 0m;

            foreach (var option in input.Options)
            {
                option.Index = index;

                if(option.Id <= 0 && option.Type == StaticVariableType.Cuantitative && (option.DinamicVariable == null || await _dinamicVariableRepository.CountAsync(p => p.Id == option.DinamicVariable.Id) == 0)) 
                { 
                    throw new UserFriendlyException("Aviso", $"La variable dinámica asociada a la opción Nº {option.Index + 1} es obligatoria");
                }
                else if (option.Id > 0 && await _staticVariableOptionRepository.CountAsync(p => p.Id == option.Id) == 0) 
                { 
                    throw new UserFriendlyException("Aviso", $"El registro de la opción Nº {option.Index + 1} no existe o ya fue eliminado");
                }

                var dbOption = ValidateEntity(ObjectMapper.Map<StaticVariableOption>(option));

                var internalIndex = 0;

                foreach (var detail in option.Details)
                {
                    detail.Index = internalIndex;

                    ValidateEntity(ObjectMapper.Map<StaticVariableOptionDetail>(detail), dbOption);

                    internalIndex++;
                }

                index++;
            }

            foreach (var option in input.Options)
            {
                if(option.Id <= 0)
                {
                    var dbOption = ValidateEntity(ObjectMapper.Map<StaticVariableOption>(option));

                    dbOption.Id = 0;
                    dbOption.StaticVariableId = staticVariable.Id;
                    dbOption.Type = option.Type;
                    weight += dbOption.Value;

                    if (staticVariable.Family == StaticVariableFamily.ProspectiveRisk)
                        dbOption.Site = StaticVariableSite.None;

                    if (staticVariable.Family == StaticVariableFamily.ProjectRisk && dbOption.Site == StaticVariableSite.None)
                        throw new UserFriendlyException("Aviso", $"La variable asociada a la opción Nº {option.Index + 1} debe especificar si es de Impacto o Probabilidad antes de continuar");

                    if (option.Type == StaticVariableType.Cuantitative)
                    {
                        var dinamicVariable = await _dinamicVariableRepository.GetAsync(option.DinamicVariable.Id);

                        dbOption.DinamicVariableId = dinamicVariable.Id;
                    }

                    var dbOptionId = await _staticVariableOptionRepository.InsertAndGetIdAsync(dbOption);
   

                    foreach (var detail in option.Details)
                    {
                        var dbOptionDetail = ValidateEntity(ObjectMapper.Map<StaticVariableOptionDetail>(detail), dbOption);

                        dbOptionDetail.Id = 0;
                        dbOptionDetail.StaticVariableOptionId = dbOptionId;
                        dbOptionDetail.StaticVariableId = staticVariable.Id;

                        await _staticVariableOptionDetailRepository.InsertAsync(dbOptionDetail);
                    }
                }
                else
                {
                    var dbOption = await _staticVariableOptionRepository.GetAsync(option.Id);

                    dbOption.Name = option.Name;
                    dbOption.Index = option.Index;
                    dbOption.Enabled = option.Enabled;
                    dbOption.Value = option.Value;
                    dbOption.Site = option.Site;

                    weight += dbOption.Value;

                    ValidateEntity(dbOption);

                    if (dbOption.StaticVariableId != staticVariable.Id)
                        throw new UserFriendlyException("Aviso", "No se puede procesar la solicitud, debido a que el cuerpo del mensaje es inválido hay registros de opciones que no pertenecen a la variable");

                    if (staticVariable.Family == StaticVariableFamily.ProspectiveRisk)
                        dbOption.Site = StaticVariableSite.None;

                    if (staticVariable.Family == StaticVariableFamily.ProjectRisk && dbOption.Site == StaticVariableSite.None)
                        throw new UserFriendlyException("Aviso", $"La variable asociada a la opción Nº {option.Index + 1} debe especificar si es de Impacto o Probabilidad antes de continuar");

                    foreach (var detail in option.Details)
                    {
                        if(detail.Id <= 0)
                        {
                            var dbOptionDetail = ValidateEntity(ObjectMapper.Map<StaticVariableOptionDetail>(detail), dbOption);

                            dbOptionDetail.Id = 0;
                            dbOptionDetail.StaticVariableOptionId = dbOption.Id;
                            dbOptionDetail.StaticVariableId = staticVariable.Id;

                            await _staticVariableOptionDetailRepository.InsertAsync(dbOptionDetail);
                        }
                        else
                        {
                            var dbOptionDetail = ValidateEntity(ObjectMapper.Map(detail, await _staticVariableOptionDetailRepository.GetAsync(detail.Id)), dbOption);

                            if(dbOptionDetail.StaticVariableId != staticVariable.Id)
                                throw new UserFriendlyException("Aviso", "No se puede procesar la solicitud, debido a que el cuerpo del mensaje es inválido hay registros de características que no pertenecen a las opciones");
                            if (dbOptionDetail.StaticVariableOptionId != dbOption.Id)
                                throw new UserFriendlyException("Aviso", "No se puede procesar la solicitud, debido a que el cuerpo del mensaje es inválido hay registros de características que no pertenecen a la variable");

                            await _staticVariableOptionDetailRepository.UpdateAsync(dbOptionDetail);
                        }
                    }

                    await _staticVariableOptionRepository.UpdateAsync(dbOption);
                }
            }

            foreach (var option in input.DeletedOptions)
            {
                if(await _staticVariableOptionRepository.CountAsync(p => p.Id == option.Id && p.StaticVariableId == staticVariable.Id) > 0)
                {
                    await _staticVariableOptionRepository.DeleteAsync(option.Id);
                    await _staticVariableOptionDetailRepository.DeleteAsync(p => p.StaticVariableOptionId == option.Id);
                    await _prospectiveRiskDetailRepository.DeleteAsync(p => p.StaticVariableOptionId == option.Id);
                }
            }

            foreach (var optionDetail in input.DeletedOptionDetails)
            {
                if (await _staticVariableOptionDetailRepository.CountAsync(p => p.Id == optionDetail.Id && p.StaticVariableId == staticVariable.Id) > 0)
                {
                    await _staticVariableOptionDetailRepository.DeleteAsync(optionDetail.Id);
                }
            }

            staticVariable.Weight = weight;
            await _staticVariableRepository.UpdateAsync(staticVariable);

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());

            return new EntityDto(staticVariable.Id);
        }

        private StaticVariable ValidateEntity(StaticVariable input)
        {
            if (input == null)
                throw new UserFriendlyException("Aviso debe ingresar el ámbito de la variable antes de continuar");

            input.Name.IsValidOrException(DefaultTitleMessage, "El ámbito de la variable es obligatorio");
            input.Name.VerifyTableColumn(StaticVariableConsts.NameMinLength, StaticVariableConsts.NameMaxLength, DefaultTitleMessage, $"El ámbito de la variable no debe exceder los {StaticVariableConsts.NameMaxLength} caracteres");

            if (input.Family == StaticVariableFamily.None)
                throw new UserFriendlyException("Aviso", "Debe especificar el tipo de variable antes de continuar");

            return input;
        }

        private StaticVariableOption ValidateEntity(StaticVariableOption input)
        { 
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la opción Nº {input.Index + 1} es obligatoria");
            input.Name.VerifyTableColumn(StaticVariableOptionConsts.NameMinLength, StaticVariableOptionConsts.NameMaxLength, DefaultTitleMessage, $"El nombre de la opción Nº {input.Index + 1} no debe exceder los {StaticVariableOptionConsts.NameMaxLength} caracteres");

            return input;
        }

        private StaticVariableOptionDetail ValidateEntity(StaticVariableOptionDetail input, StaticVariableOption option)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la característica Nº {input.Index + 1} de la opción Nº {option.Index + 1} es obligatoria");
            input.Name.VerifyTableColumn(StaticVariableOptionDetailConsts.NameMinLength, StaticVariableOptionDetailConsts.NameMaxLength, DefaultTitleMessage, $"El nombre de la característica Nº {input.Index + 1} de la opción Nº {option.Index + 1} no debe exceder los {StaticVariableOptionDetailConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
