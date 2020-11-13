using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC.CallCenter.Models.BE;
using BC.CallCenter.Models.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace BC.CallCenter.Models.Repositorios
{
    internal class clsPacientesRepository : IPacientesRepository
    {
        /// <summary>
        /// Descripción: Metodo para obtener la informacion del usuario solicitado.
        /// </summary>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public DataSet m_GetUserInfo(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_svc_Get_User_By_ID");
                pdb.AddInParameter(oCmd, "psUsuario", DbType.String, objclsPacientesBE.sUIDPaciente);

                DataSet ds = pdb.ExecuteDataSet(oCmd);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para marcar el estado de un DR bajo demanda.
        /// </summary>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_marcaDr(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            Int32 i = 0;
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("sva_DR_Marcar");
                pdb.AddInParameter(oCmd, "piIdDR", DbType.Int32, objclsPacientesBE.iIdCGUDR);
                pdb.AddInParameter(oCmd, "pbEstado", DbType.Boolean, objclsPacientesBE.bOcupado);
                pdb.AddInParameter(oCmd, "psFolio", DbType.String, objclsPacientesBE.sFolio);
                pdb.AddOutParameter(oCmd, "piValido", DbType.Int32, 0);

                pdb.ExecuteNonQuery(oCmd);

                i = Convert.ToInt32(oCmd.Parameters["piValido"].Value);

                if ((i == 0))
                {
                    objclsPacientesBE.bResult = false;
                }
                else
                    objclsPacientesBE.bResult = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para marcar el estado de un DR bajo demanda en una transacción.
        /// </summary>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="poTrans">Instancia de la Transacción.</param>
        public void m_marcaDr(Database pdb, DbTransaction poTrans, clsPacientesBE objclsPacientesBE)
        {
            Int32 i = 0;
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("sva_DR_Marcar");
                pdb.AddInParameter(oCmd, "piIdDR", DbType.Int32, objclsPacientesBE.iIdCGUDR);
                pdb.AddInParameter(oCmd, "pbEstado", DbType.Boolean, objclsPacientesBE.bOcupado);

                i = pdb.ExecuteNonQuery(oCmd, poTrans);

                if ((i == 0))
                {
                    objclsPacientesBE.bResult = false;
                }
                else
                    objclsPacientesBE.bResult = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para obtener un DR disponible.
        /// </summary>
        ///  /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        public DataSet m_obtieneDRDisponible(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("svc_DR_Disponible");

                DataSet ds = pdb.ExecuteDataSet(oCmd);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para guradar una contraseña de un usuario.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="psUsuario">Usuario.</param>
        /// <param name="psPassword">Password.</param>
        public void m_Save_Password(Database pdb, string psUsuario, string psPassword)
        {
            Int32 i = 0;

            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_sva_Upd_Password");
                pdb.AddInParameter(oCmd, "psIdUsuario", DbType.String, psUsuario);
                pdb.AddInParameter(oCmd, "psPassword", DbType.String, psPassword);

                i = pdb.ExecuteNonQuery(oCmd);

                if ((i == 0))
                {
                    throw new Exception("No se actualizo el password del usuario.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para obtener el UID con el IDdelCGU.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        public void m_GET_UID_By_IdCGU(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            int i = 0;

            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_svc_Get_UID_By_IdCGU");
                pdb.AddOutParameter(oCmd, "psUID", DbType.String, 0);
                pdb.AddInParameter(oCmd, "piIdCGU", DbType.Int32, objclsPacientesBE.iIdCGUDR);

                i = pdb.ExecuteNonQuery(oCmd);

                objclsPacientesBE.sUIDDR = oCmd.Parameters["psUID"].Value.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para obtener el ultimo mensaje guardado con el IDdelCGU.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        public void m_Get_No_Msg(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            int i = 0;

            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_svc_Get_No_Messages");
                pdb.AddOutParameter(oCmd, "piNoMessages", DbType.Int32, 0);
                pdb.AddInParameter(oCmd, "psUsuario", DbType.String, objclsPacientesBE.sUIDDR);
                pdb.AddInParameter(oCmd, "pbGrupo", DbType.Boolean, objclsPacientesBE.bGrupo);

                i = pdb.ExecuteNonQuery(oCmd);

                objclsPacientesBE.iNoMensaje = Convert.ToInt32(oCmd.Parameters["piNoMessages"].Value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para obtener el folio de la menbresia del usuario.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        public void m_Get_Folio(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            int i = 0;

            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_svc_Get_Folio_Paciente");
                pdb.AddOutParameter(oCmd, "psFolio", DbType.String, 0);
                pdb.AddInParameter(oCmd, "psUsuario", DbType.String, objclsPacientesBE.sUIDPaciente);

                i = pdb.ExecuteNonQuery(oCmd);

                objclsPacientesBE.sFolio = oCmd.Parameters["psFolio"].Value.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para poner en servicio a un usuario.
        /// </summary>
        /// /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        public void m_Marcar_EnServicio(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            Int32 i = 0;
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_sva_Marcar_EnServicio");
                pdb.AddInParameter(oCmd, "psUsuario", DbType.String, objclsPacientesBE.sUIDPaciente);
                pdb.AddInParameter(oCmd, "pbEnServicio", DbType.Boolean, objclsPacientesBE.bEnServicio);

                i = pdb.ExecuteNonQuery(oCmd);

                if ((i == 0))
                {
                    objclsPacientesBE.bResult = false;
                }
                else
                    objclsPacientesBE.bResult = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para obtener los estatus de la cuenta del paciente.
        /// </summary>
        /// /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        public DataSet m_Valida_Paciente(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_svc_Get_Status_User");
                pdb.AddInParameter(oCmd, "psUsuario", DbType.String, objclsPacientesBE.sUIDPaciente);

                DataSet ds = pdb.ExecuteDataSet(oCmd);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Metodo para actualizar la aceptación de los terminos y condiciones.
        /// </summary>
        /// <param name="objclsPacientesBE">Instancia de la clase paciente.</param>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Aceptar_Terminos_y_Condiciones(Database pdb, clsPacientesBE objclsPacientesBE)
        {
            Int32 i = 0;
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_sva_Terminos_UPD");
                pdb.AddInParameter(oCmd, "psUsuario", DbType.String, objclsPacientesBE.sUIDPaciente);
                pdb.AddInParameter(oCmd, "pbAcepTerminos", DbType.Boolean, objclsPacientesBE.bTerminosyCondiciones);

                i = pdb.ExecuteNonQuery(oCmd);

                if ((i == 0))
                {
                    throw new Exception("Hubo un error al aceptar los terminos y condiciones.");
                }
                else
                    objclsPacientesBE.bResult = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet m_getSala_DR(Database pdb)
        {

            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("svc_DR_Disponible");
              
                DataSet ds = pdb.ExecuteDataSet(oCmd);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
