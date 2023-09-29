using System.Collections.Generic;
using Contable.Application.SocialConflictTaskManagements.Dto;
using Contable.Application.TaskManagements.Dto;
using Contable.Dto;

namespace Contable.Application.Exporting
{
    public interface ITaskExcelExporter
    {
        FileDto ExportMatrixToFile(List<TaskManagementGetMatrixExcelDto> taskListDtos);
        FileDto ExportMatrixToFile(List<SocialConflictTaskManagementGetAllDto> taskListDtos);
    }
}