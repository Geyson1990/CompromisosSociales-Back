using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Contable.Application.SocialConflictTaskManagements.Dto;
using Contable.Application.TaskManagements.Dto;
using Contable.DataExporting.Excel.NPOI;
using Contable.Dto;
using Contable.Storage;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace Contable.Application.Exporting
{
    public class TaskExcelExporter : NpoiExcelExporterBase, ITaskExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;
        private readonly IWebHostEnvironment _env;

        public TaskExcelExporter(
            IWebHostEnvironment env,
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager)
            : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
            _env = env;
        }

        private void SetHeading(XSSFWorkbook excelPackage, ISheet sheet, string tittle)
        {
            byte[] data = File.ReadAllBytes(Path.Combine(_env.WebRootPath, "Common", "Images", "logopcm.png"));
            int pictureIndex = excelPackage.AddPicture(data, PictureType.PNG);
            IClientAnchor anchor = excelPackage.GetCreationHelper().CreateClientAnchor();
            anchor.Row1 = 0;
            anchor.Col1 = 0;
            sheet.CreateDrawingPatriarch().CreatePicture(anchor, pictureIndex).Resize();

            CreateBoldCell(sheet, 1, 2, "PLATAFORMA DE GESTIÓN DE CONFLICTOS", HorizontalAlignment.Center, IndexedColors.Red.Index);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, 8));

            CreateBoldCell(sheet, 2, 2, tittle, HorizontalAlignment.Center);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 2, 8));

            CreateBoldCell(sheet, 3, 0, "Reporte emitido el : " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"), HorizontalAlignment.Left);
        }

        public FileDto ExportMatrixToFile(List<TaskManagementGetMatrixExcelDto> taskListDtos)
        {
            return CreateExcelPackage(
                "Tareas.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.CreateSheet("Tareas");
                     int initRow = 4;
                     AddHeader(
                        sheet, initRow,
                        "Denominación del compromiso",
                        "Acuerdo de gestión",
                        "Responsable",
                        "Fecha de reunión",
                        "PIP/Código SNIP",
                        "PIP/Código Unificado",
                        "PIP/Proyecto",
                        "Plazo",
                        "Alerta",
                        "Criterio de estado",
                        "Avance",
                        "Fecha de registro"
                     );

                     initRow++;

                     AddObjects(excelPackage, sheet, initRow, taskListDtos,
                        _ => _.CompromiseName,
                        _ => _.Title,
                        _ => _.Responsible,
                        _ => _.StartTime,
                        _ => _.PIPMEF != null ? _.PIPMEF.SNIPCode : "NA",
                        _ => _.PIPMEF != null ? _.PIPMEF.UnifiedCode : "NA",
                        _ => _.PIPMEF != null ? _.PIPMEF.ProjectName : "NA",
                        _ => _.Deadline,
                        _ => _.Alert,
                        _ => _.Status.ToString(),
                        _ => _.Advance,
                        _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId())
                    );

                     for (var i = 0; i < initRow + taskListDtos.Count; i++)
                     {
                         if (sheet.GetRow(i + initRow) != null)
                         {
                             SetCellDataFormat(sheet.GetRow(i + initRow).Cells[3], "dd/mm/yyyy");
                             SetCellDataFormat(sheet.GetRow(i + initRow).Cells[7], "dd/mm/yyyy");
                         }
                     }

                     for (var i = 0; i < 12; i++)
                         sheet.AutoSizeColumn(i);
                     sheet.SetRowBreak(10);

                     SetHeading(excelPackage, sheet, "Inventario de Tareas");
                 });
        }

        public FileDto ExportMatrixToFile(List<SocialConflictTaskManagementGetAllDto> taskListDtos)
        {
            return CreateExcelPackage("Tareas_Conflictos.xlsx",excelPackage =>
            {
                var sheet = excelPackage.CreateSheet("Tareas");
                int initRow = 4;
                AddHeader(sheet, initRow,
                   "Tipo",
                   "Código del caso",
                   "Denominación",
                   "Unidad territorial",
                   "Tarea",
                   "Fecha de inicio",
                   "Fecha vencimiento",
                   "Estado"
                );

                initRow++;

                AddObjects(excelPackage, sheet, initRow, taskListDtos,
                   _ => _.ConflictSite == ConflictSite.SocialConflict ? "Conflicto" :
                        _.ConflictSite == ConflictSite.SocialConflictAlert ? "Alerta" :
                        _.ConflictSite == ConflictSite.SocialConflictSensible ? "Situación sensible" : "",
                   _ => _.ConflictCode,
                   _ => _.ConflictName,
                   _ => _.ConflictTerritorialUnits,
                   _ => _.Title,
                   _ => _.StartTime,
                   _ => _.Deadline,
                   _ => _.Status == TaskStatus.Pending ? "Pendiente" :
                        _.Status == TaskStatus.Completed ? "Completada" :
                        _.Status == TaskStatus.NonCompleted ? "No completada" : ""
               );

                for (var i = 0; i < initRow + taskListDtos.Count; i++)
                {
                    if (sheet.GetRow(i + initRow) != null)
                    {
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[5], "dd/mm/yyyy");
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[6], "dd/mm/yyyy");
                    }
                }

                for (var i = 0; i < 12; i++)
                    sheet.AutoSizeColumn(i);

                sheet.SetRowBreak(10);

                SetHeading(excelPackage, sheet, "Inventario de Tareas");
            });
        }
    }
}

