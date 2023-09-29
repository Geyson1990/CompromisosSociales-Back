using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SocialConflictAlertHistories.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.SocialConflictAlertHistories
{
    public interface ISocialConflictAlertHistoryAppService : IApplicationService
    {
        Task<SocialConflictAlertHistoryGetDto> Get(EntityDto input);
        Task<PagedResultDto<SocialConflictAlertHistoryGetAllDto>> GetAll(SocialConflictAlertHistoryGetAllInputDto input);
        Task<FileDto> GetMatrizToExcel(SocialConflictAlertHistoryGetAllInputDto input);
    }
}
