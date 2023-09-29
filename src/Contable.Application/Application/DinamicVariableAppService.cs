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
using Contable.Application.DinamicVariables.Dto;
using System.Linq.Expressions;
using Contable.Application.DinamicVariables;
using Abp.BackgroundJobs;
using Contable.Gdpr;
using Abp.EntityFrameworkCore;
using Contable.EntityFrameworkCore;
using Abp.Runtime.Session;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable)]
    public class DinamicVariableAppService : ContableAppServiceBase, IDinamicVariableAppService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<DinamicVariable> _dinamicVariableRepository;
        private readonly IRepository<DinamicVariableDetail> _dinamicVariableDetailRepository;
        private readonly IRepository<StaticVariable> _staticVariableRepository; 
        private readonly IRepository<StaticVariableOption> _staticVariableOptionRepository;
        private readonly IRepository<StaticVariableOptionDetail> _staticVariableOptionDetailRepository;
        private readonly IRepository<ProspectiveRisk> _prospectiveRiskRepository;
        private readonly IRepository<ProspectiveRiskDetail> _prospectiveRiskDetailRepository;
        private readonly IRepository<ProjectRiskDetail> _projectRiskDetailRepository;

        public DinamicVariableAppService(
            IBackgroundJobManager backgroundJobManager,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<DinamicVariable> dinamicVariableRepository,
            IRepository<DinamicVariableDetail> dinamicVariableDetailRepository,
            IRepository<StaticVariable> staticVariableRepository,
            IRepository<StaticVariableOption> staticVariableOptionRepository,
            IRepository<StaticVariableOptionDetail> staticVariableOptionDetailRepository,
            IRepository<ProspectiveRisk> prospectiveRiskRepository,
            IRepository<ProspectiveRiskDetail> prospectiveRiskDetailRepository,
            IRepository<ProjectRiskDetail> projectRiskDetailRepository)
        {
            _backgroundJobManager = backgroundJobManager;
            _territorialUnitRepository = territorialUnitRepository; 
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _dinamicVariableRepository = dinamicVariableRepository;
            _dinamicVariableDetailRepository = dinamicVariableDetailRepository;
            _staticVariableRepository = staticVariableRepository;
            _staticVariableOptionRepository = staticVariableOptionRepository;
            _staticVariableOptionDetailRepository = staticVariableOptionDetailRepository;
            _prospectiveRiskRepository = prospectiveRiskRepository;
            _prospectiveRiskDetailRepository = prospectiveRiskDetailRepository;
            _projectRiskDetailRepository = projectRiskDetailRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable_Create)]
        public async Task<EntityDto> Create(DinamicVariableEntityCreateDto input)
        {
            var variable = ValidateEntity(ObjectMapper.Map<DinamicVariable>(input.Variable));

            var variableId = await _dinamicVariableRepository.InsertAndGetIdAsync(variable);
            var duplicatesDistrict = input.Changes
                .GroupBy(p => p.Province.Id)
                .Where(p => p.Count() > 1);

            if (duplicatesDistrict.Count() > 0)
                throw new UserFriendlyException("Aviso", "No fue posible guardar el registro debido a inconsistencias en la solicitud");

            foreach (var change in input.Changes.Where(p => p.Value > 0))
            {
                if(await _provinceRepository.CountAsync(p => p.Id == change.Province.Id) > 0)
                {
                    await _dinamicVariableDetailRepository.InsertAsync(new DinamicVariableDetail()
                    {
                        DinamicVariableId = variableId,
                        ProvinceId = change.Province.Id,
                        Value = change.Value
                    });
                }
            }

            return new EntityDto(variableId);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable_Delete)]
        public async Task Delete(EntityDto input)
        {
            if (await _dinamicVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            await _dinamicVariableRepository.DeleteAsync(input.Id);
            await _dinamicVariableDetailRepository.DeleteAsync(p => p.DinamicVariableId == input.Id);
            await _staticVariableOptionRepository.DeleteAsync(p => p.DinamicVariableId == input.Id);
            await _staticVariableOptionDetailRepository.DeleteAsync(p => p.StaticVariableOption.DinamicVariableId == input.Id);
            await _prospectiveRiskDetailRepository.DeleteAsync(p => p.StaticVariableOption.DinamicVariableId == input.Id);
            await _projectRiskDetailRepository.DeleteAsync(p => p.StaticVariableOption.DinamicVariableId == input.Id);

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable_Disable)]
        public async Task Disable(EntityDto input)
        {
            if (await _dinamicVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var dinamicVariable = await _dinamicVariableRepository.GetAsync(input.Id);

            if (dinamicVariable.Enabled == false)
                throw new UserFriendlyException("Aviso", "La variable ya se encuentra deshabilitada, por favor refresque su búsqueda");

            dinamicVariable.Enabled = false;

            await _dinamicVariableRepository.UpdateAsync(dinamicVariable);
            await _staticVariableOptionRepository.DeleteAsync(p => p.DinamicVariableId == input.Id);
            await _staticVariableOptionDetailRepository.DeleteAsync(p => p.StaticVariableOption.DinamicVariableId == input.Id);
            await _prospectiveRiskDetailRepository.DeleteAsync(p => p.StaticVariableOption.DinamicVariableId == input.Id);
            await _projectRiskDetailRepository.DeleteAsync(p => p.StaticVariableOption.DinamicVariableId == input.Id);

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable_Enable)]
        public async Task Enable(EntityDto input)
        {
            if (await _dinamicVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var dinamicVariable = await _dinamicVariableRepository.GetAsync(input.Id);

            if (dinamicVariable.Enabled)
                throw new UserFriendlyException("Aviso", "La variable ya se encuentra habilitada, por favor refresque su búsqueda");

            dinamicVariable.Enabled = true;

            await _dinamicVariableRepository.UpdateAsync(dinamicVariable);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable)]
        public async Task<DinamicVariableGetDto> Get(EntityDto input)
        {
            if (await _dinamicVariableRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var dinamicVariable = await _dinamicVariableRepository.GetAsync(input.Id);

            return ObjectMapper.Map<DinamicVariableGetDto>(dinamicVariable);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable)]
        public async Task<PagedResultDto<DinamicVariableGetAllDto>> GetAll(DinamicVariableGetAllInputDto input)
        {
            var query = _dinamicVariableRepository
                .GetAll()
                .Where(p => p.Type == input.Type)
                .LikeAnyBidirectional(input.Filter.SplitByLike(), nameof(DinamicVariable.Name));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DinamicVariableGetAllDto>(count, ObjectMapper.Map<List<DinamicVariableGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable, AppPermissions.Pages_Management_DinamicVariable_Create, AppPermissions.Pages_Management_DinamicVariable_Edit, RequireAllPermissions = false)]
        public async Task<PagedResultDto<DinamicVariableDetailDto>> GetAllDetails(DinamicVariableGetDetailInputDto input)
        {
            var query = (from province in _provinceRepository.GetAll()
                         join department in _departmentRepository.GetAll() on province.Department.Id equals department.Id
                         join detail in _dinamicVariableDetailRepository
                         .GetAll()
                         .WhereIf(input.DinamicVariableId.HasValue == false, p => p.DinamicVariableId == 0)
                         .WhereIf(input.DinamicVariableId.HasValue, p => p.DinamicVariableId == input.DinamicVariableId.Value) on province.Id equals detail.ProvinceId
                         into DinamicVariableDetail
                         from result in DinamicVariableDetail.DefaultIfEmpty()
                         select new DinamicVariableDetailDto()
                         {
                             Department = new DinamicVariableDepartmentDto()
                             {
                                 Id = department.Id,
                                 Name = department.Name
                             },
                             Province = new DinamicVariableProvinceDto()
                             {
                                 Id = province.Id,
                                 Name = province.Name
                             },
                             DinamicVariableId = (result == null ? 0 : result.DinamicVariableId),
                             Id = (result == null ? 0 : result.Id),
                             Value = (result == null ? 0 : result.Value)
                         })
                        .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<DinamicVariableDetailDto, bool>>)
                            (expression => EF.Functions.Like(expression.Department.Name, $"%{word}%") ||
                                           EF.Functions.Like(expression.Province.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            if(count > 0)
            {
                var territorialUnits = _territorialUnitRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnitDepartments)
                    .ToList();

                foreach(var item in output)
                {
                    var avaliableTerritorialUnits = territorialUnits
                        .Where(p => p.TerritorialUnitDepartments
                        .Any(d => d.DepartmentId == item.Department.Id))
                        .ToList();

                    item.TerritorialUnits = ObjectMapper.Map<List<DinamicVariableTerritorialUnitDto>>(avaliableTerritorialUnits);
                }
            }

            return new PagedResultDto<DinamicVariableDetailDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_DinamicVariable_Edit)]
        public async Task<EntityDto> Update(DinamicVariableEntityUpdateDto input)
        {
            if (await _dinamicVariableRepository.CountAsync(p => p.Id == input.Variable.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var variable = ValidateEntity(ObjectMapper.Map(input.Variable, await _dinamicVariableRepository.GetAsync(input.Variable.Id)));

            await _dinamicVariableRepository.UpdateAsync(variable);

            var duplicatesDistrict = input.Changes
                .GroupBy(p => p.Province.Id)
                .Where(p => p.Count() > 1);

            if (duplicatesDistrict.Count() > 0)
                throw new UserFriendlyException("Aviso", "No fue posible guardar el registro debido a inconsistencias en la solicitud");

            foreach (var change in input.Changes.Where(p => p.Value > 0 || p.Id > 0))
            {
                if(change.Id <= 0)
                {
                    if (await _provinceRepository.CountAsync(p => p.Id == change.Province.Id) == 0)
                        throw new UserFriendlyException("Aviso", $"No fue posible guardar el registro debido a que la provincia{change.Province.Name} es inválido o no existe");

                    await _dinamicVariableDetailRepository.InsertOrUpdateAsync(new DinamicVariableDetail()
                    {
                        DinamicVariableId = variable.Id,
                        ProvinceId = change.Province.Id,
                        Value = change.Value
                    });
                } 
                else
                {
                    var dinamicVariableDetail = await _dinamicVariableDetailRepository.GetAsync(change.Id);

                    if (dinamicVariableDetail.DinamicVariableId != variable.Id)
                        throw new UserFriendlyException("Aviso", "No se puede guardar el registro debido a una inconsistencia en los detalles de la solicitud");

                    dinamicVariableDetail.Value = change.Value;

                    await _dinamicVariableDetailRepository.UpdateAsync(dinamicVariableDetail);
                }
            }

            await CurrentUnitOfWork.SaveChangesAsync();
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
            await FunctionManager.CallProjectRiskProcess(AbpSession.ToUserIdentifier());

            return new EntityDto(variable.Id);
        }

        private DinamicVariable ValidateEntity(DinamicVariable input)
        {
            if (input == null)
                throw new UserFriendlyException("Aviso debe ingresar el nombre de la variable antes de continuar");

            if (input.Type == DinamicVariableType.None)
                throw new UserFriendlyException("Aviso", "La información de la variable es inválida");

            input.Name.IsValidOrException(DefaultTitleMessage, "El nombre de la variable es obligatorio");
            input.Name.VerifyTableColumn(DinamicVariableConsts.NameMinLength, DinamicVariableConsts.NameMaxLength, DefaultTitleMessage, $"El nombre de la variable no debe exceder los {DinamicVariableConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
