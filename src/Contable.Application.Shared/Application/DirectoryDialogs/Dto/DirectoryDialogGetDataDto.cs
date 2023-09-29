using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryDialogs.Dto
{
    public class DirectoryDialogGetDataDto
    {
        public DirectoryDialogGetDto DirectoryDialog { get; set; }
        public List<DirectoryDialogResponsibleDto> Responsibles { get; set; }
    }
}
