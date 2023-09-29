using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflicts.Dto
{
    public class SocialConflictActorExcelExportDto
    {
        public string CaseCode { get; set; }
        public string CaseName { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Job { get; set; }
        public string Community { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string IsPoliticalAssociation { get; set; }
        public string PoliticalAssociation { get; set; }
        public string Position { get; set; }
        public string Interest { get; set; }
        public string Regions { get; set; }
        public string Site { get; set; }
        public string ActorType { get; set; }
        public string ActorMovement { get; set; }
    }
}
