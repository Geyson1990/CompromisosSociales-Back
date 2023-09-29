using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Records.Dto
{
    public class RecordGetAllDto : EntityDto
    {
        public RecordSocialConflictDto SocialConflict { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime RecordTime { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string TerritorialUnits { get; set; }
        public RecordUserDto CreatorUser { get; set; }
        public RecordUserDto EditUser { get; set; }
        public List<RecordResourceDto> Resources { get; set; }
    }
}
