using BC.CallCenter.NuevaImplementacion.Data;
using BC.CallCenter.NuevaImplementacion.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.Business
{
    public class AccesoBusiness
    {
        AccesoData accesoData = new AccesoData();

        public int UserExist(string sPhoneNumber)
        {
            List<AccesoDTO> lstAcceso;
            int iIdAcceso = 0;
            try
            {

                DataSet ds = accesoData.getAccesos(sPhoneNumber);

                lstAcceso = ds.Tables[0].AsEnumerable().
                    Select(dataRow => new AccesoDTO
                    {
                        iIdAcceso = dataRow.Field<int>("iIdAcceso"),
                        sTelefono = dataRow.Field<string>("sTelefono"),
                        dtFechaCreacion = dataRow.Field<DateTime>("dtFechaCreacion")
                    }).ToList();

                if (lstAcceso.Count > 0)
                    iIdAcceso = lstAcceso
                        .FirstOrDefault(x => x.sTelefono == sPhoneNumber)
                        .iIdAcceso;
            }
            catch (Exception e)
            {

                throw e;
            }
            return iIdAcceso;
        }

        public int saveAcceso(AccesoDTO oAcceso)
        {
            int iIdAcceso = 0;
            try
            {
                oAcceso.dtFechaCreacion = DateTime.Now;

                iIdAcceso = accesoData.save(oAcceso);
            }
            catch (Exception)
            {

                throw;
            }
            return iIdAcceso;
        }
    }
}
