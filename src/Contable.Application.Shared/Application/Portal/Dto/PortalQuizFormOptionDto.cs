using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalQuizFormOptionDto : EntityDto
    {
        public string Name { get; set; }
        public bool Extra { get; set; }
        public string Response { get; set; }    
    }
}
