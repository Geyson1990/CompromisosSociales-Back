using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.Managers;
using Contable.Application.Managers.Dto;
using Contable.Application.Extensions;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Manager)]
    public class ManagerAppService : ContableAppServiceBase, IManagerAppService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<TaskManagementPerson> _taskManagementPerson;
        private readonly EmailAddressAttribute _emailAddressAttribute;

        public ManagerAppService(
            IRepository<Person> personRepository, 
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<TaskManagementPerson> taskManagementPerson)
        {
            _personRepository = personRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _emailAddressAttribute = new EmailAddressAttribute();
            _taskManagementPerson = taskManagementPerson;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Manager_Create)]
        public async Task Create(ManagerCreateDto input)
        {
            var manager = ObjectMapper.Map<Person>(input);
            manager.Type = PersonType.Manager;

            await _personRepository.InsertAsync(await ValidateEntity(
                input: manager,
                territorialUnitId: input.TerritorialUnit == null ? -1 : input.TerritorialUnit.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Manager_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _personRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Manager));

            await _personRepository.DeleteAsync(input.Id);
            await _taskManagementPerson.DeleteAsync(p => p.PersonId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Manager)]
        public async Task<ManagerGetDataDto> Get(NullableIdDto input)
        {
            var output = new ManagerGetDataDto();

            if(input.Id.HasValue)
            {
                VerifyCount(await _personRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Manager));

                output.Manager = ObjectMapper.Map<ManagerGetDto>(_personRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnit)
                    .Where(p => p.Id == input.Id.Value)
                    .First());
            }

            output.TerritorialUnits = ObjectMapper.Map<List<ManagerTerritorialUnitDto>>(_territorialUnitRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .ToList());

            return output;  
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Manager)]
        public async Task<PagedResultDto<ManagerGetAllDto>> GetAll(ManagerGetAllInputDto input)
        {
            var query = _personRepository
                .GetAll()
                .Include(p => p.TerritorialUnit)
                .Where(p => p.Type == PersonType.Manager)
                .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<Person, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ManagerGetAllDto>(count, ObjectMapper.Map<List<ManagerGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Manager_Edit)]
        public async Task Update(ManagerUpdateDto input)
        {
            VerifyCount(await _personRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Manager));
            await _personRepository.UpdateAsync(await ValidateEntity(
                input: ObjectMapper.Map(input, await _personRepository.GetAsync(input.Id)),
                territorialUnitId: input.TerritorialUnit == null ? -1 : input.TerritorialUnit.Id));
        }

        private async Task<Person> ValidateEntity(Person input, int territorialUnitId)
        {
            input.Names = (input.Names ?? "").Trim().ToUpper();
            input.Surname = (input.Surname ?? "").Trim().ToUpper();
            input.Surname2 = (input.Surname2 ?? "").Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(input.Document) == false)
                input.Document.VerifyTableColumn(PersonConsts.DocumentMinLength, PersonConsts.DocumentMaxLength, DefaultTitleMessage, $"El DNI del gestor debe tener {PersonConsts.DocumentMaxLength} caracteres");

            input.Names.IsValidOrException(DefaultTitleMessage, $"El nombre del gestor es obligatorio");
            input.Names.VerifyTableColumn(PersonConsts.NamesMinLength, PersonConsts.NamesMaxLength, DefaultTitleMessage, $"El nombre del gestor no debe exceder los {PersonConsts.NamesMaxLength} caracteres");

            input.Surname.IsValidOrException(DefaultTitleMessage, $"El apellido paterno del gestor es obligatorio");
            input.Surname.VerifyTableColumn(PersonConsts.SurnameMinLength, PersonConsts.SurnameMaxLength, DefaultTitleMessage, $"El apellido paterno del gestor no debe exceder los {PersonConsts.SurnameMaxLength} caracteres");

            input.Surname2.VerifyTableColumn(PersonConsts.Surname2MinLength, PersonConsts.Surname2MaxLength, DefaultTitleMessage, $"El apellido materno del gestor no debe exceder los {PersonConsts.Surname2MaxLength} caracteres");

            input.Name = Regex.Replace(string.Concat((input.Surname ?? "").Trim().ToUpper(), " ", (input.Surname2 ?? "").Trim().ToUpper(), ", ", (input.Names ?? "").Trim().ToUpper()).Replace(" ,", ","), @"\s+", " ");

            if (string.IsNullOrWhiteSpace(input.EmailAddress) == false && _emailAddressAttribute.IsValid(input.EmailAddress) == false)
                throw new UserFriendlyException("Aviso", $"El correo electrónico {input.EmailAddress} del gestor es inválido");

            if (territorialUnitId > 0)
            {
                if (await _territorialUnitRepository.CountAsync(p => p.Id == territorialUnitId) == 0)
                    throw new UserFriendlyException("Aviso", "La Unidad Territorial seleccionada ya no existe o fue eliminada. Verifique la información antes de continuar");

                var territorialUnit = await _territorialUnitRepository.GetAsync(territorialUnitId);

                input.TerritorialUnit = territorialUnit;
                input.TerritorialUnitId = territorialUnit.Id;
            }
            else
            {
                input.TerritorialUnit = null;
                input.TerritorialUnitId = null;
            }

            return input;
        }
    }
}
