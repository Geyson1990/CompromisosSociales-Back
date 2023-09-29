using Abp.Application.Services.Dto;
using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceDocuments.Dto
{
    public class DialogSpaceDocumentUpdateDto : EntityDto
    {
        public EntityDto DialogSpace { get; set; }
        public EntityDto DialogSpaceDocumentType { get; set; }
        public EntityDto DialogSpaceDocumentSituation { get; set; }
        public string Document { get; set; }
        public DateTime DocumentTime { get; set; }
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

        public List<UploadResourceInputDto> UploadFiles { get; set; }
    }
}
