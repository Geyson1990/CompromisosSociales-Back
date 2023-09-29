using Abp.Application.Services.Dto;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application.DialogSpaceDocuments.Dto
{
    public class DialogSpaceDocumentGetDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public DialogSpaceDocumentUserDto CreatorUser { get; set; }
        public DialogSpaceDocumentUserDto EditUser { get; set; }

        public DialogSpaceDocumentDialogSpaceRelationDto DialogSpace { get; set; }
        public DialogSpaceDocumentTypeRelationDto DialogSpaceDocumentType { get; set; }
        public DialogSpaceDocumentSituationRelationDto DialogSpaceDocumentSituation { get; set; }
        public string Document { get; set; }
        public DateTime? DocumentTime { get; set; }
        public DateTime? InstallationTime { get; set; }
        public DateTime? InstallationMaxTime { get; set; }
        public DateTime? VigencyTime { get; set; }
        public bool HasInstallation { get; set; }
        public DocumentType Type { get; set; }
        public DocumentRange Range { get; set; }
        public DocumentRangeSide? RangeSide { get; set; }
        public DocumentExposition Exposition { get; set; }
        public DocumentRangeSide VigencyRangeSide { get; set; }
        public int? Days { get; set; }
        public int? VigencyDays { get; set; }
        public string Observation { get; set; }

        public List<DialogSpaceDocumentResourceDto> Resources { get; set; }
    }
}
