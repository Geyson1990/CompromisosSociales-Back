using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Application.Compromises.Dto;
using Contable.Application.ResponsibleActors.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementConflictGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ConflictSite Site { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Code ASC";
            }
        }
    }
}
