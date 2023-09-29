using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.SubTypologies;
using Contable.Application.SubTypologies.Dto;
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

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology_SubTypology)]
    public class SubTypologyAppService : ContableAppServiceBase, ISubTypologyAppService
    {
        private readonly IRepository<Typology> _typologyRepository;
        private readonly IRepository<SubTypology> _subTypologyRepository;

        public SubTypologyAppService(IRepository<Typology> TypologyRepository, IRepository<SubTypology> subTypologyRepository)
        {
            _typologyRepository = TypologyRepository;
            _subTypologyRepository = subTypologyRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology_SubTypology_Create)]
        public async Task Create(SubTypologyCreateDto input)
        {
            if (await _typologyRepository.CountAsync(p => p.Id == input.TypologyId) == 0)
                throw new UserFriendlyException("Aviso", "La tipología general seleccionada no existe o ya fue eliminada");

            await _subTypologyRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<SubTypology>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology_SubTypology_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _subTypologyRepository.CountAsync(p => p.Id == input.Id));
            await _subTypologyRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology_SubTypology)]
        public async Task<SubTypologyGetDto> Get(EntityDto input)
        {
            VerifyCount(await _subTypologyRepository.CountAsync(p => p.Id == input.Id));
            return ObjectMapper.Map<SubTypologyGetDto>(await _subTypologyRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Typology_SubTypology_Edit)]
        public async Task Update(SubTypologyUpdateDto input)
        {
            VerifyCount(await _subTypologyRepository.CountAsync(p => p.Id == input.Id));
            await _subTypologyRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _subTypologyRepository.GetAsync(input.Id))));
        }

        private SubTypology ValidateEntity(SubTypology input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la tipología detallada es obligatorio");
            input.Name.VerifyTableColumn(SubTypologyConsts.NameMinLength, SubTypologyConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del tipología detallada no debe exceder los {SubTypologyConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
