using Contable.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public enum LevelType
    {
        [Code("001", Description = "Utilizado para la evaluación de tres niveles")]
        PRIMARY,
        [Code("002", Description = "Utilizado para la evaluación de diez niveles")]
        SECONDARY
    }
}
