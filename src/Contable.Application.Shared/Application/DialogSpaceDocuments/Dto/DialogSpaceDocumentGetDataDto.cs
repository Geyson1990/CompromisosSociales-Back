using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceDocuments.Dto
{
    public class DialogSpaceDocumentGetDataDto
    {
        public DialogSpaceDocumentDialogSpaceRelationDto DialogSpace { get; set; }
        public DialogSpaceDocumentGetDto DialogSpaceDocument { get; set; }
        public List<DialogSpaceDocumentTypeRelationDto> DocumentTypes { get; set; }
        public List<DialogSpaceDocumentSituationRelationDto> Situations { get; set; }
    }
}
