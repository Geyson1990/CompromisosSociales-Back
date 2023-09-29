using System.Collections.Generic;
using Contable.Application.Compromises.Dto;
using Contable.Authorization.Users.Dto;
using Contable.Dto;

namespace Contable.Application.Exporting
{
    public interface ICompromiseExcelExporter
    {
        FileDto ExportAllToFile(List<CompromiseGetMatrixExcelDto> compromiseListDtos);
        FileDto ExportMatrixToFile(List<CompromiseGetMatrixExcelDto> compromiseListDtos);
    }
}