using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SocialConflictTaskManagements.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.SocialConflictTaskManagements
{
    public interface ISocialConflictTaskManagementAppService : IApplicationService
    {
        #region Tasks 
        Task<PagedResultDto<SocialConflictTaskManagementConflictGetAllDto>> GetAllConflicts(SocialConflictTaskManagementConflictGetAllInputDto input);
        Task<SocialConflictTaskManagementConflictGetDto> GetConflict(SocialConflictTaskManagementConflictGetInputDto input);
        Task<PagedResultDto<SocialConflictTaskManagementPersonGetAllDto>> GetAllPersons(SocialConflictTaskManagementPersonGetAllInputDto input);
        Task<SocialConflictTaskManagementPersonChangeOutputDto> PersonChanges(SocialConflictTaskManagementPersonChangeInputDto input);
        Task<SocialConflictTaskManagementGetDto> CreateTask(SocialConflictTaskManagementCreateDto input);
        Task DeleteTask(EntityDto input);
        Task<SocialConflictTaskManagementGetDto> GetTask(NullableIdDto input);
        Task<PagedResultDto<SocialConflictTaskManagementGetAllDto>> GetAllTask(SocialConflictTaskManagementGetAllInputDto input);
        Task<SocialConflictTaskManagementGetDto> UpdateTask(SocialConflictTaskManagementUpdateDto input);
        Task CreateTaskExtend(SocialConflictTaskManagementExtendCreateDto input);
        Task DeleteTaskExtend(EntityDto input);
        Task<SocialConflictTaskManagementExtendGetDto> GetTaskExtend(EntityDto input);
        Task ChangeStateToPending(EntityDto input);
        Task ChangeStateToNonComplete(EntityDto input);
        Task ChangeStateToComplete(EntityDto input);
        Task<EntityDto> CreateResource(SocialConflictTaskManagementCreateResourceDto input);
        Task DeleteResource(EntityDto input);
        Task<SocialConflictTaskManagementEmailConfigurationDto> GetEmailConfiguration(EntityDto input);
        Task SendNotification(SocialConflictTaskManagementSendNotificationDto input);
        #endregion
        #region Comment
        Task<SocialConflictTaskManagementCommentGetDto> CreateComment(SocialConflictTaskManagementCommentCreateDto input);
        Task DeleteComment(EntityDto input);
        #endregion
        #region Exports 
        Task<FileDto> GetMatrixToExcel(SocialConflictTaskManagementGetAllInputDto input);
        #endregion
    }
}
