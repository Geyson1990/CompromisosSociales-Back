using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseResponsibleActorDto : EntityDto
    {
        public string Name { get; set; }
        public CompromiseResponsibleTypeRelationDto ResponsibleType { get; set; }
        public CompromiseResponsibleSubTypeRelationDto ResponsibleSubType { get; set; }

    }
}
