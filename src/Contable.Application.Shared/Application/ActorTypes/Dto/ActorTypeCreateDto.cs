using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ActorTypes.Dto
{
    public class ActorTypeCreateDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool ShowDetail { get; set; }
        public bool ShowMovement { get; set; }
    }
}
