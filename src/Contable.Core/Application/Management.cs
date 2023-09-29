using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppManagements")]
    public class Management : FullAuditedEntity
    {
        [Column(TypeName = ManagementConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = ManagementConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = ManagementConsts.ShowDetailType)]
        public bool ShowDetail { get; set; }
    }
}
