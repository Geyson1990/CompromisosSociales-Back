using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SocialConflictTaskManagementHistories.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.SocialConflictTaskManagementHistories
{
    public interface ISocialConflictTaskManagementHistory : IApplicationService
    {
        Task<SocialConflictTaskManagementHistoryGetDto> Get(EntityDto input);
        Task<PagedResultDto<SocialConflictTaskManagementHistoryGetAllDto>> GetAll(SocialConflictTaskManagementHistoryGetAllInputDto input);
        Task<FileDto> GetMatrizToExcel(SocialConflictTaskManagementHistoryGetAllInputDto input);
    }
}
