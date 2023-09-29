using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectoryConflictTypes")]
    public class DirectoryConflictType : FullAuditedEntity
    {
        [Column(TypeName = DirectoryConflictTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectoryConflictTypeConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
