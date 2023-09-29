using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SocialConflictAlertHistories.Dto
{
    public class SocialConflictAlertHistoryMatrizExportDto
    {
        public string CreatorUser { get; set; }
        public DateTime CreationTime { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public string To { get; set; }
        public string Copy { get; set; }
        public string Files { get; set; }
    }
}
