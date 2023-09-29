using Abp.Domain.Repositories;
using ApiMefWS;
using Contable.Application.Compromises.Dto;
using Contable.Application.Extensions;
using Contable.Application.External.Dto;
using Contable.Application.Orders.Dto;
using Contable.Application.Parameters.Dto;
using Contable.Application.PIPMef.Dto;
using Contable.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ApiMefWS.wsPCMSoapClient;

namespace Contable.Application.External
{
    public class PipMefAppService : ContableAppServiceBase, IPipMefAppService
    {
        private readonly IRepository<PIPMEF, long> _pipmefRepository;
        private readonly IRepository<Parameter> _parameterRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _configurationRoot;

        public PipMefAppService(
            IRepository<Parameter> parameterRepository,
            IRepository<PIPMEF, long> pipmefRepository,
            IWebHostEnvironment hostingEnvironment)
        {
            _pipmefRepository = pipmefRepository;
            _parameterRepository = parameterRepository;
            _hostingEnvironment = hostingEnvironment;
            _configurationRoot = hostingEnvironment.GetAppConfiguration();
        }
        public async Task UpdateData()
        {
            var query = await _pipmefRepository
                .GetAll()
                .Include(p => p.PIPMilestone)
                .Include(p => p.PIPPhase)
                .Where(p => p.UnifiedCode != null && p.UpdatedCost == 0)
                .ToListAsync();

            foreach(var pip in query)
            {
                var request = await GetPIPDetails(pip.UnifiedCode);

                if(request != null)
                {
                    pip.UpdatedCost = request.UpdatedCost;
                    pip.Status = request.Status;
                }

                await _pipmefRepository.UpdateAsync(pip);
            }
        }

        public async Task<PIPMEF> ValidatePIPMEFCompromise(CompromiseUpdatePIPMEFDto input)
        {            
            var result = new PIPMEF();

            if (input.Id == 0)
            {
                if (input.SNIPCode.IsValid() && await _pipmefRepository.GetAll().Where(p => p.SNIPCode.Equals(input.SNIPCode)).CountAsync() > 0)
                    result = _pipmefRepository.GetAll().Where(p => p.SNIPCode.Equals(input.SNIPCode)).First();
                else if (input.UnifiedCode.IsValid() && await _pipmefRepository.GetAll().Where(p => p.UnifiedCode.Equals(input.UnifiedCode)).CountAsync() > 0)
                    result = _pipmefRepository.GetAll().Where(p => p.UnifiedCode.Equals(input.UnifiedCode)).First();
                else if (!result.SNIPCode.IsValid() && !result.UnifiedCode.IsValid())
                    result = new PIPMEF()
                    {
                        SNIPCode = input.SNIPCode,
                        UnifiedCode = input.UnifiedCode
                    };
            }

            await UpdatePIPDetails(input.SNIPCode, result);

            if (input.PIPPhase != null && await _parameterRepository.CountAsync(p => p.Id == input.PIPPhase.Id) > 0)
            {
                result.PIPPhase = await _parameterRepository.GetAsync(input.PIPPhase.Id);
                result.PIPPhaseId = result.PIPPhase.Id;
            } 
            else
            {
                result.PIPPhaseId = null;
                result.PIPPhase = null;
            }

            if (input.PIPMilestone != null && await _parameterRepository.CountAsync(p => p.Id == input.PIPMilestone.Id) > 0)
            {
                result.PIPMilestone = await _parameterRepository.GetAsync(input.PIPMilestone.Id);
                result.PIPMilestoneId = result.PIPMilestone.Id;
            }
            else
            {
                result.PIPMilestoneId = null;
                result.PIPMilestone = null;
            }           

            return await _pipmefRepository.InsertOrUpdateAsync(result); 
        }

        public async Task<PIPMEF> ValidatePIPMEFOrder(OrderPIPMEFUpdateDto input){
            var result = new PIPMEF();

            if (input.Id == 0)
            {
                if (input.SNIPCode.IsValid())
                    result = _pipmefRepository.GetAll().Where(p => p.SNIPCode.Equals(input.SNIPCode)).First();
                else if (input.UnifiedCode.IsValid())
                    result = _pipmefRepository.GetAll().Where(p => p.UnifiedCode.Equals(input.UnifiedCode)).First();
            }

            _ = UpdatePIPDetails(input.SNIPCode, result);

            return await _pipmefRepository.InsertOrUpdateAsync(result); 
        }

