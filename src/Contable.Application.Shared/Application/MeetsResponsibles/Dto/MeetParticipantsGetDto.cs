using Abp.Application.Services.Dto;

namespace Contable.Application.MeetsResponsibles.Dto
{
    public class MeetParticipantsGetDto : EntityDto
    {
        public EntityDto Meets { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string Job { get; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
