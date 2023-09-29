using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictActorGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string NameSurname { get; set; }

        public string Document { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
                Sorting = "CreationTime DESC";
            if (NameSurname.IsNullOrWhiteSpace())
                NameSurname = "";
            if (Document.IsNullOrWhiteSpace())
                Document = "";
        }
    }
}
