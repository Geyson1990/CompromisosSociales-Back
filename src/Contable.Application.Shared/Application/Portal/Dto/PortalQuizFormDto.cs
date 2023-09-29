using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalQuizFormDto : EntityDto
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public int SelectedOptionId { get; set; }
        public List<PortalQuizFormOptionDto> Options { get; set; }
    }
}
