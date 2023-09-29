using Contable.Application.SocialConflictAlerts.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    public interface ISocialConflictAlertExcelExporter
    {
        FileDto ExportMatrizToFile(List<SocialConflictAlertMatrizExportDto> records);
    }
}
