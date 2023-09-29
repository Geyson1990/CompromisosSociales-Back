using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppRecordResourceTypes")]
    public class RecordResourceType : FullAuditedEntity
    {
        [Column(TypeName = RecordResourceTypeConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = RecordResourceTypeConsts.EnabledType)]
        public bool Enabled { get; set; }

        public List<RecordResource> RecordResources { get; set; }
    }
}
