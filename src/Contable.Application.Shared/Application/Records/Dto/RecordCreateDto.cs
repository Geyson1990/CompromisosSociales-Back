using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Records.Dto
{
    public class RecordCreateDto
    {
        public RecordSocialConflictDto SocialConflict{ get; set; }        
        public string Title { get; set; }
        public DateTime? RecordTime { get; set; }
        public List<UploadResourceInputDto> UploadFiles { get; set; }
    }
}
