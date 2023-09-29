using Abp.Configuration;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceDocuments.Dto
{
    public class DialogSpaceDocumentGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? DialogSpaceId { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
                Sorting = "Document ASC";
        }
    }
}
