using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.AlertSectors;
using Contable.Application.AlertSectors.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSector)]
    public class AlertSectorAppService : ContableAppServiceBase, IAlertSectorAppService
    {
        private readonly IRepository<AlertSector> _alertSectorRepository;

        public AlertSectorAppService(IRepository<AlertSector> alertSectorRepository)
        {
            _alertSectorRepository = alertSectorRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSector_Create)]
        public async Task Create(AlertSectorCreateDto input)
        {
            await _alertSectorRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<AlertSector>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSector_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _alertSectorRepository.CountAsync(p => p.Id == input.Id));

            await _alertSectorRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSector)]
        public async Task<AlertSectorGetDto> Get(EntityDto input)
        {
            VerifyCount(await _alertSectorRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<AlertSectorGetDto>(await _alertSectorRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSector)]
        public async Task<PagedResultDto<AlertSectorGetAllDto>> GetAll(AlertSectorGetAllInputDto input)
        {
            var query = _alertSectorRepository
                .GetAll()
                .LikeAllBidirectional(input.Filter.SplitByLike()
                    .Select(word => (Expression<Func<AlertSector, bool>>) (expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<AlertSectorGetAllDto>(count, ObjectMapper.Map<List<AlertSectorGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_AlertSector_Edit)]
        public async Task Update(AlertSectorUpdateDto input)
        {
            VerifyCount(await _alertSectorRepository.CountAsync(p => p.Id == input.Id));

            await _alertSectorRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _alertSectorRepository.GetAsync(input.Id))));
        }

        private AlertSector ValidateEntity(AlertSector input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del alertSector es obligatorio");
            input.Name.VerifyTableColumn(AlertSectorConsts.NameMinLength, AlertSectorConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del alertSector no debe exceder los {AlertSectorConsts.NameMaxLength} caracteres");

            return input;
        }
    }
}
