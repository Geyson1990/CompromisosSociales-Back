using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceDocumentSituations")]
    public class DialogSpaceDocumentSituation : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceDocumentSituationConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DialogSpaceDocumentSituationConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
