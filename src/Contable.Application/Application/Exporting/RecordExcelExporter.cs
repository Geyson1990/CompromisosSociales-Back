using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Contable.Application.Exporting.Dto;
using Contable.Application.Records.Dto;
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
    public class RecordExcelExporter : NpoiExcelExporterBase, IRecordExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;
        private readonly IWebHostEnvironment _env;

        public RecordExcelExporter(
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

            CreateBoldCell(sheet, 3, 0, "Reporte emitido el : " + DateTime.Now.ToString("dd/mm/yyyy hh:mm tt"), HorizontalAlignment.Left);
        }

        public FileDto ExportMatrixToFile(List<RecordGetMatrixExcelDto> recordListDtos)
        {
            return CreateExcelPackage(DateTime.Now.ToString("MM_dd_yyyy_HH_mm_") + "INVENTARIO_DE_ACTAS.xlsx", excelPackage => {
                var sheet = excelPackage.CreateSheet("Actas");

                int initRow = 0;

                AddHeader(
                    sheet, 
                    initRow,
                    "Unidad Territorial",
                    "Departamento",
                    "Provincia",
                    "Distrito",
                    "Código del caso",
                    "Denominación del caso",
                    "Mesa de diálogo",
                    "Código del acta",
                    "Título del acta",
                    "Fecha del acta",
                    "Temática mujer",
                    "Títulos del documentos",
                    "Tipos de documentos de sustento",
                    "Fecha de registro",
                    "Registrado por",
                    "Última actualización",
                    "Actualizado por"
                    );

                    initRow++;

                    AddObjects(excelPackage, sheet, initRow, recordListDtos,
                    _ => new ExportCell(_.TerritorialUnits),
                    _ => new ExportCell(_.Departments),
                    _ => new ExportCell(_.Provinces),
                    _ => new ExportCell(_.Districts),
                    _ => new ExportCell(_.SocialConflict.Code),
                    _ => new ExportCell(_.SocialConflict.CaseName),
                    _ => new ExportCell(_.SocialConflict.Dialog),
                    _ => new ExportCell(_.Code),
                    _ => new ExportCell(_.Title),
                    _ => new ExportCell(_.RecordTime, "dd/mm/yyyy", ExportCellType.Date),
                    _ => new ExportCell(_.WomanCompromise ? "SI": "NO"),
                    _ => new ExportCell(_.ResourcesNames),
                    _ => new ExportCell(_.ResourcesTypes),
                    _ => new ExportCell(_.CreationTime, "dd/mm/yyyy", ExportCellType.Date),
                    _ => new ExportCell(_.CreatorUser == null ? "" : $"{_.CreatorUser.Name ?? ""} {_.CreatorUser.Surname ?? ""}"),
                    _ => new ExportCell(_.LastModificationTime, "dd/mm/yyyy", ExportCellType.Date),
                    _ => new ExportCell(_.EditUser == null ? "" : $"{_.EditUser.Name ?? ""} {_.EditUser.Surname ?? ""}")
                );

                for (var i = 0; i < recordListDtos.Count; i++)
                {
                    if (sheet.GetRow(i + initRow) != null)
                    {
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[9], recordListDtos[i].RecordTime, "dd/mm/yyyy");
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[13], recordListDtos[i].CreationTime, "dd/mm/yyyy");
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[15], recordListDtos[i].LastModificationTime, "dd/mm/yyyy");
                    }
                }

                for (var i = 0; i < 17; i++)
                    sheet.SetColumnWidth(i, 5000);
            });

        }

    }
}

