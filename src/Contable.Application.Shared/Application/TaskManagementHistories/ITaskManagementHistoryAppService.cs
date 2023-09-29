using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contable.Application.TaskManagementHistories.Dto;

namespace Contable.Application.TaskManagementHistories
{
    public interface ITaskManagementHistoryAppService : IApplicationService
    {
        Task<TaskManagementHistoryGetDto> Get(EntityDto input);
        Task<PagedResultDto<TaskManagementHistoryGetAllDto>> GetAll(TaskManagementHistoryGetAllInputDto input);
    }
}
