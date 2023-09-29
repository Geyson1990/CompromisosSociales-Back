using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Analysts.Dto
{
    public class AnalystGetAllDto : EntityDto
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool Enabled { get; set; }
    }
}
