using Abp.Application.Services.Dto;
using Contable.Application.Records.Dto;
using System;
using System.Collections.Generic;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseRecordDto : EntityDto<long>
    {
        public DateTime? RecordTime { get; set; }

        public string Code { get; set; }

        public string Title { get; set; }

        public RecordSocialConflictDto SocialConflict{ get; set; }

        public List<RecordResourceDto> Resources { get; set; }
    }
}
