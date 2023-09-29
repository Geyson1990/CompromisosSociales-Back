using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementExtendCreateDto
    {
        public SocialConflictTaskManagementRelationDto SocialConflictTaskManagement { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
