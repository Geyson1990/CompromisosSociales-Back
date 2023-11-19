using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilityPersonListDto : EntityDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public PersonType Type { get; set; }
    }

    public class UtilityPersonForRecordListDto : EntityDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public PersonType Type { get; set; }
        public bool AlertSend { get; set; }
    }
}
