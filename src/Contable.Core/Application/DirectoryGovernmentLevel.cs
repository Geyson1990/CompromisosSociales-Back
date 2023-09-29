using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectoryGovernmentLevels")]
    public class DirectoryGovernmentLevel : FullAuditedEntity
    {
        [Column(TypeName = DirectoryGovernmentLevelConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectoryGovernmentLevelConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
