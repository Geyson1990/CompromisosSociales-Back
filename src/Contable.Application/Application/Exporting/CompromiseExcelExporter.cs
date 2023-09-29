using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Contable.Application.Compromises.Dto;
using Contable.Application.Exporting.Dto;
using Contable.DataExporting.Excel.NPOI;
using Contable.Dto;
using Contable.Storage;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Contable.Application.Exporting
{
    public class CompromiseExcelExporter : NpoiExcelExporterBase, ICompromiseExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;
        private readonly IWebHostEnvironment _env;

        public CompromiseExcelExporter(
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

        public FileDto ExportAllToFile(List<CompromiseGetMatrixExcelDto> compromiseListDtos)
        {
            var fileName = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_") + "BD_COMPROMISOS.xlsx";

            return CreateExcelPackage(fileName, excelPackage =>
            {
                var sheet = excelPackage.CreateSheet("Compromisos");

                int initRow = 0;

                AddHeader(
                   sheet, initRow,
                   "Código del caso",
                   "Denominación del caso",
                   "Código del acta",
                   "Fecha del acta",
                   "Año",
                   "Mes",
                   "Código del compromiso",
                   "Denominación del compromiso",
                   "Descripción del compromiso",
                   "Transcripción del compromiso",
                   "Unidades Territoriales",
                   "Departamentos",
                   "Provincias",
                   "Distritos",
                   "Tipo de compromiso",
                   "PIP/Código SNIP",
                   "PIP/Código Unificado",
                   "PIP/Proyecto",
                   "PIP/Estado",
                   "PIP/Costo actualizado",
                   "PIP/PIM",
                   "PIP/PIA",
                   "PIP/Devengado",
                   "PIP/Devengado acumulado",
                   "PIP/Unidad formuladora",
                   "PIP/Unidad ejecutora",
                   "PIP/Fase y etapa",
                   "PIP/Hito",
                   "Cronograma de cumplimiento",
                   "Tipo de responsable (Provisional)",
                   "Subtipo de responsable (Provisional)",
                   "Responsable (Provisional)",
                   "Responsable Específico (Provisional)",
                   "Tipo de responsable",
                   "Subtipo de responsable",
                   "Responsable",
                   "Responsable específico",
                   "Tipo de colaborador",
                   "Subtipo de colaborador",
                   "Colaborador",
                   "Colaborador específico",
                   "Prioridad",
                   "Referencia de priorización",
                   "Abierto/Cerrado",
                   "Estado actual",
                   "Temática mujer",
                   "Etiqueta",
                   "Fecha de registro",
                   "Registrado por",
                   "Última actualización",
                   "Actualizado por"
                );

                initRow++;

                AddObjects(excelPackage, sheet, initRow, compromiseListDtos,
                   _ => new ExportCell(_.Record.SocialConflict.Code),//Código del caso
                   _ => new ExportCell(_.Record.SocialConflict.CaseName),//Denominación del caso
                   _ => new ExportCell(_.Record.Code),//Código del acta
                   _ => new ExportCell(_.Record.RecordTime, "dd/mm/yyyy", ExportCellType.Date),//Fecha del acta
                   _ => new ExportCell(_.Record.RecordTime.HasValue ? _.Record.RecordTime.Value.ToString("yyyy") : "", ExportCellType.Numeric, false),//Año
                   _ => new ExportCell(_.Record.RecordTime.HasValue ? _.Record.RecordTime.Value.ToString("MM") : "", ExportCellType.Numeric, false),//Mes
                   _ => new ExportCell(_.Code),//Código del compromiso
                   _ => new ExportCell(_.Name),//Denominación del compromiso
                   _ => new ExportCell(_.Description),//Descripción del compromiso
                   _ => new ExportCell(_.Transcription),//Transcripción del compromiso
                   _ => new ExportCell(_.TerritorialUnits),//Unidades Territoriales
                   _ => new ExportCell(_.Departments),//Departamentos
                   _ => new ExportCell(_.Provinces),//Departamentos
                   _ => new ExportCell(_.Districts),//Departamentos
                   _ => new ExportCell((_.Type == CompromiseType.PIP ? "PIP" : "ACTIVIDAD")),//Tipo de compromiso
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.SNIPCode : "") : ""),//PIP/Código SNIP
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.UnifiedCode : "") : ""),//PIP/Código Unificado
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.ProjectName : "") : ""),//PIP/Proyecto
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.Status : "") : ""),//PIP/Estado
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.UpdatedCost : 0) : 0),//PIP/Costo actualizado
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.PIM : 0) : 0),//PIP/PIM
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.PIA : 0) : 0),//PIP/PIA
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.Accrued : 0) : 0),//PIP/Devengado
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.AccumulatedAccrued : 0) : 0),//PIP/Devengado acumulado
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.FormulatingUnit : "") : ""),//PIP/Unidad formuladora
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.ExecutingUnit : "") : ""),//PIP/Unidad ejecutora
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null && _.PIPMEF.PIPPhase != null ? _.PIPMEF.PIPPhase.Value : "") : ""),//PIP/Fase y etapa
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null && _.PIPMEF.PIPMilestone != null ? _.PIPMEF.PIPMilestone.Value : "") : ""),//PIP/Hito
                   _ => new ExportCell(_.Timelines),//Cronograma de cumplimiento
                   _ => new ExportCell(_.ResponsibleActor != null && _.ResponsibleActor.ResponsibleType != null ? _.ResponsibleActor.ResponsibleType.Name : ""),//Tipo de responsable (Provisional)
                   _ => new ExportCell(_.ResponsibleActor != null && _.ResponsibleActor.ResponsibleSubType != null ? _.ResponsibleActor.ResponsibleSubType.Name : ""),//Subtipo de responsable (Provisional)
                   _ => new ExportCell(_.ResponsibleActor != null ? _.ResponsibleActor.Name : ""),//Responsable (Provisional)
                   _ => new ExportCell(_.ResponsibleSubActor != null ? _.ResponsibleSubActor.Name : ""),//Responsable Específico (Provisional)
                   _ => new ExportCell(_.ResponsibleTypes),//Tipo de responsable
                   _ => new ExportCell(_.ResponsibleSubTypes),//Subtipo de responsable
                   _ => new ExportCell(_.Responsibles),//Responsable 
                   _ => new ExportCell(_.SubResponsibles),//Responsable específico
                   _ => new ExportCell(_.InvolvedTypes),//Tipo de colaborador
                   _ => new ExportCell(_.InvolvedSubTypes),//Subtipo de colaborador
                   _ => new ExportCell(_.InvolvedResponsibles),//Colaborador
                   _ => new ExportCell(_.InvolvedSubResponsibles),//Colaborador específico
                   _ => new ExportCell(_.IsPriority ? "Priorizado" : "No Priorizado"),//Prioridad
                   _ => new ExportCell(_.PriorityReference),//Referencia de priorización
                   _ => new ExportCell(_.Status != null ? FormatState(_.Status.Value, 0) : ""),//Abierto/Cerrado
                   _ => new ExportCell(_.Status != null ? FormatState(_.Status.Value, 1) : ""),//Estado actual       
                   _ => new ExportCell(_.WomanCompromise ? "SI" : "NO"),//Temática mujer
                   _ => new ExportCell(_.CompromiseLabel != null ? _.CompromiseLabel.Name : ""),//Etiqueta
                   _ => new ExportCell(_.CreationTime, "dd/mm/yyyy", ExportCellType.Date),//Fecha de registro
                   _ => new ExportCell(_.CreatorUser == null ? null : $"{_.CreatorUser.Name ?? ""} {_.CreatorUser.Surname ?? ""}"),//Registrado por
                   _ => new ExportCell(_.LastModificationTime, "dd/mm/yyyy", ExportCellType.Date),//Última actualización
                   _ => new ExportCell(_.EditUser == null ? null : $"{_.EditUser.Name ?? ""} {_.EditUser.Surname ?? ""}")//Actualizado por
                );

                for (var i = 0; i < compromiseListDtos.Count; i++)
                {
                    if (sheet.GetRow(i + initRow) != null)
                    {
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[3], compromiseListDtos[i].Record.RecordTime, "dd/mm/yyyy");
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[48], compromiseListDtos[i].CreationTime, "dd/mm/yyyy");
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[50], compromiseListDtos[i].LastModificationTime, "dd/mm/yyyy");
                    }
                }

                for (var i = 0; i < 51; i++)
                    sheet.SetColumnWidth(i, 5000);
            });
        }
               
        public FileDto ExportMatrixToFile(List<CompromiseGetMatrixExcelDto> compromiseListDtos)
        {
            var fileName = DateTime.Now.ToString("MM_dd_yyyy_HH_mm_") + "MATRIZ_COMPROMISOS.xlsx";
            return CreateExcelPackage(fileName, excelPackage =>
            {
                var sheet = excelPackage.CreateSheet("Compromisos");

                int initRow = 0;

                AddHeader(
                   sheet, initRow,
                   "Código del caso",
                   "Denominación del caso",
                   "Fecha del acta",
                   "Denominación del compromiso",
                   "Unidad Territorial",
                   "Departamento",
                   "Provincia",
                   "Distrito",
                   "Tipo de compromiso",
                   "PIP/Código Unificado",
                   "Tipo de responsable (Provisional)",
                   "Responsable (Provisional)",
                   "Responsable específico (Provisional)",
                   "Tipo de responsable",
                   "Responsable",
                   "Responsables específicos",
                   "Abierto/Cerrado",
                   "Estado actual",
                   "Temática mujer"
                );

                initRow++;

                AddObjects(excelPackage, sheet, initRow, compromiseListDtos,
                   _ => new ExportCell(_.Record.SocialConflict.Code),
                   _ => new ExportCell(_.Record.SocialConflict.CaseName),
                   _ => new ExportCell(_.Record.RecordTime, "dd/mm/yyyy", ExportCellType.Date),
                   _ => new ExportCell(_.Name),
                   _ => new ExportCell(_.TerritorialUnits),
                   _ => new ExportCell(_.Departments),
                   _ => new ExportCell(_.Provinces),
                   _ => new ExportCell(_.Districts),
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? "PIP" : "ACTIVIDAD"),
                   _ => new ExportCell(_.Type == CompromiseType.PIP ? (_.PIPMEF != null ? _.PIPMEF.UnifiedCode : "") : "", ExportCellType.Numeric, false),
                   _ => new ExportCell(_.ResponsibleActor != null && _.ResponsibleActor.ResponsibleType != null ? _.ResponsibleActor.ResponsibleType.Name : ""),//Tipo de actor (Provisional)
                   _ => new ExportCell(_.ResponsibleActor != null ? _.ResponsibleActor.Name : ""),//Colaborador (Provisional)
                   _ => new ExportCell(_.ResponsibleSubActor != null ? _.ResponsibleSubActor.Name : ""),//Colaborador específico (Provisional)
                   _ => new ExportCell(_.ResponsibleTypes),//Tipo de actor
                   _ => new ExportCell(_.Responsibles),//Colaboradores
                   _ => new ExportCell(_.SubResponsibles),//Colaboradores específicos
                   _ => new ExportCell(_.Status != null ? FormatState(_.Status.Value, 0) : ""),
                   _ => new ExportCell(_.Status != null ? FormatState(_.Status.Value, 1) : ""),
                   _ => new ExportCell(_.WomanCompromise ? "SI" : "NO"));

                for (var i = 0; i < compromiseListDtos.Count; i++)
                {
                    if (sheet.GetRow(i + initRow) != null)
                    {
                        SetCellDataFormat(sheet.GetRow(i + initRow).Cells[2], compromiseListDtos[i].Record.RecordTime, "dd/mm/yyyy");
                    }
                }

                for (var i = 0; i < 17; i++)
                    sheet.SetColumnWidth(i, 5000); 
            });


        }

        private string FormatState(string value, int index)
        {
            if (value.IsNullOrWhiteSpace())
                return "";

            string[] values = value.Split('/'); 

            if(value.Length > index)
                return values[index].Trim();

            if(index == 0)
                return value.Trim();

            return "";
        }
       
    }
}
