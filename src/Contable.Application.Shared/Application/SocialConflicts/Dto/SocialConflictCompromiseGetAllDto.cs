using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictCompromiseGetAllDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public CompromiseType Type { get; set; }
        public SocialConflictParameterDto Status { get; set; }
    }
}
