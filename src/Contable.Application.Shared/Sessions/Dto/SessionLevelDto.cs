using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Sessions.Dto
{
    public class SessionLevelDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public string Color { get; set; }
    }
}
