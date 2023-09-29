using System.Collections.Generic;
using Contable.Application.Orders.Dto;
using Contable.Application.Records.Dto;
using Contable.Application.TaskManagements.Dto;
using Contable.Dto;

namespace Contable.Application.Exporting
{
    public interface IOrderExcelExporter
    {
        FileDto ExportMatrixToFile(List<OrderGetMatrixExcelDto> orderListDtos);
    }
}