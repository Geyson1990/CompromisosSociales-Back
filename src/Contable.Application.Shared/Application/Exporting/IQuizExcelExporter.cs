using Contable.Application.QuizDetails.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Exporting
{
    public interface IQuizExcelExporter
    {
        FileDto ExportMatrizToFile(List<QuizDetailExcelExportDto> records, List<string> headers, QuizCompleteType type);
    }
}
