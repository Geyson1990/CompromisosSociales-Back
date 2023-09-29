using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceDocumentSituations.Dto
{
    public class DialogSpaceDocumentSituationGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Name ASC";
            }
        }
    }
}
