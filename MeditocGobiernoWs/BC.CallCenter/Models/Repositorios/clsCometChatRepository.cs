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
using BC.CallCenter.Models.Info;

namespace BC.CallCenter.Models.Repositorios
{
     internal class clsCometChatRepository : ICometChatRepository
    {

        private static Int64 SecInNasec = Convert.ToInt64(Math.Pow(10, 9));
        private static Int64 OneTick = 100;
        private static Int32 AnioBase = 1970 - 1;

        /// <summary>
        /// Descripción: Método para guardar los mensajes de chat uno a uno.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objMensaje">Instancia del ObjMensaje.</param>
        public void m_SaveMensages(Database pdb, clsMensagesCCModel objMensaje)
        {
            Int32 i = 0;
            try
            {
                //Se convierte la fecha de segundos a DateTime
                DateTime dtTime = new DateTime((Convert.ToInt32(objMensaje.sTimestamp) * SecInNasec) / OneTick);
                dtTime = dtTime.AddYears(AnioBase);

                DbCommand oCmd = pdb.GetStoredProcCommand("app_sva_Menssages_Ins");
                pdb.AddInParameter(oCmd, "piMessage_id", DbType.Int32, objMensaje.iMessage_id);
                pdb.AddInParameter(oCmd, "psSender_uid", DbType.String, objMensaje.sSender_uid);
                pdb.AddInParameter(oCmd, "psReciever_uid", DbType.String, objMensaje.sReciever_uid);
                pdb.AddInParameter(oCmd, "psMessage", DbType.String, objMensaje.sMessage);
                pdb.AddInParameter(oCmd, "psTimestamp", DbType.DateTime, dtTime);
                pdb.AddInParameter(oCmd, "psRead", DbType.String, objMensaje.sRead);
                pdb.AddInParameter(oCmd, "psVisibility", DbType.String, objMensaje.sVisibility);
                pdb.AddInParameter(oCmd, "pbGrupo", DbType.Boolean, false);
                                
                i = pdb.ExecuteNonQuery(oCmd);

                if ((i == 0))
                {
                    throw new Exception("No se Guardo el Registro. Intente de Nuevo");
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Descripción: Método para guardar los mensajes de chat grupal uno a uno.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        /// <param name="objMensaje">Instancia del ObjMensaje.</param>
        public void m_SaveMsgGroup(Database pdb, clsMsgGroupCCModel objMensaje)
        {
            Int32 i = 0;
            try
            {
                DbCommand oCmd = pdb.GetStoredProcCommand("app_sva_Menssages_Ins");
                pdb.AddInParameter(oCmd, "piMessage_id", DbType.Int32, objMensaje.iMessage_id);
                pdb.AddInParameter(oCmd, "psSender_uid", DbType.String, objMensaje.sSender_uid);
                pdb.AddInParameter(oCmd, "psReciever_uid", DbType.String, objMensaje.sGuid);
                pdb.AddInParameter(oCmd, "psMessage", DbType.String, objMensaje.sMessage);
                pdb.AddInParameter(oCmd, "psTimestamp", DbType.String, objMensaje.sTimestamp);
                pdb.AddInParameter(oCmd, "psRead", DbType.String, "");
                pdb.AddInParameter(oCmd, "psVisibility", DbType.String, "");
                pdb.AddInParameter(oCmd, "pbGrupo", DbType.Boolean, objMensaje.bGrupo);

                i = pdb.ExecuteNonQuery(oCmd);

                if ((i == 0))
                {
                    throw new Exception("No se Guardo el Registro. Intente de Nuevo");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
}
}
