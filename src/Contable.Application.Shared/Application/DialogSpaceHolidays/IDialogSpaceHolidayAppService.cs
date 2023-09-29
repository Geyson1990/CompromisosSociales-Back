using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.DialogSpaceHolidays.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.DialogSpaceHolidays
{
    public interface IDialogSpaceHolidayAppService : IApplicationService
    {
        Task Create(DialogSpaceHolidayCreateDto input);
        Task Delete(EntityDto input);
        Task<DialogSpaceHolidayGetDto> Get(EntityDto input);
        Task<PagedResultDto<DialogSpaceHolidayGetAllDto>> GetAll(DialogSpaceHolidayGetAllInputDto input);
        Task Update(DialogSpaceHolidayUpdateDto input);
    }
}
