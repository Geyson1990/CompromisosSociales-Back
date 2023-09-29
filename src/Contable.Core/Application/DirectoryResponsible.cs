using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectoryResponsibles")]
    public class DirectoryResponsible : FullAuditedEntity
    {
        [Column(TypeName = DirectoryResponsibleConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectoryResponsibleConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
