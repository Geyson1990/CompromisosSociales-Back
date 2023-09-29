using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceHolidays")]
    public class DialogSpaceHoliday : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceHolidayConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = DialogSpaceHolidayConsts.HolidayType)]
        public DateTime Holiday { get; set; }

        [Column(TypeName = DialogSpaceHolidayConsts.Type)]
        public HolidayType Type { get; set; }
    }
}
