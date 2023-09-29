using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;


namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementExtendGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? TaskManagementId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
