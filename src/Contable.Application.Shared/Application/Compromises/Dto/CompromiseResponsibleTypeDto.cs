using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseResponsibleTypeDto : EntityDto
    {
        public string Name { get; set; }
        public List<CompromiseResponsibleSubTypeDto> SubTypes { get; set; }
    }
}
