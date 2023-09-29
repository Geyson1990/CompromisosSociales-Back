using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceDocumentResources")]
    public class DialogSpaceDocumentResource : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceDocumentResourceConsts.DialogSpaceDocumentIdType)]
        [ForeignKey("DialogSpaceDocument")]
        public int DialogSpaceDocumentId { get; set; }
        public DialogSpaceDocument DialogSpaceDocument { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = DialogSpaceDocumentResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
