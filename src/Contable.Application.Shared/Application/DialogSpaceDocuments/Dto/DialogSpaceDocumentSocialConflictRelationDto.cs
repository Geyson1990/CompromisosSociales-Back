using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceDocuments.Dto
{
    public class DialogSpaceDocumentSocialConflictRelationDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
    }
}
