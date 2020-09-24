using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;

namespace IMD.Meditoc.CallCenter.Mx.Business.CGU
{
    public class BusPerfil
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusPerfil));
        DatPerfil datPerfil;

        public BusPerfil()
        {
            datPerfil = new DatPerfil();
        }

        /// <summary>
        /// Guardar un perfil
        /// </summary>
        /// <param name="entPerfil"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSavePerfil(EntPerfil entPerfil)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSavePerfil);
            logger.Info(IMDSerialize.Serialize(67823458341247, $"Inicia {metodo}(EntPerfil entPerfil)", entPerfil));

            try
            {
                if (entPerfil == null)
                {
                    response.Code = -88678716283680;
                    response.Message = "No se ingresó ningun submódulo.";
                    response.Result = false;
                    return response;
                }

                response = BValidaDatos(entPerfil);

                if (response.Code != 0) //Se valida que los datos que contiene el objeto de perfil no esten vacios.
                {
                    return response;
                }

                response = datPerfil.DSavePerfil(entPerfil); //Se hace el guardado del perfil

                if (response.Code != 0)
                {
                    response.Message = "Hubo un error al guardar el perfil.";
                    response.Result = false;
                    return response;
                }

                response.Code = 0;
                response.Message = entPerfil.iIdPerfil == 0 ? "El perfil se guardó correctamente" : "El perfil se actualizo correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458341247;
                response.Message = "Ocurrió un error al intentar guardar el perfil.";

                logger.Error(IMDSerialize.Serialize(67823458341247, $"Error en {metodo}(EntPerfil entPerfil): {ex.Message}", entPerfil, ex, response));
            }
            return response;
        }

        //Obtener los perfiles del sistema
        public IMDResponse<List<EntPerfil>> BObtenerPerfil(int? iIdPerfil, bool bActivo, bool bBaja)
        {
            IMDResponse<List<EntPerfil>> response = new IMDResponse<List<EntPerfil>>();

            string metodo = nameof(this.BObtenerPerfil);
            logger.Info(IMDSerialize.Serialize(67823458357564, $"Inicia {metodo}(int? iIdPerfil, bool bActivo, bool bBaja)", iIdPerfil, bActivo, bBaja));

            try
            {
                IMDResponse<DataTable> dtPerfil = datPerfil.DObtenerPerfil(iIdPerfil, bActivo, bBaja);
                if (dtPerfil.Code != 0)
                {
                    return dtPerfil.GetResponse<List<EntPerfil>>();
                }

                List<EntPerfil> lstPerfiles = new List<EntPerfil>();

                if (dtPerfil.Code != 0)
                {
                    response.Code = -71470986457102;
                    response.Message = "No se encuentran perfiles";
                    return response;
                }


                foreach (DataRow item in dtPerfil.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(item);
                    EntPerfil entPerfil = new EntPerfil();

                    entPerfil.iIdPerfil = dr.ConvertTo<int>("iIdPerfil");
                    entPerfil.sNombre = dr.ConvertTo<string>("sNombre");
                    entPerfil.bActivo = dr.ConvertTo<bool>("bActivo");
                    entPerfil.bBaja = dr.ConvertTo<bool>("bBaja");

                    lstPerfiles.Add(entPerfil);
                }

                response.Result = lstPerfiles;
                response.Message = "Lista de perfiles.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458358341;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458358341, $"Error en {metodo}(int? iIdPerfil, bool bActivo, bool bBaja): {ex.Message}", iIdPerfil, bActivo, bBaja, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Validar datos para guardar el perfil
        /// </summary>
        /// <param name="entPerfil"></param>
        /// <returns></returns>
        public IMDResponse<bool> BValidaDatos(EntPerfil entPerfil)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458342024, $"Inicia {metodo}(EntPerfil entPerfil)", entPerfil));
            try
            {
                if (string.IsNullOrWhiteSpace(entPerfil.sNombre))
                {
                    response.Code = -51256872819328;
                    response.Message = "El nombre del perfil no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

                response.Code = 0;
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Code = 67823458342024;
                response.Message = "Ocurrió un error al intentar guardar el perfil.";

                logger.Error(IMDSerialize.Serialize(67823458341247, $"Error en {metodo}(EntPerfil entPerfil): {ex.Message}", entPerfil, ex, response));
            }
            return response;
        }
    }
}
