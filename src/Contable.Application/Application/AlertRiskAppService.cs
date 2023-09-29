using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.AlertRisks;
using Contable.Application.AlertRisks.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertRisk)]
    public class AlertRiskAppService : ContableAppServiceBase, IAlertRiskAppService
    {
        private readonly IRepository<AlertRisk> alertRiskRepository;

        public AlertRiskAppService(IRepository<AlertRisk> AlertRiskRepository)
        {
            alertRiskRepository = AlertRiskRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertRisk_Create)]
        public async Task Create(AlertRiskCreateDto input)
        {
            await alertRiskRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<AlertRisk>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertRisk_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await alertRiskRepository.CountAsync(p => p.Id == input.Id));

            await alertRiskRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertRisk)]
        public async Task<AlertRiskGetDto> Get(EntityDto input)
        {
            VerifyCount(await alertRiskRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<AlertRiskGetDto>(await alertRiskRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertRisk)]
        public async Task<PagedResultDto<AlertRiskGetAllDto>> GetAll(AlertRiskGetAllInputDto input)
        {
            var query = alertRiskRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<AlertRisk, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<AlertRiskGetAllDto>(count, ObjectMapper.Map<List<AlertRiskGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertRisk_Edit)]
        public async Task Update(AlertRiskUpdateDto input)
        {
            VerifyCount(await alertRiskRepository.CountAsync(p => p.Id == input.Id));

            await alertRiskRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await alertRiskRepository.GetAsync(input.Id))));
        }

        private AlertRisk ValidateEntity(AlertRisk input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del riesgo es obligatorio");
            input.Name.VerifyTableColumn(AlertRiskConsts.NameMinLength, AlertRiskConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del riesgo no debe exceder los {AlertRiskConsts.NameMaxLength} caracteres");
            
            input.Color.IsValidOrException(DefaultTitleMessage, $"El color del riesgo es obligatorio");
            input.Color.VerifyTableColumn(AlertRiskConsts.ColorMinLength, AlertRiskConsts.ColorMaxLength, DefaultTitleMessage, $"El color del riesgo no debe exceder los {AlertRiskConsts.ColorMaxLength} caracteres");

            return input;
        }
    }
}
