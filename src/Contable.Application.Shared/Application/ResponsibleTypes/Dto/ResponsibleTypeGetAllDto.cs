using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ResponsibleTypes.Dto
{
    public class ResponsibleTypeGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<ResponsibleTypeResponsibleSubTypeDto> SubTypes { get; set; }
    }
}
