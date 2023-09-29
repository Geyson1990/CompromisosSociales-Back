using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Contable.Application.Extensions;
using Contable.Application.TerritorialUnits;
using Contable.Application.TerritorialUnits.Dto;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.UI;
using NUglify.Helpers;
using System.Linq.Expressions;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit)]
    public class TerritorialUnitAppService : ContableAppServiceBase, ITerritorialUnitAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<TerritorialUnitDepartment> _territorialUnitDepartmentRepository;
        private readonly IRepository<TerritorialUnitCoordinator> _territorialUnitCoordinatorRepository;
        private readonly IRepository<Person> _personRepository;

        public TerritorialUnitAppService(
            IRepository<Department> departmentRepository, 
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<TerritorialUnitDepartment> territorialUnitDepartmentRepository,
            IRepository<TerritorialUnitCoordinator> territorialUnitCoordinatorRepository,
            IRepository<Person> personRepository)
        {
            _departmentRepository = departmentRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _territorialUnitDepartmentRepository = territorialUnitDepartmentRepository;
            _territorialUnitCoordinatorRepository = territorialUnitCoordinatorRepository;
            _personRepository = personRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit_Edit)]
        public async Task AddDepartment(TerritorialUnitAddDepartmentDto input)
        {
            VerifyCount(await _departmentRepository.CountAsync(p => p.Id == input.DepartmentId));

            if (await _territorialUnitDepartmentRepository.CountAsync(p => p.TerritorialUnit.Id == input.Id && p.Department.Id == input.DepartmentId) > 0)
                throw new UserFriendlyException(DefaultTitleMessage, $"El departamento ya se encuentra asociado a la unidad territorial seleccionada");

            var territorialUnit = await _territorialUnitRepository.GetAsync(input.Id);
            var department = await _departmentRepository.GetAsync(input.DepartmentId);

            await _territorialUnitDepartmentRepository.InsertAsync(new TerritorialUnitDepartment()
            {
                Department = department,
                TerritorialUnit = territorialUnit
            });
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit_Create)]
        public async Task Create(TerritorialUnitCreateDto input)
        {
            await _territorialUnitRepository.InsertAsync(await ValidateEntity(
                territorialUnit: ObjectMapper.Map<TerritorialUnit>(input),
                coordinators: input.Coordinators));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _territorialUnitRepository.CountAsync(p => p.Id == input.Id));

            await _territorialUnitRepository.DeleteAsync(input.Id);
            await _territorialUnitDepartmentRepository.DeleteAsync(p => p.TerritorialUnit.Id == input.Id);
        }

        public async Task DeleteDepartmentUnit(EntityDto input)
        {
            VerifyCount(await _territorialUnitDepartmentRepository.CountAsync(p => p.Id == input.Id));
            await _territorialUnitDepartmentRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit)]
        public async Task<TerritorialUnitGetDto> Get(EntityDto input)
        {
            VerifyCount(await _territorialUnitRepository.CountAsync(p => p.Id == input.Id));

            var territorialUnit = ObjectMapper.Map<TerritorialUnitGetDto>(_territorialUnitRepository
                .GetAll()
                .Include(p => p.Coordinators)
                .ThenInclude(p => p.Person)
                .Where(p => p.Id == input.Id)
                .First());

            if(territorialUnit.Coordinators.Any(p => p.Person == null))
            {
                territorialUnit.Coordinators = territorialUnit
                    .Coordinators
                    .Where(p => p.Person != null)
                    .ToList();

                var dbDeletedPersons = territorialUnit
                    .Coordinators
                    .Where(p => p.Person == null);

                foreach(var dbDeletedPerson in dbDeletedPersons)
                {
                    await _territorialUnitCoordinatorRepository.DeleteAsync(p => p.Id == dbDeletedPerson.Id);
                }
            }

            return territorialUnit;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit)]
        public async Task<PagedResultDto<TerritorialUnitGetAllDto>> GetAll(TerritorialUnitGetAllInputDto input)
        {
            var query = _territorialUnitRepository
                .GetAll()
                .Include(p => p.TerritorialUnitDepartments)
                .ThenInclude(p => p.Department)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(TerritorialUnit.Filter));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<TerritorialUnitGetAllDto>(count, ObjectMapper.Map<List<TerritorialUnitGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit)]
        public async Task<PagedResultDto<TerritorialUnitCoordinatorGetAllDto>> GetAllPersons(TerritorialUnitCoordinatorGetAllInputDto input)
        {
            var query = _personRepository
                .GetAll()
                .Where(p => p.Type == PersonType.Coordinator)
                .WhereIf(input.TerritorialUnitId.HasValue, p => p.TerritorialUnits.Any(p => p.TerritorialUnitId == input.TerritorialUnitId.Value) == false)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<Person, bool>>)(expression =>
                        EF.Functions.Like(expression.Name, $"%{word}%") ||
                        EF.Functions.Like(expression.EmailAddress, $"%{word}%")))
                    .ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<TerritorialUnitCoordinatorGetAllDto>(count, ObjectMapper.Map<List<TerritorialUnitCoordinatorGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit)]
        public async Task<PagedResultDto<TerritorialUnitDepartmentDto>> GetAllDepartments(TerritorialUnitGetAllDepartmentInputDto input)
        {
            var query = _departmentRepository
                .GetAll()
                .Where(p => !p.TerritorialUnitDepartments.Any(p => p.TerritorialUnit.Id == input.TerritorialUnitId))
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(Department.Filter));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<TerritorialUnitDepartmentDto>(count, ObjectMapper.Map<List<TerritorialUnitDepartmentDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_TerritorialUnit_Edit)]
        public async Task Update(TerritorialUnitUpdateDto input)
        {
            await _territorialUnitRepository.UpdateAsync(await ValidateEntity(
                territorialUnit: ObjectMapper.Map<TerritorialUnit>(input),
                coordinators: input.Coordinators));
        }

        private async Task<TerritorialUnit> ValidateEntity(TerritorialUnit territorialUnit, List<TerritorialUnitCoordinatorRelationDto> coordinators)
        {
            if (await _territorialUnitRepository.CountAsync(p => p.Name == territorialUnit.Name && p.Id != territorialUnit.Id) > 0)
                throw new UserFriendlyException(DefaultTitleMessage, "Ya existe una unidad territorial con el mismo nombre");

            territorialUnit.Name.IsValidOrException(DefaultTitleMessage, "El nombre es obligatorio");
            territorialUnit.Name.VerifyTableColumn(TerritorialUnitConsts.NameMinLength, 
                TerritorialUnitConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre no debe exceder los {TerritorialUnitConsts.NameMaxLength} caracteres");

            territorialUnit.Filter = territorialUnit.Name;
            territorialUnit.Coordinators = new List<TerritorialUnitCoordinator>();

            if (coordinators == null)
                coordinators = new List<TerritorialUnitCoordinatorRelationDto>();

            foreach(var coordinator in coordinators.DistinctBy(p => p.Person.Id))
            {

                if (coordinator.Remove)
                {
                    if (coordinator.Id > 0 && territorialUnit.Id > 0 && await _territorialUnitCoordinatorRepository.CountAsync(p => p.Id == coordinator.Id && p.TerritorialUnitId == territorialUnit.Id) > 0)
                    {
                        await _territorialUnitCoordinatorRepository.DeleteAsync(coordinator.Id);
                    }
                }
                else
                {
                    var dbCoordinator = _personRepository
                        .GetAll()
                        .Where(p => p.Id == coordinator.Person.Id)
                        .FirstOrDefault();

                    if (dbCoordinator != null && dbCoordinator.Type == PersonType.Coordinator && 
                       (territorialUnit.Id == 0 || await _territorialUnitCoordinatorRepository.CountAsync(p => p.PersonId == dbCoordinator.Id && p.TerritorialUnitId == territorialUnit.Id) == 0))
                    {
                        territorialUnit.Coordinators.Add(new TerritorialUnitCoordinator()
                        {
                            Person = dbCoordinator,
                            PersonId = dbCoordinator.Id
                        });
                    }
                }
            }

            return territorialUnit;
        }
    }
}
