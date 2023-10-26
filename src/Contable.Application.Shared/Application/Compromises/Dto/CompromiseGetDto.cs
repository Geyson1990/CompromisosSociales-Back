
using Abp.Application.Services.Dto;
using Contable.Application.External.Dto;
using Contable.Application.Parameters.Dto;
using System;
using System.Collections.Generic;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseGetDto : EntityDto<long>
    {
        public CompromiseRecordDto Record { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Transcription { get; set; }
        public string Description { get; set; }
        public CompromiseType Type { get; set; }
        public bool WomanCompromise { get; set; }
        public bool IsPriority { get; set; }
        public string PriorityReference { get; set; }
        //RQ07 - RF3
        public DateTime? DueDate { get; set; }
        public DateTime? DeadLine { get; set; }
        public CompromiseStateLocationDto CompromiseState { get; set; }
        public CompromiseSubStateLocationDto CompromiseSubState { get; set; }

        public PIPMEFDto PIPMEF { get; set; }
        public ParameterDto Status { get; set; }

        public CompromiseLabelLocationDto CompromiseLabel { get; set; }
        public CompromiseResponsibleActorDto ResponsibleActor { get; set; }
        public CompromiseResponsibleSubActorDto ResponsibleSubActor { get; set; }
        public List<CompromiseLocationDto> CompromiseLocations { get; set; }
        public List<CompromiseInvolvedDto> Involved { get; set; }
        public List<CompromiseResponsibleDto> Responsibles { get; set; }
        public List<CompromiseSituationDto> Situations { get; set; }
        public List<CompromiseTimeLineDto> Timelines { get; set; }
        public CompromiseUserDto CreatorUser { get; set; }
        public CompromiseUserDto EditUser { get; set; }
    }
}
