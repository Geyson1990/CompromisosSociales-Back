using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseSituationResourceDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string CreatorUserName { get; set; }
        public string SectionFolder { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Extension { get; set; }
        public string ClassName { get; set; }
        public string Resource { get; set; }
    }
}