        public async Task<PIPMEFDto> GetPIPDetails(string Code)
        {
            var response = await GetPipAsync(Code);

            if (response != null &&
                response.GetPipResponse != null &&
                response.GetPipResponse.GetPipResult != null &&
                response.GetPipResponse.GetPipResult.PIP != null)
            {
                //ApiMefWS.PIP
                var resultWS = response.GetPipResponse.GetPipResult.PIP;

                return new PIPMEFDto()
                {
                    SNIPCode = resultWS.Codigo,
                    UnifiedCode = resultWS.CodigoUnico,
                    ProjectName = resultWS.Nombre,
                    ViabilityDate = resultWS.FechaViabilidad,
                    AccumulatedAccrued = resultWS.DevengadoAcumulado,
                    Accrued = resultWS.DevengadoAnioActual,
                    PIM = resultWS.PIM.HasValue ? resultWS.PIM.Value : 0,
                    PIA = resultWS.PIA.HasValue ? resultWS.PIA.Value : 0,
                    FormulatingUnit = resultWS.UnidadFormuladora,
                    ExecutingUnit = resultWS.Ejecutora,
                    LastUpdateMEF = System.DateTime.Now,
                    Status = resultWS.Estado,
                    UpdatedCost = resultWS.CostoActualizado,
                    IsOk = true
                }; 
            }
            return null;
        }

        public async Task<PIPMEF> UpdatePIPDetails(string SNIPcode, PIPMEF pipmef)
        {
            if (!SNIPcode.IsValid()) 
                return pipmef;

            var response = await GetPipAsync(SNIPcode);

            if (response != null && response.GetPipResponse != null && response.GetPipResponse.GetPipResult != null && response.GetPipResponse.GetPipResult.PIP != null)
            {
                var resultWS = response.GetPipResponse.GetPipResult.PIP;

                pipmef ??= new PIPMEF();

                pipmef.SNIPCode = resultWS.Codigo;
                pipmef.UnifiedCode = resultWS.CodigoUnico;
                pipmef.ProjectName = resultWS.Nombre;
                pipmef.ViabilityDate = resultWS.FechaViabilidad;
                pipmef.AccumulatedAccrued = (double)resultWS.DevengadoAcumulado;
                pipmef.UpdatedCost = resultWS.CostoActualizado;
                pipmef.Accrued = (double)resultWS.DevengadoAnioActual;
                pipmef.PIM = resultWS.PIM.HasValue ? resultWS.PIM.Value : 0;
                pipmef.PIA = resultWS.PIA.HasValue ? resultWS.PIA.Value : 0;
                pipmef.FormulatingUnit = resultWS.UnidadFormuladora;
                pipmef.Status = resultWS.Estado;
                pipmef.ExecutingUnit = resultWS.Ejecutora;
                pipmef.LastUpdateMEF = DateTime.Now;
                pipmef.IsOk = true;
            }

            return pipmef;
        }

        private async Task<PIPMefResponse> GetPipAsync(string code)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_configurationRoot["Mef:EndPoint"]);
                    client.Timeout = TimeSpan.FromMinutes(20.00);
                    client.DefaultRequestHeaders.Host = "ws5.pide.gob.pe";

                    var body = new PIPMefRequest()
                    {
                        PIDE = new PIPMefRequestBody()
                        {
                            usuario = _configurationRoot["Mef:User"],
                            clave = _configurationRoot["Mef:Password"],
                            codigo = code
                        }
                    };

