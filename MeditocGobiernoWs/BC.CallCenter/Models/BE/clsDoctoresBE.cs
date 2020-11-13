using BC.CallCenter.Models.Info;
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
    public class clsDoctoresBE : clsDoctoresInfo
    {

        [NonSerialized]
        internal IDoctoresRepository gbloclsDoctoresRepository;

        public clsDoctoresBE()
            : this(new clsDoctoresRepository())
        {
        }

        public clsDoctoresBE(IDoctoresRepository repository)
           : base()
        {
            gbloclsDoctoresRepository = repository;
        }

    }
}
