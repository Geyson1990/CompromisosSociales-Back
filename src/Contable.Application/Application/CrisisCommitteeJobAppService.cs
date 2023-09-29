using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.CrisisCommitteeJobs;
using Contable.Application.CrisisCommitteeJobs.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_CrisisCommitteeJob)]
    public class CrisisCommitteeJobAppService : ContableAppServiceBase, ICrisisCommitteeJobAppService
    {
        private readonly IRepository<CrisisCommitteeJob> _crisisCommitteeJobRepository;

        public CrisisCommitteeJobAppService(IRepository<CrisisCommitteeJob> crisisCommitteeJobRepository)
        {
            _crisisCommitteeJobRepository = crisisCommitteeJobRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CrisisCommitteeJob_Create)]
        public async Task Create(CrisisCommitteeJobCreateDto input)
        {
            await _crisisCommitteeJobRepository.InsertAsync(ValidateActivity(ObjectMapper.Map<CrisisCommitteeJob>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CrisisCommitteeJob_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _crisisCommitteeJobRepository.CountAsync(p => p.Id == input.Id));

            await _crisisCommitteeJobRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CrisisCommitteeJob)]
        public async Task<CrisisCommitteeJobGetDto> Get(EntityDto input)
        {
            VerifyCount(await _crisisCommitteeJobRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<CrisisCommitteeJobGetDto>(await _crisisCommitteeJobRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CrisisCommitteeJob)]
        public async Task<PagedResultDto<CrisisCommitteeJobGetAllDto>> GetAll(CrisisCommitteeJobGetAllInputDto input)
        {
            var query = _crisisCommitteeJobRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<CrisisCommitteeJob, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<CrisisCommitteeJobGetAllDto>(count, ObjectMapper.Map<List<CrisisCommitteeJobGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_CrisisCommitteeJob_Edit)]
        public async Task Update(CrisisCommitteeJobUpdateDto input)
        {
            VerifyCount(await _crisisCommitteeJobRepository.CountAsync(p => p.Id == input.Id));

            await _crisisCommitteeJobRepository.UpdateAsync(ValidateActivity(ObjectMapper.Map(input, await _crisisCommitteeJobRepository.GetAsync(input.Id))));
        }

        private CrisisCommitteeJob ValidateActivity(CrisisCommitteeJob input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre comité de crisis es obligatorio");
            input.Name.VerifyTableColumn(
                CrisisCommitteeJobConsts.NameMinLength, 
                CrisisCommitteeJobConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del comité de crisis no debe exceder los {CrisisCommitteeJobConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
