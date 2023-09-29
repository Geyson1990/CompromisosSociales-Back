using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppResponsibleTypes")]
    public class ResponsibleType : FullAuditedEntity
    {
        [Column(TypeName = ResponsibleTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ResponsibleTypeConsts.EnabledType)]
        public bool Enabled { get; set; }

        public List<ResponsibleSubType> SubTypes { get; set; }
    }
}
