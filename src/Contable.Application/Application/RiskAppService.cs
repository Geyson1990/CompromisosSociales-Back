using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.Risks;
using Contable.Application.Risks.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Risk)]
    public class RiskAppService: ContableAppServiceBase, IRiskAppService
    {
        private readonly IRepository<Risk> riskRepository;
        public RiskAppService(IRepository<Risk> RiskRepository)
        {
            riskRepository = RiskRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Risk_Create)]
        public async Task Create(RiskCreateDto input)
        {
            await riskRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<Risk>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Risk_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await riskRepository.CountAsync(p => p.Id == input.Id));

            await riskRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Risk)]
        public async Task<RiskGetDto> Get(EntityDto input)
        {
            VerifyCount(await riskRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<RiskGetDto>(await riskRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Risk)]
        public async Task<PagedResultDto<RiskGetAllDto>> GetAll(RiskGetAllInputDto input)
        {
            var query = riskRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<Risk, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<RiskGetAllDto>(count, ObjectMapper.Map<List<RiskGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Risk_Edit)]
        public async Task Update(RiskUpdateDto input)
        {
            VerifyCount(await riskRepository.CountAsync(p => p.Id == input.Id));

            await riskRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await riskRepository.GetAsync(input.Id))));
        }

        private Risk ValidateEntity(Risk input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del riesgo es obligatorio");
            input.Name.VerifyTableColumn(RiskConsts.NameMinLength, RiskConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del riesgo no debe exceder los {RiskConsts.NameMaxLength} caracteres");
            
            input.Color.IsValidOrException(DefaultTitleMessage, $"El color del riesgo es obligatorio");
            input.Color.VerifyTableColumn(RiskConsts.ColorMinLength, RiskConsts.ColorMaxLength, DefaultTitleMessage, $"El color del riesgo no debe exceder los {RiskConsts.ColorMaxLength} caracteres");

            return input;
        }
    }
}
