using Abp.Application.Services.Dto;
using Contable.Application.Parameters.Dto;
using System;
using System.Collections.Generic;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseUpdateDto : EntityDto<long>
    {
        public EntityDto<long> Record { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Transcription { get; set; }
        public CompromiseType Type { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? DueDate { get; set; }
        public EntityDto Status { get; set; }
        public bool IsPriority { get; set; }
        public string PriorityReference { get; set; }
        public bool WomanCompromise { get; set; }
        public EntityDto CompromiseState { get; set; }
        public EntityDto CompromiseSubState { get; set; }
        public EntityDto ResponsibleActor { get; set; }
        public EntityDto ResponsibleSubActor { get; set; }
        public EntityDto CompromiseLabel { get; set; }
        public CompromiseUpdatePIPMEFDto PIPMEF { get; set; }
        public List<CompromiseLocationDto> CompromiseLocations { get; set; }
        public List<CompromiseSituationDto> Situations { get; set; }
        public List<CompromiseInvolvedDto> Involved { get; set; }
        public List<CompromiseResponsibleDto> Responsibles { get; set; }
        public List<CompromiseUploadResourceDto> Uploads { get; set; }
        public List<CompromiseTimeLineDto> Timelines { get; set; }
    }
}
