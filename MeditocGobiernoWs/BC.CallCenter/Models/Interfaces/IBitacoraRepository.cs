using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC.CallCenter.Models.Info;
using BC.CallCenter.Models.BE;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BC.CallCenter.Models.Interfaces
{
   public interface IBitacoraRepository
    {
        void m_Save(Database pdb, clsBitacoraBE objBitacoraBE);
    }
}
