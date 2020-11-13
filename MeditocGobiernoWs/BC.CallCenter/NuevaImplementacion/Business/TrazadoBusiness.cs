using BC.CallCenter.NuevaImplementacion.DTO;
using BC.CallCenterPortable.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.Business
{
    public class TrazadoBusiness
    {
        BitacoraBusiness bitacoraBusiness;
        AccesoBusiness accesoBusiness;
        ResponseModel oResponseModel;

        /// <summary>
        /// Método para realizar el guardado del array del trazado
        /// </summary>
        /// <param name="trazadoDTOs">La lista con los trazado</param>
        /// <returns>Un objeto response</returns>
        public ResponseModel RealizarGuardadoTrazado(List<TrazadoDTO> trazadoDTOs)
        {
            bitacoraBusiness = new BitacoraBusiness();
            accesoBusiness = new AccesoBusiness();
            oResponseModel = new ResponseModel();

            List<TrazadoDTO> trazadosNoGuardados = new List<TrazadoDTO>();
            trazadosNoGuardados.AddRange(trazadoDTOs);

            try
            {
                string sNumero = trazadoDTOs
                    .FirstOrDefault()
                    .Telefono;

                int iIdAcceso = accesoBusiness.UserExist(sNumero);

                if (iIdAcceso != 0)
                {
                    foreach (var item in trazadoDTOs)
                    {
                        int iGuardado = bitacoraBusiness.SaveTrazado(iIdAcceso, "Trazado del día: " + Convert.ToDateTime(item.Date), item.Latitud + "," + item.Longitude  );

                        if (iGuardado != 0)
                        {
                            trazadosNoGuardados.Remove(item);
                        }
                    }
                }

                oResponseModel.bRespuesta = true;
                oResponseModel.sMensaje = "Todo el trazado fue ingresado correctamente";
            }
            catch (Exception e)
            {
                oResponseModel.bRespuesta = false;               
                string sIdNoGuardados = String.Join(",", trazadosNoGuardados.Select(x => x.Id).ToArray());

                oResponseModel.sParameter1 = JsonConvert.SerializeObject(sIdNoGuardados);
                oResponseModel.sMensaje = e.Message;
            }
            return oResponseModel;
        }
    }
}
