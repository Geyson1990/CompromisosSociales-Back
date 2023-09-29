using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityRecordDto : EntityDto<long>
    {
        public UtilitySocialConflictDto SocialConflict { get; set; }
        public DateTime RecordTime { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string TerritorialUnits { get; set; }
    }
}
