using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.TaskManagements.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.TaskManagements
{
    public interface ITaskManagementAppService: IApplicationService
    {
        Task<PagedResultDto<TaskManagementCompromiseGetAllDto>> GetAllCompromises(TaskManagementCompromiseGetAllInputDto input);
        Task<TaskManagementCompromiseGetAllDto> GetCompromise(EntityDto<long> input);
        Task<PagedResultDto<TaskManagementPersonGetAllDto>> GetAllPersons(TaskManagementPersonGetAllInputDto input);
        Task<TaskManagementPersonChangeOutputDto> PersonChanges(TaskManagementPersonChangeInputDto input);

        Task<TaskManagementGetDto> CreateTask(TaskManagementCreateDto input);
        Task UpdateTask(TaskManagementUpdateDto input);
        Task DeleteTask(EntityDto<long> input );
        Task<TaskManagementGetDto> GetTask(NullableIdDto<long> input);
        Task<PagedResultDto<TaskManagementGetAllDto>> GetTaskAll(TaskManagementGetAllInputDto input);

        Task CreateTaskExtend(TaskManagementExtendCreateDto input);
        Task DeleteTaskExtend(EntityDto<long> input);
        Task<TaskManagementExtendGetDto> GetTaskExtend(NullableIdDto input);
        Task<PagedResultDto<TaskManagementExtendGetAllDto>> GetAllTaskExtend(TaskManagementExtendGetAllInputDto input);

        Task ChangeStateToPending(EntityDto<long> input);
        Task ChangeStateToNonComplete(EntityDto<long> input);
        Task ChangeStateToComplete(EntityDto<long> input);

        Task<TaskManagementEmailConfigurationDto> GetEmailConfiguration(EntityDto<long> input);
        Task SendNotification(TaskManagementSendNotificationDto input);

        Task<TaskManagementCommentGetDto> CreateComment(TaskManagementCommentCreateDto input);
        Task UpdateComment(TaskManagementCommentUpdateDto input);
        Task DeleteComment(EntityDto<long> input);
        Task<TaskManagementCommentGetDto> GetComment(NullableIdDto input);
        Task<PagedResultDto<TaskManagementCommentGetAllDto>> GetAllComment(TaskManagementCommentGetAllInputDto input);

        Task<FileDto> GetMatrixToExcel(TaskManagementGetAllExcellInputDto input);

    }
}
