using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.HelpMemories.Dto
{
    public class HelpMemorySocialConflictSensibleDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
    }
}
