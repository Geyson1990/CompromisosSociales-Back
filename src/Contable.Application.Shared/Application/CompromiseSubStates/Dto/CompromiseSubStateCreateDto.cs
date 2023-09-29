using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CompromiseSubStates.Dto
{
    public class CompromiseSubStateCreateDto
    {
        public CompromiseSubStateStateRelationDto CompromiseState { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
