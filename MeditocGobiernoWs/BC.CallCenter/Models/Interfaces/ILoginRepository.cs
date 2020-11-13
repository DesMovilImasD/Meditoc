using System.Data;
using System.Data.Common;
using BC.CallCenter.Models.BE;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BC.CallCenter.Models.Interfaces
{
    public interface ILoginRepository
    {
        DataSet m_Login(Database pdb, clsLoginBE objclsLoginBE);
        bool m_ObtenerGeometria(Database pdb, clsLoginBE objclsLoginBE);
        void m_Save_Nueva_Contrasena(clsLoginBE objclsLoginBE, Database pdb, DbTransaction poTrans);
        void m_Save_Nueva_Contrasena(clsLoginBE objclsLoginBE, Database pdb);
    }
}
