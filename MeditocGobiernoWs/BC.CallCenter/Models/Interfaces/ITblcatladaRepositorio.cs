//========================================================================
// Este archivo fue generado usando MyGeneration.
//========================================================================
using BC.Modelos.Informacion;
using System;

namespace BC.Modelos.Interfaces
{
    /// <summary>
    /// Descripción: Interfaz de los metodos usados para el acceso a datos.
    /// </summary>    
    public interface ITblcatladaRepositorio
    {
        void m_Save(BC.Modelos.Informacion.clsTblcatladaInformacion oclsTblcatladaInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb);
        void m_Save(BC.Modelos.Informacion.clsTblcatladaInformacion oclsTblcatladaInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb, System.Data.Common.DbTransaction poTrans);
        System.Data.DataSet m_Load(Microsoft.Practices.EnterpriseLibrary.Data.Database pdb);
        void ValidarLada(Microsoft.Practices.EnterpriseLibrary.Data.Database pdb, clsTblcatladaInformacion oclsTblcatladaInformacion);
    }
}
