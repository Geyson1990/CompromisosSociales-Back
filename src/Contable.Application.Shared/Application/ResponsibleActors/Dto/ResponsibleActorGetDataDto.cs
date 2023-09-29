using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ResponsibleActors.Dto
{
    public class ResponsibleActorGetDataDto
    {
        public ResponsibleActorGetDto ResponsibleActor { get; set; }
        public List<ResponsibleActorTypeDto> Types { get; set; }
    }
}
