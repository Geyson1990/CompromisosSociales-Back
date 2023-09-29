using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityPersonGetAllDto : FullAuditedEntity
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public PersonType Type { get; set; }
        public UtilityPersonUserGetAllDto User { get; set; }
    }
}
