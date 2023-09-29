using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictSensibles.Dto
{
    public class SocialConflictSensibleSugerenceDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public SocialConflictSensibleUserDto CreatorUser { get; set; }
        public string Description { get; set; }
        public bool Remove { get; set; }
        public bool Accepted { get; set; }
        public DateTime? AcceptTime { get; set; }
        public SocialConflictSensibleUserDto AcceptedUser { get; set; }
    }
}
