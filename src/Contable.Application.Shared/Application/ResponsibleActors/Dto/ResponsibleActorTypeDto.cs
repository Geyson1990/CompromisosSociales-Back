using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ResponsibleActors.Dto
{
    public class ResponsibleActorTypeDto : EntityDto
    {
        public string Name { get; set; }
        public List<ResponsibleActorSubTypeDto> SubTypes { get; set; }
    }
}
