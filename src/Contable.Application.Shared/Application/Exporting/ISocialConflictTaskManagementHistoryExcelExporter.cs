using System.Collections.Generic;
using Contable.Application.SocialConflictTaskManagementHistories.Dto;
using Contable.Dto;

namespace Contable.Application.Exporting
{
    public interface ISocialConflictTaskManagementHistoryExcelExporter
    {
        FileDto ExportMatrixToFile(byte[] report);
    }
}