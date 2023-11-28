using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Actors.Dto
{
    public class ActorGetAllDto : EntityDto
    {
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string JobPosition { get; set; }
        public string Institution { get; set; }
        public string InstitutionAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Position { get; set; }
        public string Interest { get; set; }
        public bool IsPoliticalAssociation { get; set; }
        public string PoliticalAssociation { get; set; }
        public bool Enabled { get; set; }
        public int StatusId { get; set; }
        public ActorTypeDto ActorType { get; set; }
        public ActorMovementDto ActorMovement { get; set; }   
        //public int TypologyId { get; set; }
        //public int SubTypologyId { get; set; }
        //public int ActorMovementId { get; set; }
    }
}
