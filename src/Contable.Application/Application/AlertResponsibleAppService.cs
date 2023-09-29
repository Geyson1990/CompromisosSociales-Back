using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.AlertResponsibles;
using Contable.Application.AlertResponsibles.Dto;
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

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertResponsible)]
    public class AlertResponsibleAppService : ContableAppServiceBase, IAlertResponsibleAppService
    {
        private readonly IRepository<AlertResponsible> _alertResponsibleRepository;

        public AlertResponsibleAppService(IRepository<AlertResponsible> alertResponsibleRepository)
        {
            _alertResponsibleRepository = alertResponsibleRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertResponsible_Create)]
        public async Task Create(AlertResponsibleCreateDto input)
        {
            await _alertResponsibleRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<AlertResponsible>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertResponsible_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _alertResponsibleRepository.CountAsync(p => p.Id == input.Id));

            await _alertResponsibleRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertResponsible)]
        public async Task<AlertResponsibleGetDto> Get(EntityDto input)
        {
            VerifyCount(await _alertResponsibleRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<AlertResponsibleGetDto>(await _alertResponsibleRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertResponsible)]
        public async Task<PagedResultDto<AlertResponsibleGetAllDto>> GetAll(AlertResponsibleGetAllInputDto input)
        {
            var query = _alertResponsibleRepository
                .GetAll()
                .LikeAllBidirectional(input.Filter.SplitByLike()
                    .Select(word => (Expression<Func<AlertResponsible, bool>>) (expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<AlertResponsibleGetAllDto>(count, ObjectMapper.Map<List<AlertResponsibleGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertResponsible_Edit)]
        public async Task Update(AlertResponsibleUpdateDto input)
        {
            VerifyCount(await _alertResponsibleRepository.CountAsync(p => p.Id == input.Id));

            await _alertResponsibleRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _alertResponsibleRepository.GetAsync(input.Id))));
        }

        private AlertResponsible ValidateEntity(AlertResponsible input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la subsecretaría responsable es obligatorio");
            input.Name.VerifyTableColumn(AlertResponsibleConsts.NameMinLength, 
                AlertResponsibleConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre de la subsecretaría responsable no debe exceder los {AlertResponsibleConsts.NameMaxLength} caracteres");

            input.ShortName.IsValidOrException(DefaultTitleMessage, $"El nombre corto de la subsecretaría responsable es obligatorio");
            input.ShortName.VerifyTableColumn(AlertResponsibleConsts.ShortNameMinLength, 
                AlertResponsibleConsts.ShortNameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre corto de la subsecretaría responsable no debe exceder los {AlertResponsibleConsts.ShortNameMaxLength} caracteres");

            return input;
        }
    }
}
