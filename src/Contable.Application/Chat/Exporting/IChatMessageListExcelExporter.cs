using System.Collections.Generic;
using Abp;
using Contable.Chat.Dto;
using Contable.Dto;

namespace Contable.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
