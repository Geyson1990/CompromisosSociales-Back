using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Actors.Dto
{
    public class ActorGetAllDto : EntityDto
    {
        public int TypologyId { get; set; }
        public int SubTypologyId { get; set; }
        public int FullName { get; set; }
        public int ActorTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string WorkPosition { get; set; }
        public string Institution { get; set; }
        public string InstitutionAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Position { get; set; }
        public string Details { get; set; }
        public bool ShowPolitical { get; set; }
        public bool Enabled { get; set; }
    }
}
