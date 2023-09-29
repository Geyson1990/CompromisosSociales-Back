using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Contable.Application.Parameters.Dto;
using System;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseGetAllDto : EntityDto<long>, IHasCreationTime
    {
        public string Code { get; set; }
        public string Name { get; set; }        
        public string TerritorialUnits { get; set; }
        public bool WomanCompromise { get; set; }
        public CompromiseType Type { get; set; }
        public ParameterDto Status { get; set; }
        public CompromiseRecordDto Record { get; set; }
        public CompromiseLabelLocationDto CompromiseLabel { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
