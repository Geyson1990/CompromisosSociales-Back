using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDirectorySectors")]
    public class DirectorySector : FullAuditedEntity
    {
        [Column(TypeName = DirectorySectorConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DirectorySectorConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
