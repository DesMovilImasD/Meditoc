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
    internal class clsLoginRepository : ILoginRepository
    {
        public DataSet m_Login(Database pdb, clsLoginBE objclsLoginBE)
        {
            DataSet dst = new DataSet();
            try
            {
                DbCommand oCmdr = pdb.GetStoredProcCommand("app_svc_Login_Pacientes");
                pdb.AddInParameter(oCmdr, "psUsuario", DbType.String, objclsLoginBE.sUsuarioLogin);

                dst = pdb.ExecuteDataSet(oCmdr);

                return dst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool m_ObtenerGeometria(Database pdb, clsLoginBE objclsLoginBE)
        {
            try
            {
                DbCommand oCmdr = pdb.GetStoredProcCommand("IsLocationInValidRegion");
                pdb.AddInParameter(oCmdr, "LATITUDE", DbType.Double, objclsLoginBE.sLatitud);
                pdb.AddInParameter(oCmdr, "LONGITUDE", DbType.Double, objclsLoginBE.sLongitud);
                pdb.AddOutParameter(oCmdr, "@ireturnvalue", DbType.Int32, 2);
                
                oCmdr.Parameters["@ireturnvalue"].Direction = ParameterDirection.ReturnValue;

                pdb.ExecuteNonQuery(oCmdr);
                int i = Convert.ToInt32(oCmdr.Parameters["@ireturnvalue"].Value);

                if (i == 1)
                    return true;
                else
                    return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void m_Save_Nueva_Contrasena(clsLoginBE pobjclsLoginBE, Database pdb, DbTransaction poTrans)
        {
            Int32 i = 0;
            string vstrSP = string.Empty;
            try
            {
                vstrSP = "app_sva_Upd_Password";

                DbCommand oCmd = pdb.GetStoredProcCommand(vstrSP);

                pdb.AddInParameter(oCmd, "psIdUsuario", DbType.String, pobjclsLoginBE.sUsuarioLogin);
                pdb.AddInParameter(oCmd, "psPassword", DbType.String, pobjclsLoginBE.sPasswordLogin);

                i = pdb.ExecuteNonQuery(oCmd, poTrans);

                if ((i == 0))
                {
                    throw new Exception("No se actualizo la contraseña. Intente de nuevo.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void m_Save_Nueva_Contrasena(clsLoginBE pobjclsLoginBE, Database pdb)
        {
            Int32 i = 0;
            string vstrSP = string.Empty;
            try
            {
                vstrSP = "app_sva_Upd_Password";

                DbCommand oCmd = pdb.GetStoredProcCommand(vstrSP);

                pdb.AddInParameter(oCmd, "psIdUsuario", DbType.String, pobjclsLoginBE.sUsuarioLogin);
                pdb.AddInParameter(oCmd, "psPassword", DbType.String, pobjclsLoginBE.sPasswordLogin);

                i = pdb.ExecuteNonQuery(oCmd);

                if ((i == 0))
                {
                    throw new Exception("No se actualizo la contraseña. Intente de nuevo.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
