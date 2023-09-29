using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.HelpMemories.Dto
{
    public class HelpMemoryGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? SocialConflictId { get; set; }
        public int? SocialConflictSensibleId { get; set; }
        public string SocialConflictCode { get; set; }
        public string SocialConflictSensibleCode { get; set; }
        public int? DirectoryGovernmentId { get; set; }
        public string HelpMemoryRequest { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "RequestTime ASC";
            }
        }
    }
}