                    var method = _configurationRoot["Mef:Method"];

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, method)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
                    };

                    var result = await client.SendAsync(request);

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await result.Content.ReadAsStringAsync();
                        var jObject = JObject.Parse(json);
                        var jchildren = jObject["GetPipResponse"]["GetPipResult"]["PIP"];

                        var pip = new PIPMefObject()
                        {
                            Codigo = GetJSONStringValue(jchildren["Codigo"] == null ? "" : jchildren["Codigo"].ToString()),
                            Actualizacion = GetJSONStringValue(jchildren["Actualizacion"] == null ? "" : jchildren["Actualizacion"].ToString()),
                            AnioViabilidad = GetJSONDecimalValue(jchildren["AnioViabilidad"] == null ? "" : jchildren["AnioViabilidad"].ToString()),
                            Beneficiario = GetJSONStringValue(jchildren["Beneficiario"] == null ? "" : jchildren["Beneficiario"].ToString()),
                            CodigoUnico = GetJSONStringValue(jchildren["CodigoUnico"] == null ? "" : jchildren["CodigoUnico"].ToString()),
                            ConInformeCierre = GetJSONStringValue(jchildren["ConInformeCierre"] == null ? "" : jchildren["ConInformeCierre"].ToString()),
                            CostoActualizado = GetJSONDecimalValue(jchildren["CostoActualizado"] == null ? "" : jchildren["CostoActualizado"].ToString()),
                            DesTipoFormato = GetJSONDecimalValue(jchildren["DesTipoFormato"] == null ? "" : jchildren["DesTipoFormato"].ToString()),
                            DevengadoAcumulado = GetJSONDecimalValue(jchildren["DevengadoAcumulado"] == null ? "" : jchildren["DevengadoAcumulado"].ToString()),
                            DevengadoAnioActual = GetJSONDecimalValue(jchildren["DevengadoAnioActual"] == null ? "" : jchildren["DevengadoAnioActual"].ToString()),
                            Ejecutora = GetJSONStringValue(jchildren["Ejecutora"] == null ? "" : jchildren["Ejecutora"].ToString()),
                            EjecutoraCodigo = GetJSONStringValue(jchildren["EjecutoraCodigo"] == null ? "" : jchildren["EjecutoraCodigo"].ToString()),
                            Estado = GetJSONStringValue(jchildren["Estado"] == null ? "" : jchildren["Estado"].ToString()),
                            EstadoUltimoEstudio = GetJSONStringValue(jchildren["EstadoUltimoEstudio"] == null ? "" : jchildren["EstadoUltimoEstudio"].ToString()),
                            Evaluadora = GetJSONStringValue(jchildren["Evaluadora"] == null ? "" : jchildren["Evaluadora"].ToString()),
                            EvaluadoraCodigo = GetJSONStringValue(jchildren["EvaluadoraCodigo"] == null ? "" : jchildren["EvaluadoraCodigo"].ToString()),
                            FechaRegistro = GetJSONStringValue(jchildren["FechaRegistro"] == null ? "" : jchildren["FechaRegistro"].ToString()),
                            FechaViabilidad = GetJSONStringValue(jchildren["FechaViabilidad"] == null ? "" : jchildren["FechaViabilidad"].ToString()),
                            FlagEtapas = GetJSONDecimalValue(jchildren["FlagEtapas"] == null ? "" : jchildren["FlagEtapas"].ToString()),
                            FlagExpedienteTecnico = GetJSONDecimalValue(jchildren["FlagExpedienteTecnico"] == null ? "" : jchildren["FlagExpedienteTecnico"].ToString()),
                            FuenteFinanciamiento = GetJSONStringValue(jchildren["FuenteFinanciamiento"] == null ? "" : jchildren["FuenteFinanciamiento"].ToString()),
                            Funcion = GetJSONStringValue(jchildren["Funcion"] == null ? "" : jchildren["Funcion"].ToString()),
                            IncluidoPMIEjecucion = GetJSONDecimalValue(jchildren["IncluidoPMIEjecucion"] == null ? "" : jchildren["IncluidoPMIEjecucion"].ToString()),
                            listalocalizacion = null,
                            Marco = GetJSONStringValue(jchildren["Marco"] == null ? "" : jchildren["Marco"].ToString()),
                            MontoAlternativa = GetJSONDecimalValue(jchildren["MontoAlternativa"] == null ? "" : jchildren["MontoAlternativa"].ToString()),
                            MontoCartaFianza = GetJSONDecimalValue(jchildren["MontoCartaFianza"] == null ? "" : jchildren["MontoCartaFianza"].ToString()),
                            MontoF15 = GetJSONDecimalValue(jchildren["MontoF15"] == null ? "" : jchildren["MontoF15"].ToString()),
                            MontoF16 = GetJSONDecimalValue(jchildren["MontoF16"] == null ? "" : jchildren["MontoF16"].ToString()),
                            MontoLaudo = GetJSONDecimalValue(jchildren["MontoLaudo"] == null ? "" : jchildren["MontoLaudo"].ToString()),
                            MontoReformulado = GetJSONDecimalValue(jchildren["MontoReformulado"] == null ? "" : jchildren["MontoReformulado"].ToString()),
                            NivelEstudio = GetJSONStringValue(jchildren["NivelEstudio"] == null ? "" : jchildren["NivelEstudio"].ToString()),
                            NivelGobierno = GetJSONStringValue(jchildren["NivelGobierno"] == null ? "" : jchildren["NivelGobierno"].ToString()),
                            Nombre = GetJSONStringValue(jchildren["Nombre"] == null ? "" : jchildren["Nombre"].ToString()),
                            NombreProgramaInversion = GetJSONStringValue(jchildren["NombreProgramaInversion"] == null ? "" : jchildren["NombreProgramaInversion"].ToString()),
                            NumeroConvenio = GetJSONDecimalValue(jchildren["NumeroConvenio"] == null ? "" : jchildren["NumeroConvenio"].ToString()),
                            PIA = GetJSONDecimalValue(jchildren["PIA"] == null ? "" : jchildren["PIA"].ToString()),
                            PIM = GetJSONDecimalValue(jchildren["PIM"] == null ? "" : jchildren["PIM"].ToString()),
                            Pliego = GetJSONStringValue(jchildren["Pliego"] == null ? "" : jchildren["Pliego"].ToString()),
                            Programa = GetJSONStringValue(jchildren["Programa"] == null ? "" : jchildren["Programa"].ToString()),
                            Sector = GetJSONStringValue(jchildren["Sector"] == null ? "" : jchildren["Sector"].ToString()),
                            Situacion = GetJSONStringValue(jchildren["Situacion"] == null ? "" : jchildren["Situacion"].ToString()),
                            Subprograma = GetJSONStringValue(jchildren["Subprograma"] == null ? "" : jchildren["Subprograma"].ToString()),
                            UltimoEstudio = GetJSONStringValue(jchildren["UltimoEstudio"] == null ? "" : jchildren["UltimoEstudio"].ToString()),
                            UnidadFormuladora = GetJSONStringValue(jchildren["UnidadFormuladora"] == null ? "" : jchildren["UnidadFormuladora"].ToString()),
                            UnidadFormuladoraCodigo = GetJSONStringValue(jchildren["UnidadFormuladoraCodigo"] == null ? "" : jchildren["UnidadFormuladoraCodigo"].ToString()),
                            UnidadOPMICodigo = GetJSONStringValue(jchildren["UnidadOPMICodigo"] == null ? "" : jchildren["UnidadOPMICodigo"].ToString()),
                            UnidadUEICodigo = GetJSONStringValue(jchildren["UnidadUEICodigo"] == null ? "" : jchildren["UnidadUEICodigo"].ToString())
                        };

                        return new PIPMefResponse()
                        {
                            GetPipResponse = new PIPMefResult()
                            {
                                GetPipResult = new PIPMefContainer()
                                {
                                    PIP = pip
                                }
                            }
                        };
                    }
                    else
                    {
                        var json = await result.Content.ReadAsStringAsync();

                        Logger.Error(@$"GetPipAsync: {json}");

                        return null;
                    }

                }
                catch (Exception ex)
                {
                    Logger.Error(@$"GetPipAsync: {ex.Message}");

                    return null;
                }

            }
        }

        public string GetJSONStringValue(string value) 
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";
            if (value.Contains("{") || value.Contains("}"))
                return "";

            return value.Trim();
        }

        public decimal GetJSONDecimalValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;
            if (value.Contains("{") || value.Contains("}"))
                return 0;

            return Convert.ToDecimal(value);
        }
    }
}
