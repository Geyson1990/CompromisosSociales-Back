using Contable.Application.SocialConflictAlertHistories.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    public interface ISocialConflictAlertHistoryExcelExporter
    {
        FileDto ExportMatrizToFile(byte[] report);
    }
}
