using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Contable.Application.Coordinators;
using Contable.Application.Coordinators.Dto;
using Contable.Application.Extensions;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator)]
    public class CoordinatorAppService : ContableAppServiceBase, ICoordinatorAppService
    {
        private readonly IRepository<Person> _coordinatorRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<TerritorialUnitCoordinator> _territorialUnitCoordinatorRepository;
        private readonly IRepository<TaskManagementPerson> _taskManagementPerson;
        private readonly EmailAddressAttribute _emailAddressAttribute;

        public CoordinatorAppService(
            IRepository<Person> coordinatorRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<TerritorialUnitCoordinator> territorialUnitCoordinatorRepository,
            IRepository<TaskManagementPerson> taskManagementPerson)
        {
            _coordinatorRepository = coordinatorRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _territorialUnitCoordinatorRepository = territorialUnitCoordinatorRepository;
            _emailAddressAttribute = new EmailAddressAttribute();
            _taskManagementPerson = taskManagementPerson;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator)]
        public async Task AddTerritorialUnit(CoordinatorAddTerritorialUnitDto input)
        {
            if(await _coordinatorRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Coordinator) == 0)
                throw new UserFriendlyException("Aviso", "El coordinador seleccionado ya no se encuentra disponible. Por favor verifique la información antes de continuar.");
            if (await _territorialUnitRepository.CountAsync(p => p.Id == input.TerritorialUnitId) == 0)
                throw new UserFriendlyException("Aviso", "La unidad territorial seleccionada ya no se encuentra disponible. Por favor verifique la información antes de continuar.");

            if (await _territorialUnitCoordinatorRepository.CountAsync(p => p.PersonId == input.Id && p.TerritorialUnitId == input.TerritorialUnitId) == 0)
            {
                var dbCoordinator = await _coordinatorRepository.GetAsync(input.Id);
                var dbTerritorialUnit = await _territorialUnitRepository.GetAsync(input.TerritorialUnitId);

                await _territorialUnitCoordinatorRepository.InsertAsync(new TerritorialUnitCoordinator()
                {
                    Person = dbCoordinator,
                    PersonId = dbCoordinator.Id,
                    TerritorialUnit = dbTerritorialUnit,
                    TerritorialUnitId = dbTerritorialUnit.Id
                });
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator_Create)]
        public async Task Create(CoordinatorCreateDto input)
        {
            var coordinator = ObjectMapper.Map<Person>(input);
            coordinator.Type = PersonType.Coordinator;

            await _coordinatorRepository.InsertAsync(ValidateEntity(coordinator));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _coordinatorRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Coordinator));

            await _coordinatorRepository.DeleteAsync(input.Id);
            await _taskManagementPerson.DeleteAsync(p => p.PersonId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator)]
        public async Task DeleteTerritorialUnit(EntityDto input)
        {
            VerifyCount(await _territorialUnitCoordinatorRepository.CountAsync(p => p.Id == input.Id));

            await _territorialUnitCoordinatorRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator)]
        public async Task<CoordinatorGetDto> Get(EntityDto input)
        {
            VerifyCount(await _coordinatorRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<CoordinatorGetDto>(await _coordinatorRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator)]
        public async Task<PagedResultDto<CoordinatorGetAllDto>> GetAll(CoordinatorGetAllInputDto input)
        {
            var query = _coordinatorRepository
                .GetAll()
                .Include(p => p.TerritorialUnits)
                .ThenInclude(p => p.TerritorialUnit)
                .Where(p => p.Type == PersonType.Coordinator)
                .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<Person, bool>>)
                    (expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<CoordinatorGetAllDto>(count, ObjectMapper.Map<List<CoordinatorGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator)]
        public async Task<PagedResultDto<CoordinatorTerritorialUnitGetAllDto>> GetAllTerritorialUnits(CoordinatorTerritorialUnitGetAllInputDto input)
        {
            var query = _territorialUnitRepository
                .GetAll()
                .Where(p => p.Coordinators.Any(d => d.PersonId == input.CoordinatorId) == false)
                .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<TerritorialUnit, bool>>)
                    (expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<CoordinatorTerritorialUnitGetAllDto>(count, ObjectMapper.Map<List<CoordinatorTerritorialUnitGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Coordinator_Edit)]
        public async Task Update(CoordinatorUpdateDto input)
        {
            VerifyCount(await _coordinatorRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Coordinator));

            await _coordinatorRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _coordinatorRepository.GetAsync(input.Id))));
        }

        private Person ValidateEntity(Person input)
        {
            input.Names = (input.Names ?? "").Trim().ToUpper();
            input.Surname = (input.Surname ?? "").Trim().ToUpper();
            input.Surname2 = (input.Surname2 ?? "").Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(input.Document) == false)
                input.Document.VerifyTableColumn(PersonConsts.DocumentMinLength, PersonConsts.DocumentMaxLength, DefaultTitleMessage, $"El DNI del coordinador debe tener {PersonConsts.DocumentMaxLength} caracteres");

            input.Names.IsValidOrException(DefaultTitleMessage, $"El nombre del coordinador es obligatorio");
            input.Names.VerifyTableColumn(PersonConsts.NamesMinLength, PersonConsts.NamesMaxLength, DefaultTitleMessage, $"El nombre del coordinador no debe exceder los {PersonConsts.NamesMaxLength} caracteres");

            input.Surname.IsValidOrException(DefaultTitleMessage, $"El apellido paterno del coordinador es obligatorio");
            input.Surname.VerifyTableColumn(PersonConsts.SurnameMinLength, PersonConsts.SurnameMaxLength, DefaultTitleMessage, $"El apellido paterno del coordinador no debe exceder los {PersonConsts.SurnameMaxLength} caracteres");

            input.Surname2.VerifyTableColumn(PersonConsts.Surname2MinLength, PersonConsts.Surname2MaxLength, DefaultTitleMessage, $"El apellido materno del coordinador no debe exceder los {PersonConsts.Surname2MaxLength} caracteres");

            input.Name = Regex.Replace(string.Concat((input.Surname ?? "").Trim().ToUpper(), " ", (input.Surname2 ?? "").Trim().ToUpper(), ", ", (input.Names ?? "").Trim().ToUpper()).Replace(" ,", ","), @"\s+", " ");

            if (string.IsNullOrWhiteSpace(input.EmailAddress) == false && _emailAddressAttribute.IsValid(input.EmailAddress) == false)
                throw new UserFriendlyException("Aviso", $"El correo electrónico {input.EmailAddress} del coordinador es inválido");

            return input;
        }
    }
}
