using BC.CallCenter.Models.Info;
using BC.CallCenter.Models.Interfaces;
using BC.CallCenter.Models.Repositorios;
using BC.CallCenterPortable.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.Models.BE
{
    [Serializable()]
    public class clsPacientesBE : clsPacientesInfo
    {

        [NonSerialized]
        internal IPacientesRepository gbloclsPacientesRepository;

        public clsPacientesBE()
            : this(new clsPacientesRepository())
        {
        }

        public clsPacientesBE(IPacientesRepository repository)
           : base()
        {
            gbloclsPacientesRepository = repository;
        }

        /// <summary>
        /// Descripción: Método para obtener y asignar un DR a un paciente, este metodo recupera un DR disponible
        /// y lo asigna al usuario solicitante, en caso de no haber un DR disponible este retorna un mensage.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public bool m_GetAndSetDR(Database pdb)
        {
            try
            {
                bool bResult = false;

                DataSet ds = gbloclsPacientesRepository.m_obtieneDRDisponible(pdb, (clsPacientesBE)this);

                if (ds.Tables.Count == 1)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        foreach (DataRow rw in ds.Tables[0].Rows)
                        {
                            this.sUIDDR = rw["sUIDDR"] is DBNull ? "" : rw["sUIDDR"].ToString();
                            this.iIdCGUDR = rw["iIdUsuario"] is DBNull ? 0 : Convert.ToInt32(rw["iIdUsuario"].ToString());
                        }

                        if (this.iIdCGUDR != 0)
                        {
                            gbloclsPacientesRepository.m_marcaDr(pdb, (clsPacientesBE)this);

                            if (!this.bResult)
                            {
                                throw new Exception("No se pudo completar la solicitud de ocupar al DR.");
                            }
                            else
                                bResult = this.bResult;
                        }
                        else
                        {
                            throw new Exception("Al obtener el id del DR disponible.");
                        }
                    }
                    else if (ds.Tables[0].Rows.Count == 0)
                        this.sMensajeRespuesta = "Por el momento todos los Doctores se encuentran ocupados, reintente más tarde.";
                    else if (ds.Tables[0].Rows.Count > 1)
                        this.sMensajeRespuesta = "Por el momento no hay disponibilidad, reintente más tarde (e).";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return bResult;
        }

        public bool m_GetAndSetDR(Database pdb, DbTransaction poTrans)
        {
            try
            {
                bool bResult = false;

                DataSet ds = gbloclsPacientesRepository.m_obtieneDRDisponible(pdb, (clsPacientesBE)this);

                if (ds.Tables.Count == 1)
                {
                    if (ds.Tables[0].Rows.Count == 1)
                    {
                        foreach (DataRow rw in ds.Tables[0].Rows)
                        {
                            this.sUIDDR = rw["sUIDDR"] is DBNull ? "" : rw["sUIDDR"].ToString();
                            this.iIdCGUDR = rw["iIdUsuario"] is DBNull ? 0 : Convert.ToInt32(rw["iIdUsuario"].ToString());
                        }

                        if (this.iIdCGUDR != 0)
                        {
                            gbloclsPacientesRepository.m_marcaDr(pdb, poTrans, (clsPacientesBE)this);

                            if (!this.bResult)
                            {
                                throw new Exception("No se pudo completar la solicitud de ocupar al DR.");
                            }
                            else
                                bResult = this.bResult;
                        }
                        else
                        {
                            throw new Exception("Al obtener el id del DR disponible.");
                        }
                    }
                    else if (ds.Tables[0].Rows.Count == 0)
                        this.sMensajeRespuesta = "Por el momento todos los doctores se encuentran ocupados, intente de nuevo más tarde.";
                    else if (ds.Tables[0].Rows.Count > 1)
                        this.sMensajeRespuesta = "Por el momento no hay disponibilidad, intente de nuevo más tarde (e).";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return bResult;
        }

        /// <summary>
        /// Descripción: Método para marcar un DR como ocupado o disponible.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_OcuparDR(Database pdb)
        {
            try
            {
                gbloclsPacientesRepository.m_marcaDr(pdb, (clsPacientesBE)this);

                if (!this.bResult)
                {
                    if (this.bOcupado)
                        throw new ArgumentException(ConfigurationManager.AppSettings["sMensajeFolio"]);
                    else
                        throw new Exception("No se pudo desoocupar al DR.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Descripción: Método para consultar la información de un usuario.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public bool m_GetUserInfo(Database pdb)
        {
            bool bResult = false;

            try
            {
                DataSet ds = gbloclsPacientesRepository.m_GetUserInfo(pdb, (clsPacientesBE)this);


                if (ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow rw in ds.Tables[0].Rows)
                    {
                        this.sNombre = rw["sNombre"] is DBNull ? "" : rw["sNombre"].ToString();
                        this.sApePaterno = rw["sApPaterno"] is DBNull ? "" : rw["sApPaterno"].ToString();
                        this.sApeMaterno = rw["sApMaterno"] is DBNull ? "" : rw["sApMaterno"].ToString();
                        this.sEmail = rw["sEmail"] is DBNull ? "" : rw["sEmail"].ToString();
                        this.sPassword = rw["sPassword"] is DBNull ? "" : rw["sPassword"].ToString();
                        this.iIdUsuario = rw["iIdPaciente"] is DBNull ? 0 : Convert.ToInt32(rw["iIdPaciente"].ToString());
                        this.sCodigoValidacion = this.iIdUsuario.ToString();
                    }

                    bResult = true;
                }
                else if (ds.Tables[0].Rows.Count == 0)
                    this.sMensajeRespuesta = "El usuario ingresado no existe.";
                else if (ds.Tables[0].Rows.Count > 1)
                    this.sMensajeRespuesta = "Existe más de un usuario, comuníquese con soporte.";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return bResult;
        }

        /// <summary>
        /// Descripción: Método para actualizar el Password de un usuario.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Save_Password(Database pdb, string psUsuario, string psPassword)
        {
            try
            {
                gbloclsPacientesRepository.m_Save_Password(pdb, psUsuario, psPassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Descripción: Método para obtener el UID de Cometchat mediante el ID del CGU.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_GET_UID_By_IdCGU(Database pdb)
        {
            try
            {
                gbloclsPacientesRepository.m_GET_UID_By_IdCGU(pdb, (clsPacientesBE)this);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Descripción: Método para obtener el ultimo mensaje recibido.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Get_No_Msg(Database pdb)
        {
            try
            {
                gbloclsPacientesRepository.m_Get_No_Msg(pdb, (clsPacientesBE)this);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Descripción: Método para obtener el folio de la membresia del usuario.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Get_Folio(Database pdb)
        {
            try
            {
                gbloclsPacientesRepository.m_Get_Folio(pdb, (clsPacientesBE)this);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Descripción: Método para marcar ual usuario en servicio.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Marcar_EnServicio(Database pdb)
        {
            try
            {
                gbloclsPacientesRepository.m_Marcar_EnServicio(pdb, (clsPacientesBE)this);

                if (!this.bResult)
                {
                    throw new Exception("No se pudo poner en servicio al usuario.");

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Descripción: Método para validar el estatus del usuario.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Valida_Paciente(Database pdb)
        {
            bool bResult = false;

            try
            {
                DataSet ds = gbloclsPacientesRepository.m_Valida_Paciente(pdb, (clsPacientesBE)this);


                if (ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow rw in ds.Tables[0].Rows)
                    {
                        this.bEnServicio = rw["bEnServicio"] is DBNull ? true : Convert.ToBoolean(rw["bEnServicio"]);
                        this.bBaja = rw["bBaja"] is DBNull ? true : Convert.ToBoolean(rw["bBaja"]);
                    }

                    if (bEnServicio)
                        throw new Exception("El usuario ya se encuentra usando el servicio en otro equipo.");

                    if (bEnServicio)
                        throw new Exception("El usuario se encuentra de baja.");
                }
                else if (ds.Tables[0].Rows.Count == 0)
                    this.sMensajeRespuesta = "El usuario ingresado no existe.";
                else if (ds.Tables[0].Rows.Count > 1)
                    this.sMensajeRespuesta = "Existe más de un usuario, comuníquese con soporte.";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Descripción: Método para actualizar la aceptacion de los terminos y condiciones.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        internal void m_Acepta_Termino_y_Condiciones(Database pdb)
        {
            try
            {
                gbloclsPacientesRepository.m_Aceptar_Terminos_y_Condiciones(pdb, (clsPacientesBE)this);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        internal ResponseModel m_getSala_DR(Database pdb)
        {
            ResponseModel oResponseModel = new ResponseModel();

            try
            {
                DataSet ds = gbloclsPacientesRepository.m_getSala_DR(pdb);


                if (ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow rw in ds.Tables[0].Rows)
                    {
                        oResponseModel.sParameter1 = rw["iNumberSala"] is DBNull ? "" : rw["iNumberSala"].ToString();
                        oResponseModel.sMensaje = rw["sUIDDR"] is DBNull ? "" : rw["sUIDDR"].ToString();
                        oResponseModel.bRespuesta = true;
                        //this.sNombre = rw["sNombre"] is DBNull ? "" : rw["sNombre"].ToString();
                        //this.sApePaterno = rw["sApPaterno"] is DBNull ? "" : rw["sApPaterno"].ToString();
                        //this.sApeMaterno = rw["sApMaterno"] is DBNull ? "" : rw["sApMaterno"].ToString();
                        //this.sEmail = rw["sEmail"] is DBNull ? "" : rw["sEmail"].ToString();
                        //this.sPassword = rw["sPassword"] is DBNull ? "" : rw["sPassword"].ToString();
                        //this.iIdUsuario = rw["iIdPaciente"] is DBNull ? 0 : Convert.ToInt32(rw["iIdPaciente"].ToString());
                        //this.sCodigoValidacion = this.iIdUsuario.ToString();
                    }

                    bResult = true;
                }
                else if (ds.Tables[0].Rows.Count == 0)
                {
                    oResponseModel.bRespuesta = false;
                    this.sMensajeRespuesta = "Por el momento todos los doctores se encuentran ocupados, intente de nuevo mas tarde.";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return oResponseModel;
        }

    }
}
