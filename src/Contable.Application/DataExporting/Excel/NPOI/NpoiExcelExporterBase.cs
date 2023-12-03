using System;
using System.Collections.Generic;
using System.IO;
using Abp.AspNetZeroCore.Net;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Contable.Dto;
using Contable.Storage;
using NPOI.SS.UserModel;
using NPOI.SS;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Hosting;
using Contable.Application.Exporting.Dto;
using System.Text.RegularExpressions;

namespace Contable.DataExporting.Excel.NPOI
{
    public abstract class NpoiExcelExporterBase : ContableServiceBase, ITransientDependency
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private static IDataFormat _formater;

        protected NpoiExcelExporterBase(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        protected FileDto CreateExcelPackage(string fileName, Action<XSSFWorkbook> creator)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            var workbook = new XSSFWorkbook();

            if (_formater == null)
                _formater = workbook.CreateDataFormat();

            creator(workbook);

            Save(workbook, file);

            return file;
        }

        protected void CreateBoldCell(ISheet sheet, int row, int col, string value, HorizontalAlignment align = HorizontalAlignment.Center, short color = -1)
        {
            var cell = sheet.CreateRow(row).CreateCell(col);
            cell.SetCellValue(value);
            var cellStyle = sheet.Workbook.CreateCellStyle();
            var font = sheet.Workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 13;
            cellStyle.SetFont(font);
            cellStyle.Alignment = align;
            if (color != -1) font.Color = color;
            cell.CellStyle = cellStyle;
        }

        protected void AddHeader(ISheet sheet, int row, params string[] headerTexts)
        {
            if (headerTexts.IsNullOrEmpty())
            {
                return;
            }

            sheet.CreateRow(row);

            for (var i = 0; i < headerTexts.Length; i++)
            {
                AddHeader(sheet, row, i, headerTexts[i]);
            }
        }

        protected void AddHeader(ISheet sheet, params string[] headerTexts)
        {
            AddHeader(sheet, 0, headerTexts);
        }

        protected void AddHeader(ISheet sheet, int row, int columnIndex, string headerText)
        {
            var cell = sheet.GetRow(row).CreateCell(columnIndex);
            cell.SetCellValue(headerText);
            var cellStyle = sheet.Workbook.CreateCellStyle();
            var font = sheet.Workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 12;
            cellStyle.SetFont(font);
            cellStyle.BorderBottom = BorderStyle.Medium;
            cellStyle.BorderTop = BorderStyle.Medium;
            cellStyle.BorderLeft = BorderStyle.Medium;
            cellStyle.BorderRight = BorderStyle.Medium;
            cell.CellStyle = cellStyle;
        }

        protected void AddHeader(ISheet sheet, int columnIndex, string headerText)
        {
            AddHeader(sheet, 0, columnIndex, headerText);
        }

