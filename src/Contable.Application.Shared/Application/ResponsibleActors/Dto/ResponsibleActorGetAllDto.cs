using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ResponsibleActors.Dto
{
    public class ResponsibleActorGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public ResponsibleActorTypeRelationDto ResponsibleType { get; set; }
        public ResponsibleActorSubTypeRelationDto ResponsibleSubType { get; set; }
        public List<ResponsibleSubActorGetDto> ResponsibleSubActors { get; set; }
    }
}
