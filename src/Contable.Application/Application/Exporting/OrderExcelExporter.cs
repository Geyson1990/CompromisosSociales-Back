using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Contable.Application.Orders.Dto;
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
    public class OrderExcelExporter : NpoiExcelExporterBase, IOrderExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;
        private readonly IWebHostEnvironment _env;

        public OrderExcelExporter(
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

        public FileDto ExportMatrixToFile(List<OrderGetMatrixExcelDto> orderdListDtos)
        {
            return CreateExcelPackage(
                "Pedidos.xlsx",
                    excelPackage =>
                    {
                        var sheet = excelPackage.CreateSheet("Pedidos");

                        int initRow = 4;

                        AddHeader(
                        sheet, initRow,
                        "Código",
                        "Documento",
                        "Código del caso",
                        "Denominación del caso",
                        "Unidad territorial",
                        "Departamento",
                        "Provincia",
                        "Distrito",
                        "Categoria",
                        "Denominación del proyecto/actividad",                       
                        "Código SNIP",
                        "Código unificado",
                        "Descripción",
                        "Responsable",
                        "Observaciones",
                        "Fecha del pedido",
                        "Fecha de registro"
                        );

                        initRow++;

                        AddObjects(excelPackage, sheet, initRow, orderdListDtos,
                        _ => _.Code,
                        _ => _.Document,
                        _ => _.SocialConflict != null? _.SocialConflict.Code : "",
                        _ => _.SocialConflict != null? _.SocialConflict.CaseName : "",
                        _ => _.TerritorialUnit != null? _.TerritorialUnit.Name : "",
                        _ => _.Department != null? _.Department.Name : "",
                        _ => _.Province != null? _.Province.Name : "",
                        _ => _.District != null ? _.District.Name : "",
                        _ => _.Type.ToString(),
                        _ => _.Name,
                        _ => _.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.SNIPCode : "") : "NA",
                        _ => _.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.UnifiedCode : "") : "NA",
                        _ => _.Description,
                        _ => _.Responsible,
                        _ => _.Observation,
                        _ => _.OrderDate,
                        _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId())
                    );

                    for (var i = 0; i < initRow + orderdListDtos.Count; i++)
                    {
                        if (sheet.GetRow(i + initRow) != null)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[15], "dd/mm/yyyy");
                    }

                    for (var i = 0; i < 16; i++)
                        sheet.AutoSizeColumn(i);


                    SetHeading(excelPackage, sheet, "Inventario de Pedidos");                   
                }
            );
           
        }

    }
}

