using BC.CallCenter.Models.Info;
using BC.CallCenter.Models.Interfaces;
using BC.CallCenter.Models.Repositorios;
using BC.CallCenterPortable.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.Models.BE
{
    [Serializable()]
    public class clsBitacoraBE : clsBitacoraInfo
    {

        [NonSerialized]
        internal IBitacoraRepository gbloclsBitacoraRepository;

        public clsBitacoraBE()
            : this(new clsBitacoraRepository())
        {
        }

        public clsBitacoraBE(IBitacoraRepository repository)
           : base()
        {
            gbloclsBitacoraRepository = repository;
        }

        /// <summary>
        /// Descripción: Método para realizar el guardado de la bitacora.
        /// </summary>
        /// <param name="pdb">Instancia de la Base de Datos.</param>
        public void m_Save(Database pdb)
        {
            try
            {
                gbloclsBitacoraRepository.m_Save(pdb, (clsBitacoraBE)this);                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
