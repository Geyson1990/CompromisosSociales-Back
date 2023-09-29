using Abp.AspNetZeroCore.Net;
using Contable.Application.Exporting.Dto;
using Contable.Application.SocialConflictAlertHistories.Dto;
using Contable.DataExporting.Excel.NPOI;
using Contable.Dto;
using Contable.Storage;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    public class SocialConflictAlertHistoryExcelExporter : NpoiExcelExporterBase, ISocialConflictAlertHistoryExcelExporter
    {
        public SocialConflictAlertHistoryExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        public FileDto ExportMatrizToFile(byte[] report)
        {
            var file = new FileDto("HISTORIAL_ENVIO_ALERTAS.xlsx", MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);

            Save(file.FileToken, report);

            return file;
        }
    }
}
