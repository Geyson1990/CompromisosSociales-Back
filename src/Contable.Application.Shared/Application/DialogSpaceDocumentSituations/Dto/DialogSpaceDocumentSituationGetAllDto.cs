using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceDocumentSituations.Dto
{
    public class DialogSpaceDocumentSituationGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
