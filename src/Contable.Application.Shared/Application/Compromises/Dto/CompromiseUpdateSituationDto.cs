using Abp.Application.Services.Dto;
using Contable.Application.Parameters.Dto;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseUpdateSituationDto : EntityDto<long>
    {
        public ParameterDto Criterion { get; set; }
        public ParameterDto Status { get; set; }
        public CompromiseSituationDto Situation { get; set; }
    }
}
