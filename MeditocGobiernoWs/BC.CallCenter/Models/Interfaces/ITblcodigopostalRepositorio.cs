//========================================================================
// Este archivo fue generado usando MyGeneration.
//========================================================================
using System;

namespace BC.Modelos.Interfaces
{
	/// <summary>
    /// Descripción: Interfaz de los metodos usados para el acceso a datos.
    /// </summary>    
    public  interface ITblcodigopostalRepositorio
    {
		void m_Save(BC.Modelos.Informacion.clsTblcodigopostalInformacion oclsTblcodigopostalInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb);
		void m_Save(BC.Modelos.Informacion.clsTblcodigopostalInformacion oclsTblcodigopostalInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb, System.Data.Common.DbTransaction poTrans);
		System.Data.DataSet m_Load(Microsoft.Practices.EnterpriseLibrary.Data.Database pdb);
		int ValidarCP(Microsoft.Practices.EnterpriseLibrary.Data.Database pdb, BC.Modelos.Informacion.clsTblcodigopostalInformacion oclsTblcodigopostalInformacion);
	}
}
