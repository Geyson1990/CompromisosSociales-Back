using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppReports")]
    public class Report : FullAuditedEntity
    {
        [Column(TypeName = ReportConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ReportConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = ReportConsts.EnabledType)]
        public bool Enabled { get; set; }
    }
}
