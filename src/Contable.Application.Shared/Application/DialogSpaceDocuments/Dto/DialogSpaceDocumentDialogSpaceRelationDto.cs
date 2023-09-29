using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceDocuments.Dto
{
    public class DialogSpaceDocumentDialogSpaceRelationDto : EntityDto
    {
        public string CaseName { get; set; }
        public DialogSpaceDocumentDialogSpaceTypeRelationDto DialogSpaceType { get; set; }
        public DialogSpaceDocumentSocialConflictRelationDto SocialConflict { get; set; }
    }
}
