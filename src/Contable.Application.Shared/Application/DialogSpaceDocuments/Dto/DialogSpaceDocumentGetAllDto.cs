using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceDocuments.Dto
{
    public class DialogSpaceDocumentGetAllDto : EntityDto
    {
        public string Document { get; set; }
        public string DocumentType { get; set; }
        public string Situation { get; set; }
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
    }
}
