using BC.CallCenter.Clases;
using BC.CallCenter.Models.Interfaces;
using BC.CallCenter.Models.Repositorios;
using BC.CallCenterPortable.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.Models.BE
{
    [Serializable()]
    public class clsLoginBE : LoginModel
    {

        [NonSerialized]
        internal ILoginRepository gbloclsLoginRepository;

        public clsLoginBE()
            : this(new clsLoginRepository())
        {
        }

        public clsLoginBE(ILoginRepository repository)
           : base()
        {
            gbloclsLoginRepository = repository;
        }

        internal bool ObtenerGeometriaValida(Database pdb)
        {
            try
            {
                bool esValido = gbloclsLoginRepository.m_ObtenerGeometria(pdb, (clsLoginBE)this);

                return esValido;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Descripción: Método para realizar la autentificación del usuario solicitado.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        internal bool m_Login(Database pdb)
        {
            bool bResultado = false;

            try
            {
                string sPasswordOr = "";

                DataSet ds = gbloclsLoginRepository.m_Login(pdb, (clsLoginBE)this);

                if (ds.Tables[0].Rows.Count == 1)
                {
                    foreach (DataRow rw in ds.Tables[0].Rows)
                    {
                        this.iIdUsuario = rw["iIdUsuario"] is DBNull ? 0 : Convert.ToInt32(rw["iIdUsuario"].ToString());
                        this.sUIDCliente = rw["sUIDCliente"] is DBNull ? "" : rw["sUIDCliente"].ToString();                        
                        this.sNombre = rw["sNombre"] is DBNull ? "" : rw["sNombre"].ToString();
                        this.sSexo = rw["sSexo"] is DBNull ? "" : rw["sSexo"].ToString();
                        this.bDoctor = rw["bDoctor"] is DBNull ? false : Convert.ToBoolean(rw["bDoctor"]);
                        sPasswordOr = rw["sPassword"] is DBNull ? "" : rw["sPassword"].ToString();
                        this.sFolio = rw["sFolio"] is DBNull ? "" : rw["sFolio"].ToString();
                        this.sInstitucion = rw["sInstitucion"] is DBNull ? "" : rw["sInstitucion"].ToString();
                        this.bAceptoTerminoCondicion = rw["bTerminos"] is DBNull ? false : Convert.ToBoolean(rw["bTerminos"]);
                    }

                    if (sPasswordOr == "")
                    {
                        throw new Exception("Favor de verificar los datos, acceso no permitido (e).");
                    }

                    SistemaSeguridad.SistemaSeguridad oSistemaSeguridad = new SistemaSeguridad.SistemaSeguridad();
                    
                    string a = oSistemaSeguridad.Desencriptar(sPasswordOr, clsEnums.sDescSemilla);
                    if (this.sPasswordLogin == a)
                    {
                        bResultado = true;
                    }
                       

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

            return bResultado;
        }

        /// <summary>
        /// Descripción: Método para realizar el cambio de contraseña de un usuario en forma transaccional.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="poTrans">Instancia de la Transacción de Datos.</param
        internal void m_Save_Nueva_Contrasena(Database pdb)
        {
            try
            {
                gbloclsLoginRepository.m_Save_Nueva_Contrasena((clsLoginBE)this, pdb);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para realizar el cambio de contraseña de un usuario.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        internal void m_Save_Nueva_Contrasena(Database pdb, DbTransaction poTrans)
        {
            try
            {
                gbloclsLoginRepository.m_Save_Nueva_Contrasena((clsLoginBE)this, pdb, poTrans);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
