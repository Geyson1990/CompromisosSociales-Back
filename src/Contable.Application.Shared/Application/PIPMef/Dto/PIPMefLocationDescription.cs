using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.PIPMef.Dto
{
    public class PIPMefLocationDescription
    {
        public string Codigo { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string CentroPoblado { get; set; }
        public string Ubigeo { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
    }
}
