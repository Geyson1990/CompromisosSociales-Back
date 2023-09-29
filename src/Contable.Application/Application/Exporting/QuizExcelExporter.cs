using Contable.Application.Exporting.Dto;
using Contable.Application.QuizDetails.Dto;
using Contable.DataExporting.Excel.NPOI;
using Contable.Dto;
using Contable.Storage;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    internal class QuizExcelExporter : NpoiExcelExporterBase, IQuizExcelExporter
    {
        public QuizExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        private void SetHeading(ISheet sheet, string title)
        {
            CreateBoldCell(sheet, 0, 0, title, HorizontalAlignment.Center);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 7));
        }

        public FileDto ExportMatrizToFile(List<QuizDetailExcelExportDto> records, List<string> headers, QuizCompleteType type)
        {
            var fileName = type == QuizCompleteType.PUBLIC ? "MATRIZ_ENCUESTA_CIUDADANO.xlsx" : "MATRIZ_ENCUESTA_USUARIO.xlsx";
            var sheetName = type == QuizCompleteType.PUBLIC ? "Matriz Encuesta Ciudadano" : "Matriz Encuesta al Usuario";
            var headerName = type == QuizCompleteType.PUBLIC ? "Matriz Encuesta Ciudadano" : "Matriz Encuesta al Usuario";

            return CreateExcelPackage(fileName, excelPackage =>
            {
                var sheet = excelPackage.CreateSheet(sheetName);
                var initRow = 0;

                SetHeading(sheet, headerName);
                initRow++;

                AddHeader(sheet, initRow,
                "Nombre",
                "Apellidos Paterno",
                "Apellido materno",
                "Correo electrónico",
                "Tipo de envió del formulario",
                "Estado",
                "Fecha de creación",
                "Hora de creación",
                "Fecha de actualización",
                "Hora de actualización",
                "Usuario que realizó la actualización");

                for (var i = 0; i < headers.Count; i++)
                    AddHeader(sheet, 1, i + 11, headers[i]);

                AddHeader(sheet, 1, headers.Count + 11, "Recursos");

                initRow++;

                AddObjects(excelPackage, sheet, initRow, records,
                //Aspectos generales
                _ => new ExportCell(_.Name),
                _ => new ExportCell(_.Surname),
                _ => new ExportCell(_.SecondSurname),
                _ => new ExportCell(_.EmailAddress),
                _ => new ExportCell(_.Type == QuizCompleteType.ADMINITRATIVE ? "Usuario" : _.Type == QuizCompleteType.PUBLIC ? "Ciudadano" : ""),
                _ => new ExportCell(_.State),
                _ => new ExportCell(_.CreationTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.CreationTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastModificationTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastModificationTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.AdministrativeUser)
                );

                for (var i = 0; i < records.Count; i++)
                {
                    if (sheet.GetRow(i + initRow) != null)
                    {
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[6], records[i].CreationTime, "dd/mm/yyyy");
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[7], records[i].CreationTime, "HH:mm");

                        if (records[i].LastModificationTime.HasValue)
                        {
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[8], records[i].LastModificationTime.Value, "dd/mm/yyyy");
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[9], records[i].LastModificationTime.Value, "HH:mm");
                        }

                        for(var q = 0; q < records[i].Quetions.Count; q++)
                        {
                            var cell = sheet.GetRow(i + initRow).CreateCell(q + 11);
                            cell.SetCellValue(records[i].Quetions[q].Description);
                        }

                        var resourceCell = sheet.GetRow(i + initRow).CreateCell(records[i].Quetions.Count + 11);
                        resourceCell.SetCellValue(records[i].Resources ?? "");
                    }
                }

                for (var i = 0; i < (headers.Count + 11); i++)
                    sheet.SetColumnWidth(i, 5000);
            });

        }
    }
}
