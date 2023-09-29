using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DinamicVariables.Dto
{
    public class DinamicVariableGetDetailInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? DinamicVariableId { get; set; }
        public string Filter { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "Department.Name ASC, Province.Name ASC";
            }
        }
    }
}
