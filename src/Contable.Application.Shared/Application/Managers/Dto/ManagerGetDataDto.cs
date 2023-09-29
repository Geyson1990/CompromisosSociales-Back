using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Managers.Dto
{
    public class ManagerGetDataDto
    {
        public ManagerGetDto Manager { get; set; }
        public List<ManagerTerritorialUnitDto> TerritorialUnits { get; set; }
    }
}
