using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseStateDto : Entity
    {
        public string Name { get; set; }
        public List<CompromiseSubStateDto> SubStates { get; set; }
    }
}
