using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.CGU;
using IMD.Meditoc.CallCenter.Mx.Entities.CGU;
using log4net;
using System;

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

        public IMDResponse<bool> BSavePerfil(EntPerfil entPerfil)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSavePerfil);
            logger.Info(IMDSerialize.Serialize(67823458341247, $"Inicia {metodo}(EntPerfil entPerfil)", entPerfil));

            try
            {
                if (entPerfil == null)
                {
                    response.Code = 67823458341247;
                    response.Message = "No se ingresó ningun sub módulo.";
                    response.Result = false;
                    return response;
                }

                response = bValidaDatos(entPerfil); 

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


        public IMDResponse<bool> bValidaDatos(EntPerfil entPerfil)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.bValidaDatos);
            logger.Info(IMDSerialize.Serialize(67823458342024, $"Inicia {metodo}(EntPerfil entPerfil)", entPerfil));
            try
            {
                if (entPerfil.sNombre == "")
                {
                    response.Code = 67823458342024;
                    response.Message = "El nombre del perfil no puede ser vacio.";
                    response.Result = false;

                    return response;
                }

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
