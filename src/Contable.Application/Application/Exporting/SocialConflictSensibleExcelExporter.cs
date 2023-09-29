using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Contable.Application.Exporting.Dto;
using Contable.Application.SocialConflictSensibles.Dto;
using Contable.DataExporting.Excel.NPOI;
using Contable.Dto;
using Contable.Storage;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Contable.Application.Exporting
{
    public class SocialConflictSensibleExcelExporter : NpoiExcelExporterBase, ISocialConflictSensibleExcelExporter
    {
        public SocialConflictSensibleExcelExporter(ITempFileCacheManager tempFileCacheManager) : base(tempFileCacheManager)
        {
        }

        private void SetHeading(ISheet sheet, string title)
        {
            CreateBoldCell(sheet, 0, 0, title, HorizontalAlignment.Center);
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 7));
        }

        public FileDto ExportMatrizToFile(List<SocialConflictSensibleMatrizExportDto> records)
        {
            return CreateExcelPackage("SITUACIONES_SENSIBLES.xlsx", excelPackage =>
            {
                var sheet = excelPackage.CreateSheet("SITUACIONES SENSIBLES");
                var initRow = 0;

                SetHeading(sheet, "Listado de Situaciones Sensibles al Conflicto (Matriz de Situaciones Sensibles al Conflicto)");
                initRow++;

                AddHeader(sheet, initRow,
                "Código de caso",
                "Nombre del caso detallado",
                "Problemática",
                "Nivel de riesgo",
                "Fecha",
                "Observación",
                "Estado de la situación sensible al conflicto",
                "Fecha de estado",
                "Observación",
                "Unidad Territorial",
                "Departamento",
                "Provincia",
                "Distrito",
                "Centro Poblado",
                "Localidad-Comunidad-Otros",
                "Cobertura geográfica",
                "Coordinador de la UT",
                "Gestor responsable de la situación sensible al conflicto",
                "Responsable de la SSPI (Analista)",
                "Tipología del conflicto",
                "Actor",
                "Posición",
                "Interés",
                "Fecha",
                "Tipo de gestión",
                "Gestión",
                "Responsable de la gestión",
                "Fecha",
                "Situación actual (Interna)",
                "Proyección y acciones propuestas",
                "Persona que registra",
                "Estado (Nombre de la situación sensible al conflicto)",
                "Estado (Problemática)",
                "Estado (Nivel de riesgo)",
                "Estado (Estado del caso)",
                "Estado (Gestión realizada)",
                "Estado (Situación actual)",
                "Registrado por",
                "Fecha de registro",
                "Última actualización por",
                "Última fecha de actualización");
                initRow++;

                AddObjects(excelPackage, sheet, initRow, records,
                //Aspectos generales
                _ => new ExportCell(_.Code),
                _ => new ExportCell(_.CaseName),
                _ => new ExportCell(_.Problem),
                //Nivel de Riesgo
                _ => new ExportCell(_.LastRisk),
                _ => new ExportCell(_.LastRiskTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastRiskDescription),
                //Estado del caso
                _ => new ExportCell(_.LastCondition),
                _ => new ExportCell(_.LastConditionTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastConditionDescription),
                //Aspectos generales
                _ => new ExportCell(_.TerritorialUnits),
                _ => new ExportCell(_.Departments),
                _ => new ExportCell(_.Provinces),
                _ => new ExportCell(_.Districts),
                _ => new ExportCell(_.Regions),
                _ => new ExportCell(_.Ubications),
                _ => new ExportCell(_.GeographicType == GeographycType.National ? "Nacional" : _.GeographicType == GeographycType.Location ? "Regional" : "Local"),
                _ => new ExportCell(_.CoordinatorName),
                _ => new ExportCell(_.ManagerName),
                _ => new ExportCell(_.AnalystName),
                _ => new ExportCell(_.TypologyDescription),
                //Actores
                _ => new ExportCell(_.ActorDescriptions),
                _ => new ExportCell(_.ActorPositions),
                _ => new ExportCell(_.ActorInterests),
                //Gestiones realizada
                _ => new ExportCell(_.LastManagementTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastManagement),
                _ => new ExportCell(_.LastConditionDescription),
                _ => new ExportCell(_.LastManagementManager),
                //Situación actual
                _ => new ExportCell(_.LastStateTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastState),
                _ => new ExportCell(_.LastStateDescription),
                _ => new ExportCell(_.LastStateManager),
                //Estado (Aprobado/No aprobado)
                _ => new ExportCell(_.CaseNameVerification ? "Aprobado" : "No aprobado"),
                _ => new ExportCell(_.ProblemVerification ? "Aprobado" : "No aprobado"),
                _ => new ExportCell(_.RiskVerification ? "Aprobado" : "No aprobado"),
                _ => new ExportCell(_.ConditionVerification ? "Aprobado" : "No aprobado"),
                _ => new ExportCell(_.ManagementVerification ? "Aprobado" : "No aprobado"),
                _ => new ExportCell(_.StateVerification ? "Aprobado" : "No aprobado"),
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
                        if (records[i].LastRiskTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[4], records[i].LastRiskTime.Value, "dd/mm/yyyy");
                        if (records[i].LastStateTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[7], records[i].LastStateTime.Value, "dd/mm/yyyy");
                        if (records[i].LastManagementTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[23], records[i].LastManagementTime.Value, "dd/mm/yyyy");
                        if (records[i].LastStateTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[27], records[i].LastStateTime.Value, "dd/mm/yyyy");

                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[38], records[i].CreationTime, "dd/mm/yyyy");

                        if (records[i].LastModificationTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[40], records[i].LastModificationTime.Value, "dd/mm/yyyy");
                    }
                }

                for (var i = 0; i < 41; i++)
                    sheet.SetColumnWidth(i, 5000);
            });
        }

        public FileDto ExportManagementToFile(List<SocialConflictSensibleManagementExportDto> records)
        {
            return CreateExcelPackage("GESTIONES_REALIZADAS.xlsx", excelPackage =>
            {
                var sheet = excelPackage.CreateSheet("GESTIONES REALIZADAS");
                var initRow = 0;

                SetHeading(sheet, "Listado de Situaciones Sensibles al Conflicto (Gestiones Realizadas)");
                initRow++;

                AddHeader(sheet, initRow,
                "Código",
                "Nivel de riesgo",
                "Fecha (Nivel de riesgo)",
                "Observación (Nivel de riesgo)",
                "Estado de la situación sensible al conflicto",
                "Fecha de estado",
                "Nombre del caso detallado",
                "Unidad Territorial",
                "Departamento",
                "Provincia",
                "Distrito",
                "Centro Poblado",
                "Localidad-Comunidad-Otros",
                "Fecha (Gestión realizada)",
                "Tipo de gestión",
                "Gestión",
                "Personas de sociedad civil - N° Hombres",
                "Personas de sociedad civil - N° Mujeres",
                "Funcionarios del estado - N° Hombres",
                "Funcionarios del estado - N° Mujeres",
                "Personas de la empresa - N° Hombres",
                "Personas de la empresa - N° Mujeres",
                "Responsable de la gestión",
                "Estado (Aprobado /No aprobado)");

                initRow++;

                AddObjects(excelPackage, sheet, initRow, records,
                //Aspectos generales
                _ => new ExportCell(_.CaseCode),
                //Nivel de Riesgo
                _ => new ExportCell(_.LastCaseRisk),
                _ => new ExportCell(_.LastCaseRiskTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastCaseRiskDescription),
                //Aspectos generales
                _ => new ExportCell(_.CaseName),
                //Estado del Caso
                _ => new ExportCell(_.LastCaseCondition),
                _ => new ExportCell(_.LastCaseConditionTime, "dd/mm/yyyy", ExportCellType.Date),
                //Ubicación
                _ => new ExportCell(_.TerritorialUnits),
                _ => new ExportCell(_.Departments),
                _ => new ExportCell(_.Provinces),
                _ => new ExportCell(_.Districts),
                _ => new ExportCell(_.Regions),
                _ => new ExportCell(_.Ubications),
                //Gestiones realizadas
                _ => new ExportCell(_.ManagementTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.Management),
                _ => new ExportCell(_.ManagementDescription),
                _ => new ExportCell(_.CivilMen.HasValue ? $"{_.CivilMen.Value}" : "", ExportCellType.Numeric),
                _ => new ExportCell(_.CivilWomen.HasValue ? $"{_.CivilMen.Value}" : "", ExportCellType.Numeric),
                _ => new ExportCell(_.StateMen.HasValue ? $"{_.StateMen.Value}" : "", ExportCellType.Numeric),
                _ => new ExportCell(_.StateWomen.HasValue ? $"{_.StateWomen.Value}" : "", ExportCellType.Numeric),
                _ => new ExportCell(_.CompanyMen.HasValue ? $"{_.CompanyMen.Value}" : "", ExportCellType.Numeric),
                _ => new ExportCell(_.CompanyWomen.HasValue ? $"{_.CompanyWomen.Value}" : "", ExportCellType.Numeric),
                _ => new ExportCell(_.ManagementManager),
                _ => new ExportCell(_.Verification ? "Aprobado" : "No aprobado")
                );

                for (var i = 0; i < records.Count; i++)
                {
                    if (sheet.GetRow(i + initRow) != null)
                    {
                        if (records[i].LastCaseRiskTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[2], records[i].LastCaseRiskTime.Value, "dd/mm/yyyy");
                        if (records[i].LastCaseConditionTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[6], records[i].LastCaseConditionTime.Value, "dd/mm/yyyy");

                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[13], records[i].ManagementTime, "dd/mm/yyyy");
                    }
                }

                for (var i = 0; i < 24; i++)
                    sheet.SetColumnWidth(i, 5000);
            });
        }

        public FileDto ExportStateToFile(List<SocialConflictSensibleStateExportDto> records)
        {
            return CreateExcelPackage("SITUACION_ACTUAL.xlsx", excelPackage =>
            {
                var sheet = excelPackage.CreateSheet("SITUACIÓN ACTUAL");
                var initRow = 0;

                SetHeading(sheet, "Listado de Situaciones Sensibles al Conflicto (Situación Actual)");
                initRow++;


                AddHeader(sheet, initRow,
                "Código de caso",
                "Nivel de riesgo",
                "Fecha (Nivel de riesgo)",
                "Observación (Nivel de riesgo)",
                "Estado de la situación sensible al conflicto",
                "Fecha de estado",
                "Nombre del caso detallado",
                "Unidad Territorial",
                "Departamento",
                "Provincia",
                "Distrito",
                "Centro Poblado",
                "Localidad-Comunidad-Otros",
                "Fecha (Situación actual)",
                "Situación actual (Interna)",
                "Proyección y acciones propuestas",
                "Persona que registra",
                "Estado (Aprobado /No aprobado)");
                initRow++;

                AddObjects(excelPackage, sheet, initRow, records,
                //Aspectos generales
                _ => new ExportCell(_.CaseCode),
                //Nivel de Riesgo
                _ => new ExportCell(_.LastCaseRisk),
                _ => new ExportCell(_.LastCaseRiskTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.LastCaseRiskDescription),
                //Estado del Caso
                _ => new ExportCell(_.LastCaseCondition),
                _ => new ExportCell(_.LastCaseConditionTime, "dd/mm/yyyy", ExportCellType.Date),
                //Aspectos generales
                _ => new ExportCell(_.CaseName),
                //Ubicación
                _ => new ExportCell(_.TerritorialUnits),
                _ => new ExportCell(_.Departments),
                _ => new ExportCell(_.Provinces),
                _ => new ExportCell(_.Districts),
                _ => new ExportCell(_.Regions),
                _ => new ExportCell(_.Ubications),
                //Situacion actual
                _ => new ExportCell(_.StateTime, "dd/mm/yyyy", ExportCellType.Date),
                _ => new ExportCell(_.State),
                _ => new ExportCell(_.StateDescription),
                _ => new ExportCell(_.StateManager),
                _ => new ExportCell(_.Verification ? "Aprobado" : "No aprobado")
                );

                for (var i = 0; i < records.Count; i++)
                {
                    if (sheet.GetRow(i + initRow) != null)
                    {
                        if (records[i].LastCaseRiskTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[2], records[i].LastCaseRiskTime.Value, "dd/mm/yyyy");
                        if (records[i].LastCaseConditionTime.HasValue)
                            SetCellDataFormat(sheet.GetRow(i + initRow).Cells[5], records[i].LastCaseConditionTime.Value, "dd/mm/yyyy");

                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[13], records[i].StateTime, "dd/mm/yyyy");
                    }
                }

                for (var i = 0; i < 18; i++)
                    sheet.SetColumnWidth(i, 5000);
            });
        }
    }
}
