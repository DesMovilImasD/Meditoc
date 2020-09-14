using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Catalogos;
using IMD.Meditoc.CallCenter.Mx.Entities.Catalogos;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.Catalogos
{
    public class BusEspecialidad
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusEspecialidad));

        DatEspecialidad datEspecialidad;

        public BusEspecialidad()
        {
            datEspecialidad = new DatEspecialidad();
        }

        public IMDResponse<bool> BSaveEspecialidad(EntEspecialidad entEspecialidad)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458450027, $"Inicia {metodo}"));

            try
            {
                if (entEspecialidad == null)
                {
                    response.Code = 87987834567;
                    response.Message = "No se ingresó información completa";
                    return response;
                }

                if (entEspecialidad.bActivo && !entEspecialidad.bBaja)
                {
                    if (string.IsNullOrWhiteSpace(entEspecialidad.sNombre))
                    {
                        response.Code = 39875698730498;
                        response.Message = "No se ingresó el nombre de la especialidad";
                        return response;
                    }
                }

                IMDResponse<bool> respuestaSaveEspecialidad = datEspecialidad.DSaveEspecialidad(entEspecialidad.iIdEspecialidad, entEspecialidad.sNombre, entEspecialidad.iIdUsuarioMod, entEspecialidad.bActivo, entEspecialidad.bBaja);
                if (respuestaSaveEspecialidad.Code != 0)
                {
                    return respuestaSaveEspecialidad;
                }

                response.Code = 0;
                response.Message = "La especialidad se guardó correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458450804;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458450804, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntEspecialidad>> BGetEspecialidad(int? piIdEspecialidad = null)
        {
            IMDResponse<List<EntEspecialidad>> response = new IMDResponse<List<EntEspecialidad>>();

            string metodo = nameof(this.BGetEspecialidad);
            logger.Info(IMDSerialize.Serialize(67823458451581, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataTable> respuestaGetEspecialidades = datEspecialidad.DGetEspecialidad(piIdEspecialidad);
                if (respuestaGetEspecialidades.Code != 0)
                {
                    return respuestaGetEspecialidades.GetResponse<List<EntEspecialidad>>();
                }

                List<EntEspecialidad> lstEspecialidades = new List<EntEspecialidad>();
                foreach (DataRow drEspecialidad in respuestaGetEspecialidades.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(drEspecialidad);

                    EntEspecialidad especialidad = new EntEspecialidad
                    {
                        bActivo = dr.ConvertTo<bool>("bActivo"),
                        bBaja = dr.ConvertTo<bool>("bBaja"),
                        dtFechaCreacion = dr.ConvertTo<DateTime>("dtFechaCreacion"),
                        iIdEspecialidad = dr.ConvertTo<int>("iIdEspecialidad"),
                        sNombre = dr.ConvertTo<string>("sNombre")
                    };

                    especialidad.sFechaCreacion = especialidad.dtFechaCreacion.ToString("dd/MM/yyyy HH:mm");

                    lstEspecialidades.Add(especialidad);
                }

                response.Code = 0;
                response.Message = "Especialidades consultadas";
                response.Result = lstEspecialidades;
            }
            catch (Exception ex)
            {
                response.Code = 67823458452358;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458452358, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
