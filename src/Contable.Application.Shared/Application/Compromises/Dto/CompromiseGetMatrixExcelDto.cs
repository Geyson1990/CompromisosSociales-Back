using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Contable.Application.External.Dto;
using Contable.Application.Parameters.Dto;
using System;
using System.Collections.Generic;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseGetMatrixExcelDto : EntityDto<long>, IHasCreationTime
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Transcription { get; set; }
        public bool IsPriority { get; set; }
        public string PriorityReference { get; set; }
        public CompromiseType Type { get; set; }
        public ParameterDto Status { get; set; }
        public PIPMEFDto PIPMEF { get; set; }
        public CompromiseRecordDto Record { get; set; } //incluye el caso conflictivo
        public CompromiseResponsibleActorDto ResponsibleActor { get; set; }
        public CompromiseResponsibleSubActorDto ResponsibleSubActor { get; set; }
        public CompromiseLabelLocationDto CompromiseLabel { get; set; }
        public string ResponsibleTypes { get; set; }
        public string ResponsibleSubTypes { get; set; }
        public string Responsibles { get; set; }
        public string SubResponsibles { get; set; }
        public string InvolvedTypes { get; set; }
        public string InvolvedSubTypes { get; set; }
        public string InvolvedResponsibles { get; set; }
        public string InvolvedSubResponsibles { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public bool WomanCompromise { get; set; }
        public string Timelines { get; set; }
        public string TerritorialUnits { get; set; }
        public string Departments { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Advance { get; set; }
        public CompromiseUserDto CreatorUser { get; set; }
        public CompromiseUserDto EditUser { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DeadLine { get; set; }
    }
}
