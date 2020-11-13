using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Configuration;

namespace BC.CallCenter.Clases
{
    class clsBDPersonalizada
    {
        public static Database CreateDatabase(string connectionString)
        {

            SistemaSeguridad.SistemaSeguridad DES = new SistemaSeguridad.SistemaSeguridad();
            string a = ConfigurationManager.ConnectionStrings[connectionString].ProviderName;
            DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(a);
            connectionString = DES.Desencriptar(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString, clsEnums.sDescripcionEnum(clsEnums.enumSemilla.sSemilla));
            return new GenericDatabase(connectionString, dbProviderFactory);
        }
    }
}
