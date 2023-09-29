using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CrisisCommitteeJobs.Dto
{
    public class CrisisCommitteeJobCreateDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool ShowDescription { get; set; }
    }
}
