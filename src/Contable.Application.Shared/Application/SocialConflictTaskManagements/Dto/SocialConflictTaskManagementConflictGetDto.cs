using Abp.Application.Services.Dto;
using Contable.Application.Compromises.Dto;
using Contable.Application.ResponsibleActors.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementConflictGetDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Tasks { get; set; }
        public ConflictSite Type { get; set; }
    }
}
