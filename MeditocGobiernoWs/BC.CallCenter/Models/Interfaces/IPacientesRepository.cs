using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC.CallCenter.Models.BE;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BC.CallCenter.Models.Interfaces
{
   public interface IPacientesRepository
    {
        DataSet m_obtieneDRDisponible(Database pdb, clsPacientesBE objclsPacientesBE);
        void m_marcaDr(Database pdb, clsPacientesBE objclsPacientesBE);
        void m_marcaDr(Database pdb, DbTransaction poTrans, clsPacientesBE objclsPacientesBE);
        DataSet m_GetUserInfo(Database pdb, clsPacientesBE objclsPacientesBE);
        void m_Save_Password(Database pdb, string psUsuario, string psPassword);
        void m_GET_UID_By_IdCGU(Database pdb, clsPacientesBE objclsPacientesBE);
        void m_Get_No_Msg(Database pdb, clsPacientesBE objclsPacientesBE);
        void m_Get_Folio(Database pdb, clsPacientesBE objclsPacientesBE);
        void m_Marcar_EnServicio(Database pdb, clsPacientesBE objclsPacientesBE);
        DataSet m_Valida_Paciente(Database pdb, clsPacientesBE objclsPacientesBE);
        void m_Aceptar_Terminos_y_Condiciones(Database pdb, clsPacientesBE objclsPacientesBE);
        DataSet m_getSala_DR(Database pdb);
    }
}
