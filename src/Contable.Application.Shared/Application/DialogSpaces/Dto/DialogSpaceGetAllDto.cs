using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceGetAllDto : EntityDto
    {
        public int Year { get; set; }
        public int Count { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public string SocialConflictCaseName { get; set; }
        public string Person { get; set; }
        public string Type { get; set; }
        public string TerritorialUnits { get; set; }
        public string Locations { get; set; }
        public string Document { get; set; }
        public DateTime DocumentTime { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentSituation { get; set; }
        public string DocumentObservation { get; set; }
    }
}
