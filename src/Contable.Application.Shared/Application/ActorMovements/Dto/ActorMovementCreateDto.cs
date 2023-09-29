using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ActorMovements.Dto
{
    public class ActorMovementCreateDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Enabled { get; set; }
    }
}
