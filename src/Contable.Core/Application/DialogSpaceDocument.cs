using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceDocuments")]
    public class DialogSpaceDocument : FullAuditedEntity
    {
        [ForeignKey("DialogSpace")]
        [Column(TypeName = DialogSpaceDocumentConsts.DialogSpaceIdType)]
        public int DialogSpaceId { get; set; }
        public DialogSpace DialogSpace { get; set; }

        [ForeignKey("DialogSpaceDocumentType")]
        [Column(TypeName = DialogSpaceDocumentConsts.DialogSpaceDocumentTypeIdType)]
        public int DialogSpaceDocumentTypeId { get; set; }
        public DialogSpaceDocumentType DialogSpaceDocumentType { get; set; }

        [ForeignKey("DialogSpaceDocumentSituation")]
        [Column(TypeName = DialogSpaceDocumentConsts.DialogSpaceDocumentSituationIdType)]
        public int? DialogSpaceDocumentSituationId { get; set; }
        public DialogSpaceDocumentSituation DialogSpaceDocumentSituation { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.DocumentTimeType)]
        public DateTime DocumentTime { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.InstallationTimeType)]
        public DateTime? InstallationTime { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.InstallationMaxTimeType)]
        public DateTime? InstallationMaxTime { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.VigencyTimeType)]
        public DateTime? VigencyTime { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.HasInstallationType)]
        public bool HasInstallation { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.Type)]
        public DocumentType Type { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.RangeType)]
        public DocumentRange Range { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.RangeSideType)]
        public DocumentRangeSide? RangeSide { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.ExpositionType)]
        public DocumentExposition Exposition { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.VigencyRangeSideType)]
        public DocumentRangeSide VigencyRangeSide { get; set; }
        
        [Column(TypeName = DialogSpaceDocumentConsts.DaysType)]
        public int? Days { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.VigencyDaysType)]
        public int? VigencyDays { get; set; }

        [Column(TypeName = DialogSpaceDocumentConsts.ObservationType)]
        public string Observation { get; set; }

        public List<DialogSpaceDocumentResource> Resources { get; set; }
    }
}
