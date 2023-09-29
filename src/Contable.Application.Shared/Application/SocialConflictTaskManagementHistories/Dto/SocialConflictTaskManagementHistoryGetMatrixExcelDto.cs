using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictTaskManagementHistories.Dto
{
    public class SocialConflictTaskManagementHistoryGetMatrixExcelDto : EntityDto
    {
        public string CreatorUser { get; set; }
        public DateTime CreationTime { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public string To { get; set; }
        public string Copy  { get; set; }
    }
}
