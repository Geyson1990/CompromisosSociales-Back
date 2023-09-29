using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlerts.Dto
{
    public class SocialConflictAlertSectorLocationDto : EntityDto
    {
        public DateTime? CreationTime { get; set; }
        public DateTime SectorTime { get; set; }
        public SocialConflictAlertSectorDto AlertSector { get; set; }
        public string Description { get; set; }
        public bool Remove { get; set; }
    }
}
