using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppRecordResources")]
    public class RecordResource : FullAuditedEntity<long>
    {
        public Record Record { get; set; }

        [Column(TypeName = RecordResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = RecordResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = RecordResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = RecordResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = RecordResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = RecordResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = RecordResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = RecordResourceConsts.AssetType)]
        public string Name { get; set; }               

        [Column(TypeName = RecordResourceConsts.ResourceType)]
        public string Resource { get; set; }

        [Column(TypeName = RecordResourceConsts.RecordResourceTypeIdType)]
        [ForeignKey("RecordResourceType")]
        public int? RecordResourceTypeId { get; set; }
        public RecordResourceType RecordResourceType { get; set; }

        //[Column(TypeName = RecordResourceConsts.RecordResourceTypeIdType)]
        //[ForeignKey("RecordId")]
        //public int? RecordId { get; set; }
    }
}
