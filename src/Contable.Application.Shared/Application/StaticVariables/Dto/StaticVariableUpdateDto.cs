using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.StaticVariables.Dto
{
    public class StaticVariableUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public List<StaticVariableOptionUpdateDto> Options { get; set; }
        public List<EntityDto> DeletedOptions { get; set; }
        public List<EntityDto> DeletedOptionDetails { get; set; }
    }
}
