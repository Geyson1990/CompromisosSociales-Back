using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceTypes")]
    public class DialogSpaceType : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DialogSpaceTypeConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}

