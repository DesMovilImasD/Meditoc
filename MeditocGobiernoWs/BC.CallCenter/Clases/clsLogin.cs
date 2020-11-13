using System;
using BC.CallCenterPortable.Models;
using BC.CallCenter.Models.BE;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.Common;
using BC.Clases;
using BC.CallCenter.NuevaImplementacion.Business;
using BC.CallCenter.NuevaImplementacion.DTO;
using BC.CallCenter.NuevaImplementacion.Data;
using System.Collections.Generic;

namespace BC.CallCenter.Clases
{
    public class clsLogin
    {
        clsLoginBE oclsLoginBE;
        AccesoDTO accesoDTO;
        Database db;
        private clsBitacora oclsBitacora = new clsBitacora();
        public clsLogin()
        {
            db = clsBDPersonalizada.CreateDatabase("cnxCallCenter");
        }

        /// <summary>
        /// Descripción: Método para realizar la autentificación del usuario solicitado.
        /// </summary>
        /// <param name="pobjLoginModel">Instancia del modelo de objeto.</param>
        public LoginModel m_Login(LoginModel pobjLoginModel)
        {
            LoginModel objclsLoginModel = new LoginModel();
            AccesoBusiness oAccesoB = new AccesoBusiness();
            BitacoraBusiness bitacoraBusiness = new BitacoraBusiness();
            clsTblcatlada oLada = new clsTblcatlada();            

            objclsLoginModel = pobjLoginModel;

            objclsLoginModel.bResult = false;

            try
            {
                oclsLoginBE = new clsLoginBE();

                var sNumero = objclsLoginModel.sUsuarioLogin.Split('_');

                accesoDTO = new AccesoDTO
                {
                    sTelefono = objclsLoginModel.sUsuarioLogin
                };

                if (sNumero.Length > 1)
                {
                    accesoDTO.sTelefono = sNumero[1];
                }

                oclsLoginBE.sLongitud = objclsLoginModel.sLongitud;
                oclsLoginBE.sLatitud = objclsLoginModel.sLatitud;

                int iIdAcceso = oAccesoB.UserExist(accesoDTO.sTelefono);

                if (iIdAcceso == 0)
                    accesoDTO.iIdAcceso = oAccesoB.saveAcceso(accesoDTO);
                else
                    accesoDTO.iIdAcceso = iIdAcceso;

                if (!oclsLoginBE.ObtenerGeometriaValida(db) && Convert.ToBoolean(ConfigurationManager.AppSettings["bActivarGeolocalizacion"]))
                    throw new ArgumentException(ConfigurationManager.AppSettings["sMensajeGeometria"]);
                              
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings["bActivarGeolocalizacion"]))
                    oLada.ValidarLada(accesoDTO.sTelefono.Substring(0, 3));
                
                bitacoraBusiness.save(accesoDTO.iIdAcceso, 0, 0, clsEnums.sDescripcionEnum(clsEnums.enumEstatusBitacora.LOGIN), "Inicio login: " + accesoDTO.sTelefono, oclsLoginBE.sLatitud + ", " + oclsLoginBE.sLongitud);

                objclsLoginModel.bResult = true;
                objclsLoginModel.bAceptoTerminoCondicion = true;
                objclsLoginModel.sTelefonoDRs = string.IsNullOrEmpty(ConfigurationManager.AppSettings["sTelefono"]) ? "" : ConfigurationManager.AppSettings["sTelefono"].ToString();
                objclsLoginModel.iIdUsuario = accesoDTO.iIdAcceso;
                objclsLoginModel.rutasIceServer = getIceLinkServer();
                objclsLoginModel.sMensajeRespuesta = "Login completado exitosamente.";

            }
            catch (Exception ex)
            {
                objclsLoginModel.sMensajeRespuesta = ex.Message;
                bitacoraBusiness.save(accesoDTO.iIdAcceso, 0, 0, clsEnums.sDescripcionEnum(clsEnums.enumEstatusBitacora.ERROR), ex.Message, oclsLoginBE.sLatitud + ", " + oclsLoginBE.sLongitud);
            }

            return objclsLoginModel;
        }

        public List<IceLinkServer> getIceLinkServer()
        {
            List<IceLinkServer> iceLinkServers;
            IceLinkServer oIceLinkServers;
            try
            {
                string sIceLinkServer = ConfigurationManager.AppSettings["sIceLinkServers"].ToString();
                iceLinkServers = new List<IceLinkServer>();
                string[] oServer = sIceLinkServer.Split('|');

                foreach (var item in oServer)
                {
                    string[] oDatos = item.Split(',');

                    oIceLinkServers = new IceLinkServer
                    {
                        sServer = oDatos[0],
                        sUser = oDatos[1],
                        sPassword = oDatos[2]
                    };

                    iceLinkServers.Add(oIceLinkServers);
                }

                return iceLinkServers;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
