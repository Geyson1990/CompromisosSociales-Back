using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementGetAllExcellInputDto : IShouldNormalize, ISortedResultRequest
    {
        public string Sorting { get; set; }

        public int? Compromise { get; set; }

        public string Filter { get; set; }

        public TaskStatus Status { get; set; }

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
