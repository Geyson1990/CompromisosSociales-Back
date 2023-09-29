using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppResponsibleSubTypes")]
    public class ResponsibleSubType : FullAuditedEntity
    {
        [Column(TypeName = ResponsibleSubTypeConsts.ResponsibleTypeId)]
        [ForeignKey("ResponsibleType")]
        public int ResponsibleTypeId { get; set; }
        public ResponsibleType ResponsibleType { get; set; }

        [Column(TypeName = ResponsibleSubTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ResponsibleSubTypeConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
