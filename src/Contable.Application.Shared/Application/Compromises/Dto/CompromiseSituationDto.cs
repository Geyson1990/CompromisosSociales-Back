using Abp.Application.Services.Dto;
using System;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseSituationDto : EntityDto<long>
    {
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public CompromiseSituationResourceDto Resource { get; set; }
        public bool Remove { get; set; }
    }
}