        protected void AddObjects<T>(XSSFWorkbook excelPackage, ISheet sheet, int startRowIndex, IList<T> items, params Func<T, object>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
            {
                return;
            }

            for (var i = startRowIndex; i < startRowIndex + items.Count; i++)
            {
                var row = sheet.CreateRow(i);

                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    var cell = row.CreateCell(j);

                    var value = propertySelectors[j](items[i - startRowIndex]);
                    if (value != null)
                    {
                        cell.SetCellValue(value.ToString());
                    }
                }
            }
        }

        protected void AddObjects<T>(XSSFWorkbook excelPackage, ISheet sheet, int startRowIndex, IList<T> items, params Func<T, ExportCell>[] propertySelectors)
        {
            if (items.IsNullOrEmpty() || propertySelectors.IsNullOrEmpty())
                return;

            for (var i = startRowIndex; i < startRowIndex + items.Count; i++)
            {
                var row = sheet.CreateRow(i);

                for (var j = 0; j < propertySelectors.Length; j++)
                {
                    var cell = row.CreateCell(j);

                    var cellValue = propertySelectors[j](items[i - startRowIndex]);

                    if (cellValue != null)
                    {
                        if (cellValue.Type == ExportCellType.Numeric)
                        {
                            var value = Regex.Replace((cellValue.Value ?? "0").Trim(), @"[^0-9.]", "");

                            if (!string.IsNullOrEmpty(value) && (cellValue.HasEmpty || (!cellValue.HasEmpty && value != "0")))
                            {
                                cell.SetCellType(CellType.Numeric);

                                if (value.LastIndexOf('.') == -1)
                                {
                                    cell.SetCellValue(Convert.ToInt64(value));
                                }
                                else
                                {
                                    var cellStyle = excelPackage.CreateCellStyle();
                                    cellStyle.DataFormat = _formater.GetFormat("###,###,##0.00");
                                    cell.CellStyle = cellStyle;
                                    cell.SetCellValue(Convert.ToDouble(value));
                                }
                            }
                            else
                            {
                                cell.SetCellType(CellType.Blank);
                            }
                        }
                        if (cellValue.Type == ExportCellType.Decimal)
                        {
                            if (!cellValue.HasEmpty && cellValue.Decimal == 0)
                            {
                                cell.SetCellType(CellType.Blank);
                            }
                            else
                            {
                                var cellStyle = excelPackage.CreateCellStyle();
                                cellStyle.DataFormat = _formater.GetFormat("###,###,##0.00");
                                cell.CellStyle = cellStyle;
                                cell.SetCellType(CellType.Numeric);
                                cell.SetCellValue(Convert.ToDouble(cellValue.Decimal));
                            }
                        }
                        if (cellValue.Type == ExportCellType.DateTime)
                        {
                            if (cellValue.DateTime.HasValue)
                            {
                                var cellDateFormat = excelPackage.CreateDataFormat();
                                var cellStyle = excelPackage.CreateCellStyle();
                                cellStyle.DataFormat = cellDateFormat.GetFormat(cellValue.Format);

                                cell.CellStyle = cellStyle;
                                cell.SetCellValue(cellValue.DateTime.Value);
                            }
                            else
                            {
                                cell.SetCellType(CellType.Blank);
                            }
                        }
                        if (cellValue.Type == ExportCellType.Date)
                        {
                            if (cellValue.DateTime.HasValue)
                            {
                                var cellStyle = excelPackage.CreateCellStyle();
                                cellStyle.DataFormat = _formater.GetFormat(cellValue.Format);

                                cell.CellStyle = cellStyle;
                                cell.SetCellValue(new DateTime(cellValue.DateTime.Value.Year, cellValue.DateTime.Value.Month, cellValue.DateTime.Value.Day));
                            }
                            else
                            {
                                cell.SetCellType(CellType.Blank);
                            }
                        }
                        if (cellValue.Type == ExportCellType.String)
                        {
                            cell.SetCellType(CellType.String);
                            cell.SetCellValue(cellValue.Value);
                        }

                    }
                }
            }
        }

        public void Save(string fileToken, byte[] bytes)
        {
            _tempFileCacheManager.SetFile(fileToken, bytes);
        }

        protected void Save(XSSFWorkbook excelPackage, FileDto file)
        {
            using (var stream = new MemoryStream())
            {
                excelPackage.Write(stream);
                _tempFileCacheManager.SetFile(file.FileToken, stream.ToArray());
            }
        }

        protected void SetCellDataFormat(ICell cell, string dataFormat)
        {
            if (cell == null)
            {
                return;
            }

            var dateStyle = cell.Sheet.Workbook.CreateCellStyle();
            var format = cell.Sheet.Workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat(dataFormat);
            cell.CellStyle = dateStyle;
            if (DateTime.TryParse(cell.StringCellValue, out var datetime))
            {
                cell.SetCellValue(datetime);
            }
        }

        protected void SetCellDataFormat(ICell cell, DateTime? dateTime, string dataFormat)
        {
            if (cell == null)
            {
                return;
            }

            var dateStyle = cell.Sheet.Workbook.CreateCellStyle();
            var format = cell.Sheet.Workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat(dataFormat);
            cell.CellStyle = dateStyle;

            if (dateTime.HasValue)
                cell.SetCellValue(dateTime.Value);
        }

        protected byte[] CreateExcelPackageCustom(string fileName, Action<XSSFWorkbook> creator)
        {
            using (var memoryStream = new MemoryStream())
            {
                var workbook = new XSSFWorkbook();

                if (_formater == null)
                    _formater = workbook.CreateDataFormat();

                creator(workbook);

                SaveWorkBook(workbook, memoryStream);

                return memoryStream.ToArray();
            }
        }

        private void SaveWorkBook(XSSFWorkbook workbook, Stream stream)
        {
            workbook.Write(stream);
            stream.Position = 0;
        }
    }
}
