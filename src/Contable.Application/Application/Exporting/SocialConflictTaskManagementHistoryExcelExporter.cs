using Abp.AspNetZeroCore.Net;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Contable.Application.Exporting.Dto;
using Contable.Application.SocialConflictTaskManagementHistories.Dto;
using Contable.DataExporting.Excel.NPOI;
using Contable.Dto;
using Contable.Storage;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    public class SocialConflictTaskManagementHistoryExcelExporter : NpoiExcelExporterBase, ISocialConflictTaskManagementHistoryExcelExporter
    {
        public SocialConflictTaskManagementHistoryExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {

        }

        public FileDto ExportMatrixToFile(byte[] report)
        {
            var file = new FileDto("HISTORIAL_ENVIO_NOTIFICACIONES_TC.xlsx", MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);

            Save(file.FileToken, report);

            return file;
        }
    }
}
