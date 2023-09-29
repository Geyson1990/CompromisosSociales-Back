using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppOrders")]
    public class Order: FullAuditedEntity<long>
    {
        [Required]
        [Column(TypeName = OrderConsts.CodeType)]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = OrderConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = OrderConsts.DatetimeType)]
        public DateTime? OrderDate { get; set; }

        [Column(TypeName = OrderConsts.Type)]
        public OrderType Type { get; set; }

        [Column(TypeName = OrderConsts.DescriptionType)]
        public string Description { get; set; }

        [Column(TypeName = OrderConsts.ObservationType)]
        public string Observation { get; set; }

        [Column(TypeName = OrderConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = OrderConsts.ResponsibleType)]
        public string Responsible { get; set; }

        public SocialConflict SocialConflict { get; set; }
        public PIPMEF PIPMEF { get; set; }        
        public TerritorialUnit TerritorialUnit { get; set; }
        public Department Department { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
    }
}
