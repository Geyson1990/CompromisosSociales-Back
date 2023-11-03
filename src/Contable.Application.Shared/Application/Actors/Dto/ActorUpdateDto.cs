using Abp.Application.Services.Dto;

namespace Contable.Application.Actors.Dto
{
    public class ActorUpdateDto : EntityDto
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
        public ActorTypeDto ActorType { get; set; }
        public ActorMovementDto ActorMovement { get; set; }
        //public ActorTypologyDto Typology { get; set; }
        //public ActorSubTypologyDto SubTypology { get; set; }
    }
}
