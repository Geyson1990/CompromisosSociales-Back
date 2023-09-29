using Abp.Application.Services;
using Contable.Application.Records.Dto;
using Contable.Application.SocialConflicts.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    public interface ISocialConflictExcelExporter : IApplicationService
    {
        FileDto ExportMatrizToFile(List<SocialConflictMatrizExportDto> records);
        FileDto ExportManagementToFile(List<SocialConflictManagementExportDto> records);
        FileDto ExportStateToFile(List<SocialConflictStateExportDto> records);
        FileDto ExportActorToFile(List<SocialConflictActorExcelExportDto> records);        
    }
}
