using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.AlertDemands;
using Contable.Application.AlertDemands.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertDemand)]
    public class AlertDemandAppService : ContableAppServiceBase, IAlertDemandAppService
    {
        private readonly IRepository<AlertDemand> _alertDemandRepository;

        public AlertDemandAppService(IRepository<AlertDemand> alertDemandRepository)
        {
            _alertDemandRepository = alertDemandRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertDemand_Create)]
        public async Task Create(AlertDemandCreateDto input)
        {
            await _alertDemandRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<AlertDemand>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertDemand_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _alertDemandRepository.CountAsync(p => p.Id == input.Id));

            await _alertDemandRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertDemand)]
        public async Task<AlertDemandGetDto> Get(EntityDto input)
        {
            VerifyCount(await _alertDemandRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<AlertDemandGetDto>(await _alertDemandRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertDemand)]
        public async Task<PagedResultDto<AlertDemandGetAllDto>> GetAll(AlertDemandGetAllInputDto input)
        {
            var query = _alertDemandRepository
                .GetAll()
                .LikeAllBidirectional(input.Filter.SplitByLike()
                    .Select(word => (Expression<Func<AlertDemand, bool>>) (expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<AlertDemandGetAllDto>(count, ObjectMapper.Map<List<AlertDemandGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertDemand_Edit)]
        public async Task Update(AlertDemandUpdateDto input)
        {
            VerifyCount(await _alertDemandRepository.CountAsync(p => p.Id == input.Id));

            await _alertDemandRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _alertDemandRepository.GetAsync(input.Id))));
        }

        private AlertDemand ValidateEntity(AlertDemand input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de demanda es obligatorio");
            input.Name.VerifyTableColumn(AlertDemandConsts.NameMinLength, AlertDemandConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del tipo de demanda no debe exceder los {AlertDemandConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
