using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DialogSpaceHolidays;
using Contable.Application.DialogSpaceHolidays.Dto;
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
    [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceHoliday)]
    public class DialogSpaceHolidayAppService : ContableAppServiceBase, IDialogSpaceHolidayAppService
    {
        private readonly IRepository<DialogSpaceHoliday> _dialogSpaceHolidayRepository;

        public DialogSpaceHolidayAppService(IRepository<DialogSpaceHoliday> dialogSpaceHolidayRepository)
        {
            _dialogSpaceHolidayRepository = dialogSpaceHolidayRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceHoliday_Create)]
        public async Task Create(DialogSpaceHolidayCreateDto input)
        {
            await _dialogSpaceHolidayRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<DialogSpaceHoliday>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceHoliday_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _dialogSpaceHolidayRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceHolidayRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceHoliday)]
        public async Task<DialogSpaceHolidayGetDto> Get(EntityDto input)
        {
            VerifyCount(await _dialogSpaceHolidayRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DialogSpaceHolidayGetDto>(await _dialogSpaceHolidayRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceHoliday)]
        public async Task<PagedResultDto<DialogSpaceHolidayGetAllDto>> GetAll(DialogSpaceHolidayGetAllInputDto input)
        {
            var query = _dialogSpaceHolidayRepository
                .GetAll()
                .WhereIf(input.Type.HasValue, p => p.Type == input.Type.Value)
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.Holiday >= input.StartTime.Value && p.Holiday <= input.EndTime.Value)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DialogSpaceHoliday, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DialogSpaceHolidayGetAllDto>(count, ObjectMapper.Map<List<DialogSpaceHolidayGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_DialogSpaceHoliday_Edit)]
        public async Task Update(DialogSpaceHolidayUpdateDto input)
        {
            VerifyCount(await _dialogSpaceHolidayRepository.CountAsync(p => p.Id == input.Id));

            await _dialogSpaceHolidayRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _dialogSpaceHolidayRepository.GetAsync(input.Id))));
        }

        private DialogSpaceHoliday ValidateEntity(DialogSpaceHoliday input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del día feriado es obligatorio");
            input.Name.VerifyTableColumn(DialogSpaceHolidayConsts.NameMinLength, 
                DialogSpaceHolidayConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del día feriado no debe exceder los {DialogSpaceHolidayConsts.NameMaxLength} caracteres");

            input.Holiday = new DateTime(input.Holiday.Year, input.Holiday.Month, input.Holiday.Day, 0, 0, 0, DateTimeKind.Unspecified);

            return input;
        }
    }
}
