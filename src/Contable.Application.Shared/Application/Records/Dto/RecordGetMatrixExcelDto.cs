using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Records.Dto
{
    public class RecordGetMatrixExcelDto : EntityDto<long>, IHasCreationTime
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public DateTime RecordTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string TerritorialUnits { get; set; }
        public string Departments { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }

        public string ResourcesNames { get; set; }
        public string ResourcesTypes { get; set; }

        public bool WomanCompromise { get; set; }

        public RecordUserDto CreatorUser { get; set; }
        public RecordUserDto EditUser { get; set; }
        public RecordSocialConflictDto SocialConflict { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
