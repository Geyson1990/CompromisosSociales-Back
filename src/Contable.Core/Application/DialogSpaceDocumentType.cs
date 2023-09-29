using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceDocumentTypes")]
    public class DialogSpaceDocumentType : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceDocumentTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DialogSpaceDocumentTypeConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
