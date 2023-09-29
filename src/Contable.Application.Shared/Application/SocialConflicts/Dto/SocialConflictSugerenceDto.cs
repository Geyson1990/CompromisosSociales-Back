using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictSugerenceDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public SocialConflictUserDto CreatorUser { get; set; }
        public string Description { get; set; }
        public bool Remove { get; set; }
        public bool Accepted { get; set; }
        public DateTime? AcceptTime { get; set; }
        public SocialConflictUserDto AcceptedUser { get; set; }
    }
}
