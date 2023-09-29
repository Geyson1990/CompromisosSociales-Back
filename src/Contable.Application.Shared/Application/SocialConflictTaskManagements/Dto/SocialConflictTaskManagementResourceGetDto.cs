using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementResourceGetDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string CreatorUserName { get; set; }
        public string SectionFolder { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Extension { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Resource { get; set; }
        public bool Remove { get; set; }
    }
}
