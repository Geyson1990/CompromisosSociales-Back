using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Managements.Dto
{
    public class ManagementUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public bool ShowDetail { get; set; }
    }
}
