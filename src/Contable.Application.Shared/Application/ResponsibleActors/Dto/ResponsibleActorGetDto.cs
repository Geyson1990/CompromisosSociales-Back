using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ResponsibleActors.Dto
{
    public class ResponsibleActorGetDto : EntityDto
    {
        public string Name { get; set; }
        public ResponsibleActorType Type { get; set; }
        public ResponsibleActorTypeRelationDto ResponsibleType { get; set; }
        public ResponsibleActorSubTypeRelationDto ResponsibleSubType { get; set; }
    }
}
