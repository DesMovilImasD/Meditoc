//========================================================================
// Este archivo fue generado usando MyGeneration.
//========================================================================
using System;

namespace BC.Modelos.Interfaces
{
	/// <summary>
    /// Descripción: Interfaz de los metodos usados para el acceso a datos.
    /// </summary>    
    public  interface ITblpreguntasRepositorio
    {
		void m_Save(BC.Modelos.Informacion.clsTblpreguntasInformacion oclsTblpreguntasInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb);
		void m_Save(BC.Modelos.Informacion.clsTblpreguntasInformacion oclsTblpreguntasInformacion, Microsoft.Practices.EnterpriseLibrary.Data.Database pdb, System.Data.Common.DbTransaction poTrans);
		System.Data.DataSet m_Load(Microsoft.Practices.EnterpriseLibrary.Data.Database pdb);
	}
}
