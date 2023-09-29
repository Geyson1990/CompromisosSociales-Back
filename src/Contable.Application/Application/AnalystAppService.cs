using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Contable.Application.Analysts;
using Contable.Application.Analysts.Dto;
using Contable.Application.Extensions;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Analyst)]
    public class AnalystAppService: ContableAppServiceBase, IAnalystAppService
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<TaskManagementPerson> _taskManagementPerson;
        private readonly EmailAddressAttribute _emailAddressAttribute;

        public AnalystAppService(
            IRepository<Person> personRepository, 
            IRepository<TaskManagementPerson> taskManagementPerson)
        {
            _personRepository = personRepository;
            _taskManagementPerson = taskManagementPerson;
            _emailAddressAttribute = new EmailAddressAttribute();
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Analyst_Create)]
        public async Task Create(AnalystCreateDto input)
        {
            var analyst = ObjectMapper.Map<Person>(input);
            analyst.Type = PersonType.Analyst;

            await _personRepository.InsertAsync(ValidateEntity(analyst));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Analyst_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _personRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Analyst));

            await _personRepository.DeleteAsync(input.Id);
            await _taskManagementPerson.DeleteAsync(p => p.PersonId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Analyst)]
        public async Task<AnalystGetDto> Get(EntityDto input)
        {
            VerifyCount(await _personRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Analyst));

            return ObjectMapper.Map<AnalystGetDto>(await _personRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Analyst)]
        public async Task<PagedResultDto<AnalystGetAllDto>> GetAll(AnalystGetAllInputDto input)
        {
            var query = _personRepository
                .GetAll()
                .Where(p => p.Type == PersonType.Analyst)
                .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<Person, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<AnalystGetAllDto>(count, ObjectMapper.Map<List<AnalystGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Analyst_Edit)]
        public async Task Update(AnalystUpdateDto input)
        {
            VerifyCount(await _personRepository.CountAsync(p => p.Id == input.Id && p.Type == PersonType.Analyst));

            await _personRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _personRepository.GetAsync(input.Id))));
        }

        private Person ValidateEntity(Person input)
        {
            input.Names = (input.Names ?? "").Trim().ToUpper();
            input.Surname = (input.Surname ?? "").Trim().ToUpper();
            input.Surname2 = (input.Surname2 ?? "").Trim().ToUpper();
            
            if (string.IsNullOrWhiteSpace(input.Document) == false)
                input.Document.VerifyTableColumn(PersonConsts.DocumentMinLength, PersonConsts.DocumentMaxLength, DefaultTitleMessage, $"El DNI del analista debe tener {PersonConsts.DocumentMaxLength} caracteres");

            input.Names.IsValidOrException(DefaultTitleMessage, $"El nombre del analista es obligatorio");
            input.Names.VerifyTableColumn(PersonConsts.NamesMinLength, PersonConsts.NamesMaxLength, DefaultTitleMessage, $"El nombre del analista no debe exceder los {PersonConsts.NamesMaxLength} caracteres");

            input.Surname.IsValidOrException(DefaultTitleMessage, $"El apellido paterno del analista es obligatorio");
            input.Surname.VerifyTableColumn(PersonConsts.SurnameMinLength, PersonConsts.SurnameMaxLength, DefaultTitleMessage, $"El apellido paterno del analista no debe exceder los {PersonConsts.SurnameMaxLength} caracteres");

            input.Surname2.VerifyTableColumn(PersonConsts.Surname2MinLength, PersonConsts.Surname2MaxLength, DefaultTitleMessage, $"El apellido materno del analista no debe exceder los {PersonConsts.Surname2MaxLength} caracteres");

            input.Name = Regex.Replace(string.Concat((input.Surname ?? "").Trim().ToUpper(), " ", (input.Surname2 ?? "").Trim().ToUpper(), ", ", (input.Names ?? "").Trim().ToUpper()).Replace(" ,", ","), @"\s+", " ");

            if (string.IsNullOrWhiteSpace(input.EmailAddress) == false && _emailAddressAttribute.IsValid(input.EmailAddress) == false)
                throw new UserFriendlyException("Aviso", $"El correo electrónico {input.EmailAddress} del analista es inválido");

            return input;
        }
    }
}
