using Contable.Application.Exporting.Dto;
using Contable.Application.SocialConflictAlerts.Dto;
using Contable.DataExporting.Excel.NPOI;
using Contable.Dto;
using Contable.Storage;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    public class SocialConflictAlertExcelExporter : NpoiExcelExporterBase, ISocialConflictAlertExcelExporter
    {
        public SocialConflictAlertExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        private void SetHeading(ISheet sheet, string title)
        {
            CreateBoldCell(sheet, 0, 0, title, HorizontalAlignment.Center);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 7));
        }

        public FileDto ExportMatrizToFile(List<SocialConflictAlertMatrizExportDto> records)
        {
            return CreateExcelPackage("ALERTAS.xlsx", excelPackage =>
            {
                var sheet = excelPackage.CreateSheet("ALERTAS");
                var initRow = 0;

                SetHeading(sheet, "Listado Alertas de situaciones conflictivas (Matriz Alertas de situaciones conflictivas)");
                initRow++;

                AddHeader(sheet, initRow,
                "Código",
                "Fecha de emisión",
                "Nombre de la alerta",
                "Nivel de riesgo",
                "Fecha",
                "Hora",
                "Descripción",
                "Información principal",
                "Unidad Territorial",
                "Caso",
                "Departamento",
                "Provincia",
                "Distrito",
                "Centro Poblado",
                "Localidad-Comunidad-Otros",
                "Tipo de demanda",
                "Interés o Demanda",
                "Tipología del conflicto",
                "Tipología detallada",
                "Subsecretaría responsable",
                "Responsable de la alerta",
                "Responsable de la intervención",
                "Coordinador",
                "Acciones realizadas desde el ejecutivo",
                "Recomendaciones",
                "Información adicional",
                "Fuente",
                "Tipo de fuente",
                "Link de consulta",
                "Actor",
                "Posición",
                "Interés",
                "Atención de los sectores",
                "Fecha",
                "Hora",
                "Descripción",
                "Actualización de alerta",
                "Fecha",
                "Hora",
                "Cierre de alerta",
                "Fecha",
                "Hora",
                "Observación",
                "Registrado por",
                "Fecha de registro",
                "Última actualización",
                "Fecha de actualización");
                initRow++;

                AddObjects(excelPackage, sheet, initRow, records,
                //Aspectos generales
                _ => new ExportCell(_.AlertCode),
                _ => new ExportCell(_.AlertTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.AlertName),
                //Nivel de Riesgo
                _ => new ExportCell(_.LastCaseRisk),
                _ => new ExportCell(_.LastCaseRiskTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastCaseRiskTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastCaseRiskDescription),
                //Aspectos generales
                _ => new ExportCell(_.Information),
                _ => new ExportCell(_.TerritorialUnit),
                _ => new ExportCell(_.CaseName),
                _ => new ExportCell(_.Departments),
                _ => new ExportCell(_.Provinces),
                _ => new ExportCell(_.Districts),
                _ => new ExportCell(_.Regions),
                _ => new ExportCell(_.Ubications),
                //Información complementaria
                _ => new ExportCell(_.DemandType),
                _ => new ExportCell(_.Demand),
                _ => new ExportCell(_.TypologyDescription),
                _ => new ExportCell(_.SubTypologyDescription),
                _ => new ExportCell(_.AlertResponsible),
                _ => new ExportCell(_.AnalystName),
                _ => new ExportCell(_.ManagerName),
                _ => new ExportCell(_.CoordinatorName),
                _ => new ExportCell(_.Actions),
                _ => new ExportCell(_.Recommendations),
                _ => new ExportCell(_.AditionalInformation),
                _ => new ExportCell(_.Source),
                _ => new ExportCell(_.SourceType),
                _ => new ExportCell(_.Link),
                //Actores
                _ => new ExportCell(_.ActorDescriptions),
                _ => new ExportCell(_.ActorPositions),
                _ => new ExportCell(_.ActorInterests),
                //Atención de Sectores
                _ => new ExportCell(_.Attention),
                _ => new ExportCell(_.AttentionTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.AttentionTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.AttentionDescription),
                //Actualización de la alerta
                _ => new ExportCell(_.LastStateDescription),
                _ => new ExportCell(_.LastStateTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastStateTime, "dd/mm/yyyy", ExportCellType.Date),
                //Cierre de alerta
                _ => new ExportCell(_.LastSeal),
                _ => new ExportCell(_.LastSealTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastSealTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastSealDescription),
                //Aspectos generales
                _ => new ExportCell(_.CreatorUser),
                _ => new ExportCell(_.CreationTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastModificationUser),
                _ => new ExportCell(_.LastModificationTime, "dd/mm/yyyy", ExportCellType.Date)
                );

                for (var i = 0; i < records.Count; i++)
                {
                    if (sheet.GetRow(i + initRow) != null)
                    {
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[1], records[i].AlertTime, "dd/mm/yyyy");

                        if (records[i].LastCaseRiskTime.HasValue)
                        {
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[4], records[i].LastCaseRiskTime.Value, "dd/mm/yyyy");
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[5], records[i].LastCaseRiskTime.Value, "HH:mm");
                        }

                        if (records[i].AttentionTime.HasValue)
                        {
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[33], records[i].AttentionTime.Value, "dd/mm/yyyy");
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[34], records[i].AttentionTime.Value, "HH:mm");
                        }

                        if (records[i].LastStateTime.HasValue)
                        {
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[37], records[i].LastStateTime.Value, "dd/mm/yyyy");
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[38], records[i].LastStateTime.Value, "HH:mm");
                        }

                        if (records[i].LastSealTime.HasValue)
                        {
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[40], records[i].LastSealTime.Value, "dd/mm/yyyy");
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[41], records[i].LastSealTime.Value, "HH:mm");
                        }

                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[44], records[i].CreationTime, "dd/mm/yyyy");

                        if (records[i].LastModificationTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[46], records[i].LastModificationTime.Value, "dd/mm/yyyy");
                    }
                }

                for (var i = 0; i < 46; i++)
                    sheet.SetColumnWidth(i, 5000);
            });
        }
    }
}
