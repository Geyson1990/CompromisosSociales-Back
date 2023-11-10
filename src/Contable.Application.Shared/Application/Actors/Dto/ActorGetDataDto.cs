using System;
using System.Collections.Generic;
using System.Text;
using Contable.Application.Managers.Dto;

namespace Contable.Application.Actors.Dto
{
    public class ActorGetDataDto
    {
        public ActorGetDto Actor { get; set; }
        public List<ActorTypeDto> ActorTypes { get; set; }
        public List<ActorMovementDto> ActorMovements { get; set; }
    }
}
