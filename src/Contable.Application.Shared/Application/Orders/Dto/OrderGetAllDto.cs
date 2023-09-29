using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Orders.Dto
{
    public class OrderGetAllDto : EntityDto<long>
    {
        public OrderSocialConflictDto SocialConflict { get; set; }
        public OrderTerritorialUnitDto TerritorialUnit { get; set; }
        public CompromiseType Type { get; set; }
        public string Name { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
