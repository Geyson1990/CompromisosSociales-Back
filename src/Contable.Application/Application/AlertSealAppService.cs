using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.AlertSeals;
using Contable.Application.AlertSeals.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSeal)]
    public class AlertSealAppService : ContableAppServiceBase, IAlertSealAppService
    {
        private readonly IRepository<AlertSeal> _alertSealRepository;

        public AlertSealAppService(IRepository<AlertSeal> alertSealRepository)
        {
            _alertSealRepository = alertSealRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSeal_Create)]
        public async Task Create(AlertSealCreateDto input)
        {
            await _alertSealRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<AlertSeal>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSeal_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _alertSealRepository.CountAsync(p => p.Id == input.Id));

            await _alertSealRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSeal)]
        public async Task<AlertSealGetDto> Get(EntityDto input)
        {
            VerifyCount(await _alertSealRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<AlertSealGetDto>(await _alertSealRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSeal)]
        public async Task<PagedResultDto<AlertSealGetAllDto>> GetAll(AlertSealGetAllInputDto input)
        {
            var query = _alertSealRepository
                .GetAll()
                .LikeAllBidirectional(input.Filter.SplitByLike()
                    .Select(word => (Expression<Func<AlertSeal, bool>>) (expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<AlertSealGetAllDto>(count, ObjectMapper.Map<List<AlertSealGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSeal_Edit)]
        public async Task Update(AlertSealUpdateDto input)
        {
            VerifyCount(await _alertSealRepository.CountAsync(p => p.Id == input.Id));

            await _alertSealRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _alertSealRepository.GetAsync(input.Id))));
        }

        private AlertSeal ValidateEntity(AlertSeal input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del cierre de alerta es obligatorio");
            input.Name.VerifyTableColumn(AlertSealConsts.NameMinLength, AlertSealConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del cierre de alerta no debe exceder los {AlertSealConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
