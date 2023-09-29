using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaceHolidays.Dto
{
    public class DialogSpaceHolidayUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public DateTime Holiday { get; set; }
        public HolidayType Type { get; set; }
    }
}
