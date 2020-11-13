using BC.CallCenter.Clases;
using BC.CallCenter.NuevaImplementacion.Data;
using BC.CallCenter.NuevaImplementacion.DTO;
using BC.CallCenterPortable.Models;
using BC.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.Business
{
    class EncuestaBusiness
    {
        AccesoBusiness AccesoBusiness;
        BitacoraBusiness BitacoraBusiness;
        EncuestaDTO EncuestaDTO;
        EncuestaData encuestaData;

        /// <summary>
        /// Método que hace una consulta al servicio de gobierno y retorna el tipo de Folio
        /// </summary>
        /// <param name="oCuestionario">La peticion que llega</param>
        /// <param name="sToken">El token para gobierno</param>
        /// <param name="sURL">La URL del servicio de gobierno</param>
        /// <returns>Un objeto tipo encuesta</returns>
        public EncuestaDTO TipoFolio(CuestionarioModel oCuestionario, string sToken, string sURL)
        {
            var sParametros = new NameValueCollection();
            WebClient oConsumo = new WebClient();
            clsTblcodigopostal oCodigoPostal = new clsTblcodigopostal();
            clsTblcatlada oLada = new clsTblcatlada();
            EncuestaDTO encuestaDTO = new EncuestaDTO();
            AccesoBusiness = new AccesoBusiness();

            try
            {
                if (oCuestionario.sError == null)
                    oCuestionario.sError = "";

                sParametros.Add("unidad_notificante", "MEDITOC");

                foreach (var item in oCuestionario.lstPreguntas)
                {
                    if (item.sNombre != null)
                    {
                        if (item.sParam == "cp")
                        {
                            encuestaDTO.sCP = item.sNombre;
                            oCodigoPostal.ValidarCP(item.sNombre);
                        }

                        if (item.sParam == "telefono")
                        {
                            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["bActivarGeolocalizacion"]))
                                oLada.ValidarLada(item.sNombre);

                            int iIdAcceso = AccesoBusiness.UserExist(item.sNombre);

                            if (iIdAcceso == 0)
                            {
                                AccesoDTO accesoDTO = new AccesoDTO
                                {
                                    sTelefono = item.sNombre,
                                    dtFechaCreacion = DateTime.Now
                                };

                                AccesoBusiness.saveAcceso(accesoDTO);
                            }

                            encuestaDTO.iIdAcceso = iIdAcceso;
                        }

                        sParametros.Add(item.sParam, item.sNombre);
                    }
                    else
                    {
                        sParametros.Add(item.sParam, item.bPrecionado.ToString());
                    }
                }

                oConsumo.Headers.Set("token", sToken);

                var Json = oConsumo.UploadValues(sURL, "POST", sParametros);
                dynamic m = JsonConvert.DeserializeObject(Encoding.ASCII.GetString(Json));
                encuestaDTO.sFolio = m.data.FolioApp;
                encuestaDTO.sTipoFolio = m.data.FolioApp == null ? "Tipo 1" : "Tipo 2";//Comprobamos que tipo de folio es

            }
            catch (Exception e)
            {
                throw e;
            }

            return encuestaDTO;

        }

        /// <summary>
        /// Mpetodo que valida el cuestionario
        /// </summary>
        /// <param name="cuestionarioModel">El modelo del cuestionario</param>
        /// <param name="sToken">El token para gobierno</param>
        /// <param name="sURL">La URL del servicio de gobierno</param>
        /// <returns>Una resuesta con los parametros del modelo</returns>
        public ResponseModel ValidarCuestionario(CuestionarioModel cuestionarioModel, string sToken, string sURL)
        {
            ResponseModel responseModel = new ResponseModel();
            List<string> lstTextDialogos = new List<string>();
            encuestaData = new EncuestaData();
            BitacoraBusiness = new BitacoraBusiness();

            try
            {
                EncuestaDTO = TipoFolio(cuestionarioModel, sToken, sURL);
                EncuestaDTO.dtFechaCreacion = DateTime.Now;

                EncuestaDTO.iIdEncuesta = encuestaData.save(EncuestaDTO);//Guardamos el registro de la encuesta
                BitacoraBusiness.save(EncuestaDTO.iIdAcceso, EncuestaDTO.iIdEncuesta, 0, clsEnums.sDescripcionEnum(clsEnums.enumEstatusBitacora.ENCUESTA), "Su tipo de folio es: " + EncuestaDTO.sTipoFolio, cuestionarioModel.sLatitud + "," + cuestionarioModel.sLongitud  ); //Se guarda el registro en la bitacora

                responseModel.sFolio = EncuestaDTO.sFolio;

                if (EncuestaDTO.sFolio != null)
                {
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sTituloFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloUno"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(EncuestaDTO.sFolio + ConfigurationManager.AppSettings["sSubTituloDos"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloTres"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloCuatro"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloCinco"].Replace("\\n", "\n"));

                    responseModel.bTipoFolio = true;
                }
                else
                {
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sTituloFolioSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloUnoSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloDosSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloTresSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloCuatroSinFolio"].Replace("\\n", "\n"));
                    lstTextDialogos.Add(ConfigurationManager.AppSettings["sSubTituloCincoSinFolio"].Replace("\\n", "\n"));

                    responseModel.bTipoFolio = false;
                }
                responseModel.sParameter1 = JsonConvert.SerializeObject(lstTextDialogos);

            }
            catch (Exception e)
            {

                throw e;
            }
            return responseModel;
        }

    }
}
