using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.Managements;
using Contable.Application.Managements.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Management)]
    public class ManagementAppService: ContableAppServiceBase, IManagementAppService
    {
        private readonly IRepository<Management> _managementRepository;

        public ManagementAppService(IRepository<Management> managementRepository)
        {
            _managementRepository = managementRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Management_Create)]
        public async Task Create(ManagementCreateDto input)
        {
            await _managementRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<Management>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Management_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _managementRepository.CountAsync(p => p.Id == input.Id));

            await _managementRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Management)]
        public async Task<ManagementGetDto> Get(EntityDto input)
        {
            VerifyCount(await _managementRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<ManagementGetDto>(await _managementRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Management)]
        public async Task<PagedResultDto<ManagementGetAllDto>> GetAll(ManagementGetAllInputDto input)
        {
            var query = _managementRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<Management, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ManagementGetAllDto>(count, ObjectMapper.Map<List<ManagementGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Management_Edit)]
        public async Task Update(ManagementUpdateDto input)
        {
            VerifyCount(await _managementRepository.CountAsync(p => p.Id == input.Id));

            await _managementRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _managementRepository.GetAsync(input.Id))));
        }

        private Management ValidateEntity(Management input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de gestión es obligatorio");
            input.Name.VerifyTableColumn(ManagementConsts.NameMinLength, ManagementConsts.NameMaxLength, DefaultTitleMessage, $"El nombre del tipo de gestión no debe exceder los {ManagementConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
