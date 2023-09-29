using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? Compromise { get; set; }
        public string Filter { get; set; }

        public TaskStatus Status { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CompromiseName { get; set; }
                
        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
