using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.ResponsibleTypes;
using Contable.Application.ResponsibleTypes.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType)]
    public class ResponsibleTypeAppService: ContableAppServiceBase, IResponsibleTypeAppService
    {
        private readonly IRepository<ResponsibleType> _responsibleTypeRepository;
        private readonly IRepository<ResponsibleSubType> _responsibleSubTypeRepository;

        public ResponsibleTypeAppService(
            IRepository<ResponsibleType> responsibleTypeRepository, 
            IRepository<ResponsibleSubType> responsibleSubTypeRepository)
        {
            _responsibleTypeRepository = responsibleTypeRepository;
            _responsibleSubTypeRepository = responsibleSubTypeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType_Create)]
        public async Task Create(ResponsibleTypeCreateDto input)
        {
            await _responsibleTypeRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<ResponsibleType>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _responsibleTypeRepository.CountAsync(p => p.Id == input.Id));

            await _responsibleTypeRepository.DeleteAsync(p => p.Id == input.Id);
            await _responsibleSubTypeRepository.DeleteAsync(p => p.ResponsibleTypeId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType)]
        public async Task<ResponsibleTypeGetDto> Get(EntityDto input)
        {
            VerifyCount(await _responsibleTypeRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<ResponsibleTypeGetDto>(await _responsibleTypeRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType)]
        public async Task<PagedResultDto<ResponsibleTypeGetAllDto>> GetAll(ResponsibleTypeGetAllInputDto input)
        {
            var query = _responsibleTypeRepository
                .GetAll()
                .Include(p => p.SubTypes)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<ResponsibleType, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ResponsibleTypeGetAllDto>(count, ObjectMapper.Map<List<ResponsibleTypeGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_ResponsibleType_Edit)]
        public async Task Update(ResponsibleTypeUpdateDto input)
        {
            VerifyCount(await _responsibleTypeRepository.CountAsync(p => p.Id == input.Id));

            await _responsibleTypeRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _responsibleTypeRepository.GetAsync(input.Id))));
        }

        private ResponsibleType ValidateEntity(ResponsibleType input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del tipo de actor es obligatorio");
            input.Name.VerifyTableColumn(
                ResponsibleTypeConsts.NameMinLength, 
                ResponsibleTypeConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del tipo de actor no debe exceder los {ResponsibleTypeConsts.NameMaxLength} caracteres");
            
            return input;
        }
    }
}
