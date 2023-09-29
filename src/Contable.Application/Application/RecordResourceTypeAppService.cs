using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.RecordResourceTypes;
using Contable.Application.RecordResourceTypes.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_RecordResourceType)]
    public class RecordResourceTypeAppService: ContableAppServiceBase, IRecordResourceTypeAppService
    {
        private readonly IRepository<RecordResourceType> _recordResourceTypeRepository;

        public RecordResourceTypeAppService(IRepository<RecordResourceType> recordResourceTypeRepository)
        {
            _recordResourceTypeRepository = recordResourceTypeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_RecordResourceType_Create)]
        public async Task Create(RecordResourceTypeCreateDto input)
        {
            await _recordResourceTypeRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<RecordResourceType>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_RecordResourceType_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _recordResourceTypeRepository.CountAsync(p => p.Id == input.Id));

            await _recordResourceTypeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_RecordResourceType)]
        public async Task<RecordResourceTypeGetDto> Get(EntityDto input)
        {
            VerifyCount(await _recordResourceTypeRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<RecordResourceTypeGetDto>(await _recordResourceTypeRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_RecordResourceType)]
        public async Task<PagedResultDto<RecordResourceTypeGetAllDto>> GetAll(RecordResourceTypeGetAllInputDto input)
        {
            var query = _recordResourceTypeRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<RecordResourceType, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<RecordResourceTypeGetAllDto>(count, ObjectMapper.Map<List<RecordResourceTypeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_RecordResourceType_Edit)]
        public async Task Update(RecordResourceTypeUpdateDto input)
        {
            VerifyCount(await _recordResourceTypeRepository.CountAsync(p => p.Id == input.Id));

            await _recordResourceTypeRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _recordResourceTypeRepository.GetAsync(input.Id))));
        }

        private RecordResourceType ValidateEntity(RecordResourceType input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de documento de sustento es obligatorio");
            input.Name.VerifyTableColumn(RecordResourceTypeConsts.NameMinLength, 
                RecordResourceTypeConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del tipo de documento de sustento no debe exceder los {RecordResourceTypeConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
