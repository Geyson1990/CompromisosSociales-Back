using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Orders.Dto
{
    public class OrderGetMatrixExcelInputDto : IShouldNormalize, ISortedResultRequest
    {
        public string Sorting { get; set; }

        public int? Type { get; set; }
        public string SocialConflictCode { get; set; }
        public int? TerritorialUnitId { get; set; }
        public string Filter { get; set; }

        public void Normalize()
        {
            if (Sorting.IsNullOrWhiteSpace())
            {
                Sorting = "CreationTime DESC";
            }
        }
    }
}
