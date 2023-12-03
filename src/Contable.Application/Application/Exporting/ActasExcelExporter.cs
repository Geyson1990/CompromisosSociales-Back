using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Contable.Application.Exporting.Dto;
using Contable.Application.SocialConflicts.Dto;
using Contable.DataExporting.Excel.NPOI;
using Contable.Dto;
using Contable.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Contable.Application.Exporting
{
    public class ActasExcelExporter : NpoiExcelExporterBase, IActasExcelExporter
    {
        public ActasExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        private void SetHeading(ISheet sheet, string title)
        {
            CreateBoldCell(sheet, 0, 0, title, HorizontalAlignment.Center);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 7));
        }

        public byte[] ExportMatrizToFile(List<ActaMatrizExportDto> records)
        {
            return CreateExcelPackageCustom("ACTAS_CONFLICTIVIDAD.xlsx", excelPackage =>
            {
                var sheet = excelPackage.CreateSheet("ACTAS");
                var initRow = 0;

                SetHeading(sheet, "Listado de Actas");
                initRow++;

                AddHeader(sheet, initRow,
                "Unidad Territorial",
                "Nombre de conflicto social",
                "Actas"                );
                initRow++;

                AddObjects(excelPackage, sheet, initRow, records,
                //Aspectos generales
               
                _ => new ExportCell(_.TerritorialUnit),
                _ => new ExportCell(_.SocialConflictName),
                _ => new ExportCell(_.Actas)                
                );
                             

            });
        }

       
    }
}
