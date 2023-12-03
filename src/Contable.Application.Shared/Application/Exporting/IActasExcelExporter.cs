using Abp.Application.Services;
using Contable.Application.SocialConflicts.Dto;
using Contable.Dto;
using System.Collections.Generic;

namespace Contable.Application.Exporting
{
    public interface IActasExcelExporter : IApplicationService
    {
        byte[] ExportMatrizToFile(List<ActaMatrizExportDto> records);
       
    }
}
