using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Runtime.Validation;
using Contable.Application.Compromises.Dto;
using Contable.Application.ResponsibleActors.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagements.Dto
{
    public class SocialConflictTaskManagementConflictGetInputDto 
    {
        public int Id { get; set; }
        public ConflictSite Site { get; set; }
    }
}
