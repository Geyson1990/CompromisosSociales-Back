using Abp.Application.Services.Dto;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseInvolvedDto : EntityDto
    {
        public CompromiseResponsibleActorDto ResponsibleActor { get; set; }
        public CompromiseResponsibleSubActorDto ResponsibleSubActor { get; set; }
        public bool Remove { get; set; }
    }
}
