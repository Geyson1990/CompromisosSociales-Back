using System.Collections.Generic;
using Contable.Application.Records.Dto;
using Contable.Application.TaskManagements.Dto;
using Contable.Dto;

namespace Contable.Application.Exporting
{
    public interface IRecordExcelExporter
    {
        FileDto ExportMatrixToFile(List<RecordGetMatrixExcelDto> recordListDtos);
    }
}