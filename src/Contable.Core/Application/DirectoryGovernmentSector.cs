using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectoryGovernmentSectors")]
    public class DirectoryGovernmentSector : FullAuditedEntity
    {
        [Column(TypeName = DirectoryGovernmentSectorConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectoryGovernmentSectorConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
