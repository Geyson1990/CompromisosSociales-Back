using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppSituationResources")]
    public class SituationResource : FullAuditedEntity<long>
    {
        public long SituationId { get; set; }
        public Situation Situation { get; set; }

        [Column(TypeName = SituationResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = SituationResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = SituationResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = SituationResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = SituationResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = SituationResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = SituationResourceConsts.AssetType)]
        public string ClassName { get; set; }
        
        [Column(TypeName = SituationResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
