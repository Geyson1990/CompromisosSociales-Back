using Abp.Application.Services.Dto;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementCommentGetAllDto : EntityDto<long>
    {
        public string Description { get; set; }
    }
}
