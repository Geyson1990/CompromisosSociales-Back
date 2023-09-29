using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectoryGovernmentTypes")]
    public class DirectoryGovernmentType : FullAuditedEntity
    {
        [Column(TypeName = DirectoryGovernmentTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectoryGovernmentTypeConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
