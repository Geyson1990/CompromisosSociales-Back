using Abp.Application.Services;
using Contable.Application.SocialConflicts.Dto;
using Contable.Dto;
using System.Collections.Generic;

namespace Contable.Application.Exporting
{
    public interface IActasExcelExporter : IApplicationService
    {
        FileDto ExportMatrizToFile(List<ActaMatrizExportDto> records);
       
    }
}
