using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceGetDataDto
    {
        public DialogSpaceGetDto DialogSpace { get; set; }
        public List<DialogSpaceTerritorialUnitDto> TerritorialUnits { get; set; }
        public List<DialogSpaceDepartmentDto> Departments { get; set; }
        public List<DialogSpaceDialogSpaceTypeRelatioDto> Types { get; set; }
        public List<DialogSpacePersonRelationDto> Persons { get; set; }
    }
}
