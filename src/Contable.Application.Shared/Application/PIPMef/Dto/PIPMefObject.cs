using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.PIPMef.Dto
{
    public class PIPMefObject
    {
        public string Codigo { get; set; }
        public string CodigoUnico { get; set; }
        public string Estado { get; set; }
        public string Nombre { get; set; }
        public string FechaRegistro { get; set; }
        public string Funcion { get; set; }
        public string Programa { get; set; }
        public string Subprograma { get; set; }
        public string UnidadFormuladora { get; set; }
        public string UnidadFormuladoraCodigo { get; set; }
        public string UnidadUEICodigo { get; set; }
        public string UnidadOPMICodigo { get; set; }
        public string NivelGobierno { get; set; }
        public string Sector { get; set; }
        public string Pliego { get; set; }
        public string Evaluadora { get; set; }
        public string EvaluadoraCodigo { get; set; }
        public string Ejecutora { get; set; }
        public string EjecutoraCodigo { get; set; }
        public string Situacion { get; set; }
        public string UltimoEstudio { get; set; }
        public string EstadoUltimoEstudio { get; set; }
        public string NivelEstudio { get; set; }
        public string Beneficiario { get; set; }
        public string FuenteFinanciamiento { get; set; }
        public decimal MontoAlternativa { get; set; }
        public decimal MontoReformulado { get; set; }
        public decimal MontoF15 { get; set; }
        public decimal MontoF16 { get; set; }
        public decimal MontoLaudo { get; set; }
        public decimal MontoCartaFianza { get; set; }
        public decimal CostoActualizado { get; set; }
        public decimal DevengadoAcumulado { get; set; }
        public decimal DevengadoAnioActual { get; set; }
        public decimal DesTipoFormato { get; set; }
        public decimal FlagExpedienteTecnico { get; set; }
        public decimal AnioViabilidad { get; set; }
        public string FechaViabilidad { get; set; }
        public string Actualizacion { get; set; }
        public decimal NumeroConvenio { get; set; }
        public string NombreProgramaInversion { get; set; }
        public decimal? PIM { get; set; }
        public decimal? PIA { get; set; }
        public string Marco { get; set; }
        public string ConInformeCierre { get; set; }
        public decimal IncluidoPMIEjecucion { get; set; }
        public decimal FlagEtapas { get; set; }
        public PIPMefLocation listalocalizacion { get; set; }
    }
}
