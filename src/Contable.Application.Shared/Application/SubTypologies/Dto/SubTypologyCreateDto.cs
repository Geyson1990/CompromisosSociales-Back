using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SubTypologies.Dto
{
    public class SubTypologyCreateDto 
    {
        public int TypologyId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
