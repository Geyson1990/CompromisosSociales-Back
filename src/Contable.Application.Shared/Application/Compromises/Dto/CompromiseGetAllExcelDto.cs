using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Contable.Application.External.Dto;
using Contable.Application.Parameters.Dto;
using System;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseGetAllExcelDto : EntityDto<long>, IHasCreationTime
    {
        public string Code { get; set; }
        public string Name { get; set; }        
        public CompromiseType Type { get; set; }
        public ParameterDto Status { get; set; }
        public PIPMEFDto PIPMEF { get; set; }        
        public CompromiseRecordDto Record { get; set; } //incluye el caso conflictivo
        public CompromiseResponsibleSubActorDto Responsible { get; set; }
        public DateTime CreationTime { get; set; }

        public string TerritorialUnits { get; set; }
        public string Departments { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Advance { get; set; }        
    }
}
