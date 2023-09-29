using Abp.Application.Services;
using Contable.Application.Records.Dto;
using Contable.Application.SocialConflictSensibles.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    public interface ISocialConflictSensibleExcelExporter : IApplicationService
    {
        FileDto ExportMatrizToFile(List<SocialConflictSensibleMatrizExportDto> records);
        FileDto ExportManagementToFile(List<SocialConflictSensibleManagementExportDto> records);
        FileDto ExportStateToFile(List<SocialConflictSensibleStateExportDto> records);
    }
}
